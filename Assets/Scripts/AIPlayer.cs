﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPlayer : MonoBehaviour
{
    public DiceRoller diceRoller;

    public void AIRollDice()
    {
        diceRoller.RollTheDice();
    }
}
