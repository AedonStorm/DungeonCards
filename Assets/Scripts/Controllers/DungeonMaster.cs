using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum cardType { enemy, chest, coin, potion }

public class DungeonMaster {

    private int currentDungeon = 0;
    private int difficult = 0;

    private GameObject cardCoin;
    private GameObject cardPotion;
    private GameObject cardChest;

    private GameObject[] cardEnemyList;

    public DungeonMaster()
    {
        cardCoin = Resources.Load<GameObject>("CardTypes/CoinCard");
        cardPotion = Resources.Load<GameObject>("CardTypes/PotionCard");
        cardChest = Resources.Load<GameObject>("CardTypes/ChestCard");

        cardEnemyList = Resources.LoadAll<GameObject>("CardTypes/Enemies/");
    }

    //ToDo: crear un contador actualizado del número de cartas de cada tipo en la mesa.

    public GameObject getRandomCard()
    {
        int rand = Random.Range(0, 100);

        GameObject card;

        if (rand < 30) card = cardCoin;
        else if (rand < 40) card = cardPotion;
        else if (rand < 60) card = cardChest;
        else
        {
            int randEnemy = Random.Range(0, 100);
            if (randEnemy < 35) card = cardEnemyList[1];
            else if (randEnemy < 60) card = cardEnemyList[0];
            else if (randEnemy < 75) card = cardEnemyList[5];
            else if (randEnemy < 85) card = cardEnemyList[4];
            else if (randEnemy < 95) card = cardEnemyList[2];
            else  card = cardEnemyList[3];
        }

        //card.GetComponent<Card>().initializePower(Random.Range(1, 19));
        return card;
    }

    //public Card InitializeCard()
    //{

    //}

}
