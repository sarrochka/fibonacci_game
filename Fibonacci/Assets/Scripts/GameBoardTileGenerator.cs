using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class GameBoardTileGenerator : MonoBehaviour
{
    public GameObject prefab_bg;

    public GameBoardTile GenerateTile(int x, int y, bool revealed)
    {
        GameObject spawnedTile = Instantiate(prefab_bg);

        var boardTile = spawnedTile.GetComponent<GameBoardTile>();

        if (boardTile == null)
        {
            boardTile = spawnedTile.AddComponent<GameBoardTile>();
        }

        boardTile.x = x * (int)Math.Round(spawnedTile.transform.localScale.x);
        boardTile.y = y * (int)Math.Round(spawnedTile.transform.localScale.y);
        boardTile.revealed = revealed;

        return boardTile;
    }
}
