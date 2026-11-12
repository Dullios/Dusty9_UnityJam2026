using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInteraction : MonoBehaviour
{
    public bool inTrigger;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag== "Tile")
            inTrigger = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Tile")
            inTrigger = false;
    }
}
