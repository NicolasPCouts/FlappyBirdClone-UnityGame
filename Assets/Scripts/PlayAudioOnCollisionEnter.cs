using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudioOnCollisionEnter : MonoBehaviour
{
    public AudioSource source;

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if(collision.gameObject.tag == "Player")
        {
            if (!collision.gameObject.GetComponent<FlappyBirdController>().IsDead)
                source.Play();
        }
    }
}
