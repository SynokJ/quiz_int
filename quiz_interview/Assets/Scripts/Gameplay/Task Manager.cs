using UnityEngine;

public class TaskManager : MonoBehaviour
{
    [SerializeField] private LevelDataSO[] levelDataSOs;
    [SerializeField] private LevelGrid _levelGrid;

    public event System.Action<CubeData> OnRndCubeGenerated;
    public event System.Action OnGameFinished;

    private int _currentLevelID;

    public CubeData TargetCube { get => _targetCube; }
    private CubeData _targetCube;

    private void Start()
    {
        InitDataSet();
    }

    public void InitDataSet()
    {
        _levelGrid.SetCurrentLevelData(levelDataSOs[_currentLevelID]);
        _levelGrid.InitLevelDataset();
        GenerateTarget();
    }

    public bool ShiftToNextLevel()
    {
        _currentLevelID++;
        return _currentLevelID < levelDataSOs.Length;
    }

    public void FinishTheGame()
        => OnGameFinished?.Invoke();

    public void GenerateTarget()
    {
        int size = _levelGrid.ActivatedCubesData.Length;
        if (size == 0) return;

        int targetId = Random.Range(0, size);

        Debug.Log(targetId + " => " + _levelGrid.ActivatedCubesData.Length);

        _targetCube = _levelGrid.ActivatedCubesData[targetId];
        OnRndCubeGenerated?.Invoke(_targetCube);
    }

    public bool IsAnswerRight(CubeData data)
    {
        bool result = default;
        if (data is NumericCube num && _targetCube is NumericCube targetNum)
            result = num.Number.Equals(targetNum.Number);
        else if (data is LetterCube let && _targetCube is LetterCube targetLet)
            result = let.Letter.Equals(targetLet.Letter);
        return result;
    }
}
