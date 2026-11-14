using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterInteraction : MonoBehaviour
{
    public bool interact;
    public bool inTrigger;
    int triggeredNumber;

    public void OnInteract(InputValue value)
    {
        ActionInput(value.isPressed);
    }

    public void ActionInput(bool interacted)
    {
        interact = interacted;
    }

    private void Update()
    {
        Activate();
    }

    void Activate()
    {
        if(interact && inTrigger)
        {
            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Tile")
        {
            inTrigger = true;
            triggeredNumber = other.GetComponent<TileBehaviour>().tileValue;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Tile")
        {
            inTrigger = false;
            triggeredNumber = 0;
        }
    }
}
