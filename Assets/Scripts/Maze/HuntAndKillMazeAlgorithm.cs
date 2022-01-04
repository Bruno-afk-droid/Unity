using UnityEngine;
using System.Collections;
public class HuntAndKillMazeAlgorithm : MazeAlgorithm {

    private int currentRow = 0;
    private int currentColumn = 0;

    private bool courseComplete = false;

    public HuntAndKillMazeAlgorithm(MazeCell[,] mazeCells) : base(mazeCells) {
    }

    public override void CreateMaze() {
        HuntAndKill ();
    }

    private void HuntAndKill () {
        mazeCells[currentRow, currentColumn].visited = true;

        while (! courseComplete) {
            Kill();
            Hunt();
        }
    }

    //the function that creates an random path until it reaches an death end
    private void Kill() {
        while (RouteStillAvailable (currentRow, currentColumn)) {

            int direction = ProceduralNumberGenerator.getNextNumber ();

            if (direction == 1 && CellIsAvailable(currentRow - 1, currentColumn)) {
                // up
                DestroyWallIfItExists(mazeCells[currentRow, currentColumn].northWall);
                DestroyWallIfItExists(mazeCells[currentRow - 1, currentColumn].southWall);
                currentRow--;
            }else if (direction == 2 && CellIsAvailable(currentRow + 1, currentColumn)) {
                // down
                DestroyWallIfItExists(mazeCells[currentRow, currentColumn].southWall);
                DestroyWallIfItExists(mazeCells[currentRow + 1, currentColumn].northWall);
                currentRow++;
            }else if (direction == 3 && CellIsAvailable(currentRow, currentColumn + 1)) {
                // right
                DestroyWallIfItExists(mazeCells[currentRow, currentColumn].eastWall);
                DestroyWallIfItExists(mazeCells[currentRow, currentColumn + 1].westWall);
                currentColumn++;
            }else if (direction == 4 && CellIsAvailable(currentRow, currentColumn - 1)) {
                // left
                DestroyWallIfItExists(mazeCells[currentRow, currentColumn].westWall);
                DestroyWallIfItExists(mazeCells[currentRow, currentColumn - 1].eastWall);
                currentColumn--;
            }

            mazeCells[currentRow, currentColumn].visited = true;
        }
    }

    // Seach for an startPoint for the new path
    private void Hunt() {
        courseComplete = true;

        for (int r=0; r < mazeRows; r++) {
            for(int c=0; c < mazeColumns; c++) {
                if(!mazeCells[r,c].visited && CellHasAnAdjacentVisitedCell(r,c)) {
                    courseComplete = false;
                    currentRow = r;
                    currentColumn = c;
                    DestroyAdjacentWall(currentRow, currentColumn);
                    mazeCells[currentRow, currentColumn].visited = true;
                    return;
                }
            }
        }
    }
    //check if there is still an direction available
    private bool RouteStillAvailable(int row, int column) {
        int availableRoutes = 0;

        if (row > 0 && !mazeCells[row-1,column].visited) {
            availableRoutes++;
        }

        if (row < mazeRows - 1 && !mazeCells[row + 1, column].visited)
        {
            availableRoutes++;
        }

        if (column > 0 && !mazeCells[row, column-1].visited)
        {
            availableRoutes++;
        }

        if (column < mazeColumns-1 && !mazeCells[row, column+1].visited)
        {
            availableRoutes++;
        }

        return availableRoutes > 0;
    }

    //check if the index suits into the mazecells array and isn't visisted
    private bool CellIsAvailable(int row, int column){
        if (row >= 0 && row < mazeRows && column >= 0 && column < mazeColumns && !mazeCells[row, column].visited) {
            return true;
        }
        else {
            return false;
        }
    }

    //Destroys the wall if it exists
    private void DestroyWallIfItExists(GameObject wall) {
        if (wall != null) {
            GameObject.Destroy (wall);
        }
    }

    //checks if the cell has an adjecent visitedCell
    private bool CellHasAnAdjacentVisitedCell(int row, int column) {
        int visitedCells = 0;


        if (row > 0 && mazeCells [row - 1,column].visited) {
            visitedCells++;
        }


        if (row < (mazeRows-2) && mazeCells[row + 1, column].visited) {
            visitedCells++;
        }


        if (column > 0 && mazeCells[row, column - 1].visited)
        {
            visitedCells++;
        }


        if (column < (mazeColumns - 2) && mazeCells[row, column+1].visited)
        {
            visitedCells++;
        }

        return visitedCells > 0;
    }

    //Destroys the adjecent MazeCell wall
    private void DestroyAdjacentWall(int row, int column) {
        bool wallDestroyed = false;

        while (!wallDestroyed) {

            int direction = ProceduralNumberGenerator.getNextNumber();

            if (direction == 1 && row > 0 && mazeCells [row - 1, column].visited) 
            {
                DestroyWallIfItExists(mazeCells[row, column].northWall);
                DestroyWallIfItExists(mazeCells[row - 1, column].southWall);
                wallDestroyed = true;
            }else if (direction == 2 && row < (mazeRows-2) && mazeCells[row + 1, column].visited)
            {
                DestroyWallIfItExists(mazeCells[row, column].southWall);
                DestroyWallIfItExists(mazeCells[row + 1, column].northWall);
                wallDestroyed = true;
            }
            else if (direction == 3 && column > 0 && mazeCells[row, column-1].visited)
            {
                DestroyWallIfItExists(mazeCells[row, column].westWall);
                DestroyWallIfItExists(mazeCells[row, column-1].eastWall);
                wallDestroyed = true;
            }
            else if (direction == 4 && column < (mazeColumns - 2) && mazeCells[row, column+1].visited)
            {
                DestroyWallIfItExists(mazeCells[row, column].eastWall);
                DestroyWallIfItExists(mazeCells[row, column+1].westWall);
                wallDestroyed = true;
            }

        }
    }

}
