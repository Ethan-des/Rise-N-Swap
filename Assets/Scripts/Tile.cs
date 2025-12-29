using UnityEngine;

public class Tile : MonoBehaviour
{
    //Tile Coordinates
    public int x;
    public int y;

    [Header("Colors")]
    [SerializeField] private Color _1, _2, _3, _4, _5, _default;

    //What determines a tile's color
    [Header("Renderers")]
    [SerializeField] private SpriteRenderer outlineRenderer;
    [SerializeField] private SpriteRenderer fillRenderer;
    [SerializeField] private SpriteRenderer highlightRenderer;

    //For debugging, makes sure the tile is still there even when invisible
    [SerializeField] private GameObject _highlight;

    /*void Start()
    {
        SetColor(5);
    }*/

    //Made to awake the renderer
    void Awake()
    {
        // Outline should ALWAYS be black
        outlineRenderer.color = _default;

        // Fill starts as default
        fillRenderer.color = _default;

        // Highlight hidden by default
        highlightRenderer.color = Color.white;
        highlightRenderer.gameObject.SetActive(false);
    }

    // Set the tile’s color directly
    public void SetColor(int color)
    {
        switch (color)
        {
            case 1:
                fillRenderer.color = _1;
                outlineRenderer.color = Color.black;
                break;
            case 2:
                fillRenderer.color = _2;
                outlineRenderer.color = Color.black;
                break;
            case 3:
                fillRenderer.color = _3;
                outlineRenderer.color = Color.black;
                break;
            case 4:
                fillRenderer.color = _4;
                outlineRenderer.color = Color.black;
                break;
            case 5:
                fillRenderer.color = _5;
                outlineRenderer.color = Color.black;
                break;
            default:
                fillRenderer.color = _default;
                outlineRenderer.color = Color.black;
                break;
        }
    }

    // Highlight on mouse hover
    void OnMouseEnter()
    {
        highlightRenderer.gameObject.SetActive(true);
    }

    void OnMouseExit()
    {
        highlightRenderer.gameObject.SetActive(false);
    }
}