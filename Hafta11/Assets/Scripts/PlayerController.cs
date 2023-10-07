using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PlayerController : MonoBehaviourPunCallbacks
{
    [HideInInspector]
    public int id;

    [Header("Info")]
    public float moveSpeed;
    public float jumpForce;

    [Header("Compenents")]
    public Rigidbody rg;
    public Player photonPlayer;
    
    void Start()
    {
        
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
}
