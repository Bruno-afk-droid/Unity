using UnityEngine;

public class MazeCell {
    public bool visited = false;
    public GameObject northWall, southWall, eastWall, westWall, floor;

    //method to cleanup all wall objects
    public void Destroy() {
        if (DestroyWallIfItExists(northWall)) northWall = null;
        if (DestroyWallIfItExists(southWall)) southWall = null;
        if (DestroyWallIfItExists(eastWall)) eastWall = null;
        if (DestroyWallIfItExists(westWall)) westWall = null;
        if (DestroyWallIfItExists(floor)) floor = null;
    }

    //method to cleanup specific wall 
    private bool DestroyWallIfItExists(GameObject wall)
    {
        if (wall != null)
        {
            GameObject.Destroy(wall);
            return true;
        }
        else return false;
    }
}
