using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject heroeCard;
    private Player player;
    private DungeonMaster dungeonMaster;
    private TableController tableController;

    public GameObject book;

    [Header("Canvas")]
    public GameObject coinCanvas;
    private TextMeshProUGUI coinText;

    void Start()
    {
        player = new Player();
        dungeonMaster = new DungeonMaster();

        //heroeCard.GetComponent<HeroeCard>().initializePower(Random.Range(6, 14));

         tableController = Instantiate(book, transform).GetComponent<TableController>();
        tableController.setupTableController(this, dungeonMaster, heroeCard);

        //Canvas
        coinText = Instantiate(coinCanvas, transform).GetComponentInChildren<TextMeshProUGUI>();

        
        init();
    }

    private void init()
    {
        updateCoinCanvas();
    }
    private void updateCoinCanvas()
    {
        coinText.text = player.coins.ToString();
    }

    public void onCoin(int modifier)
    {
        player.coins += modifier;
        updateCoinCanvas();
    }

    public void onSetHeroePower(int modifier)
    {
        //player.
    }
}
