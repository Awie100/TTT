using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CreateController : MonoBehaviourPunCallbacks
{

    public TMP_InputField input;
    public GameObject errorButton;

    public void Create()
    {
        if (string.IsNullOrEmpty(input.text))
        {
            StartCoroutine(createFail("Room Name is Empty or Null."));
            return;
        }

        PhotonNetwork.CreateRoom(input.text.Trim(), new RoomOptions() {MaxPlayers = 2});
        MenuController.Inst.Loading();

    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        MenuController.Inst.Create();
        StartCoroutine(createFail(message));
    }

    public IEnumerator createFail(string message)
    {
        Debug.Log("createFailed");

        TMP_Text text = errorButton.GetComponentInChildren<TMP_Text>();
        text.text = "Error: " + message;
        errorButton.gameObject.SetActive(true);

        yield return new WaitForSeconds(3);

        errorButton.gameObject.SetActive(false);

    }

    public override void OnJoinedRoom()
    {
        Debug.Log("joined " + PhotonNetwork.CurrentRoom.Name);
        if (!PhotonNetwork.IsMasterClient) return;
        PhotonNetwork.LoadLevel("TicTacOnline");
    }
}
