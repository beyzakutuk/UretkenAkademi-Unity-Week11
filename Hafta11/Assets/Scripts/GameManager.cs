using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class GameManager : MonoBehaviourPunCallbacks
{
    public bool gameEnded = false;

    public string playerPrefabLocation;

    public Transform[] spawnPoints;
    public PlayerController[] players;
    private int playersInGame;

    public static GameManager instance;

    private void Awake()
    {
        instance = this;
    }


    void Start()
    {
        players = new PlayerController[PhotonNetwork.PlayerList.Length];
    }

    [PunRPC]
    void ImInGame()
    {
        playersInGame++;
        if (playersInGame == PhotonNetwork.PlayerList.Length)
            spawnPlayer();
    }

    void spawnPlayer()
    {
        GameObject playerobj = PhotonNetwork.Instantiate(playerPrefabLocation, spawnPoints[Random.Range(0, spawnPoints.Length)].position, Quaternion.identity);
        PlayerController playerScript = playerobj.GetComponent<PlayerController>();

        playerScript.photonView.RPC("Initialize", RpcTarget.All, PhotonNetwork.LocalPlayer);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
