using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private Color _1, _2, _3, _4, _5, _default;
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private GameObject _highlight;

    /*void Awake()
    {
        _renderer = GetComponentInChildren<SpriteRenderer>();
    }*/

    // Set the tile’s color directly
    public void SetColor(int color)
    {
        if(color == 1)
        {
            _renderer.color = _1;
        }
        else if(color == 2)
        {
            _renderer.color = _2;
        }
        else if (color == 3)
        {
            _renderer.color = _3;
        }
        else if (color == 4)
        {
            _renderer.color = _4;
        }
        else if (color == 5)
        {
            _renderer.color = _5;
        }
        else
        {
            _renderer.color = _default;
        }
    }

    // Highlight on mouse hover
    void OnMouseEnter()
    {
        if (_highlight != null) _highlight.SetActive(true);
    }

    void OnMouseExit()
    {
        if (_highlight != null) _highlight.SetActive(false);
    }
}