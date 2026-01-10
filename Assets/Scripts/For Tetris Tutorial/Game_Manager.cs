using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Manager : MonoBehaviour
{
    public GameObject[] Tetrominos;
    public float movementFrequency = 0.8f;
    private float passedTime = 0;
    private GameObject currentTetromino;
    // Start is called before the first frame update
    void Start()
    {
        SpawnTetromino();
    }

    // Update is called once per frame
    void Update()
    {
        passedTime += Time.deltaTime;
        if(passedTime >= movementFrequency){
            passedTime -= movementFrequency;
            MoveTetromino(Vector3.down);
        }
        UserInput();
    }

    void UserInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MoveTetromino(Vector3.left);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            MoveTetromino(Vector3.right);
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            currentTetromino.transform.Rotate(0, 0, 90);
            if (!IsValidPosition())
            {
                currentTetromino.transform.Rotate(0, 0, -90);
            }
        }
    }    

    void SpawnTetromino()
    {
        int index = Random.Range(0, Tetrominos.Length);

        //Gets a random tetrimino game object
        currentTetromino = Instantiate(Tetrominos[index], new Vector3(5, 19, 0), Quaternion.identity);
    }

    void MoveTetromino(Vector3 direction)
    {
        //Adding direction to the current position
        currentTetromino.transform.position += direction;

        if (!IsValidPosition())
        {
            currentTetromino.transform.position -= direction;
            if(direction == Vector3.down)
            {
                //GetComponent<Grid_Script>().UpdateGrid(currentTetromino.transform);
                CheckForLines();
                SpawnTetromino();
            }
        }
    }

    bool IsValidPosition()
    {
        return true;
    }

    void CheckForLines()
    {
        //To implement later
    }
}
