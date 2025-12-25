using System.Collections;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [Header("Grid Settings")]
    [SerializeField] private int _width = 10;
    [SerializeField] private int _height = 10;
    [SerializeField] private Tile _tilePrefab;

    [Header("References")]
    [SerializeField] private Transform _cam;

    [Header("Colors")]
    public Color[] possibleColors; // Assign 5 colors in inspector

    private Tile[,] _tiles;

    void Start()
    {
        GenerateGrid(); //Generates grid
        StartCoroutine(FillGridCoroutine()); //Fills each row with random colors
    }

    void GenerateGrid()
    {
        // Create the 2D array to hold tile references
        _tiles = new Tile[_width, _height];

        // Loop through each row (y) and column (x) to instantiate tiles
        for (int y = 0; y < _height; y++)
        {
            for (int x = 0; x < _width; x++)
            {
                var spawnedTile = Instantiate(_tilePrefab, new Vector3(x, y, 0), Quaternion.identity);
                spawnedTile.name = $"Tile {x} {y}";
                _tiles[x, y] = spawnedTile;
            }
        }

        // Center the camera
        if (_cam != null)
        {
            _cam.transform.position = new Vector3((_width - 1) / 2f, (_height - 1) / 2f, -20f);
        }
    }

    void FillRow(int row)
    {
        // Safety checks to avoid IndexOutOfRangeException
        if (_tiles == null) return; // safety
        if (row < 0 || row >= _height) return; // prevent invalid row
        for (int x = 0; x < _width; x++)
        {
            if (_tiles[x, row] != null)
            {
                // Ensure possibleColors array has elements
                Color randomColor = possibleColors[Random.Range(0, possibleColors.Length)];
                _tiles[x, row].SetColor(randomColor);
            }
        }
    }

    IEnumerator FillGridCoroutine()
    {
        // Loop from bottom row (0) to top row (_height - 1)
        for (int y = 0; y < _height; y++)
        {
            // Fill this row with random colors
            FillRow(y);

            // Wait for a short delay before filling the next row
            yield return new WaitForSeconds(0.3f);
        }
    }
}