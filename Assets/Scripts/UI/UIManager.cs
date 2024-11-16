using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    public TMP_Text pickupText;
    public TMP_Text promptText;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void UpdatePickupCount(int count)
    {
        if (pickupText != null)
        {
            pickupText.text = $"Pick Ups: {count}";
        }
    }
    
    public void UpdatePromptText(string text)
    {
        if (promptText != null)
        {
            promptText.text = text;
        } 
    }    
}