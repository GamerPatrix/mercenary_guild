using UnityEngine;

public class GoToMainMenuButton : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void GoBakc()
    {
        Loader.LoadMainMenu();
    }
}
