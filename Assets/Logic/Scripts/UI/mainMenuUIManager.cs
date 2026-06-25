using UnityEngine;

public class MainMenuUIManager : MonoBehaviour
{
    [SerializeField]
    GameObject Options;
    [SerializeField]
    GameObject Main;

    public void goToMain()
    {
        Main.SetActive(true);
        Options.SetActive(false);
    }

    public void goToOptions()
    {
        Options.SetActive(true);
        Main.SetActive(false);
    }
}
