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
    private UnityEngine.Color defaultColor = new UnityEngine.Color(231f / 255, 208f / 255, 151f / 255, 1f);
    // TODO: map of number-to-color
    private UnityEngine.Color activeColor = new UnityEngine.Color(195f / 255, 30f / 255, 167f / 255, 1f);

    public GameBoardTile[,] BoardPieces { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(background);
        BoardPieces = new GameBoardTile[width, height];

        for (int x = 0; x < width; ++x)
        {
            for (int y = 0; y < height; ++y)
            {
                bool revealed = (x == 0 && y == 0);
                BoardPieces[x, y] = generator.GenerateTile(x, y, revealed);
                
                if (revealed)
                {
                    BoardPieces[x, y].SetColor(activeColor);
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

        //TODO: step in possible direction one-by-one ?

        bool positionHasChanged = true;
        Vector3 desiredPosition = BoardPieces[x, y].transform.position;
        Vector3 delta = new Vector3(stepX * (int)Math.Round(BoardPieces[x, y].transform.localScale.x),
                        stepY * (int)Math.Round(BoardPieces[x, y].transform.localScale.y), 0);
        Vector3 prevDesiredPosition = BoardPieces[x, y].transform.position;

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
                if (BoardPieces[x, y].revealed)
                {
                    Vector3 desiredPosition = NewTilePosition(x, y);

                    if (!BoardPieces[(int)(desiredPosition.x), (int)(desiredPosition.y)].revealed)
                    {
                        BoardPieces[x, y].revealed = false;
                        BoardPieces[x, y].SetColor(defaultColor);
                        BoardPieces[(int)(desiredPosition.x), (int)(desiredPosition.y)].SetColor(activeColor);
                        BoardPieces[(int)(desiredPosition.x), (int)(desiredPosition.y)].revealed = true;
                    }
                }
            }
        }

    }
}
