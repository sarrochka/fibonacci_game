using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBoard : MonoBehaviour
{

    public GameBoardTileGenerator generator;
    public GameObject background;
    public int width = 4;
    public int height = 4;

    private GameBoardTile[,] boardPieces;
    public GameBoardTile[,] BoardPieces { get { return boardPieces; } }
    private ActiveTile[,] activeTiles;
    public ActiveTile[,] ActiveTiles { get { return activeTiles; } }

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(background);
        // TODO: change it to be just an image and use only active tiles
        boardPieces = new GameBoardTile[width, height];

        for (int x = 0; x < width; ++x)
        {
            for (int y = 0; y < height; ++y)
            {
                boardPieces[x, y] = generator.GenerateTile(x, y);
            }
        }

        activeTiles = new ActiveTile[width, height];

        for (int x = 0; x < width; ++x)
        {
            for (int y = 0; y < height; ++y)
            {
                bool revealed = (x == 0 && y == 0); 
                activeTiles[x, y] = generator.GenerateActiveTile(x, y, revealed);
            }
        }
    }

}
