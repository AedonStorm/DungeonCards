using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction { none = 0, left = -1, right = +1, up = -4, down = +4 }

public class TableController : MonoBehaviour {

    private GameController gameController;
    private DungeonMaster dungeonMaster;

    //size 3x4
    private List<Card> tableCardList;

    private GameObject heroeCard;
    private HeroeCard currentHeroeCard;
    private int currentHeroePosition;

    private TableController instance;

    public void setupTableController(GameController gameController, DungeonMaster dungeonMaster, GameObject heroeCard) {
        this.gameController = gameController;
        this.dungeonMaster = dungeonMaster;
        this.heroeCard = heroeCard;
    }

    // Use this for initialization
    void Start () {
        tableCardList = new List<Card>();
        currentHeroePosition = Random.Range(0, 11);

        for (int i = 0; i < transform.childCount; i++)
        {
            Card card = null;
            if (i == currentHeroePosition) {
                GameObject obj = Instantiate(heroeCard, transform.GetChild(i).position, Quaternion.identity);
                card = obj.GetComponent<Card>();
                currentHeroeCard = obj.GetComponent<HeroeCard>();
                currentHeroeCard.initializePower(Random.Range(6, 14));
            }
            else
            {
                card = Instantiate( dungeonMaster.getRandomCard(), transform.GetChild(i).position, Quaternion.Euler(0, 0, 0)).GetComponent<Card>();
                card.GetComponent<Card>().initializePower(Random.Range(1, 19));
            }

            tableCardList.Add(card);
        }

        updateDirections(currentHeroePosition);
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit.collider != null)
            {
                //  Debug.Log("Target Position: " + hit.collider.gameObject.transform.position);
                if (hit.collider.tag == "Direction")
                {
                    enableDirection(false);
                    int nextPosition = hit.collider.GetComponent<DirectionButton>().movePosition;// + currentHeroePosition;

                    ActionImpl actionImpl = tableCardList[nextPosition + currentHeroePosition].activateAction();
                     Debug.Log("Power: " + actionImpl.power);
                    switch (actionImpl.action)
                    {
                        case Action.None:
                            break;
                        case Action.Chest:
                            break;
                        case Action.Power:
                            //Debug.Log("Power: " + actionImpl.power);
                            currentHeroeCard.setPower(actionImpl.power);
                            break;
                        case Action.Coins:
                            //Debug.Log("Coins: "+ actionImpl.power);
                            gameController.onCoin(actionImpl.power);
                            break;
                        default:
                            break;
                    }
                    move(nextPosition);
                }
            }
        }
    }

    private void updateDirections(int newDirection)
    {
        //0 1 2 3
        //4 5 6 7
        //8 9 10 11
        if (newDirection == 0 || newDirection == 4 || newDirection == 8) currentHeroeCard.directionLeft.disable(); else currentHeroeCard.directionLeft.gameObject.SetActive(true);
        if (newDirection < 4) currentHeroeCard.directionUp.disable(); else currentHeroeCard.directionUp.gameObject.SetActive(true);
        if (newDirection == 3 || newDirection == 7 || newDirection == 11) currentHeroeCard.directionRight.disable(); else currentHeroeCard.directionRight.gameObject.SetActive(true);
        if (newDirection >= 8) currentHeroeCard.directionDown.disable(); else currentHeroeCard.directionDown.gameObject.SetActive(true);
    }

    public void move(int nextPosition)
    {
        // currentHeroePosition += nextPosition;
        var nextHeroePosition = currentHeroePosition + nextPosition;
        tableCardList[nextHeroePosition].outMove();
        currentHeroeCard.slowMove(tableCardList[nextHeroePosition].transform.position);

        updateHeroePosition(nextPosition);
    }

    private void updateHeroePosition(int nextPosition)
    {
        int newDirection = currentHeroePosition + nextPosition;
        Destroy(tableCardList[newDirection].gameObject, 0.5f);
        tableCardList[newDirection] = currentHeroeCard;
        int replacementPosition = getReplacementPosition(intToDirection(nextPosition));
        updateDirections(newDirection);

        StartCoroutine(moveReplacementCard(replacementPosition, nextPosition));
    }

    private IEnumerator moveReplacementCard(int replacementPosition, int nextPosition)
    {
        yield return new WaitForSeconds(0.3f);
        tableCardList[replacementPosition].slowMove(transform.GetChild(currentHeroePosition).position);
        tableCardList[currentHeroePosition] = tableCardList[replacementPosition];

        StartCoroutine(createNewCard(replacementPosition, nextPosition));
    }

    private IEnumerator createNewCard(int replacementPosition, int nextPosition)
    {
        yield return new WaitForSeconds(0.3f);
        Card newCard = Instantiate(dungeonMaster.getRandomCard(), transform.GetChild(replacementPosition).position, Quaternion.Euler(0, 0, 0)).GetComponent<Card>();
        newCard.GetComponent<Card>().initializePower(Random.Range(1, 19));
        tableCardList[replacementPosition] = newCard;
        currentHeroePosition += nextPosition;
        enableDirection(true);
    }

    private Direction intToDirection (int dir)
    {
        switch (dir)
        {
            case 1: return Direction.right;
            case -1: return Direction.left;
            case 4: return Direction.down;
            case -4: return Direction.up;
        }

        return Direction.none;
    }


    private void enableDirection(bool enable)
    {
        currentHeroeCard.directionLeft.GetComponent<Collider2D>().enabled = enable;
        currentHeroeCard.directionRight.GetComponent<Collider2D>().enabled = enable;
        currentHeroeCard.directionUp.GetComponent<Collider2D>().enabled = enable;
        currentHeroeCard.directionDown.GetComponent<Collider2D>().enabled = enable;
    }

    private int getReplacementPosition(Direction currentDirection)
    {
        switch (currentDirection)
        {
            case Direction.left:
                if (currentHeroePosition == 3) return currentHeroePosition + 4;
                else if (currentHeroePosition == 7 || currentHeroePosition == 11) return currentHeroePosition - 4;
                else return currentHeroePosition + 1;
            case Direction.right:
                if (currentHeroePosition == 0) return currentHeroePosition + 4;
                else if (currentHeroePosition == 4 || currentHeroePosition == 8) return currentHeroePosition - 4;
                else return currentHeroePosition - 1;
            case Direction.up:
                if (currentHeroePosition == 9 || currentHeroePosition == 11) return currentHeroePosition - 1;
                else if (currentHeroePosition == 8 || currentHeroePosition == 10) return currentHeroePosition + 1;
                else return currentHeroePosition + 4;
            case Direction.down:
                if (currentHeroePosition == 1 || currentHeroePosition == 3) return currentHeroePosition - 1;
                else if (currentHeroePosition == 0 || currentHeroePosition == 2) return currentHeroePosition + 1;
                else return currentHeroePosition - 4;
            default:
                break;
        }

        return -1;
    }
    
}
