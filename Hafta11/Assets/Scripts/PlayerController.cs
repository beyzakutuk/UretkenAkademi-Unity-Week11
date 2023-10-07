using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SocialPlatforms.Impl;

public class PlayerController : MonoBehaviourPunCallbacks
{
    [HideInInspector]
    public int id;

    [Header("Info")]
    public float moveSpeed;
    public float jumpForce;
    private int playerScore1 = 0;
    private int playerScore2 = 0;

    [Header("Compenents")]
    public Rigidbody rg;
    public Player photonPlayer;
    
    void Start()
    {
        PlayerPrefs.SetInt("gol1", playerScore1);
        PlayerPrefs.SetInt("gol2", playerScore2);
    }

    void Update()
    {
        if(photonView.IsMine)
        {
            Move();
            if(Input.GetKeyDown(KeyCode.Space))
            {
                TryJump();
            }
        }
        
    }

    void Move()
    {
        float x = Input.GetAxis("Horizontal") * moveSpeed;
        float z = Input.GetAxis("Vertical") * moveSpeed;

        rg.velocity = new Vector3(-x, rg.velocity.y, -z);
    }

    void TryJump()
    {
        Ray ray = new Ray(transform.position, Vector3.down);

        if (Physics.Raycast(ray, 0.7f))
        {
            rg.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    public void Initialize(Player player)
    {
        photonPlayer = player;
        id = player.ActorNumber;

        GameManager.instance.players[id - 1] = this;

        if (!photonView.IsMine)
            rg.isKinematic = true;
    }
}
