using UnityEngine;

[System.Serializable]
public class NumericCube : CubeData
{
    public int Number { get => _number; }
    [SerializeField] private int _number;

    public override CubeData Execute()
        => this;
}
