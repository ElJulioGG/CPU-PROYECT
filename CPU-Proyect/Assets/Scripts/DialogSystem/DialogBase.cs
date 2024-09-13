using System.Collections;
using UnityEngine;
using TMPro;

namespace DialogSystem
{
    public class DialogBaseClass : MonoBehaviour
    {
        public bool finished { get; protected set; }

        // The method to start the coroutine for writing text
        public void StartDialog(string input, TMP_Text textHolder, Color textColor, TMP_FontAsset textFont, float delay, AudioClip soundEffect, float delayBetweenLines)
        {
            StartCoroutine(WriteText(input, textHolder, textColor, textFont, delay, soundEffect, delayBetweenLines));
        }

        // Coroutine for the typewriter effect
        protected IEnumerator WriteText(string input, TMP_Text textHolder, Color textColor, TMP_FontAsset textFont, float delay, AudioClip soundEffect, float delayBetweenLines)
        {
            // Reset the text holder's text and apply color/font settings
            textHolder.text = "";
            textHolder.color = textColor;
            textHolder.font = textFont;

            for (int i = 0; i < input.Length; i++)
            {
                textHolder.text += input[i]; // Add each character to the text
                if (soundEffect != null) SoundDialogManager.instance.PlaySound(soundEffect); // Play sound if provided
                yield return new WaitForSeconds(delay); // Wait between characters
            }

            // Wait for the player to release the 'E' key
            yield return new WaitUntil(() => Input.GetKeyUp(KeyCode.E));

            finished = true; // Mark the dialog as finished
        }
    }
}
