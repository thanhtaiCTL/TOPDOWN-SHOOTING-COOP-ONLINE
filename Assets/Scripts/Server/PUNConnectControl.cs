using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PUNConnectControl : MonoBehaviourPunCallbacks
{
    private string gameVersion = "1";

    public byte numberPlayer = 2;
    public MatchView matchView;

    private void Start()
    {
        
    }

    public void Connect()
    {
       if(numberPlayer <= 1)
       {
            PhotonNetwork.OfflineMode = true;
            PhotonNetwork.LoadLevel("InGame");
       }
       else
        {
            PhotonNetwork.AutomaticallySyncScene = true;
            if(PhotonNetwork.IsConnected)
            {
                JoinRoom();
            }
            else
            {
                PhotonNetwork.GameVersion = gameVersion;
                PhotonNetwork.ConnectUsingSettings();
            }
       }
    }

    private void JoinRoom()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnConnected()
    {
        matchView.SetInfo("OnConnected()");
        Debug.LogError("OnConnected");
    }

    public override void OnConnectedToMaster()
    {
        matchView.SetInfo("OnConnectedToMaster()");
        Connect();
        Debug.LogError("OnConnectedToMaster");
    }

    public override void OnCreatedRoom()
    {
        matchView.SetInfo("OnCreatedRoom");
        Debug.LogError("OnCreatedRoom");
    }

    public override void OnJoinedRoom()
    {
        matchView.SetInfo("OnJoinedRoom");
        Debug.LogError("OnJoinedRoom");
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        matchView.SetInfo("OnJoinRandomFailed()");
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = numberPlayer;
        roomOptions.EmptyRoomTtl = 10;
        PhotonNetwork.CreateRoom("K2-2018", roomOptions);
        Debug.LogError("OnJoinRandomFailed");
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        matchView.SetInfo("OnCreateRoomFailed()");
        Debug.LogError("OnCreateRoomFailed");
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        matchView.SetInfo("OnJoinRoomFailed");
        Debug.LogError("OnJoinRoomFailed");
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        if(PhotonNetwork.IsMasterClient)
        {
            Debug.LogError("@@@@@@OnPlayerEnteredRoom Count: " + PhotonNetwork.CurrentRoom.PlayerCount);
            matchView.SetInfo("@@@@@@OnPlayerEnteredRoom Count: " + PhotonNetwork.CurrentRoom.PlayerCount);
            if (PhotonNetwork.CurrentRoom.PlayerCount >= numberPlayer)
                PhotonNetwork.LoadLevel("InGame");
        }
        else
        {
            matchView.SetInfo("OnPlayerEnteredRoom Count: " + PhotonNetwork.CurrentRoom.PlayerCount);
            Debug.LogError("OnPlayerEnteredRoom Count: " + PhotonNetwork.CurrentRoom.PlayerCount);
        }
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        matchView.SetInfo("OnPlayerLeftRoom Count: " + PhotonNetwork.CurrentRoom.PlayerCount);
        Debug.LogError("OnPlayerLeftRoom Count: " + PhotonNetwork.CurrentRoom.PlayerCount);
    }

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        matchView.SetInfo("-------OnMasterClientSwitched---------");
        Debug.LogError("------ - OnMasterClientSwitched-------- - ");
    }
}
