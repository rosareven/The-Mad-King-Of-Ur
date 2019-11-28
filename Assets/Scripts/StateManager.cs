using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class StateManager : MonoBehaviour
{
    public enum State
    {
        Initialise, WaitForRoll, Rolling, WaitForMove
    }

    private State _state = State.Initialise;
    public bool isAiTurn;
    public Button rollDiceButton;
    public AIPlayer ai;
    public Text whoseTurn;
    public Text clippy;

    public void SetState(State newState)
    {
        _state = newState;
        switch (_state)
        {
            case State.Initialise:
                break;
            case State.WaitForRoll:
                EnableDice();
                break;
            case State.Rolling:
                DisableDice();
                break;
            case State.WaitForMove:
                ShowClippy(true);
                break;
        }
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1") && _state == State.WaitForMove)
        {
            NextTurn();
            SetState(State.WaitForRoll);
            ShowClippy(false);
        }

        if (!isAiTurn)
        {
            whoseTurn.text = "Player's Turn";
        }
        else
        {
            whoseTurn.text = "AI's Turn";
        }
    }

    private void ShowClippy(Boolean x)
    {
        clippy.GetComponent<Text>().enabled = x;
    }

    private void NextTurn()
    {
        isAiTurn = !isAiTurn;
    }
    
    private void EnableDice()
    {
        if(!isAiTurn)
        {
            rollDiceButton.interactable = true;
            rollDiceButton.GetComponentInChildren<Text>().enabled = true;
        }
        else
        {
            ai.AIRollDice();
        }
    }

    private void DisableDice()
    {
        rollDiceButton.interactable = false;
        rollDiceButton.GetComponentInChildren<Text>().enabled = false;
    }
}
