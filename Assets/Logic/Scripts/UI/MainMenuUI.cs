using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    [Header("UI Button References")]
    [SerializeField] private Button playButton;
    [SerializeField] private Button optionsButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private MainMenuUIManager mainMenuUIManager;


    private void Awake()
    {
        // Assign the button click listeners automatically via code
        if (playButton != null) playButton.onClick.AddListener(PlayGame);
        if (optionsButton != null) optionsButton.onClick.AddListener(OpenOptions);
        if (quitButton != null) quitButton.onClick.AddListener(QuitGame);
    }

    private void PlayGame()
    {
        Debug.Log("Loading game...");
        Loader.LoadGame();
    }

    private void OpenOptions()
    {
        mainMenuUIManager.goToOptions();
        Debug.Log("Opening options menu...");
        
    }

    private void QuitGame()
    {
        Debug.Log("Quitting application...");
        Application.Quit();

#if UNITY_EDITOR
        // ensures game stops inside the Unity Editor
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}