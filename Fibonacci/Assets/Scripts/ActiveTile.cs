using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ActiveTile : MonoBehaviour
{
    public int x;
    public int y;
    public bool revealed;

    // Start is called before the first frame update
    void Start()
    {
        this.transform.position = transform.position + new Vector3(x, y, 1 - Convert.ToInt32(revealed));
    }

    // Update is called once per frame
    void Update()
    {

    }
}