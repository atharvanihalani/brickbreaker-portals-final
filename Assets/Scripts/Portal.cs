using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    Scene myScene;
    [SerializeField] Portal portalPair;
    AudioSource audioSource;

    void Awake()
    {
        this.myScene = GetComponentInParent<Scene>();
        this.audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound()
    {
        this.audioSource.Play(0);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Ball")
        {
            this.myScene.Teleport(this.portalPair.GetPos(), this);
            Debug.Log("teleport");
        }
    }

    public Vector3 GetPos()
    {
        return transform.position;
    }

}
