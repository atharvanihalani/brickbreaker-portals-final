using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class UnbreakableBricksMap : MonoBehaviour
{
    AudioSource audioSource;

    void Awake() 
    {
        this.audioSource = GetComponent<AudioSource>();
    }


    void OnCollisionEnter2D(Collision2D other)
    {
        this.audioSource.Play(0);
    }
}
