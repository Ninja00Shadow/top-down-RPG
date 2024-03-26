using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEditor;

public class TileAutomata : MonoBehaviour
{
    [Range(0, 100)] public int initialChance;

    [Range(1, 8)] public int birthLimit;

    [Range(1, 8)] public int deathLimit;

    [Range(1, 10)] public int numberOfRepetitions;
    private int count = 0;

    private int[,] terrainMap;
    public Vector3Int tileMapSize;

    public Tilemap topMap;
    public Tilemap bottomMap;
    public RuleTile topTile;
    public RuleTile bottomTile;

    int width;
    int height;

    public void DoSimulation(int repetitions)
    {
        ClearMap(false);
        width = tileMapSize.x;
        height = tileMapSize.y;

        if (terrainMap == null)
        {
            terrainMap = new int[width, height];
            InitialPosition();
        }

        for (int i = 0; i < repetitions; i++)
        {
            terrainMap = GenerateMap(terrainMap);
        }
        
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector3Int pos = new Vector3Int(-x + width / 2, -y + height / 2, 0);
                if (terrainMap[x, y] == 1)
                {
                    topMap.SetTile(pos, topTile);
                }
                else
                {
                    bottomMap.SetTile(pos, bottomTile);
                }
            }
        }
    }

    public void InitialPosition()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                terrainMap[x, y] = Random.Range(1, 101) < initialChance ? 1 : 0;
            }
        }
    }

    public int[,] GenerateMap(int[,] oldMap)
    {
        int[,] newMap = new int[width, height];
        int neighbourWallTiles;
        BoundsInt myB = new BoundsInt(-1, -1, 0, 3, 3, 1);

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                neighbourWallTiles = 0;
                foreach (var b in myB.allPositionsWithin)
                {
                    if (b.x == 0 && b.y == 0) continue;
                    if (x + b.x >= 0 && x + b.x < width && y + b.y >= 0 && y + b.y < height)
                    {
                        neighbourWallTiles += oldMap[x + b.x, y + b.y];
                    }
                    else
                    {
                        neighbourWallTiles++;
                    }
                }
                
                if (oldMap[x, y] == 1)
                {
                    if (neighbourWallTiles < deathLimit) newMap[x, y] = 0;
                    else
                    {
                        newMap[x, y] = 1;
                    }
                }

                if (oldMap[x, y] == 0)
                {
                    if (neighbourWallTiles > birthLimit) newMap[x, y] = 1;
                    
                    else
                    {
                        newMap[x, y] = 0;
                    }
                }
            }
        }


        return newMap;
    }

    // Update is called once per frame
    void Start()
    {
        DoSimulation(numberOfRepetitions);
    }

    public void ClearMap(bool complete)
    {
        topMap.ClearAllTiles();
        bottomMap.ClearAllTiles();

        if (complete)
        {
            terrainMap = null;
        }
    }
}