using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class AltBricksMap : MonoBehaviour
{
    [SerializeField] TileBase myTileBase;
    Scene myScene;
    Tilemap myTilemap;
    AudioSource audioSource;

    void Awake()
    {
        this.myScene = GetComponentInParent<Scene>();
        this.myTilemap = GetComponent<Tilemap>();
        this.audioSource = GetComponent<AudioSource>();
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        this.audioSource.Play(0);
    }

    public List<Vector3Int> GetBrickPositions()
    {
        List<Vector3Int> altBrickPositions = new List<Vector3Int>();
        this.myTilemap.CompressBounds();
        BoundsInt bounds = myTilemap.cellBounds;
        foreach (Vector3Int pos in bounds.allPositionsWithin)
        {
            if (myTilemap.GetTile(pos) != null)
            {
                altBrickPositions.Add(pos);
            }
        }

        return altBrickPositions;
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
