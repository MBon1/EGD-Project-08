using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawingScript : MonoBehaviour
{
    private Texture2D drawingTexture;
    private Color[] textureColors;
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        // Create a new texture for the drawing
        drawingTexture = new Texture2D(512, 512);
        textureColors = new Color[drawingTexture.width * drawingTexture.height];

        // Set the initial color of the texture to be white
        for (int i = 0; i < textureColors.Length; i++)
        {
            textureColors[i] = Color.white;
        }

        // Set the texture colors to the texture
        drawingTexture.SetPixels(textureColors);
        drawingTexture.Apply();

        // Get the SpriteRenderer component and set the sprite to be the drawing texture
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = Sprite.Create(drawingTexture, new Rect(0, 0, drawingTexture.width, drawingTexture.height), new Vector2(0.5f, 0.5f));
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the mouse button is being held down
        if (Input.GetMouseButton(0))
        {
            // Get the mouse position in world coordinates
            Vector2 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // Get the pixel coordinates from the world coordinates
            int x = Mathf.RoundToInt(worldPos.x * drawingTexture.width);
            int y = Mathf.RoundToInt(worldPos.y * drawingTexture.height);

            // Set the color of the pixel to black
            drawingTexture.SetPixel(x, y, Color.black);
            drawingTexture.Apply();
        }
    }

    // Save the texture as a PNG file when the user presses the space bar
    private void OnApplicationQuit()
    {
        byte[] bytes = drawingTexture.EncodeToPNG();
        System.IO.File.WriteAllBytes(Application.dataPath + "/../SavedScreen.png", bytes);
    }
}
