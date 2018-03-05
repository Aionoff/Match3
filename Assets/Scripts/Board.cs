using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour {
    public int widthBoard;
    public int hightBoard;
    public GameObject[] tilePrefabs;

    private BackgroundTile[,] tiles;
    
	void Start () {
        tiles = new BackgroundTile[widthBoard, hightBoard];
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
                tile.name = "[" + i + ", " + j + "]";
            }
        }
    }

    private GameObject GetRandomTile()
    {
        int index = (int) Mathf.Round(Random.Range(0.0f, tilePrefabs.Length - 1));

        return tilePrefabs[index];
    }
}
