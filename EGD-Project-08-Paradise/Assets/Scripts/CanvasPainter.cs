using System;
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
    public int textureScaleAmount = 1;

    public Sprite Drawing { get; private set; }

    private void Start()
    {
        brush = tiles[0];
        DisplayBrushColor();


        /*texture = new Texture2D(16, 16);
        // Get the SpriteRenderer component and set the sprite to be the drawing texture
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));*/
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

        Debug.Log(cellPos);

        if (Mathf.Abs(cellPos.x) < canvasBounds.x && Mathf.Abs(cellPos.y) < canvasBounds.y) // If brush is in bounds
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

        drawing();
        
        return score / tileCount;
    }

    private void drawing()
    {
        // Create a new texture
        Texture2D texture = new Texture2D(16, 16);

        // Set the pixels to a solid red color
        Color[] pixels = new Color[16 * 16];
        for (int i = 0; i < pixels.Length; i++)
        {
            pixels[i] = Color.clear;
        }

        Vector3Int origin = new Vector3Int(-canvasBounds.x, -canvasBounds.y, 0);
        for (int i = 0; i < canvasBounds.x * 2; i++)
        {
            for (int j = 0; j < canvasBounds.y * 2; j++)
            {
                Vector3Int cellPos = origin;
                cellPos.x += i;
                cellPos.y += j;

                TileBase drawnTile = tilemap.GetTile<TileBase>(cellPos);

                Color color = Color.clear;

                if (drawnTile != null)
                {
                    int index = Array.IndexOf(tiles, drawnTile);
                    if (index >= 0)
                        color = paletteColors[index];
                }
                pixels[j * 16 + i] = color;
            }
        }


        texture.SetPixels(pixels);
        texture.Apply();

        Texture2D scaledTexture = ScaleTexture(texture, textureScaleAmount);

        // Create a new sprite from the texture
        Drawing = Sprite.Create(scaledTexture, new Rect(0, 0, scaledTexture.width, scaledTexture.height), Vector2.zero);

        // Assign the sprite to a sprite renderer component
        GetComponent<SpriteRenderer>().sprite = Drawing;
    }

    private Texture2D ScaleTexture(Texture2D texture, int scaleFactor)
    {
        int targetX = texture.width * scaleFactor;
        int targetY = texture.height * scaleFactor;

        RenderTexture rt = new RenderTexture(targetX, targetY, 24);
        RenderTexture.active = rt;
        Graphics.Blit(texture, rt);
        Texture2D result = new Texture2D(targetX, targetY);
        result.ReadPixels(new Rect(0, 0, targetX, targetY), 0, 0);
        result.Apply();
        return result;

        /*// Calculate the new dimensions for the scaled texture
        int newWidth = texture.width * scaleFactor;
        int newHeight = texture.height * scaleFactor;

        // Create a new texture with the desired dimensions
        Texture2D scaledTexture = new Texture2D(newWidth, newHeight);

        for (int i = 0; i < newWidth; i++)
        {
            for (int j = 0; j < newHeight; j++)
            {
                int x = Mathf.FloorToInt(i / (float)scaleFactor);
                int y = Mathf.FloorToInt(j / (float)scaleFactor);
                Color c = texture.GetPixel(x, y);
                
                scaledTexture.SetPixel(i, j, c);
            }
        }
        texture.Apply();

        return scaledTexture;*/
    }
}
