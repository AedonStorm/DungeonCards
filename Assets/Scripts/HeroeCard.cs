using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HeroeCard : Card {

    public DirectionButton directionLeft;
    public DirectionButton directionRight;
    public DirectionButton directionUp;
    public DirectionButton directionDown;

    private TableController tableController;

    public void setTableController(TableController tableController) { this.tableController = tableController; }

    public void move(int nextPosition)
    {
        tableController.move(nextPosition);
    }

    public override void activateAction()
    {
        //throw new NotImplementedException();
    }
}
