using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCalls : MonoBehaviourPunCallbacks
{
    public PhotonView gamePV;
    public Rules rules;
    public Bounds bounds;
    public InventoryManager inv;
    public List<TileManager> tiles = new List<TileManager>();

    public void PlaceTiles(List<TileInfo> tileList)
    {
        foreach (var info in tileList)
        {
            var inst = PhotonNetwork.InstantiateRoomObject("Tile", transform.position, Quaternion.identity);
            photonView.RPC("SetTile", RpcTarget.All, info.sides, info.value, info.place, info.position, inst.GetPhotonView().ViewID);
        }
    }

    [PunRPC]
    public void SetTile(int sides, int value, bool place, Vector3 pos, int id)
    {
        var inst = PhotonView.Find(id).gameObject;
        inst.transform.SetParent(transform);
        inst.transform.localScale = new Vector3(1, 1, 1);
        inst.transform.localPosition = pos;

        var script = inst.GetComponent<TileManager>();
        script.MakeTile(sides, value, place, photonView.ViewID);
        script.placePiece += Place;

        bounds.Encapsulate(inst.GetComponent<Renderer>().bounds);

        tiles.Add(script);
        transform.localScale = new Vector3(1, 1, 1) / Mathf.Max(bounds.extents.x, bounds.extents.y) * 2;
    }

    public void Place(TileManager tile)
    {
        if (rules.PlaceValid(tile, inv.selected))
        {
            tile.MakePiece(inv.selected.pInfo);
        }
    }

    [PunRPC]
    public void BoardUp() 
    {
        rules.BoardUpdate(tiles);
        if (rules.WinCheck(tiles))
        {
            Debug.Log("Winner!");
            StartRPC();

        }
        else if (rules.TieCheck(tiles))
        {
            Debug.Log("Tie!");
            StartRPC();
        }
    }

    [PunRPC]
    public void Dest()
    {
        foreach (var item in tiles)
        {
            item.Dest();
        }

        photonView.RPC("RoundStart", RpcTarget.All);

        PlaceTiles(rules.GetTiles());
    }

    [PunRPC]
    public void RoundStart()
    {
        transform.localScale = new Vector3(1, 1, 1);
        bounds = new Bounds(transform.position, Vector3.zero);

        tiles.Clear();

        inv.Clear();
        inv.Populate(rules.GetPieces());
    }

    public void StartRPC()
    {
        photonView.RPC("Dest", RpcTarget.MasterClient);
    }


}
