using UnityEngine;

[System.Serializable]
public abstract class CubeData
{
    public Sprite Sprite { get => _sprite; }
    [SerializeField] private Sprite _sprite;

    public CubeData()
    {
    }

    ~CubeData()
    {
        _sprite = null;
    }

    public abstract CubeData Execute();

}
