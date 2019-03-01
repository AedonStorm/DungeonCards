using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCard : Card {

    public override ActionImpl activateAction()
    {
        return new ActionImpl(Action.Coins, getPower());
    }

    protected override void finalAction()
    {
        //throw new NotImplementedException();
    }
}
