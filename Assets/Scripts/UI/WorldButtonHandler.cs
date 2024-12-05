using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class WorldButtonHandler : MonoBehaviour
{
    [Header("Object References")]
    [SerializeField] Text levelOneText;
    [SerializeField] Text levelTwoText;
    [SerializeField] Text levelThreeText;

    [Header("Numerics")]
    int countIndex;

    [Header("Miscellaneous")]
    [SerializeField] Color highlightColor;
    [SerializeField] string levelToLoad1;
    [SerializeField] string levelToLoad2;
    [SerializeField] string levelToLoad3;

    private void Start()
    {
        StartCoroutine(Cooldown());
    }
    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(1);
        countIndex = 0;
        UpdateSelection();
    }

    private void Update()
    {
        UpdateSelection();

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            countIndex--;
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
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
            levelOneText.color = highlightColor;
            levelTwoText.color = Color.black;
            levelThreeText.color = Color.black;

            if (Input.GetKeyDown(KeyCode.Return))
            {
                SceneManager.LoadScene(levelToLoad1);
            }
        }
        if (countIndex == 1)
        {
            levelOneText.color = Color.black;
            levelTwoText.color = highlightColor;
            levelThreeText.color = Color.black;

            if (Input.GetKeyDown(KeyCode.Return))
            {
                SceneManager.LoadScene(levelToLoad2);
            }
        }

        if (countIndex == 2)
        {
            levelOneText.color = Color.black;
            levelTwoText.color = Color.black;
            levelThreeText.color = highlightColor;

            if (Input.GetKeyDown(KeyCode.Return))
            {
                SceneManager.LoadScene(levelToLoad3);
            }
        }
    }
}