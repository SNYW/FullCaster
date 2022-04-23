using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public float leftSpeed;
    public float maxX;
    public List<LevelTile> allLiveTiles;
    public GameObject[] possibleTiles;
    public LevelTile currentTile;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            IncrementTiles();
        }

        if(GameManager.Instance.playerState == GameManager.PlayerState.Moving)
        {
            allLiveTiles.RemoveAll(tile => tile == null);
            allLiveTiles.ForEach(tile => HandleTile(tile));
        }

        if(currentTile.incrementX >= currentTile.transform.position.x)
        {
            IncrementTiles();
        }
    }

    private void HandleTile(LevelTile tile)
    {
        tile.transform.position += Vector3.left * leftSpeed * Time.deltaTime;
        if (tile.transform.position.x < maxX)
        {
            Destroy(tile.gameObject);
        }
    }

    private void IncrementTiles()
    {
        var xOffset = currentTile.floorTile.GetComponent<Collider>().bounds.size.x;
        var newTile = Instantiate(Utils.RandomFromList(possibleTiles.ToList())).GetComponent<LevelTile>();
        Vector3 newpos = new Vector3(currentTile.transform.position.x + xOffset, currentTile.transform.position.y, currentTile.transform.position.z);
        newTile.transform.position = newpos;
        currentTile = newTile;
        allLiveTiles.Add(newTile);
    }
}
