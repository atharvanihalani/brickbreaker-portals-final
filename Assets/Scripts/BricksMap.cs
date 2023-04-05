using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BricksMap : MonoBehaviour
{
    [SerializeField] TileBase myTileBase;
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

    public List<Vector3Int> GetBrickPositions() 
    {
        List<Vector3Int> brickPositions = new List<Vector3Int>();
        this.myTilemap.CompressBounds();
        BoundsInt bounds = myTilemap.cellBounds;
        foreach (Vector3Int pos in bounds.allPositionsWithin)
        {
            if (myTilemap.GetTile(pos) != null)
            {
                brickPositions.Add(pos);
            }
        }

        return brickPositions;
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

    public void ClearAll()
    {
        this.myTilemap.ClearAllTiles();
    }

    public void AddBricksAt(Vector3Int[] positions)
    {
        TileBase[] tileArray = new TileBase[positions.Length];
        for (int i = 0; i < tileArray.Length; i++)
        {
            tileArray[i] = this.myTileBase;
        }
        this.myTilemap.SetTiles(positions, tileArray);
    }
}
