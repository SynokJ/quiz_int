using System;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class LevelGrid : MonoBehaviour
{
    public event Action<int, int, CubeData[]> OnGridGenerated;

    [SerializeField] private TaskManager _taskManager;

    public CubeData[] CubeData { get => _cubeData; }
    private CubeData[] _cubeData;

    public CubeData[] ActivatedCubesData { get => _activatedCubesData; }
    private CubeData[] _activatedCubesData;

    private LevelDataSO _levelData;

    public void SetCurrentLevelData(LevelDataSO levelData)
    {
        _levelData = levelData;
    }

    public void InitLevelDataset()
    {
        InitCurrentLevelData();
        GenerateLevelData();
    }

    private void InitCurrentLevelData()
    {

        if (_levelData.TryGetRndDataset(out CubeData[] data))
            InitFoundDataset(data);
        else
            TryFindNewDataset();
    }

    private void InitFoundDataset(CubeData[] data)
    {
        _cubeData = new CubeData[data.Count()];
        data.CopyTo(_cubeData, 0);
        ShuffleArray();
    }

    private void TryFindNewDataset()
    {
        if (_taskManager.ShiftToNextLevel())
            _taskManager.InitDataSet();
        else
            _taskManager.FinishTheGame();
    }

    private void ShuffleArray()
    {
        int size = _cubeData.Length;
        for (int i = 0; i < size; i++)
        {
            CubeData tmp = _cubeData[i];
            int r = UnityEngine.Random.Range(i, size);
            _cubeData[i] = _cubeData[r];
            _cubeData[r] = tmp;
        }
    }

    private void GenerateLevelData()
    {
        InitRndCellData();

        _levelData.CheckForData(out int column, out int row);
        OnGridGenerated?.Invoke(column, row, _activatedCubesData);
    }

    private void InitRndCellData()
    {
        int size = GetAppropreateSize();
        _activatedCubesData = new CubeData[size];
        for (int i = 0; i < size; ++i)
            _activatedCubesData[i] = _cubeData[i];
    }

    private int GetAppropreateSize()
    {
        _levelData.CheckForData(out int c, out int r);
        return System.Math.Min(c * r, _cubeData.Length);
    }

    public bool CheckForRighgtAnswer(CubeData data)
       => _taskManager.IsAnswerRight(data);

    public void UpdateGridData(CubeData data)
    {
        int size = _activatedCubesData.Length;
        for (int i = 0; i < size; ++i)
        {
            bool isTheSame = IsTheSameData(_activatedCubesData[i], data);
            if (isTheSame) UpdateCellDataInGeneral(_activatedCubesData[i]);
        }

        GenerateLevelData();
        _taskManager.GenerateTarget();
    }

    private bool IsTheSameData(CubeData data, CubeData target)
    {
        if (data == null || target == null) return false;

        if (data is NumericCube num && target is NumericCube targetNum)
            return num.Number.Equals(targetNum.Number);
        else if (data is LetterCube let && target is LetterCube targetLet)
            return let.Letter.Equals(targetLet.Letter);
        return default;
    }

    private void UpdateCellDataInGeneral(CubeData data)
    {
        _cubeData = RemoveCubeData(_cubeData, data);
        if (_cubeData.Length == 0)
        {
            _taskManager.InitDataSet();
            return;
        }

        int size = _activatedCubesData.Length;
        for (int i = 0; i < size; ++i)
        {
            if (i < _cubeData.Length && IsTheSameData(_cubeData[i], _activatedCubesData[i]))
                continue;

            if (i >= _cubeData.Length)
            {
                _activatedCubesData = RemoveCubeData(_activatedCubesData, _activatedCubesData[i]);
                continue;
            }
            else
            {
                _activatedCubesData[i] = _cubeData[i];
                break;
            }
        }
    }

    private CubeData[] RemoveCubeData(CubeData[] collection, CubeData data)
    {
        CubeData[] result = new CubeData[collection.Length - 1];
        int size = collection.Length;
        int iterator = 0;
        for (int i = 0; i < size; ++i)
        {
            if (IsTheSameData(collection[i], data)) continue;
            if (iterator >= result.Length) break;
            result[iterator] = collection[i];
            iterator++;
        }

        return result;
    }
}
