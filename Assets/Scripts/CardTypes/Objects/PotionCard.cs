﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionCard : Card
{
    public override ActionImpl activateAction()
    {
        return new ActionImpl(Action.Power, getPower());
    }

    protected override void finalAction()
    {
        //throw new NotImplementedException();
    }
}
