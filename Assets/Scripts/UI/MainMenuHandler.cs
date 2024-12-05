using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenuHandler : MonoBehaviour
{
    [Header("Object References")]
    [SerializeField] Text startText;
    [SerializeField] Text levelText;
    [SerializeField] Text exitText;
    [SerializeField] GameObject mainMenuPanel;
    [SerializeField] GameObject levelSelectPanel;
    [SerializeField] WorldButtonHandler worldButtonHandler1;
    [SerializeField] WorldButtonHandler worldButtonHandler2;
    [SerializeField] WorldButtonHandler worldButtonHandler3;

    [Header("Numerics")]
    int countIndex;

    [Header("Miscellaneous")]
    [SerializeField] Color highlightColor;

    private void Start()
    {
        countIndex = 0;
        UpdateSelection();
    }

    private void Update()
    {
        UpdateSelection();

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            countIndex--;
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            countIndex++;
        }

        if (countIndex >= 3)
        {
            countIndex = 2;
        }

        if (countIndex <= -1)
        {
            countIndex = 0;
        }
    }

    private void UpdateSelection()
    {
        if (countIndex == 0)
        {
            startText.color = highlightColor;
            levelText.color = Color.black;
            exitText.color = Color.black;

            if (Input.GetKeyDown(KeyCode.Return))
            {
                StartGame();
            }
        }

        if (countIndex == 1)
        {
            startText.color = Color.black;
            levelText.color = highlightColor;
            exitText.color = Color.black;

            if (Input.GetKeyDown(KeyCode.Return))
            {
                LevelSelect();
            }
        }

        if (countIndex == 2)
        {
            startText.color = Color.black;
            levelText.color = Color.black;
            exitText.color = highlightColor;

            if (Input.GetKeyDown(KeyCode.Return))
            {
                ExitGame();
            }
        }
    }

    private void StartGame()
    {
        SceneManager.LoadScene("Level1-1");
    }

    private void LevelSelect()
    {
        levelSelectPanel.SetActive(true);
        mainMenuPanel.SetActive(false);
    }

    private void ExitGame()
    {
        Application.Quit();
    }
}