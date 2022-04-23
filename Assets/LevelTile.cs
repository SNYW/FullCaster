using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LevelTile : MonoBehaviour
{
    public GameObject floorTile;
    public TileType tileType;
    public int incrementX;
    

    public enum TileType
    {
        Battle
    }

    private void OnEnable()
    {
        NavMeshBuildSource source = new NavMeshBuildSource();
        source.sourceObject = floorTile;
    }

}
