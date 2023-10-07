using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine.UI;

public class Menu : MonoBehaviourPunCallbacks
{
    [Header("Screens")]
    public GameObject mainScreen;
    public GameObject lobbyScreen;

    [Header("Main Screen")]
    public Button createRoomButton;
    public Button joinRoomButton;

    [Header("Lobby Screen")]
    public TextMeshProUGUI playerListText;
    public Button startGameButton;

    void Start()
    {
        createRoomButton.interactable = false;
        joinRoomButton.interactable = false;
        
    }

    public override void OnConnectedToMaster()
    {
        createRoomButton.interactable = true;
        joinRoomButton.interactable = true;
    }

    void setScreen(GameObject screen)
    {
        mainScreen.SetActive(false);
        lobbyScreen.SetActive(false);

        screen.SetActive(true);
    }

    public void onCreateRoomButton(TMP_InputField roomName)
    {
        NetworkManager.instance.CreateRoom(roomName.text);
    }

    public void onJoinRoomButton(TMP_InputField roomName)
    {
        NetworkManager.instance.joinRoom(roomName.text);
    }

    public void onPlayerNameUpdate(TMP_InputField playerName)
    {
        PhotonNetwork.NickName = playerName.text;
    }

    public override void OnJoinedRoom()
    {
        setScreen(lobbyScreen);
        photonView.RPC("updateLobbyUI", RpcTarget.All);
    }

    [PunRPC]
    public void updateLobbyUI()
    {
        playerListText.text = " ";

        foreach(Player player in PhotonNetwork.PlayerList)
        {
            playerListText.text += player.NickName + "\n";
        }

        if(PhotonNetwork.IsMasterClient)
        {
            startGameButton.interactable = true;
        }
        else
        {
            startGameButton.interactable = false;
        }
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        updateLobbyUI();
    }

    public void onLobbyLeaveButton()
    {
        PhotonNetwork.LeaveRoom();
        setScreen(mainScreen);
    }

    public void onStartGameButton()
    {
        NetworkManager.instance.photonView.RPC("ChangeScene", RpcTarget.All, "Game");
    }

    void Update()
    {
        
    }
}
