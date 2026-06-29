using UnityEngine;

public class LoadingScreen : MonoBehaviour
{
    bool first = true;
    int timeount = 0;
    private void Update()
    {
        if (timeount--<=0 && first)
        {
            first = false;
            Loader.LoaderCallBack();
        } 
    }
}
