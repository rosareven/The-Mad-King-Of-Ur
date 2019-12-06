using UnityEngine;

public class Tile : MonoBehaviour
{
    public PlayerStone occupant;
    public bool isInvincible;
    public bool isRollAgain;
    public bool isFinishTile;

    public void ToggleOccupant(PlayerStone player)
    {
        occupant = player;
    }
}