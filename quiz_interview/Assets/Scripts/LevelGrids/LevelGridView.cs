using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LevelGrid))]
public class LevelGridView : MonoBehaviour
{
    [SerializeField] private GameObject _gridPref;

    private float _cellSize;
    private LevelGrid _levelGrid;

    public Queue<GameObject> GeneratedCellPrefs { get => _generatedCubes; }
    private Queue<GameObject> _generatedCubes = new Queue<GameObject>();

    #region Subscribe to LevelGrid model
    private void OnEnable()
    {
        _levelGrid.OnGridGenerated += GenerateViewOfGrid;
    }

    private void OnDisable()
    {
        _levelGrid.OnGridGenerated -= GenerateViewOfGrid;
    }

    private void Awake()
    {
        _levelGrid = GetComponent<LevelGrid>();
        _cellSize = _gridPref.transform.localScale.x;
    }
    #endregion

    private void GenerateViewOfGrid(int column, int row, CubeData[] data)
    {
        ClearBeforeGeneration();
        PrepareData(column, row, out float hw, out float hh);

        int r = 0, c = 1;
        int size = column * row;

        for (int i = 0; i < size; ++i)
        {
            if (i >= data.Length) continue;

            float startXPos = -hw + _cellSize * (c - 1);
            float startYPos = -hh + _cellSize * r;
            GenerateCell(startXPos, startYPos, data[i]);

            if (c < column) c++;
            else if (c >= column)
            {
                c = 1;
                r++;
            }
        }
    }

    private void PrepareData(int row, int column, out float hw, out float hh)
    {
        float gridHeightByCellAmount = _cellSize * row;
        float gridWidthByCellAmount = _cellSize * column;

        hw = gridWidthByCellAmount * 0.5f;
        hh = gridHeightByCellAmount * 0.5f;
    }

    private void GenerateCell(float startXPos, float startYPos, CubeData data)
    {
        Vector2 originPos = new Vector2(startXPos, startYPos);
        GameObject tempObj = Instantiate(_gridPref, originPos, Quaternion.identity);
        _generatedCubes.Enqueue(tempObj);

        Cell cell = tempObj.GetComponent<Cell>();
        cell.InitData(data, _levelGrid.CheckForRighgtAnswer, _levelGrid.UpdateGridData);
    }

    private void ClearBeforeGeneration()
    {
        foreach (GameObject obj in _generatedCubes)
            Destroy(obj);
        _generatedCubes.Clear();
    }
}
