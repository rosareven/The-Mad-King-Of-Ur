using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStone : MonoBehaviour
{
    public int player;
    public Tile[] movePath;
    public Tile currentTile;
    
    private Vector3 _startingLocation;

    private void Start()
    {
        _startingLocation = transform.position;
    }
}
