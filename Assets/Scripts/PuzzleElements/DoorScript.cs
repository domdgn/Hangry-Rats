using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private int doorID; // ID to link door to specific button(s)
    [SerializeField] private float openSpeed = 2f;
    [SerializeField] private float openDistance = 3f;

    private Vector3 closedPosition;
    private Vector3 openPosition;
    private bool isOpen;

    private void Start()
    {
        closedPosition = transform.position;
        openPosition = closedPosition + Vector3.up * openDistance; // Door moves upward when opened
        DoorManager.Instance.RegisterDoor(this, doorID);
    }

    private void Update()
    {
        // Smoothly move the door to its target position
        Vector3 targetPosition = isOpen ? openPosition : closedPosition;
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * openSpeed);
    }

    public void SetDoorState(bool open)
    {
        isOpen = open;
    }
}