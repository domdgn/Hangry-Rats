using System.Collections;
using System.Collections.Generic;
using UnityEditor.EditorTools;
using UnityEngine;
public class ButtonColliderHandler : MonoBehaviour
{
    [SerializeField] private int buttonID; // ID to link button to specific door(s)
    [SerializeField] private bool toggleButton; // If true, button toggles door state. If false, door only opens when pressed
    private bool isPressed;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("Rat"))
        {
            isPressed = true;
            DoorManager.Instance.ActivateDoors(buttonID, true);
            Debug.Log($"Button {buttonID} Activated");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("Rat"))
        {
            isPressed = false;
            if (!toggleButton)
            {
                DoorManager.Instance.ActivateDoors(buttonID, false);
                Debug.Log($"Button {buttonID} Deactivated");
            }
        }
    }
}