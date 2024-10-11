using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteObject : MonoBehaviour
{
    public bool canBePressed;
    public bool obtained = false; // Saber si la nota ha sido golpeada
    public KeyCode keyToPress;
    private RhythmManager rhythmManager;
    public GameObject hitEffect, goodEffect, perfectEffect, missedEffect;

    // Para indicar si es una flecha izquierda o derecha (solo para flechas especiales)
    public bool isLeftArrow;  // true para izquierda, false para derecha

    // Nuevo: Para identificar si la flecha es especial o no
    public bool isSpecialArrow; // true si la flecha es especial

    private PlayerController playerController;

    void Start()
    {
        // Encuentra al PlayerController
        playerController = FindObjectOfType<PlayerController>();
        rhythmManager = FindObjectOfType<RhythmManager>(); // Encuentra al RhythmManager

    }

    void Update()
    {
        if (Input.GetKeyDown(keyToPress))
        {
            if (canBePressed)
            {
                obtained = true;
                gameObject.SetActive(false);

                if (Mathf.Abs(transform.position.y) > 0.25f)
                {
                    Debug.Log("Hit");
                    GameManagerM1.instance.NormalHit();
                    Instantiate(hitEffect, transform.position, hitEffect.transform.rotation);
                }
                else if (Mathf.Abs(transform.position.y) > 0.05f)
                {
                    Debug.Log("Good");
                    GameManagerM1.instance.GoodHit();
                    Instantiate(goodEffect, transform.position, goodEffect.transform.rotation);
                }
                else
                {
                    Debug.Log("Perfect");
                    GameManagerM1.instance.PerfectHit();
                    Instantiate(perfectEffect, transform.position, perfectEffect.transform.rotation);
                }

                // Llama a AddScore cuando la nota es golpeada
                RhythmManager rhythmManager = FindObjectOfType<RhythmManager>();
                rhythmManager.AddScore(); // Incrementa el puntaje

                if (isSpecialArrow)
                {
                    if (isLeftArrow)
                    {
                        playerController.MovePlayerLeft();
                    }
                    else
                    {
                        playerController.MovePlayerRight();
                    }
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Activator")
        {
            canBePressed = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Activator")
        {
            canBePressed = false;

            // Verificar si la nota no fue golpeada
            if (!obtained)
            {
                GameManagerM1.instance.NoteMissed();
                Instantiate(missedEffect, transform.position, missedEffect.transform.rotation);

                // Verificar si el jugador se ha movido
                bool playerMoved = playerController.IsMoving(); // Asegúrate de implementar este método en PlayerController
                RhythmManager rhythmManager = FindObjectOfType<RhythmManager>();
                //rhythmManager.CheckForMissedHit(playerMoved); // Llama a la función de pérdida de vida
            }
        }
    }

}
