using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteObject : MonoBehaviour
{
    public bool canBePressed;
    public bool obtained = false; // Nueva variable para saber si la nota ha sido golpeada
    public KeyCode keyToPress;

    void Update()
    {
        if (Input.GetKeyDown(keyToPress))
        {
            if (canBePressed)
            {
                obtained = true; // Indicamos que la nota ha sido golpeada
                gameObject.SetActive(false);
                GameManagerM1.instance.NoteHit();
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
            // Solo ejecutamos NoteMissed si la nota no fue golpeada
            if (!obtained)
            {
                GameManagerM1.instance.NoteMissed();
            }
        }
    }
}
