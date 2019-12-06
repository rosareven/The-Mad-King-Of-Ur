using UnityEngine;
using UnityEngine.UI;

public class PlayerStone : MonoBehaviour
{
    public int currentTilePosition;
    public Text diceTotal;
    public int enemy;
    public Tile movableTile;
    public Tile[] movePath;
    public int player;
    public StateManager stateManager;

    private void Start()
    {
        currentTilePosition = 0;
        movePath[currentTilePosition].ToggleOccupant(this);
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, movePath[currentTilePosition].transform.position,
            10 * Time.deltaTime);
    }

    private void OnMouseOver()
    {
        if (stateManager.state == StateManager.State.WaitForMove && player == 1 && !stateManager.isAiTurn)
            ShowLegalMoves(int.Parse(diceTotal.text));
    }

    private void OnMouseDown()
    {
        var diceValue = int.Parse(diceTotal.text);
        if (stateManager.state == StateManager.State.WaitForMove && player == 1 && !stateManager.isAiTurn &&
            currentTilePosition + diceValue < 16)
            if (ProbeTile(diceValue) != null)
                MovePiece(diceValue);
    }

    private void OnMouseExit()
    {
        var col = transform.GetChild(0).GetComponent<SpriteRenderer>().color;
        col.a = 1f;
        transform.GetChild(0).GetComponent<SpriteRenderer>().color = col;
        stateManager.highlight.GetComponent<Renderer>().enabled = false;
    }

    private void ShowLegalMoves(int diceValue)
    {
        if (currentTilePosition + diceValue < 16) movableTile = ProbeTile(diceValue);
        if (movableTile == null)
        {
            var col = transform.GetChild(0).GetComponent<SpriteRenderer>().color;
            col.a = 0.5f;
            transform.GetChild(0).GetComponent<SpriteRenderer>().color = col;
        }
        else
        {
            stateManager.highlight.GetComponent<Renderer>().enabled = true;
            stateManager.highlight.transform.position = movableTile.transform.position;
        }
    }

    public Tile ProbeTile(int diceValue)
    {
        var probe = movePath[currentTilePosition + diceValue];

        if (probe.occupant == null)
            return probe;
        if (probe.occupant.player == enemy && !probe.isInvincible)
            return probe;
        if (probe.isFinishTile)
            return probe;
        return null;
    }

    public void MovePiece(int diceValue)
    {
        movePath[currentTilePosition].ToggleOccupant(null);
        currentTilePosition += diceValue;
        if (movePath[currentTilePosition].occupant != null && movePath[currentTilePosition].occupant.player == enemy)
        {
            var enemyStone = movePath[currentTilePosition].occupant;
            enemyStone.currentTilePosition = 0;
        }

        movePath[currentTilePosition].ToggleOccupant(this);
        if (movePath[currentTilePosition].isRollAgain)
            stateManager.RollAgain();
        else
            stateManager.NextTurn();
    }
}