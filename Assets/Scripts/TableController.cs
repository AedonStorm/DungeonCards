using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableController : MonoBehaviour {

    //size 3x4
    private List<Card> tableCardList;

    [SerializeField] private GameObject defaultCard;
    [SerializeField] private GameObject heroeCard;
    private HeroeCard currentHeroeCard;
    private int currentHeroePosition;

    private TableController instance;

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
            }
            else
            {
                card = Instantiate(defaultCard, transform.GetChild(i).position, Quaternion.Euler(0, 180, 0)).GetComponent<Card>();
            }

            tableCardList.Add(card);
        }

        updateDirecctions();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit.collider != null)
            {
                Debug.Log("Target Position: " + hit.collider.gameObject.transform.position);
                if (hit.collider.tag == "Direction")
                {
                    hideDirection();
                    int nextPosition = hit.collider.GetComponent<DirectionButton>().movePosition;
                    move(nextPosition);
                }
            }
        }
    }

    private void updateDirecctions()
    {
        //0 1 2 3
        //4 5 6 7
        //8 9 10 11
        
        if (currentHeroePosition == 0 || currentHeroePosition == 4 || currentHeroePosition == 8) currentHeroeCard.directionLeft.SetActive(false); else currentHeroeCard.directionLeft.SetActive(true);
        if (currentHeroePosition < 4) currentHeroeCard.directionUp.SetActive(false); else currentHeroeCard.directionUp.SetActive(true);
        if (currentHeroePosition == 3 || currentHeroePosition == 7 || currentHeroePosition == 11) currentHeroeCard.directionRight.SetActive(false); else currentHeroeCard.directionRight.SetActive(true);
        if (currentHeroePosition >= 8) currentHeroeCard.directionDown.SetActive(false); else currentHeroeCard.directionDown.SetActive(true);
    }
    
    private void updateHeroePosition()
    {
        Destroy(tableCardList[currentHeroePosition].gameObject);
        tableCardList[currentHeroePosition] = currentHeroeCard;
        updateDirecctions();
    }


    public int getCurrentHeroePosition() { return currentHeroePosition; }

    public void move(int nextPosition)
    {
        currentHeroePosition += nextPosition;
        tableCardList[currentHeroePosition].outMove();
        currentHeroeCard.slowMove(tableCardList[currentHeroePosition].transform.position);

        Invoke("updateHeroePosition", 0.5f);
    }


    private void hideDirection()
    {
        currentHeroeCard.directionLeft.SetActive(false);
        currentHeroeCard.directionRight.SetActive(false);
        currentHeroeCard.directionUp.SetActive(false);
        currentHeroeCard.directionDown.SetActive(false);
    }
}
