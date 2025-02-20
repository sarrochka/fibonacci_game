﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBoardTile : MonoBehaviour
{
    public int x;
    public int y;
    public bool revealed;

    // Start is called before the first frame update
    void Start()
    {
        this.transform.position = transform.position + new Vector3(x, y, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetColor(Color newColor)
    {

        var sprite = GetComponent<SpriteRenderer>();
        sprite.color = newColor;
    }
}
