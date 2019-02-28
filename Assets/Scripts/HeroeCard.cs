using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HeroeCard : Card {

    public GameObject directionLeft;
    public GameObject directionRight;
    public GameObject directionUp;
    public GameObject directionDown;

    private TableController tableController;

    public void setTableController(TableController tableController) { this.tableController = tableController; }

    public void move(int nextPosition)
    {
        tableController.move(nextPosition);
    }
}
