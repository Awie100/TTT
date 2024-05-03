using Photon.Pun;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;

public class RoomController : MonoBehaviourPunCallbacks, IPunObservable
{
    public TMP_Text title;
    Room room;

    // Start is called before the first frame update
    void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        room = PhotonNetwork.CurrentRoom;
        title.text = room.Name + " ( 1 / " + room.MaxPlayers + " )";
    }

    // Update is called once per frame
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        if (!PhotonNetwork.IsMasterClient) return;
        UpText();
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        if (!PhotonNetwork.IsMasterClient) return;
        UpText();
    }

    public void UpText()
    {
        title.text = room.Name + " ( " + room.PlayerCount + " / " + room.MaxPlayers + " )";
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(title.text);
        }
        else
        {
            title.text = (string)stream.ReceiveNext();
        }
    }
}
