using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public void RestartButtonClicked()
        => SceneManager.LoadScene(0);
}
