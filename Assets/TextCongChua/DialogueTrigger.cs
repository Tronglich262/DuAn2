using System;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue DialogueScript;
    private bool playerDetectedd;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playerDetectedd = true;
            DialogueScript.ToggleIndicator(playerDetectedd);
            
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playerDetectedd = false;
            DialogueScript.ToggleIndicator(playerDetectedd);
            DialogueScript.EndDialogue();
        }
        
    }

    private void Update()
    {
        if (playerDetectedd && Input.GetKeyDown(KeyCode.E))
        {
            DialogueScript.StartDialogue();
        }
    }
}
