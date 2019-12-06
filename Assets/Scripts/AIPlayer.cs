using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class AIPlayer : MonoBehaviour
{
    private List<PlayerStone> _movableStones = new List<PlayerStone>();
    public DiceRoller diceRoller;
    public PlayerStone[] stones;
    public StateManager stateManager;

    public void AIRollDice()
    {
        diceRoller.RollTheDice();
    }

    public void ProbeStones()
    {
        foreach (var stone in stones)
        {
            if (stone.currentTilePosition + diceRoller.diceTotal < 16)
                if (stone.ProbeTile(diceRoller.diceTotal) != null)
                    _movableStones.Add(stone);
        }
        Debug.Log(_movableStones+" "+diceRoller.diceTotal);
    }

    public void MoveStone()
    {
        if (_movableStones.Count > 0)
        {
            var rand = new Random();
            var chosenStoneIndex = rand.Next(_movableStones.Count);
            var chosenStone = _movableStones[chosenStoneIndex];
            _movableStones.Clear();
            chosenStone.MovePiece(diceRoller.diceTotal);
        }
        else
        {
            stateManager.NextTurn();
        }
    }
}