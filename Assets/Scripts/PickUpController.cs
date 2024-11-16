using UnityEngine;
using TMPro;

public class PickUpController : MonoBehaviour
{
    public static PickUpController Instance { get; private set; }

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
        ResetPickups();
    }

    public void CollectPickup()
    {
        pickUpCount++;
        UpdateUI();
    }

    public int GetPickUpCount()
    {
        return pickUpCount;
    }

    public void ResetPickups()
    {
        pickUpCount = 0;
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (UIManager.Instance != null)
        {
            UIManager.Instance.UpdatePickupCount(pickUpCount);
        }
    }
}