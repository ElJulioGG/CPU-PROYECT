using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using UnityEngine.InputSystem;

public class NPCInteraction : MonoBehaviour
{
    // Start is called before the first frame update
    private NPCController npc;
    private PlayerControls playerControls;
    public bool showInteract;

    [SerializeField] private Vector2 movement;
    [SerializeField] InputAction rollAction;
    [SerializeField] InputAction interactAction;
    private void Awake()
    {
        playerControls = new PlayerControls();
    }
    private void OnEnable()
    {
        playerControls.Enable(); // Enable the input actions
    }
    private void Update()
    {
        PlayerInput();
        if (GameManager.instance.playerCanAction)
        {
            if ((interactAction.WasPressedThisFrame()) && GameManager.instance.playerCanDialog)
            {
                showInteract = false;
                print("Player in dialog");
                GameManager.instance.playerIsInDialog = true;
                GameManager.instance.playerCanMove = false;
                GameManager.instance.playerCanDialog = false;
                npc.ActiveDialog();

            }
        }
    }
    private void PlayerInput()
    {
        movement = playerControls.Movement.Move.ReadValue<Vector2>();
        rollAction = playerControls.Actions.Roll;
        interactAction = playerControls.Actions.Interact;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "NPC" && !GameManager.instance.playerIsInDialog)
        {
            showInteract = true;
            GameManager.instance.playerCanDialog = true;
            npc = collision.gameObject.GetComponent<NPCController>();
           
            print("Player Can Dialog");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "NPC")
        {
            showInteract = false;
            npc = null;
            GameManager.instance.playerCanDialog = false;
            
        }
    }
    private bool inDialogue()
    {
        if (npc != null)
        {
            GameManager.instance.playerIsInDialog = true;
            GameManager.instance.playerCanMove = false;
            return npc.DialogActive();
        }
        else
        {
            GameManager.instance.playerIsInDialog = false;
            return false;
        }
    }
}
