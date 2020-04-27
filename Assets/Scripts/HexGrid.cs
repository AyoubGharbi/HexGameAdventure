using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexGrid : MonoBehaviour
{
    private const float _dimensionX = 2f;
    private const float _dimensionZ = 1.73f;

    [SerializeField] private int _width = 5;
    [SerializeField] private int _height = 5;
    [SerializeField] private GameObject _hexPrefab;

    private List<GameObject> _hexes = new List<GameObject>();

    private void Start()
    {
        CreateGrid();
    }

    void CreateGrid()
    {
        for (int x = 0; x < _width; x++)
        {
            for (int z = 0; z < _height; z++)
            {
                var isOdd = x % 2 != 0;
                var xPos = x * _dimensionX * 0.75f;
                var zPos = z * _dimensionZ + (isOdd ? _dimensionZ / 2 : 0);
                var hexPos = new Vector3(xPos, 0f, zPos);
                var hex = Instantiate(_hexPrefab, hexPos, Quaternion.identity);
                hex.transform.SetParent(this.transform);
                _hexes.Add(hex);
            }
        }
    }

    private void OnValidate()
    {
        UnityEditor.EditorApplication.delayCall += () =>
        {
            ClearHexes();
            CreateGrid();
        };
    }

    void ClearHexes()
    {
        _hexes.ForEach(hex =>
        {
            DestroyImmediate(hex);
        });

        _hexes.Clear();
    }
}
