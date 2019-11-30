using UnityEngine;

public class AIPlayer : MonoBehaviour
{
    public DiceRoller diceRoller;

    public void AIRollDice()
    {
        diceRoller.RollTheDice();
    }
}