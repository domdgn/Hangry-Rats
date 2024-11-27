using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
    public static DoorManager Instance { get; private set; }

    private Dictionary<int, List<Door>> doorGroups = new Dictionary<int, List<Door>>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void RegisterDoor(Door door, int doorID)
    {
        if (!doorGroups.ContainsKey(doorID))
        {
            doorGroups[doorID] = new List<Door>();
        }
        doorGroups[doorID].Add(door);
    }

    public void ActivateDoors(int buttonID, bool open)
    {
        if (doorGroups.ContainsKey(buttonID))
        {
            foreach (Door door in doorGroups[buttonID])
            {
                door.SetDoorState(open);
            }
        }
    }
}
