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
        [SerializeField] private AudioInfoSO currentAudioInfo;


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
            LineApear = WriteText(input, textHolder, textColor, textFont, delay, delayBeetweenLines);
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
        public void StartDialog(string input, TMP_Text textHolder, Color textColor, TMP_FontAsset textFont, float delay, float delayBetweenLines)
        {
            StartCoroutine(WriteText(input, textHolder, textColor, textFont, delay, delayBetweenLines));
        }

        // Coroutine for the typewriter effect
        protected IEnumerator WriteText(string input, TMP_Text textHolder, Color textColor, TMP_FontAsset textFont, float delay, float delayBetweenLines)
        {
            AudioClip[] sounds = currentAudioInfo.sounds;
            int soundPerCharFrequency = currentAudioInfo.soundPerCharFrequency;
            float minPitch = currentAudioInfo.minPitch;
            float maxPitch = currentAudioInfo.maxPitch;
            bool makePredictable = currentAudioInfo.makePredictable;

            // Reset the text holder's text and apply color/font settings
            textHolder.text = "";
            textHolder.color = textColor;
            textHolder.font = textFont;
            textHolder.maxVisibleCharacters = 0;

            for (int i = 0; i < input.Length; i++)
            {
                textHolder.text += input[i]; 
                //To play the sound every certain amount of frames
                if (textHolder.maxVisibleCharacters % soundPerCharFrequency == 0)
                {
                    SoundDialogManager.instance.StopSounds();
                    AudioClip soundClip = null;
                    if (sounds != null) {
                        if (makePredictable) {
                            char currentCharacter = textHolder.text[textHolder.maxVisibleCharacters];
                            int hashCode = currentCharacter.GetHashCode();
                            
                            int predictableIndex = hashCode % sounds.Length;
                            soundClip = sounds[predictableIndex];

                            int minPitchInt = (int)(minPitch * 100);
                            int maxPitchInt = (int)(maxPitch * 100);
                            int pitchRangeInt = maxPitchInt - minPitchInt;
                            //if cant divide by 0
                            if(pitchRangeInt != 0)
                            {
                                int predictablePitchInt = (hashCode % pitchRangeInt) + minPitchInt;
                                float predictablePitch = predictablePitchInt / 100f;
                                SoundDialogManager.instance.ChangePitch(predictablePitch);
                            }
                            else
                            {
                                SoundDialogManager.instance.ChangePitch(minPitch);
                            }
                            
                        } 
                        else {
                            SoundDialogManager.instance.ChangePitch(Random.Range(minPitch, maxPitch));
                            int randomIndex = Random.Range(0, sounds.Length);
                            soundClip = sounds[randomIndex];
                            
                        }
                        SoundDialogManager.instance.PlaySound(soundClip);


                    } 
                }
                textHolder.maxVisibleCharacters++;
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