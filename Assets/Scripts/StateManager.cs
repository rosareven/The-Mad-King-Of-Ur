using UnityEngine;
using UnityEngine.UI;

public class StateManager : MonoBehaviour
{
    public enum State
    {
        Initialise,
        WaitForRoll,
        Rolling,
        WaitForMove
    }

    public AIPlayer ai;
    public Text clippy;
    public bool isAiTurn;
    public Button rollDiceButton;

    public State state = State.Initialise;
    public Text whoseTurn;

    public void SetState(State newState)
    {
        state = newState;
        switch (state)
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
        if (!isAiTurn)
        {
            whoseTurn.text = "Player's Turn";
        }
        else
        {
            whoseTurn.text = "AI's Turn";
            if (state == State.WaitForMove) NextTurn();
        }
    }

    private void ShowClippy(bool x)
    {
        clippy.GetComponent<Text>().enabled = x;
    }

    public void NextTurn()
    {
        isAiTurn = !isAiTurn;
        SetState(State.WaitForRoll);
        ShowClippy(false);
    }

    public void RollAgain()
    {
        SetState(State.WaitForRoll);
        ShowClippy(false);
    }

    private void EnableDice()
    {
        if (!isAiTurn)
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