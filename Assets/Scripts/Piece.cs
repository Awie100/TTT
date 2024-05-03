using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviourPunCallbacks
{
    public Mesh mesh;
    public MeshFilter mf;
    public MeshCollider mc;

    public int player, value, sides;
    public float radius, width;
    public bool ring;
    public Color tint;
    public object[] pInfo;

    public delegate void OnClick();
    public OnClick onClick;
    public void DrawPoly()
    {
        if (ring) mesh = MeshManager.RingMesh(sides, radius, width);
        else mesh = MeshManager.PolyMesh(sides, radius);

        mf.mesh = mesh;
        mc.sharedMesh = mesh;

        ColorSet(tint);
    }

    private void OnMouseDown()
    {
        onClick?.Invoke();
    }

    public void ColorSet(Color col)
    {
        MaterialPropertyBlock block = new MaterialPropertyBlock();
        Renderer renderer = GetComponent<Renderer>();

        renderer.GetPropertyBlock(block);
        block.SetColor("_Color", col);
        renderer.SetPropertyBlock(block);

    }

    public void SetValues(object[] info)
    {
        sides = (int)info[0];
        radius = (float)info[1];
        width = (float)info[2];
        ring = (bool)info[3];
        value = (int)info[4];
        var tv3 = (Vector3)info[5];
        tint = new Color(tv3.x, tv3.y, tv3.z);
        pInfo = info;
    }

    [PunRPC]
    public void DrawPiece(object[] info)
    {
        SetValues(info);
        DrawPoly();
    }


}
