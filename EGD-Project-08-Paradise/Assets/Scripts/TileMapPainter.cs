using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapPainter : MonoBehaviour
{
    public ExampleDialogueScript dialogueScript;
    public int completionCondition = 0;
    public bool enablePainting = true;
    public Tilemap tilemap; // Reference to the tile map
    public TileBase[] tiles; // Array of tiles to paint with
    public TileBase dirtTile;

    protected Vector2 mouseWorldPos = Vector2.zero;

    void Start()
    {
        //Paint();
    }

    void Update()
    {
        if (!enablePainting)
            return;

        if (completionCondition == 0)
        {
            if (!tilemap.ContainsTile(dirtTile))
            {
                dialogueScript.GrassPlanted();
            }
        }

        if (Input.GetMouseButton(0)) // Left mouse button is pressed
        {
            mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Paint();
        }
    }

    void Paint()
    {
        Vector3Int cellPos = tilemap.WorldToCell(mouseWorldPos);
        //Debug.Log("Mouse Pos: " + mouseWorldPos + " || Cell: " + cellPos + " || Occupied: " + (tilemap.GetTile(cellPos) != null));

        if (tilemap.HasTile(cellPos)) // If there is already a tile in this cell, remove it
        {
        /*    tilemap.SetTile(cellPos, null);
        }
        else // Otherwise, paint a new tile
        {*/
            int tileIndex = Random.Range(0, tiles.Length); // Choose a random tile from the array
            tilemap.SetTile(cellPos, tiles[tileIndex]);
        }
    }
}
