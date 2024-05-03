using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshManager : MonoBehaviour
{
    public static Mesh PolyMesh(int sides, float inrad)
    {
        Mesh mesh = new Mesh();

        List<Vector3> vx = new List<Vector3>();
        List<int> tris = new List<int>();
        List<Vector2> uvs = new List<Vector2>();

        float ang = Mathf.PI * (1f / sides - 0.5f);
        float dang = Mathf.PI * 2 / sides;

        for (int i = 0; i < sides; i++)
        {
            var vec = new Vector3(Mathf.Cos(ang + i * dang), Mathf.Sin(ang + i * dang));
            vx.Add(vec * inrad);
            uvs.Add((vec + new Vector3(1,1)) / 2);
        }

        for (int i = 0; i < sides - 2; i++)
        {
            tris.Add(0);
            tris.Add(i + 2);
            tris.Add(i + 1);
        }

        mesh.vertices = vx.ToArray();
        mesh.triangles = tris.ToArray();
        mesh.uv = uvs.ToArray();

        return mesh;
    }

    public static Mesh RingMesh(int sides, float inrad, float width)
    {
        Mesh mesh = new Mesh();

        List<Vector3> vx = new List<Vector3>();
        List<int> tris = new List<int>();
        List<Vector2> uvs = new List<Vector2>();

        float ang = Mathf.PI * (1f / sides - 0.5f);
        float dang = Mathf.PI * 2 / sides;

        var initVec = new Vector3(Mathf.Cos(ang), Mathf.Sin(ang));
        vx.Add(initVec * inrad);
        uvs.Add((initVec + new Vector3(1, 1)) / 2);

        vx.Add(initVec * inrad * (1 - width));
        uvs.Add((initVec * (1 - width) + new Vector3(1, 1)) / 2);


        for (int i = 1; i < sides; i++)
        {
            var vec = new Vector3(Mathf.Cos(ang + i * dang), Mathf.Sin(ang + i * dang));
            vx.Add(vec * inrad);
            uvs.Add((vec + new Vector3(1, 1)) / 2);

            vx.Add(vec * inrad * (1 - width));
            uvs.Add((vec * (1 - width) + new Vector3(1, 1)) / 2);


            var ind = vx.Count - 1;

            tris.Add(ind);
            tris.Add(ind - 1);
            tris.Add(ind - 2);
            tris.Add(ind - 3);
            tris.Add(ind - 2);
            tris.Add(ind - 1);
        }

        var count = vx.Count - 1;
        tris.Add(1);
        tris.Add(0);
        tris.Add(count);
        tris.Add(count - 1);
        tris.Add(count);
        tris.Add(0);


        mesh.vertices = vx.ToArray();
        mesh.triangles = tris.ToArray();
        mesh.uv = uvs.ToArray();

        return mesh;
    }
}
