using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
    public static DoorManager Instance { get; private set; }

    private Dictionary<int, List<DoorScript>> doorGroups = new Dictionary<int, List<DoorScript>>();

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

    public void RegisterDoor(DoorScript door, int doorID)
    {
        if (!doorGroups.ContainsKey(doorID))
        {
            doorGroups[doorID] = new List<DoorScript>();
        }
        doorGroups[doorID].Add(door);
    }

    public void ActivateDoors(int buttonID, bool open)
    {
        if (doorGroups.ContainsKey(buttonID))
        {
            foreach (DoorScript door in doorGroups[buttonID])
            {
                door.SetDoorState(open);
            }
        }
    }
}
