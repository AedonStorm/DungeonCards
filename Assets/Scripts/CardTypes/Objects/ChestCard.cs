using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestCard : Card {

    public override ActionImpl activateAction()
    {
        return new ActionImpl(Action.Chest, getPower());
    }

    protected override void finalAction()
    {
        //throw new NotImplementedException();
    }
}
