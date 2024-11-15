using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatCollisionHandler : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Debug.Log("Rat hit player");
        }
    }
}
