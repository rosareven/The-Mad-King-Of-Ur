using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class DiceRoller : MonoBehaviour
{
    private Sprite[] _allDiceImages;

    public int diceTotal;
    public Text diceDisplay;
    public Sprite[] diceImageOne;
    public Sprite[] diceImageZero;
    public int[] diceValues;
    public StateManager stateManager;

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

    private IEnumerator AnimateDice()
    {
        stateManager.SetState(StateManager.State.Rolling);
        for (var j = 0; j < 5; j++)
        {
            for (var i = 0; i < diceValues.Length; i++)
            {
                transform.GetChild(i).GetComponent<Image>().sprite =
                    _allDiceImages[Random.Range(0, _allDiceImages.Length)];
                transform.GetChild(i).GetComponent<RectTransform>().rotation =
                    new Quaternion(Random.Range(0, 360), Random.Range(0, 360), 0, 0);
            }

            yield return new WaitForSeconds(0.1f);
        }

        GetDiceResult();
    }

    private void GetDiceResult()
    {
        for (var i = 0; i < diceValues.Length; i++)
        {
            diceValues[i] = Random.Range(0, 2);
            var die = transform.GetChild(i).GetComponent<Image>();
            if (diceValues[i] == 0)
                die.sprite = diceImageZero[Random.Range(0, diceImageZero.Length)];
            else
                die.sprite = diceImageOne[Random.Range(0, diceImageOne.Length)];
            die.GetComponentInParent<RectTransform>().rotation =
                new Quaternion(Random.Range(0, 360), Random.Range(0, 360), 0, 0);
        }

        diceTotal = diceValues[0] + diceValues[1] + diceValues[2] + diceValues[3];
        diceDisplay.text = diceTotal.ToString();
        stateManager.SetState(StateManager.State.WaitForMove);
    }
}