using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridBuilder : MonoBehaviour
{
    private const float _dimensionX = 2f;
    private const float _dimensionZ = 1.73f;

    [SerializeField] private int _width = 5;
    [SerializeField] private int _height = 5;
    [SerializeField] private EditorHexTile _editorHexPrefab;
    [SerializeField] private HexTile _hexTilePrefab;
    [SerializeField] private Transform _levelHexesTransform;

    [HideInInspector][SerializeField]
    private List<EditorHexTile> _editorHexes = new List<EditorHexTile>();

    [HideInInspector]
    [SerializeField]
    private List<HexTile> _hexes = new List<HexTile>();

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
                var hex = Instantiate(_editorHexPrefab, hexPos, Quaternion.identity);
                hex.transform.SetParent(this.transform);
                hex.Init(new Vector2(x,z));
                _editorHexes.Add(hex);
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
            DestroyImmediate(hex.gameObject);
        });

        _hexes.Clear();
    }

    public void InstantiateHexTile(Vector2 coords, Vector3 position)
    {
        var hex = Instantiate(_hexTilePrefab, position, Quaternion.identity);
        hex.transform.SetParent(_levelHexesTransform);
        hex.Init(coords);
        _hexes.Add(hex);
    }
}
