using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


public class TileManager : MonoBehaviourPunCallbacks
{
    public Mesh mesh;
    public MeshFilter meshFilter;
    public MeshCollider meshCollider;
    public Transform display;
    public PhotonView grid;

    public int value;
    public int sides;
    public bool place = true;
    public List<Piece> pieces;



    public delegate void Place(TileManager tile);
    public Place placePiece;


    public void MakeTile(int sides, int value, bool place, int grid)
    {
        this.sides = sides;
        this.value = value;
        this.place = place;
        this.grid = PhotonView.Find(grid);

        mesh = MeshManager.PolyMesh(sides, 1f);

        display.localScale = new Vector3(1, 1, 1) * Mathf.Cos(Mathf.PI / sides);

        meshFilter.mesh = mesh;
        meshCollider.sharedMesh = mesh;
    }

    private void OnMouseDown()
    {
        MouseDown();
    }

    public void MakePiece(object[] piece)
    {
        photonView.RPC("PieceMaster", RpcTarget.MasterClient, piece);
    }

    [PunRPC]
    public void PieceMaster(object[] piece)
    {
        var pie = PhotonNetwork.InstantiateRoomObject("Piece", Vector3.zero, Quaternion.identity);
        photonView.RPC("SetPiece", RpcTarget.All, piece, pie.GetPhotonView().ViewID);
    }

    [PunRPC]
    public void SetPiece(object[] piece, int id)
    {
        var pie = PhotonView.Find(id).gameObject;
        pie.transform.SetParent(display);
        pie.transform.localScale = new Vector3(1, 1, 1);
        pie.transform.localPosition = Vector3.zero;
        var script = pie.GetComponent<Piece>();
        script.DrawPiece(piece);
        pieces.Add(script);
        script.onClick += MouseDown;

        if(PhotonNetwork.IsMasterClient) grid.RPC("BoardUp", RpcTarget.MasterClient);

    }

    private void MouseDown()
    {
        if (!place) return;

        placePiece(this);

        Debug.Log("peeep");
    }

    public void Dest()
    {
        foreach (var piece in pieces)
        {
            PhotonNetwork.Destroy(piece.gameObject);
        }

        PhotonNetwork.Destroy(gameObject);
    }
}
