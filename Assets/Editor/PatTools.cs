using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

[InitializeOnLoad]
public static class PatTools
{
    private const string PREFS_KEY_ORIGINAL_SCENE = "PatTools_OriginalScenePath";
    private const string PREFS_KEY_SHOULD_RETURN = "PatTools_ShouldReturn";

    //constructor runs whenever editor loads or changes play states
    static PatTools()
    {
        EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
    }

    [MenuItem("patTools/Play From First Scene")]
    public static void PlayFromFirstScene()
    {
        if (EditorBuildSettings.scenes.Length == 0)
        {
            Debug.LogError("patTools: No scenes found in Build Settings!");
            return;
        }

        if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
        {
            // Save current scene path
            string currentScenePath = EditorSceneManager.GetActiveScene().path;
            EditorPrefs.SetString(PREFS_KEY_ORIGINAL_SCENE, currentScenePath);

            // Set flag indicating we used tool to launch game
            EditorPrefs.SetBool(PREFS_KEY_SHOULD_RETURN, true);

            // Load first scene and play
            string firstScenePath = EditorBuildSettings.scenes[0].path;
            EditorSceneManager.OpenScene(firstScenePath);
            EditorApplication.isPlaying = true;
        }
    }

    private static void OnPlayModeStateChanged(PlayModeStateChange state)
    {
        // Only trigger when completely return to Edit Mode
        if (state == PlayModeStateChange.EnteredEditMode)
        {
            // return if this tool was used to launch the game
            if (EditorPrefs.GetBool(PREFS_KEY_SHOULD_RETURN, false))
            {
                string originalScenePath = EditorPrefs.GetString(PREFS_KEY_ORIGINAL_SCENE, "");

                if (!string.IsNullOrEmpty(originalScenePath))
                {
                    // Delay opening slightly ensures Unity has finished cleaning up
                    EditorApplication.delayCall += () =>
                    {
                        EditorSceneManager.OpenScene(originalScenePath);
                    };
                }

                
                EditorPrefs.DeleteKey(PREFS_KEY_ORIGINAL_SCENE);
                EditorPrefs.DeleteKey(PREFS_KEY_SHOULD_RETURN);
            }
        }
    }
}