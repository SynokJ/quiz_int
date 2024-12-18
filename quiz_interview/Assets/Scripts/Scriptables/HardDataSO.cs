using UnityEngine;

[CreateAssetMenu(fileName = "HardLevelDataSO", menuName = "ScriptableObjects/HardLevelDataSO")]
public class HardDataSO : LevelDataSO
{
    public override LevelDataSO CheckForData(out int column, out int row)
    {
        column = 3;
        row = 3;
        return this;
    }
}
