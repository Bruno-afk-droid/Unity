using UnityEngine;
using System.Collections;

public class ProceduralNumberGenerator {
    public static int currentPosition;
    public const string key = "1234241233421234123412341234123412344";

    public static int getNextNumber() {
        string currentNum = key.Substring(currentPosition++ % key.Length, 1);

        return Random.Range(1, 5);
    }

    public static Vector2Int getNextVector(int r,int c)
    {
        string currentNum = key.Substring(currentPosition++ % key.Length, 1);

        return new Vector2Int(Random.Range(1,r), Random.Range(1, c));
    }
}
