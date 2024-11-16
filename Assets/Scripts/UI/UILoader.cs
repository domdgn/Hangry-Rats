using UnityEngine;
using UnityEngine.SceneManagement;

public class UILoader : MonoBehaviour
{
    private void Awake()
    {
        // Make the UI persistent
        DontDestroyOnLoad(gameObject);

        // Make sure we only have one UI
        if (FindObjectsOfType<UILoader>().Length > 1)
        {
            Destroy(gameObject);
        }
    }
}