using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeLoader : MonoBehaviour {
    public int mazeRows, mazeColumns;
    public GameObject wall;
    public GameObject Exit;
    public float size = 2f;

    public MazeCell[,] mazeCells;
    public GameObject Player;

    void Start()
    {
        GenerateMaze();
        Player = Instantiate(Player);
        Player.GetComponent<playerScript>().maze = mazeCells;
    }

    // Update is called once per frame
    void Update()
    {

    }

    //the code and functions 
    public void GenerateMaze()
    {
        DestroyMaze();
        InitializeMaze();
        transform.position = new Vector3(mazeRows * 3, Mathf.Max(mazeRows * 3, mazeColumns * 3), mazeColumns * 3);
        //Create HuntAndKillMazeAlgorithm
        MazeAlgorithm ma = new HuntAndKillMazeAlgorithm(mazeCells);
        ma.CreateMaze();
    }

    //Initialize all the MazeCells for the maze
    private void InitializeMaze()
    {
        mazeCells = new MazeCell[mazeRows, mazeColumns];

        for (int r = 0; r < mazeRows; r++) {
            for (int c = 0; c < mazeColumns; c++) {
                //Create the MazeCell with floor and walls
                mazeCells[r, c] = new MazeCell();

                mazeCells[r, c].floor = Instantiate(wall, new Vector3(r * size, -(size / 2f), c * size), Quaternion.identity);
                mazeCells[r, c].floor.name = "Floor " + r + "," + c;
                mazeCells[r, c].floor.transform.Rotate(Vector3.right, 90f);

                if (c == 0) {
                    mazeCells[r, c].westWall = Instantiate(wall, new Vector3(r * size, 0, (c * size) - (size / 2f)), Quaternion.identity);
                    mazeCells[r, c].westWall.name = "West Wall " + r + "," + c;
                }

                mazeCells[r, c].eastWall = Instantiate(wall, new Vector3(r * size, 0, (c * size) + (size / 2f)), Quaternion.identity);
                mazeCells[r, c].eastWall.name = "East Wall " + r + "," + c;

                if (r == 0) {
                    mazeCells[r, c].northWall = Instantiate(wall, new Vector3((r * size) - (size / 2f), 0, c * size), Quaternion.identity);
                    mazeCells[r, c].northWall.name = "North Wall " + r + "," + c;
                    mazeCells[r, c].northWall.transform.Rotate(Vector3.up * 90f);
                }

                mazeCells[r, c].southWall = Instantiate(wall, new Vector3((r * size) + (size / 2f), 0, c * size), Quaternion.identity);
                mazeCells[r, c].southWall.name = "South Wall " + r + "," + c;
                mazeCells[r, c].southWall.transform.Rotate(Vector3.up * 90f);

                //share the previos southWall with the northWall
                if (r != 0) {
                    mazeCells[r, c].northWall = mazeCells[r-1, c].southWall;
                }

                //share the previos eastWall with the westWall
                if(c!=0){
                    mazeCells[r, c].westWall = mazeCells[r, c-1].eastWall;
                }

            }
        }

        //initilize the MazeExit in a random position
        Vector2Int v = ProceduralNumberGenerator.getNextVector(mazeRows, mazeColumns);
        Exit = Instantiate(Exit, new Vector3(v.x * size, 0, v.y * size), Quaternion.identity);
        Exit.name = "Maze Exit " + v.x + "," + v.y;
        Exit.transform.Rotate(Vector3.right, 0f);
    }

    //Destroys all MazeCells   
    private void DestroyMaze()
    {
        if(mazeCells != null)
        for (int r = 0; r < mazeRows; r++) {
            for (int c = 0; c < mazeColumns; c++)
            {
                mazeCells[r, c].Destroy();
            }
        }
    }
}
