using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SecurityLevelManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private TMP_Text text;
    private Image imagen;
    public Color newColor;

    void Start()
    {
        imagen = GetComponent<Image>();
        

    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.securityLevel < 35)
        {
            imagen.sprite = sprites[0];
            newColor = new Color(0.1f, 0.5f, 0.9f);

        }
        else if (GameManager.instance.securityLevel < 65)
        {
            imagen.sprite = sprites[1];
            newColor = new Color(1f,0.9f,0.2f);
        }
        else
        {
            imagen.sprite = sprites[2];
            newColor = new Color(1f,0f,0f);
        }
        text.color = newColor;
        text.text = GameManager.instance.securityLevel.ToString() + "%";
    }
}
