using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    //determines the height and width of the playfield grid
    [SerializeField] private int _width, _height;

    //determines what the grid is made from
    [SerializeField] private Tile _tilePrefab;

    //reference to the game's camera
    [SerializeField] private Transform _cam;

    void Start(){
        Debug.Log("Generating grid");
        GenerateGrid();
    }

    void GenerateGrid(){
        for (int x = 0; x < _width; x++){
            for (int y = 0; y < _height; y++){
                var spawnedTile = Instantiate(_tilePrefab, new Vector3(x, y, 0), Quaternion.identity);
                //Determines where each tile is placed
                spawnedTile.name = $"Tile {x} {y}";

                //Colors in the tiles
                //Is x even and y odd or is y odd and x even?
                var isOffset = (x % 2 == 0 && y % 2 != 0) || (x % 2 != 0 && y % 2 == 0);
                spawnedTile.Init(isOffset);
            }
        }

        _cam.transform.position = new Vector3((float)_width / 2 - 0.5f, (float)_height / 2 - 0.5f, -20);
    }
}
