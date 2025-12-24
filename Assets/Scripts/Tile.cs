using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    //Changes tile's color based on code from GridManager
    [SerializeField] private Color _baseColor, _offsetColor;
    //[SerializeField] private Color _offsetColor;
    [SerializeField] private SpriteRenderer _renderer;

    public void Init(bool isOffset){
       
        if (_renderer == null)
            _renderer = GetComponent<SpriteRenderer>();
       

        Color c = isOffset ? _offsetColor : _baseColor;
        c.a = 1f; // Tiles are fully opaque
        _renderer.color = c;

        Debug.Log($"{name} | isOffset={isOffset} | color={c}");
    }
}