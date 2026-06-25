using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

[InitializeOnLoad]
public static class PatTools
{
    private const string PREFS_KEY_ORIGINAL_SCENE = "PatTools_OriginalScenePath";
    private const string PREFS_KEY_SHOULD_RETURN = "PatTools_ShouldReturn";

    // This constructor runs automatically whenever the editor loads or changes play states
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
            // 1. Save the current scene path
            string currentScenePath = EditorSceneManager.GetActiveScene().path;
            EditorPrefs.SetString(PREFS_KEY_ORIGINAL_SCENE, currentScenePath);

            // 2. Set a flag indicating we used the tool to launch the game
            EditorPrefs.SetBool(PREFS_KEY_SHOULD_RETURN, true);

            // 3. Load the first scene and play
            string firstScenePath = EditorBuildSettings.scenes[0].path;
            EditorSceneManager.OpenScene(firstScenePath);
            EditorApplication.isPlaying = true;
        }
    }

    private static void OnPlayModeStateChanged(PlayModeStateChange state)
    {
        // Only trigger when we completely return to Edit Mode
        if (state == PlayModeStateChange.EnteredEditMode)
        {
            // Only return if this specific tool was used to launch the game
            if (EditorPrefs.GetBool(PREFS_KEY_SHOULD_RETURN, false))
            {
                string originalScenePath = EditorPrefs.GetString(PREFS_KEY_ORIGINAL_SCENE, "");

                if (!string.IsNullOrEmpty(originalScenePath))
                {
                    // Delay the opening slightly to ensure Unity has finished cleaning up the play session
                    EditorApplication.delayCall += () =>
                    {
                        EditorSceneManager.OpenScene(originalScenePath);
                    };
                }

                // Clean up the flags so it doesn't happen on standard play mode clicks
                EditorPrefs.DeleteKey(PREFS_KEY_ORIGINAL_SCENE);
                EditorPrefs.DeleteKey(PREFS_KEY_SHOULD_RETURN);
            }
        }
    }
}