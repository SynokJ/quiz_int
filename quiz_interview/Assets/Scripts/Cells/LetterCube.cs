using UnityEngine;

[System.Serializable]
public class LetterCube : CubeData
{
    public char Letter { get => _letter; }
    [SerializeField] private char _letter;

    public override CubeData Execute()
        => this;
}
