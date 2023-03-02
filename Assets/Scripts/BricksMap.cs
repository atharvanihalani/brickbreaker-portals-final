using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BricksMap : MonoBehaviour
{
    Scene myScene;
    Tilemap myTilemap;

    void Awake()
    {
        this.myScene = GetComponentInParent<Scene>();
        this.myTilemap = GetComponent<Tilemap>();
    }

    public int GetBrickCount()
    {
        int bricks = 0;
        this.myTilemap.CompressBounds();
        BoundsInt bounds = myTilemap.cellBounds;
        foreach (Vector3Int pos in bounds.allPositionsWithin)
        {
            if (myTilemap.GetTile(pos) != null)
            {
                bricks++;
            }
        }
        return bricks;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.tag == "Ball")
        {
            Vector2 hitPoint = other.GetContact(0).point;
            Vector3 hitPoint3D = new Vector3(hitPoint.x, hitPoint.y, 0);
            Vector3Int hitPointCell = this.myTilemap.WorldToCell(hitPoint3D);
            
            this.myTilemap.SetTile(hitPointCell, null);
            this.myScene.SubtractBrick();
        }
    }
}
