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

    public SpriteRenderer[] spriteRenderers;
    public float drawSpeed = 0.05f;
    public GameObject canvas;

    float taskScore = 0;


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
        if (!enablePainting)
            return;

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
            Score();
            Debug.Log(taskScore);
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

    public void Score()
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

        taskScore = score / tileCount;
        drawing();
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
        //GetComponent<SpriteRenderer>().sprite = Drawing;

        StartCoroutine(RenderSprites());
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
    }

    IEnumerator RenderSprites()
    {
        canvas.SetActive(false);
        enablePainting = false;

        foreach (SpriteRenderer renderer in spriteRenderers)
        {
            renderer.sprite = Drawing;
            yield return new WaitForSeconds(drawSpeed);
        }
        yield return new WaitForSeconds(1f);

        if (completionCondition == 1)
        {
            dialogueScript.BuildingsAdded(taskScore);
        }
        if (completionCondition == 2)
        {
            dialogueScript.PeopleAdded(spriteRenderers);
        }
    }

    public IEnumerator TimedPaint(float duration)
    {
        yield return new WaitForSeconds(duration);
        enablePainting = false;
        Score();
        Debug.Log(taskScore);
    }
}
