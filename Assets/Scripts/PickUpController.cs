using UnityEngine;
using TMPro;

public class PickUpController : MonoBehaviour
{
    public static PickUpController Instance { get; private set; }

    [Header("UI References")]
    [SerializeField] private TMP_Text pickupText;

    private int pickUpCount;

    private void Awake()
    {
        // Implement Singleton pattern
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

    private void Start()
    {
        pickUpCount = 0;
        UpdatePickUpCountText();
    }

    public void CollectPickup()
    {
        pickUpCount++;
        UpdatePickUpCountText();
    }

    private void UpdatePickUpCountText()
    {
        string displayText = $"Pick Ups: {pickUpCount}";

        // Update TextMeshPro if available
        pickupText.text = displayText;
    }
}