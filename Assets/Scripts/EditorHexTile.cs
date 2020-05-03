using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorHexTile : MonoBehaviour
{
    [HideInInspector][SerializeField]
    private Vector2 _gridCoords;

    public Vector2 Coords => _gridCoords;

    public void Init(Vector2 coords)
    {
        _gridCoords = coords;
    }
 
}
