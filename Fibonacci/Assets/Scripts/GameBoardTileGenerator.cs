using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class GameBoardTileGenerator : MonoBehaviour
{
    public GameObject prefab_bg;
    public GameObject prefab_active;


    public GameBoardTile GenerateTile(int x, int y)
    {
        GameObject spawnedTile = Instantiate(prefab_bg);

        var boardTile = spawnedTile.GetComponent<GameBoardTile>();

        if (boardTile == null)
        {
            boardTile = spawnedTile.AddComponent<GameBoardTile>();
        }

        boardTile.x = x * (int)Math.Round(spawnedTile.transform.localScale.x);
        boardTile.y = y * (int)Math.Round(spawnedTile.transform.localScale.y);

        return boardTile;
    }

    public ActiveTile GenerateActiveTile(int x, int y, bool revealed)
    {
        GameObject spawnedTile = Instantiate(prefab_active);

        var activeTile = spawnedTile.GetComponent<ActiveTile>();

        if (activeTile == null)
        {
            activeTile = spawnedTile.AddComponent<ActiveTile>();
        }

        activeTile.x = x * (int)Math.Round(spawnedTile.transform.localScale.x);
        activeTile.y = y * (int)Math.Round(spawnedTile.transform.localScale.y);
        activeTile.revealed = revealed;

        return activeTile;
    }

}
