using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using TMPro;
using System.Collections.ObjectModel;
using UnityEngine.InputSystem;

namespace DialogSystem
{
    public class DialogLine : DialogBaseClass
    {


        private PlayerControls playerControls;
        private TMP_Text textHolder;
        [SerializeField] private Vector2 movement;
        [SerializeField] InputAction skipDialogAction;
        [SerializeField] InputAction interactAction;

        [Header("Text")]
        [SerializeField] private string input;
        [SerializeField] private Color textColor;
        [SerializeField] private TMP_FontAsset textFont;
        [Header("Time Vaariables")]
        [SerializeField] private float delay;
        [SerializeField] private float delayBeetweenLines;

        [Header("Sound Variables")]
        [SerializeField] private AudioClip sound;

        [Header("Character Image")]
        [SerializeField] private Sprite charSprite;
        [SerializeField] private Image imageHolder;
        private bool canClick = false;

        private IEnumerator LineApear;
        private void Awake()
        {
            playerControls = new PlayerControls();
            imageHolder.sprite = charSprite;
            imageHolder.preserveAspect = true;
        }
        private void OnEnable()
        {
            playerControls.Enable(); // Enable the input actions
            ResetLine();
            LineApear = WriteText(input, textHolder, textColor, textFont, delay, sound, delayBeetweenLines);
            StartCoroutine(LineApear);
        }

        private void Update()
        {
            PlayerInput();
            if (skipDialogAction.WasPerformedThisFrame() && canClick)
            {
                if (textHolder.text != input)
                {
                    StopCoroutine(LineApear);
                    textHolder.text = input;
                }
                else
                {
                    finished = true;
                    finishedPlayingLines = true;
                }
            }
        }

        private void ResetLine()
        {
            textHolder = GetComponent<TMP_Text>();
            textHolder.text = "";
            finished = false;
            finishedPlayingLines = false;
        }

        public bool finished { get; protected set; }
        public bool finishedPlayingLines { get; protected set; }

        private void PlayerInput()
        {
            interactAction = playerControls.Actions.Interact;
            skipDialogAction = playerControls.Actions.SkipDialog;
        }
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

            print("PRUBA PROFE");
            // Wait for the player to release the 'E' key
            //while(interactAction.phase != InputActionPhase.Canceled)
            //{
            //    yield return null;
            //}
            finishedPlayingLines = true;
              yield return new WaitUntil(() => interactAction.WasPressedThisFrame() && finishedPlayingLines);
            //yield return new WaitForSeconds(delayBetweenLines);

            print("PRUBA PROFE2");
            finished = true; // Mark the dialog as finished
        }
    }


}