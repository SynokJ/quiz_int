using UnityEngine;

[CreateAssetMenu(fileName = "SimpleLevelDataSO", menuName = "ScriptableObjects/SimpleLevelDataSO")]
public class SimpleLevelDataSO : LevelDataSO
{
    public override LevelDataSO CheckForData(out int column, out int row)
    {
        column = 3;
        row = 1;
        return this;
    }
}
