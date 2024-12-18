using System.Collections.Generic;
using UnityEngine;

public abstract class LevelDataSO : ScriptableObject
{
    public NumericCube[] NumericData { get => _numericData; }
    [SerializeField] private NumericCube[] _numericData;

    public LetterCube[] LetterCube { get => _letterCube; }
    [SerializeField] private LetterCube[] _letterCube;

    private List<CubeData[]> _cubeDatas = new List<CubeData[]>();
    private int _currentId = 0;

    private void OnEnable()
    {
        _currentId = 0;
        _cubeDatas.Add(_numericData);
        _cubeDatas.Add(_letterCube);
    }

    private void OnDisable()
    {
        _cubeDatas.Remove(_numericData);
        _cubeDatas.Remove(_letterCube);
        _cubeDatas.Clear();
    }

    public abstract LevelDataSO CheckForData(out int column, out int row);

    public bool TryGetRndDataset(out CubeData[] data)
    {
        data = null;
        if (_currentId >= _cubeDatas.Count)
        {
            _currentId = 0;
            return false;
        }

        data = _cubeDatas[_currentId];
        _currentId++;
        return data != null && data.Length != 0;
    }
}
