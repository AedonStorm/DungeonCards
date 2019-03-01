using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player {

    //public HeroeCard heroe;
    private int _coins;

    public Player()
    {
        //heroe = new HeroeCard();
        _coins = 0;
    }

    public int coins
    {
        get
        {
            return _coins;
        }

        set
        {
            _coins = value;
        }
    }
}
