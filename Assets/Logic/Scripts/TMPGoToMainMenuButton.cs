using UnityEngine;

public class TMPGoToMainMenuButton : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void goBakc()
    {
        Loader.LoadMainMenu();
    }
}
