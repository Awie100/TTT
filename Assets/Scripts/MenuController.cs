using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine.UI;

public class MenuController : MonoBehaviourPunCallbacks
{
    public static MenuController Inst;

    public GameObject list, create, loading, button;
    public Transform rlObject;
    List<TMP_Text> roomCache;

    public void Cancel()
    {
        loading.SetActive(false);
        create.SetActive(false);
        list.SetActive(true);
    }

    public void Create()
    {
        list.SetActive(false);
        loading.SetActive(false);
        create.SetActive(true);
    }

    public void Loading()
    {
        list.SetActive(false);
        create.SetActive(false);
        loading.SetActive(true);
    }

    private void Start()
    {
        Inst = this;
        roomCache = new List<TMP_Text>();
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        Debug.Log(roomList.Count);
        foreach (RoomInfo info in roomList)
        {

            var tmp = roomCache.Find(i => i.name == info.Name);

            if(tmp)
            {
                roomCache.Remove(tmp);
                Destroy(tmp.transform.parent.gameObject);
            }

            if (info.RemovedFromList) return;
            var butt = Instantiate(button, rlObject);
            var text = butt.GetComponentInChildren<TMP_Text>();
            text.name = info.Name;
            text.text = "Room: " + info.Name + " ( "  + info.PlayerCount + " / " + info.MaxPlayers + " )";
            roomCache.Add(text);
            butt.GetComponent<Button>().onClick.AddListener(delegate { RoomEnter(info.Name); });

        }
    }



    public void RoomEnter(string name)
    {
        Loading();
        Debug.Log("ROO ENET");
        PhotonNetwork.JoinRoom(name);
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Cancel();
        Debug.Log("room fail: " + message);
    }

    public override void OnJoinedLobby()
    {
        Cancel();
        Debug.Log("joined lobby");
    }
}
