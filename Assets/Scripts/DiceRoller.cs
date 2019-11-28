using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UI;

public class DiceRoller : MonoBehaviour
{
    public int[] diceValues;
    public Sprite[] diceImageOne;
    public Sprite[] diceImageZero;
    public Text diceDisplay;
    public StateManager stateManager;

    private int _diceTotal;
    private Sprite[] _allDiceImages;
    
    private void Start()
    {
        diceValues = new int[4];
        _allDiceImages = diceImageOne.Concat(diceImageZero).ToArray();
        stateManager.isAiTurn = false;
    }

    public void RollTheDice()
    {
        diceDisplay.text = "";
        StartCoroutine("AnimateDice");
    }

    IEnumerator AnimateDice()
    {
        stateManager.SetState(StateManager.State.Rolling);
        for(int j = 0; j < 5; j++)
        {
            for (int i = 0; i < diceValues.Length; i++)
            {
                transform.GetChild(i).GetComponent<Image>().sprite =
                    _allDiceImages[Random.Range(0, _allDiceImages.Length)];
                transform.GetChild(i).GetComponent<RectTransform>().rotation =
                    new Quaternion((float) Random.Range(0, 360), (float) Random.Range(0, 360), (float) 0, (float) 0);
            }

            yield return new WaitForSeconds(0.1f);
        }

        GetDiceResult();
    }

    private void GetDiceResult()
    {
        for (int i = 0; i < diceValues.Length; i++)
        {
            diceValues[i] = Random.Range(0, 2);
            Image die = transform.GetChild(i).GetComponent<Image>();
            if (diceValues[i] == 0)
            {
                die.sprite = diceImageZero[Random.Range(0, diceImageZero.Length)];
            }
            else
            {
                die.sprite = diceImageOne[Random.Range(0, diceImageOne.Length)];
            }
            die.GetComponentInParent<RectTransform>().rotation = 
                new Quaternion((float)Random.Range(0,360),(float)Random.Range(0,360),(float)0,(float)0);
        }

        _diceTotal = diceValues[0] + diceValues[1] + diceValues[2] + diceValues[3];
        diceDisplay.text = _diceTotal.ToString();
        stateManager.SetState(StateManager.State.WaitForMove);
    }
}