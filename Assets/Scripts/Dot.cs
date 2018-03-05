using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dot : MonoBehaviour {
    public int row;
    public int column;

    private float swipeAngle = 0;
    private Vector2 firstPosition;
    private Vector2 secondPosition;
    private GameObject otherDot;
    private Board board;

    // Use this for initialization
    void Start () {
        board = FindObjectOfType<Board>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnMouseDown()
    {
        firstPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void OnMouseUp()
    {
        secondPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        CalculateAngle();
    }

    private void CalculateAngle()
    {
        swipeAngle = Mathf.Atan2(secondPosition.y - firstPosition.y, secondPosition.x - firstPosition.x) * 180/Mathf.PI;

    }
}
