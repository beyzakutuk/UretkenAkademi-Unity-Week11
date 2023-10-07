using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.Linq;

public class GameManager : MonoBehaviourPunCallbacks
{
    public bool gameEnded = false;
    public float TimeToWin;
    public float invictibleDuration;

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
        photonView.RPC("imInGame", RpcTarget.All);
    }

    [PunRPC]
    void imInGame()
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
    /*
    public PlayerController GetPlayer(int playerid)
    {
        return players.First(x => x.id == playerid);
    }

    public PlayerController GetPlayer(GameObject playerobj)
    {
        return players.First(x => x.gameObject == playerobj);
    }
    */
    // Update is called once per frame
    void Update()
    {
        
    }
}
