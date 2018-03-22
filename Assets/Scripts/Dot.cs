using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dot : MonoBehaviour {
    [Header("Board variables")]
    public int column;
    public int row;
    public int targetX;
    public int targetY;
    public bool isMatched;
    public int previousColumn;
    public int previousRow;

    private float swipeAngle = 0;
    private Vector2 firstPosition;
    private Vector2 secondPosition;
    private Vector2 tempPosition;
    private GameObject otherDot;
    private Board board;

    // Use this for initialization
    void Start () {
        board = FindObjectOfType<Board>();
        isMatched = false;
        targetX = (int)transform.position.x;
        targetY = (int)transform.position.y;
        row = targetY;
        column = targetX;
        previousColumn = column;
        previousRow = row;
    }
	
	void Update () {
        FindMatches();

        if (isMatched)
        {
            SpriteRenderer matchedSprite = GetComponent<SpriteRenderer>();
            matchedSprite.color = new Color(0f, 0f, 0f, 0.2f);
        }

        targetX = column;
        targetY = row;


        if (Mathf.Abs(targetX - transform.position.x) > .1)
        {
            tempPosition = new Vector2(targetX, transform.position.y);
            transform.position = Vector2.Lerp(transform.position, tempPosition, .4f);
        }
        else
        {
            tempPosition = new Vector2(targetX, transform.position.y);
            transform.position = tempPosition;

            board.tiles[column, row] = this.gameObject;
        }

        if (Mathf.Abs(targetY - transform.position.y) > .1)
        {
            tempPosition = new Vector2(transform.position.x, targetY);
            transform.position = Vector2.Lerp(transform.position, tempPosition, .4f);
        }
        else
        {
            tempPosition = new Vector2(transform.position.x, targetY);
            transform.position = tempPosition;

            board.tiles[column, row] = this.gameObject;
        }
    }

    public IEnumerator CheckMove()
    {
        yield return new WaitForSeconds(0.5f);

        if (otherDot != null)
        {
            if (!otherDot.GetComponent<Dot>().isMatched && !isMatched)
            {
                otherDot.GetComponent<Dot>().row = row;
                otherDot.GetComponent<Dot>().column = column;

                row = previousRow;
                column = previousColumn;

                Debug.Log("row: " + row + "; col: " + column + " prRow: " + previousRow + "prCol: " + previousColumn);
            }

            otherDot = null;
        }
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
       // Debug.Log(swipeAngle);
        Move();
    }

    private void Move()
    {
        if (board.widthBoard - 1 > column && swipeAngle > -45 && swipeAngle <= 45)
        {
            //move right
            otherDot = board.tiles[column + 1, row];
            otherDot.GetComponent<Dot>().column--;
            column++;
            Debug.Log("right");
        }
        else if (board.hightBoard - 1 > row && swipeAngle > 45 && swipeAngle <= 135)
        {
            //move up
            otherDot = board.tiles[column, row + 1];
            otherDot.GetComponent<Dot>().row--;
            row++;
            Debug.Log("Up");
        }
        else if (column > 0 && (swipeAngle > 135 || swipeAngle <= -135))
        {
            //move left
            otherDot = board.tiles[column-1, row];
            otherDot.GetComponent<Dot>().column++;
            column--;
            Debug.Log("left");
        }
        else if (row > 0 && (swipeAngle < -45 && swipeAngle >= -135))
        {
            //move down
            otherDot = board.tiles[column, row - 1];
            otherDot.GetComponent<Dot>().row++;
            row--;
            Debug.Log("Down");
        }

        StartCoroutine(CheckMove());
    }

    void FindMatches()
    {
        if(column > 0 && column < board.widthBoard - 1)
        {
            GameObject leftDot1 = board.tiles[column - 1, row];
            GameObject rightDot1 = board.tiles[column + 1, row];

            if(leftDot1.tag == tag && rightDot1.tag == tag)
            {
                leftDot1.GetComponent<Dot>().isMatched = true;
                rightDot1.GetComponent<Dot>().isMatched = true;
                isMatched = true;
            }
        }

        if (row > 0 && row < board.hightBoard - 1)
        {
            GameObject upDot1 = board.tiles[column, row + 1];
            GameObject downDot1 = board.tiles[column, row - 1];

            if (upDot1.tag == tag && downDot1.tag == tag)
            {
                upDot1.GetComponent<Dot>().isMatched = true;
                downDot1.GetComponent<Dot>().isMatched = true;
                isMatched = true;
            }
        }
    }
}
