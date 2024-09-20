using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerM1 : MonoBehaviour
{
    public AudioSource theMusic;
    public bool startPlaying;
    public BeatScroller theBS;
    public static GameManagerM1 instance;
    public int currentScore;
    public int scorePetNote = 100;
    public int scorePerGoodNote = 125;
    public int scorePerfectNote = 150;

    public int currentMultiplier;
    public int multiplierTracker;
    public int[] multiplierThreholds;


    public Text scoreText;
    public Text multiText;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        scoreText.text = "Score: 0";
        currentMultiplier = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (!startPlaying)
        {
            if (Input.anyKeyDown) {
                startPlaying = true;
                theBS.hasStarted = true;
                theMusic.Play();
            }
        }
    }

    public void NoteHit() {
        Debug.Log("Chevere");
        if (currentMultiplier-1<multiplierThreholds.Length) {
            multiplierTracker++;
            if (multiplierThreholds[currentMultiplier - 1] <= multiplierTracker)
            {
                multiplierTracker = 0;
                currentMultiplier++;
            }
        }

        multiText.text = "Multiplier: x" + currentMultiplier;
        Debug.Log(currentScore);
        //currentScore += scorePetNote * currentMultiplier;
        scoreText.text = "Score: " + currentScore;
    }
    public void NormalHit() {
        currentScore += scorePetNote * currentMultiplier;
        NoteHit();
    }

    public void GoodHit()
    {
        currentScore += scorePerGoodNote * currentMultiplier;
        NoteHit();

    }

    public void PerfectHit()
    {
        currentScore += scorePerfectNote * currentMultiplier;
        NoteHit();

    }

    public void NoteMissed()
    {
        Debug.Log("PIpipipi");
        currentMultiplier = 1;
        multiplierTracker = 0;
        multiText.text = "Multiplier: x" + currentMultiplier;
    }

}
