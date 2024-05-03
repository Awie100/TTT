using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileInfo
{
    public int sides;
    public int value;
    public bool place;
    public Vector3 position;

    public TileInfo(int sides, float xpos, float ypos, int value = 0, bool place = true)
    {
        this.sides = sides;
        this.value = value;
        this.position = new Vector3(xpos, ypos);
        this.place = place;
    }
}
