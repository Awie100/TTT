using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviourPunCallbacks
{
    public GridCalls grid;
    public InventoryManager inv;
    public Rules rules;


    private void Start()
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
            photonView.RPC("GridRPC", RpcTarget.MasterClient);
        }
    }

    [PunRPC]
    public void GridRPC()
    {
        grid = PhotonNetwork.InstantiateRoomObject("Grid", Vector3.zero, Quaternion.identity).GetComponent<GridCalls>();
        photonView.RPC("SetGrid", RpcTarget.All, grid.photonView.ViewID);
        grid.StartRPC();
    }


    [PunRPC]
    public void SetGrid(int id)
    {
        var grid = PhotonView.Find(id).gameObject.GetComponent<GridCalls>();
        grid.gamePV = photonView;
        grid.rules = rules;
        grid.inv = inv;
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        
    }
}
