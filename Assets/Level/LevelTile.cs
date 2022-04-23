using UnityEngine;

public class LevelTile : MonoBehaviour
{
    public int enemyAmount;

    public GameObject floorTile;
    public TileType tileType;
    public int incrementX;


    public enum TileType
    {
        Battle
    }

    private void Start()
    {
        SpawnManager.Instance.AddEnemies(enemyAmount);
    }

}
