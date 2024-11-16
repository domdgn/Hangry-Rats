using UnityEngine;
using UnityEngine.SceneManagement;

public class GameInitialiser : MonoBehaviour
{
    private void Start()
    {
        if (SceneManager.GetSceneByName("UI").isLoaded == false)
        {
            SceneManager.LoadScene("UI", LoadSceneMode.Additive);
        }
    }
}