using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioController : MonoBehaviour
{
    public AudioClip jumpClip;
    public AudioClip landClimpClip;
    public AudioClip spiritJumpClip;
    public AudioClip spiritHitClip;
    public AudioClip spiritDeathClip;
    public AudioClip gameOverClip;
    public AudioSource myAS;

    public void playSound(string sound)
    {
        switch (sound)
        {
            case "jump":
                myAS.clip = jumpClip;
                //myAS.time = 0.3f;
                myAS.Play();
                break;
            case "land":
                myAS.clip = landClimpClip;
                myAS.Play();
                break;
            case "spiritDeath":
                myAS.clip = spiritDeathClip;
                myAS.Play();
                break;
            case "spiritHit":
                myAS.clip = spiritHitClip;
                myAS.Play();
                break;
            case "spiritJump":
                myAS.clip = spiritJumpClip;
                myAS.Play();
                break;
            case "gameOver":
                myAS.clip = gameOverClip;
                myAS.Play();
                break;
        }
            

    }
}
