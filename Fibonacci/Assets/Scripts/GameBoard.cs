using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Windows;
using System;
using UnityEngine;

public class GameBoard : MonoBehaviour
{

    public GameBoardTileGenerator generator;
    public GameObject background;
    public int width = 4;
    public int height = 4;
    private int stepX = 0;
    private int stepY = 0;

    public GameBoardTile[,] BoardPieces { get; private set; }
    public ActiveTile[,] ActiveTiles { get; private set; }
    private Point[, ] boardMask;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(background);
        // TODO: change it to be just an image and use only active tiles
        BoardPieces = new GameBoardTile[width, height];
        boardMask = new Point[width, height];

        for (int x = 0; x < width; ++x)
        {
            for (int y = 0; y < height; ++y)
            {
                BoardPieces[x, y] = generator.GenerateTile(x, y);
                boardMask[x, y] = new Point(-1, -1);
            }
        }

        ActiveTiles = new ActiveTile[width, height];

        for (int x = 0; x < width; ++x)
        {
            for (int y = 0; y < height; ++y)
            {
                bool revealed = (x == 0 && y == 0);
                //Debug.Log(revealed);
                ActiveTiles[x, y] = generator.GenerateActiveTile(x, y, revealed);
                if (revealed)
                {
                    boardMask[x, y].X = x;
                    boardMask[x, y].Y = y;
                }
            }
        }
    }

    private void SetStep()
    {
        if (SwipeController.swipeDirection == Swipe.Down)
        {
            stepX = 0;
            stepY = -1;
        }
        else if (SwipeController.swipeDirection == Swipe.Up)
        {
            stepX = 0;
            stepY = 1;
        }
        else if (SwipeController.swipeDirection == Swipe.Left)
        {
            stepX = -1;
            stepY = 0;
        }
        else if (SwipeController.swipeDirection == Swipe.Right)
        {
            stepX = 1;
            stepY = 0;
        }
        else
        {
            stepX = 0;
            stepY = 0;
        }
    }


    private Vector3 NewTilePosition(int x, int y)
    {
        /*
         Returns new tile's position - the farthest available in given direction
         */

        bool positionHasChanged = true;
        Vector3 desiredPosition = ActiveTiles[x, y].transform.position;
        Vector3 delta = new Vector3(stepX * (int)Math.Round(ActiveTiles[x, y].transform.localScale.x),
                        stepY * (int)Math.Round(ActiveTiles[x, y].transform.localScale.y), 0);
        Vector3 prevDesiredPosition = ActiveTiles[x, y].transform.position;

        while (positionHasChanged)
        {
            prevDesiredPosition = desiredPosition;
            desiredPosition += delta;
            desiredPosition.x = Math.Max(desiredPosition.x, 0);
            desiredPosition.x = Math.Min(desiredPosition.x, width - 1);
            desiredPosition.y = Math.Max(desiredPosition.y, 0);
            desiredPosition.y = Math.Min(desiredPosition.y, height - 1);

            if (prevDesiredPosition == desiredPosition)
            {
                break;
            }
        }
        

        return desiredPosition;
    } 
    // Update is called once per frame
    void Update()
    {
        Debug.Log(SwipeController.swipeDirection);
        SetStep();

        for (int x = 0; x < width; ++x)
        {
            for (int y = 0; y < height; ++y)
            {
                if (ActiveTiles[x, y].revealed)
                {

                    Vector3 desiredPosition = NewTilePosition(x, y);
                    if (boardMask[(int)(desiredPosition.x), (int)(desiredPosition.y)].X == -1 && 
                        boardMask[(int)(desiredPosition.x), (int)(desiredPosition.y)].Y == -1)
                    {
                        boardMask[(int)(ActiveTiles[x, y].transform.position.x),
                                  (int)(ActiveTiles[x, y].transform.position.y)].X = -1;
                        boardMask[(int)(ActiveTiles[x, y].transform.position.x),
                                  (int)(ActiveTiles[x, y].transform.position.y)].Y = -1;
                        ActiveTiles[x, y].transform.position = desiredPosition;
                        boardMask[(int)(desiredPosition.x), (int)(desiredPosition.y)].X = x;
                        boardMask[(int)(desiredPosition.x), (int)(desiredPosition.y)].Y = y;
                    }
                }
            }
        }

    }
}
