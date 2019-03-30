using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public Image codeProgressBar, artProgressBar, writeProgressBar, coffeeProgressBar, soundProgressBar;
    public GameObject progressPanel, gameOverPanel;
    bool showPanel = false;

    public float gameProgress;
    public float codeProgress, artProgress, writeProgress, coffeeProgress, soundProgress;
    float codeProgressMax = 30f, artProgressMax = 40f, writeProgressMax = 70f, coffeeProgressMax = 80f, soundProgressMax = 10f;
    float codeObjective, artObjective, writeObjective, coffeeObjective, soundObjective;
    float totalProgress;

    public float decay;

    [SerializeField]
    float timer = 180;

    public bool gameStarted;

    void Start()
    {
        StartCoroutine(Timer());
        totalProgress = codeObjective + artObjective + writeObjective + coffeeObjective + soundObjective;

    }


    void Update()
    {
        ProgressUpdate();
        if (timer <= 0)
        {
            StopCoroutine(Timer());
            gameOverPanel.SetActive(true);
            gameStarted = false;
        }
    }


    void ProgressUpdate()
    {
        if (codeProgress > codeProgressMax)
            codeProgress = codeProgressMax;
        if (artProgress > artProgressMax)
            artProgress = artProgressMax;
        if (writeProgress > writeProgressMax)
            writeProgress = writeProgressMax;
        if (coffeeProgress > coffeeProgressMax)
            coffeeProgress = coffeeProgressMax;
        if (soundProgress > soundProgressMax)
            soundProgress = soundProgressMax;

        if (codeProgress <= 0)
            codeProgress = 0;
        if (artProgress <= 0)
            artProgress = 0;
        if (writeProgress <= 0)
            writeProgress = 0;
        if (coffeeProgress <= 0)
            coffeeProgress = 0;
        if (soundProgress <= 0)
            soundProgress = 0;

        gameProgress = codeProgress + artProgress + writeProgress + coffeeProgress + soundProgress;
        codeProgressBar.fillAmount = (codeProgress / codeProgressMax);
        artProgressBar.fillAmount = (artProgress / artProgressMax);
        writeProgressBar.fillAmount = (writeProgress / writeProgressMax);
        coffeeProgressBar.fillAmount = (coffeeProgress / coffeeProgressMax);
        soundProgressBar.fillAmount = (soundProgress / soundProgressMax);

    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(1f);
        timer--;
        codeProgress -= decay;
        artProgress -= decay;
        writeProgress -= decay;
        coffeeProgress -= decay;
        soundProgress -= decay;
        StartCoroutine(Timer());
    }
}
