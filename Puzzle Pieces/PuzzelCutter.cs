using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PuzzelCutter : MonoBehaviour
{
    public Texture2D texture;
    bool presslock;
    public GameObject []imagePlaceHolder;
    public int blocksPerLinePublic=3;
    Texture2D[,] imagePieces;
    public static int PiecesInPlace;
    void Start()
    {
        presslock = false;
        PiecesInPlace = 0;
        scale(texture, 500, 500, FilterMode.Bilinear);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)&&!presslock)
        {
            presslock = true;
            imagePieces= getSlices(texture,blocksPerLinePublic);
            Rect rec = new Rect(0, 0, imagePieces[0,0].width, imagePieces[0, 0].height);

            int counter = 0;
            for (int y = 0; y < blocksPerLinePublic; y++)
            {
                for (int x = 0; x < blocksPerLinePublic; x++)
                {
                    imagePlaceHolder[counter].GetComponent<SpriteRenderer>().sprite = Sprite.Create(imagePieces[x,y], rec, new Vector2(0, 0), 1);
                    counter++;
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            SceneManager.LoadScene(0);
        }
    }
    Texture2D[,] getSlices(Texture2D image, int blocksPerLine)
    {
        int imageSize = Mathf.Min(image.width, image.height);
        int blockSize = imageSize / blocksPerLine;
        Texture2D[,] blocks = new Texture2D[blocksPerLine, blocksPerLine];
        for (int y = 0; y < blocksPerLine; y++)
        {
            for (int x = 0; x < blocksPerLine; x++)
            {
                Texture2D block = new Texture2D(blockSize, blockSize);
                block.SetPixels(image.GetPixels(x * blockSize, y * blockSize, blockSize, blockSize));
                block.Apply();
                blocks[x, y] = block;
            }
        }
        return blocks;
    }
    public static Texture2D scaled(Texture2D src, int width, int height, FilterMode mode = FilterMode.Trilinear)
    {
        Rect texR = new Rect(0, 0, width, height);
        _gpu_scale(src, width, height, mode);

        //Get rendered data back to a new texture
        Texture2D result = new Texture2D(width, height, TextureFormat.ARGB32, true);
        result.Resize(width, height);
        result.ReadPixels(texR, 0, 0, true);
        return result;
    }
    public static void scale(Texture2D tex, int width, int height, FilterMode mode = FilterMode.Trilinear)
    {
        Rect texR = new Rect(0, 0, width, height);
        _gpu_scale(tex, width, height, mode);

        // Update new texture
        tex.Resize(width, height);
        tex.ReadPixels(texR, 0, 0, true);
        tex.Apply(true);    //Remove this if you hate us applying textures for you :)
    }

    // Internal unility that renders the source texture into the RTT - the scaling method itself.
    static void _gpu_scale(Texture2D src, int width, int height, FilterMode fmode)
    {
        //We need the source texture in VRAM because we render with it
        src.filterMode = fmode;
        src.Apply(true);

        //Using RTT for best quality and performance. Thanks, Unity 5
        RenderTexture rtt = new RenderTexture(width, height, 32);

        //Set the RTT in order to render to it
        Graphics.SetRenderTarget(rtt);

        //Setup 2D matrix in range 0..1, so nobody needs to care about sized
        GL.LoadPixelMatrix(0, 1, 1, 0);

        //Then clear & draw the texture to fill the entire RTT.
        GL.Clear(true, true, new Color(0, 0, 0, 0));
        Graphics.DrawTexture(new Rect(0, 0, 1, 1), src);
    }
}
