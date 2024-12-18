using UnityEngine;

[RequireComponent(typeof(TaskManager))]
public class TaskView : MonoBehaviour
{
    private const string _DEFAULT_PHRASE = "Your task is to find ";
    private const string _END_PANEL_FADE_IN = "fade_in";
    private const string _TASK_FADE_IN = "fade_in";

    [SerializeField] private TMPro.TextMeshProUGUI _taskText;
    [SerializeField] private Animator _taskTextAnim;

    [SerializeField] private GameObject _endGamePanel;
    [SerializeField] private Animator _panelAnimator;

    private TaskManager _taskManager;

    #region Subscribe to Task Model
    private void OnEnable()
    {
        _taskManager.OnRndCubeGenerated += OnTargetCubeGenerated;
        _taskManager.OnGameFinished += OnGameFinished;
    }

    private void OnDisable()
    {
        _taskManager.OnRndCubeGenerated -= OnTargetCubeGenerated;
        _taskManager.OnGameFinished -= OnGameFinished;
    }

    private void Awake()
    {
        _taskManager= GetComponent<TaskManager>();
    }
    #endregion

    private void Start()
    {
        _endGamePanel.SetActive(false);
        _taskTextAnim.SetTrigger(_TASK_FADE_IN);
    }

    public void OnTargetCubeGenerated(CubeData data)
    {
        string textByType = GetTextByType(data);
        _taskText.text = _DEFAULT_PHRASE + textByType;
    }

    private string GetTextByType(CubeData data)
    {
        if(data is NumericCube num)
            return " number: " + num.Number.ToString();
        else if(data is LetterCube let)
            return " letter: " + let.Letter.ToString();

        return default;
    }

    private void OnGameFinished()
    {
        _endGamePanel.SetActive(true);
        _panelAnimator.SetTrigger(_END_PANEL_FADE_IN);
    }
}
