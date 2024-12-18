using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class Cell : MonoBehaviour, IClickable
{

    public event System.Action<Sprite> OnViewDataUpdated;
    public event System.Action OnRightAnswerClicked;
    public event System.Action OnWrongAnswerClicked;

    private CubeData _currentCubeData;

    public delegate bool OnCellClicked(CubeData data);
    private OnCellClicked _onCellClicked;

    public delegate void OnSwitchCellData(CubeData data);
    private OnSwitchCellData _onCellSwitched;

    public void InitData(CubeData data, OnCellClicked OnPressed, OnSwitchCellData OnSwitched)
    {
        _currentCubeData = data;
        OnViewDataUpdated?.Invoke(data.Sprite);

        _onCellClicked = OnPressed;
        _onCellSwitched = OnSwitched;
    }

    public void OnClicked()
    {
        bool res = (bool)_onCellClicked?.Invoke(_currentCubeData);
        if (res)
        {
            OnRightAnswerClicked?.Invoke();
            _onCellSwitched?.Invoke(_currentCubeData);
        }
        else
            OnWrongAnswerClicked?.Invoke();
    }
}
