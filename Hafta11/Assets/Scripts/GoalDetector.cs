using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalDetector : MonoBehaviour
{
    private int score1 = 0; // for player1
    private int score2 = 0; // for player2



    void Start()
    {
        PlayerPrefs.SetInt("Score1", score1);
        PlayerPrefs.SetInt("Score2", score2);
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Sınır1"))
        {
            score2++;
            PlayerPrefs.SetInt("Score2", score2);
        }

        if(collision.gameObject.CompareTag("Sınır2"))
        {
            score1++;
            PlayerPrefs.SetInt("Score1", score1);
        }
    }

    void Update()
    {
        
        
    }
}
