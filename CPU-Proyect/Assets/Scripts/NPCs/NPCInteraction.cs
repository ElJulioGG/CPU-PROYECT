using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteraction : MonoBehaviour
{
    // Start is called before the first frame update
    private NPCController npc;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "NPC")
        {
            GameManager.instance.playerCanDialog = true;

            npc = collision.gameObject.GetComponent<NPCController>();
            print("Player Can Dialog");
            if (Input.GetKey(KeyCode.E))
            {
                print("Player in dialog");
                GameManager.instance.playerIsInDialog = true;
                GameManager.instance.playerCanMove = false;
                npc.ActiveDialog();

            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "NPC")
        {
            npc = null;
            GameManager.instance.playerCanDialog = false;
        }
    }
    private bool inDialogue()
    {
        if (npc != null)
        {
            GameManager.instance.playerIsInDialog = true;
            return npc.DialogActive();
        }
        else
        {
            GameManager.instance.playerIsInDialog = false;
            return false;
        }
    }
}
