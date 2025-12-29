using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [Header("Grid Settings")]
    [SerializeField] private int _width = 6;
    [SerializeField] private int _height = 12;
    [SerializeField] private Tile _tilePrefab;
    //var spawnedTile
    [Header("References")]
    [SerializeField] private Transform _cam;

    private Dictionary<Vector2Int, Tile> _tiles;

    [Header("Colors")]
    public Color[] possibleColors; // Assign 5 colors in inspector

    //private Tile[,] _tiles;

    void Start()
    {
        GenerateGrid(); //Generates grid
        RowFill();
        
    }

    void GenerateGrid()
    {
        // Create the 2D array to hold tile references
        _tiles = new Dictionary<Vector2Int, Tile>();

        // Loop through each row (y) and column (x) to instantiate tiles
        for (int y = 0; y < _height; y++)
        {
            for (int x = 0; x < _width; x++)
            {
                Tile tile = Instantiate(_tilePrefab, new Vector3(x, y, 0), Quaternion.identity);
                tile.name = $"Tile {x} {y}";

                tile.x = x;
                tile.y = y;

                // the grid's square's colors are dependent on 1, 2, 3, 4, or 5 
                //var isOffset = UnityEngine.Random.Range(1, 6);
                //tile.SetColor(isOffset);

                _tiles[new Vector2Int(x, y)] = _tilePrefab;
            }
        }

        // Center the camera
        if (_cam != null)
        {
            _cam.transform.position = new Vector3((_width - 1) / 2f, (_height - 1) / 2f, -20f);
        }

        //RowFill();
        //tile.SetColor(0); // forces _default color
    }

    //Fills first row one by one
    void RowFill()
    {
        for (int x = 0; x < _width; x++)
        {
            Tile tile = GetTileAt(x, 0);
            if (tile != null)
            {
                int color = UnityEngine.Random.Range(1, 6);
                tile.SetColor(color);
            }
        }
    }

    bool IsBottom(Tile tile)
    {
        return tile.y == 0;
    }

    bool IsTop(Tile tile)
    {
        return tile.y == _height - 1;
    }

    Tile GetTileAt(int x, int y)
    {
        _tiles.TryGetValue(new Vector2Int(x, y), out Tile tile);
        return tile;
    }

    Tile GetBelow(Tile tile) => GetTileAt(tile.x, tile.y - 1);
    Tile GetAbove(Tile tile) => GetTileAt(tile.x, tile.y + 1);
    Tile GetLeft(Tile tile) => GetTileAt(tile.x - 1, tile.y);
    Tile GetRight(Tile tile) => GetTileAt(tile.x + 1, tile.y);

    bool IsTouchingBottom(Tile tile)
    {
        return tile.y == 0 || GetBelow(tile) == null;
    }


}