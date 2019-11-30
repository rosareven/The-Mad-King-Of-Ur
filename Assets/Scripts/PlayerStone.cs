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
        transform.position = movePath[currentTilePosition].transform.position;
        movePath[currentTilePosition].ToggleOccupant(this);
    }

    private void OnMouseOver()
    {
        if (stateManager.state == StateManager.State.WaitForMove && player == 1 && !stateManager.isAiTurn)
            ShowLegalMoves(int.Parse(diceTotal.text));
    }

    private void OnMouseDown()
    {
        if (stateManager.state == StateManager.State.WaitForMove && player == 1 && !stateManager.isAiTurn)
            MovePiece(int.Parse(diceTotal.text));
    }

    private void ShowLegalMoves(int diceValue)
    {
        movableTile = ProbeTile(diceValue);
    }

    private Tile ProbeTile(int diceValue)
    {
        var probe = movePath[currentTilePosition + diceValue];
        if (probe.occupant == null)
            return probe;
        if (probe.occupant.player == enemy && !probe.isInvincible)
            return probe;
        return null;
    }

    private void MovePiece(int diceValue)
    {
        movePath[currentTilePosition].ToggleOccupant(null);
        currentTilePosition += diceValue;
        transform.position = movePath[currentTilePosition].transform.position;
        if (movePath[currentTilePosition].occupant != null && movePath[currentTilePosition].occupant.player == enemy)
        {
            var enemyStone = movePath[currentTilePosition].occupant;
            enemyStone.currentTilePosition = 0;
            enemyStone.transform.position = enemyStone.movePath[0].transform.position;
        }

        movePath[currentTilePosition].ToggleOccupant(this);
        if (movePath[currentTilePosition].isRollAgain)
            stateManager.RollAgain();
        else
            stateManager.NextTurn();
    }
}