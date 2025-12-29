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

    private Dictionary<Vector2, Tile> _tiles;

    [Header("Colors")]
    public Color[] possibleColors; // Assign 5 colors in inspector

    //private Tile[,] _tiles;

    void Start()
    {
        GenerateGrid(); //Generates grid
        //RowFill();
        
    }

    void GenerateGrid()
    {
        // Create the 2D array to hold tile references
        _tiles = new Dictionary<Vector2, Tile>();

        // Loop through each row (y) and column (x) to instantiate tiles
        for (int y = 0; y < _height; y++)
        {
            for (int x = 0; x < _width; x++)
            {
                Tile spawnedTile = Instantiate(_tilePrefab, new Vector3(x, y, 0), Quaternion.identity);
                spawnedTile.name = $"Tile {x} {y}";

                // the grid's square's colors are dependent on 1, 2, 3, 4, or 5 
                var isOffset = UnityEngine.Random.Range(1, 6);
                spawnedTile.SetColor(isOffset);

                _tiles[new Vector2(x, y)] = _tilePrefab;
            }
        }

        // Center the camera
        if (_cam != null)
        {
            _cam.transform.position = new Vector3((_width - 1) / 2f, (_height - 1) / 2f, -20f);
        }
    }

    /*void RowFill()
    {
        for (int x = 0; x < _width; x++)
        {
            // the grid's square's colors are dependent on 1, 2, 3, 4, or 5 
            var isOffset = UnityEngine.Random.Range(1, 6);
            Vector2 key = new Vector2(x, 0);

            if (_tiles.TryGetValue(x, 0))
            {
                //tile.SetColor(isOffset);
            }
        }
    }*/
}