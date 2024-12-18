using UnityEngine;

[CreateAssetMenu(fileName = "MediumLevelDataSO", menuName = "ScriptableObjects/MediumLevelDataSO")]
public class MediumDataSO : LevelDataSO
{
    public override LevelDataSO CheckForData(out int column, out int row)
    {
        column = 3;
        row = 2;
        return this;
    }
}
