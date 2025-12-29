using UnityEngine.Tilemaps;

//enum = enumerated type of data
//This enum is to hold information on all Tetrimono shapes
public enum Tetromino
{
    I,
    O,
    T,
    J,
    L,
    S,
    Z
}

//Custom data structure that stores data for each tetrimino
[System.Serializable]
public struct TetrominoData
{
    public Tetromino tetromino;
    public Tile tile;
}