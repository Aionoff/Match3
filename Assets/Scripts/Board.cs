using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour {
    public int widthBoard;
    public int hightBoard;
    public GameObject[] tilePrefabs;
    public GameObject[,] tiles;
    
	void Start () {
        tiles = new GameObject[widthBoard, hightBoard];
        SetUp();
	}
	
    private void SetUp()
    {
        for (int i = 0; i < widthBoard; i++)
        {
            for (int j = 0; j < hightBoard; j++)
            {
                var position = new Vector2(i, j);
                var tile = Instantiate(GetRandomTile(), position, Quaternion.identity);
                tile.transform.parent = this.transform;
                tile.name = "[i = " + i + ", j = " + j + "]";
                tiles[i, j] = tile;
                tiles[i, j].GetComponent<Dot>().targetY = j;
                tiles[i, j].GetComponent<Dot>().targetX = i;
            }
        }
    }

    private GameObject GetRandomTile()
    {
        int index = (int) Mathf.Round(Random.Range(0.0f, tilePrefabs.Length - 1));

        return tilePrefabs[index];
    }
}
