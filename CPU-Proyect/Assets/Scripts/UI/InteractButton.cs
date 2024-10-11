using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractButton : MonoBehaviour
{
    private Animator animator;
    private Collider2D parentCollider;
    private NPCInteraction npc;

    
    void Start()
    {

        animator = GetComponent<Animator>();    
        npc = GameObject.Find("Player").GetComponent<NPCInteraction>();

      
        parentCollider = transform.parent.GetComponent<Collider2D>();

        if (parentCollider == null)
        {
            Debug.LogError("Parent object doesn't have a Collider2D!");
        }
    }

 
    void Update()
    {
        if (npc.showInteract)
        {
            animator.SetBool("Show", true);
        }
        else
        {
            animator.SetBool("Show", false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       
        if (parentCollider != null && collision.CompareTag("Player"))
        {
            print("YEADHHHHHHH");
            animator.SetTrigger("FadeIn");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
    
        if (parentCollider != null && collision.CompareTag("Player"))
        {
            animator.SetTrigger("FadeOut");
        }
    }
}
