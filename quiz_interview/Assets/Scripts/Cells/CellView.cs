using UnityEngine;

public class CellView : MonoBehaviour
{
    private const string _TRIGGER_FADE_IN_NAME = "fade_in";
    private const string _TRIGGER_FADE_OUT_NAME = "fade_out";
    private const string _TRIGGER_BOUNCE_NAME = "on_bounce";

    [SerializeField] private UnityEngine.UI.Image _veiwImg;
    [SerializeField] private Cell _cell;

    [SerializeField] private Animator _animator;
    [SerializeField] private GameObject _particlePref; 

    private void OnEnable()
    {
        _cell.OnViewDataUpdated += UpdateCellView;
        _cell.OnRightAnswerClicked += PlayRightAnim;
        _cell.OnWrongAnswerClicked += PlayWrongAnim;
    }

    private void OnDisable()
    {
        _cell.OnViewDataUpdated -= UpdateCellView;
        _cell.OnRightAnswerClicked -= PlayRightAnim;
        _cell.OnWrongAnswerClicked -= PlayWrongAnim;
    }

    private void Start()
    {
        _animator.SetTrigger(_TRIGGER_FADE_IN_NAME);
    }

    private void UpdateCellView(Sprite sprite)
        => _veiwImg.sprite = sprite;

    private void PlayWrongAnim()
    {
        _animator.SetTrigger(_TRIGGER_BOUNCE_NAME);
    }

    private void PlayRightAnim()
    {
        _animator.SetTrigger(_TRIGGER_FADE_OUT_NAME);
        Instantiate(_particlePref, transform.position, Quaternion.identity);
    }
}
