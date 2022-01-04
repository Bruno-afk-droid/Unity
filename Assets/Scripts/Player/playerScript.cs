using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerScript : MonoBehaviour
{
    public MazeCell[,] maze;
    public Vector2 mazePosition = new Vector2(0.0f,0.0f);

    private float movementSpeed = 0.1f;
    private MazeCell currentMazeCell;
    private Vector2 vel = new Vector2(0.0f, 0.0f);

    // Start is called before the first frame update
    void Start()
    {
        MoveToCell(Mathf.RoundToInt(mazePosition.x), Mathf.RoundToInt(mazePosition.y));
    }

    // Update is called once per frame
    void Update()
    {
        
        if (vel.x == 0 && vel.y == 0)
        { //if the player is not moving check if there is input for direction
            MoveToCell((int)mazePosition.x, (int)mazePosition.y);
            Movement();
        }
        else
        { //else move the player to the next cell
                if (vel.x != 0)
                {
                    vel.x = Mathf.Round(vel.x * 10.0f) * 0.1f;
                    mazePosition.x = Mathf.Round(mazePosition.x * 10.0f) * 0.1f;
                    mazePosition.x += Sign(vel.x) * movementSpeed;
                    vel.x -= Sign(vel.x) * movementSpeed;
                }

                if (vel.y != 0) {
                    vel.y = Mathf.Round(vel.y * 10.0f) * 0.1f;
                    mazePosition.y = Mathf.Round(mazePosition.y * 10.0f) * 0.1f;
                    mazePosition.y += Sign(vel.y) * movementSpeed;
                    vel.y -= Sign(vel.y) * movementSpeed;
                }
        }
        //set the render position of the player
        transform.position= new Vector3(mazePosition.x * 6,1,mazePosition.y * 6);
        
    }

    //this is the movement code for the player
    private void Movement()
    {
        /* // this was a test to detect witch is available 
        string STR = "";
        if(currentMazeCell.northWall == null) STR += "Wall left ";
        if(currentMazeCell.southWall == null) STR+="Wall right ";
        if(currentMazeCell.eastWall == null) STR += "Wall up";
        if (currentMazeCell.westWall == null) STR += "Wall down ";
        print(STR);
        */

        vel.x = 0;
        vel.y = 0;
        Vector2Int Direction = new Vector2Int((int)Input.GetAxisRaw("Horizontal"), (int)Input.GetAxisRaw("Vertical"));
        if(Direction.x > 0 && (currentMazeCell.southWall == null)) {
            //move right
            vel.x = 1.0f;
        }
        else if (Direction.x < 0 && (currentMazeCell.northWall == null)) {
            //move left
            vel.x = -1.0f;
        }
        else if (Direction.y > 0 && (currentMazeCell.eastWall == null)) {
            //move up
            vel.y = 1.0f;
        }
        else if (Direction.y < 0 && (currentMazeCell.westWall == null)) {
            //move down
            vel.y = -1.0f;
        }
    }

    private void MoveToCell(int r,int c)
    {
        //set new coordinates for cell
        currentMazeCell = maze[r, c];
    }

    //I create this Sign funcion instead of using Mathf.Sign because Mathf.Sign can't return 0 when the value is 0
    private int Sign(float i)
    {
        if (i < 0) return -1;
        if (i > 0) return 1;
        return 0;
    }
}
