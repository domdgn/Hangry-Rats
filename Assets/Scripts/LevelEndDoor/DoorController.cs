using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class DoorController : MonoBehaviour
{
    [Header("Door Settings")]
    [SerializeField] private int requiredPickups = 3;
    [SerializeField] private string nextLevelName = "Level2";
    [SerializeField] private Color lockedColor = Color.red;
    [SerializeField] private Color unlockedColor = Color.green;

    private SpriteRenderer spriteRenderer;
    private BoxCollider2D doorCollider;
    private bool isUnlocked = false;
    private bool playerInRange = false;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        doorCollider = GetComponent<BoxCollider2D>();
        doorCollider.isTrigger = false;
        spriteRenderer.color = lockedColor;

        if (UIManager.Instance != null)
        {
            UIManager.Instance.promptText.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        bool hasEnoughPickups = PickUpController.Instance.GetPickUpCount() >= requiredPickups;

        if (playerInRange)
        {
            if (UIManager.Instance != null)
            {
                UIManager.Instance.UpdatePromptText($"Collect {requiredPickups} pickups to unlock\n({PickUpController.Instance.GetPickUpCount()}/{requiredPickups})");
            }
        }

        if (!isUnlocked && hasEnoughPickups)
        {
            Unlock();
        }
    }

    private void Unlock()
    {
        isUnlocked = true;
        spriteRenderer.color = unlockedColor;
    }

    // Use a trigger collider as a detection zone around the door
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            if (UIManager.Instance != null)
            {
                UIManager.Instance.promptText.gameObject.SetActive(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            if (UIManager.Instance != null)
            {
                UIManager.Instance.promptText.gameObject.SetActive(false);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && isUnlocked)
        {
            SceneManager.LoadScene(nextLevelName);
            PickUpController.Instance.ResetPickups();
        }
    }
}