using UnityEngine;

public class DoNotDestroyOnLoad : MonoBehaviour
{
    private void Awake()
    {
        // Make this object persist across scene loads
        DontDestroyOnLoad(gameObject);
    }
}
