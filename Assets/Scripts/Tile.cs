using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private GameObject _highlight;

    void Awake()
    {
        _renderer = GetComponentInChildren<SpriteRenderer>();
    }

    // Set the tile’s color directly
    public void SetColor(Color color)
    {
        _renderer.color = color;
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