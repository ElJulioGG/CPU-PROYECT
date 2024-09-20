using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteObject : MonoBehaviour
{
    public bool canBePressed;
    public bool obtained = false; // Nueva variable para saber si la nota ha sido golpeada
    public KeyCode keyToPress;


    public GameObject hiteEffect,godEffect,perfectEffect,missedEffect;
    void Update()
    {
        if (Input.GetKeyDown(keyToPress))
        {
            if (canBePressed)
            {
                obtained = true; // Indicamos que la nota ha sido golpeada
                gameObject.SetActive(false);
                //GameManagerM1.instance.NoteHit();
                if (Mathf.Abs(transform.position.y) > 0.25) {
                    Debug.Log("Hit");
                   GameManagerM1.instance.NormalHit();
                    Instantiate(hiteEffect, transform.position, hiteEffect.transform.rotation);
                }
                else if (Mathf.Abs(transform.position.y) > 0.05f)
                {
                    Debug.Log("Good");
                    GameManagerM1.instance.GoodHit();
                    Instantiate(godEffect, transform.position, godEffect.transform.rotation);

                }
                else 
                {
                    Debug.Log("Perfect");
                    GameManagerM1.instance.PerfectHit();
                    Instantiate(perfectEffect, transform.position, perfectEffect.transform.rotation);

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
            // Solo ejecutamos NoteMissed si la nota no fue golpeada
            if (!obtained)
            {
                GameManagerM1.instance.NoteMissed();
                Instantiate(missedEffect, transform.position, missedEffect.transform.rotation);

            }
        }
    }
}
