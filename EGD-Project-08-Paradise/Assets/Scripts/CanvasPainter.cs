using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CanvasPainter : TileMapPainter
{
    public Color[] paletteColors;
    public Tilemap stencil;
    public Tilemap palette;
    TileBase brush;

    public Vector2Int canvasBounds = new Vector2Int(16, 16);

    private void Start()
    {
        brush = tiles[0];
        DisplayBrushColor();
    }

    void Update()
    {
        mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetKey(KeyCode.RightShift) || Input.GetKey(KeyCode.LeftShift))
        {
            SetBrush();
        }
        else if (Input.GetMouseButton(0)) // Left mouse button is pressed
        {
            Paint();
        }
        else if (Input.GetMouseButton(1))
        {
            Erase();
        }
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            Debug.Log(Score());
        }
    }

    void Paint()
    {
        Vector3Int cellPos = tilemap.WorldToCell(mouseWorldPos);

        if (Mathf.Abs(cellPos.x) < canvasBounds.x && Mathf.Abs(cellPos.y) < canvasBounds.y && !tilemap.HasTile(cellPos)) // If there is already a tile in this cell, remove it
        {
            tilemap.SetTile(cellPos, brush);
        }
    }

    void Erase()
    {
        Vector3Int cellPos = tilemap.WorldToCell(mouseWorldPos);
        tilemap.SetTile(cellPos, null);
    }

    void SetBrush()
    {
        Vector3Int cellPos = tilemap.WorldToCell(mouseWorldPos);

        TileBase tile = palette.GetTile<TileBase>(cellPos);
        if (tile != null) // If there is already a tile in this cell, remove it
        {
            brush = tile;
            DisplayBrushColor();
        }
    }

    void DisplayBrushColor()
    {
        Vector3Int origin = new Vector3Int(canvasBounds.x + 3, -canvasBounds.y, 0);

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                Vector3Int cellPos = origin;
                cellPos.x += i;
                cellPos.y += j;

                palette.SetTile(cellPos, brush);
            }
        }
    }

    public float Score()
    {
        float score = 0;
        int tileCount = 0;
        Vector3Int origin = new Vector3Int(-canvasBounds.x, -canvasBounds.y, 0);
        for (int i = 0; i < canvasBounds.x * 2; i++)
        {
            for (int j = 0; j < canvasBounds.y * 2; j++)
            {
                Vector3Int cellPos = origin;
                cellPos.x += i;
                cellPos.y += j;

                TileBase stencilTile = stencil.GetTile<TileBase>(cellPos);
                TileBase drawnTile = tilemap.GetTile<TileBase>(cellPos);
                
                if (stencilTile == drawnTile)
                {
                    score += 1;
                }
                tileCount++;
            }
        }

        return score / tileCount;
    }
}
