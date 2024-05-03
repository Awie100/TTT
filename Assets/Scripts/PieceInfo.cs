using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceInfo
{
    public int sides;
    public float radius;
    public float width;
    public bool ring;
    public int value;
    public Color tint;

    public PieceInfo(int value, int sides, float radius, float width, bool ring = false, Color tint = new Color())
    {
        this.value = value;
        this.sides = sides;
        this.radius = radius;
        this.width = width;
        this.ring = ring;
        this.tint = tint;
    }

    public object[] Deconstruct()
    {
        Vector3 tv3 = (Vector4)tint;
        return new object[] { sides, radius, width, ring, value, tv3};
    }
}
