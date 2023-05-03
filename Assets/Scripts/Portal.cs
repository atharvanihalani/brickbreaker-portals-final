using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    Scene myScene;
    [SerializeField] Portal portalPair;

    void Awake()
    {
        this.myScene = GetComponentInParent<Scene>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Ball")
        {
            
            this.myScene.Teleport(this.portalPair.GetPos());
            Debug.Log("teleport");
        }
    }

    public Vector3 GetPos()
    {
        return transform.position;
    }

}
