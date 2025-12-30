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

    [Header("Game Over Screen")]
    [SerializeField] GameObject gameOverPanel;

    public Boolean gameover = false;

    //private Tile[,] _tiles;

    //Bag Randomizer
    private List<int> colorBag = new List<int>();
    private int bagIndex = 0;

    IEnumerator Start()
    {
        //To make sure game over panel isn't present at start of game
        gameOverPanel.gameObject.SetActive(false);
        GenerateGrid();

        // Initial spawn
        yield return StartCoroutine(SpawnBottomRow());

        while (gameover == false) // rising rows loop
        {
            yield return new WaitForSeconds(1f);
            yield return StartCoroutine(UpOne());
            yield return StartCoroutine(SpawnBottomRow());
            CheckTopRow(); // check game over after new row spawned
        }

        if (gameover)
        {
            Debug.Log("Ending game loop due to Game Over");
            // You can stop coroutines or show a UI here
        }

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

                _tiles[new Vector2Int(x, y)] = tile;
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
    IEnumerator SpawnBottomRow()
    {
        Debug.Log("Starting Reset Loop");
        // Reset bottom row instantly
        for (int x = 0; x < _width; x++)
        {
            Tile tile = GetTileAt(x, 0);
            if (tile != null)
            {
                tile.SetColor(0); // default color
            }
        }
        Debug.Log("Stoping Reset Loop");

        Debug.Log("Starting Fill Loop");
        for (int x = 0; x < _width; x++)
        {
            Tile tile = GetTileAt(x, 0);
            if (tile != null)
            {
                int color = GetNextColorFromBag();
                tile.SetColor(color);
            }
            yield return new WaitForSeconds(1);

        }
        Debug.Log("Stopping Fill Loop");
    }

    int[] CacheBottomRow()
    {
        int[] colors = new int[_width];

        for (int x = 0; x < _width; x++)
        {
            Tile tile = GetTileAt(x, 0);
            colors[x] = tile != null ? tile.ColorIndex : 0;
        }

        return colors;
    }

    IEnumerator UpOne()
    {

        // Move everything UP (top → bottom)
        for (int y = _height - 1; y > 0; y--)// top row is _height - 1
        {
            for (int x = 0; x < _width; x++)
            {
                Tile above = GetTileAt(x, y);
                Tile below = GetTileAt(x, y - 1);

                if (above != null && below != null && below.ColorIndex != 0)
                {
                    above.SetColor(below.ColorIndex);
                }
            }
        }
        yield return null; // let Unity redraw one frame
    }

    void InitializeBag()
    {
        colorBag.Clear();
        for (int i = 1; i <= possibleColors.Length; i++)
        {
            colorBag.Add(i);
        }

        // Shuffle the bag
        for (int i = 0; i < colorBag.Count; i++)
        {
            int j = UnityEngine.Random.Range(i, colorBag.Count);
            int temp = colorBag[i];
            colorBag[i] = colorBag[j];
            colorBag[j] = temp;
        }

        bagIndex = 0;
    }

    //To determine if there is a game over or not
    void CheckTopRow()
    {
        Debug.Log("Starting Game Over Check Loop");
        
        for (int x = 0; x < _width; x++)
        {
            Tile tile = GetTileAt(x, _height - 1); // top row is _height - 1
            if (tile != null && tile.ColorIndex != 0) // non-default color = occupied
            {
                gameover = true;
                gameOverPanel.gameObject.SetActive(true);
                Debug.Log("Game Over! Tile reached the top row at column: " + x);
                break; // no need to check further
            }

        }
        Debug.Log("Stoping Game Over Check Loop");
    }

    int GetNextColorFromBag()
    {
        if (colorBag.Count == 0 || bagIndex >= colorBag.Count)
        {
            InitializeBag();
        }

        int color = colorBag[bagIndex];
        bagIndex++;
        return color;
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