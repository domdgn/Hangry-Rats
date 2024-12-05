using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelectHandler : MonoBehaviour
{
    [Header("Object References")]
    [SerializeField] GameObject world1;
    [SerializeField] GameObject world2;
    [SerializeField] GameObject world3;
    [SerializeField] GameObject mainMenuPanel;
    [SerializeField] GameObject levelSelectPanel;
     
    [Header("Numerics")]
    int countIndex;
    private void Start()
    {
        countIndex = 0;
    }

    private void Update()
    {
        if (countIndex >= 3)
        {
            countIndex = 2;
        }

        if (countIndex <= -1)
        {
            countIndex = 0;
        }

        UpdateSelection();
    }

    private void UpdateSelection()
    {
        if (countIndex == 0)
        {
            world1.SetActive(true);
            world2.SetActive(false);
            world3.SetActive(false);
        }

        if (countIndex == 1)
        {
            world1.SetActive(false);
            world2.SetActive(true);
            world3.SetActive(false);
        }

        if (countIndex == 2)
        {
            world1.SetActive(false);
            world2.SetActive(false);
            world3.SetActive(true);
        }
    }

    public void Back()
    {
            countIndex--;
    }

    public void Next()
    {
            countIndex++;
    }

    public void Return()
    {
        mainMenuPanel.SetActive(true);
        levelSelectPanel.SetActive(false);
    }
}