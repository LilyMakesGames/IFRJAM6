using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public Image progressBar;
    public GameObject panel;
    bool showPanel = false;

    public float gameProgress = 1;
    public float codeProgress, artProgress, writingProgress, coffeeProgress, soundProgress;
    float codeProgressMax = 30, artProgressMax = 40, writingProgressMax = 70, coffeeProgressMax = 80, soundProgressMax = 10;
    float totalProgress;

    float timer = 60;

    public bool gameStarted;

    void Start()
    {
        StartCoroutine(Timer());
        totalProgress = codeProgressMax + artProgressMax + writingProgressMax + coffeeProgressMax + soundProgressMax;

    }


    void Update()
    {
        if (codeProgress > codeProgressMax)
            codeProgress = codeProgressMax;
        if (artProgress > artProgressMax)
            artProgress = artProgressMax;
        if (writingProgress > writingProgressMax)
            writingProgress = writingProgressMax;
        if (coffeeProgress > coffeeProgressMax)
            coffeeProgress = coffeeProgressMax;
        if (soundProgress > soundProgressMax)
            soundProgress = soundProgressMax;
        gameProgress = codeProgress + artProgress + writingProgress + coffeeProgress + soundProgress;
        progressBar.fillAmount = (gameProgress/totalProgress);
    }

    IEnumerator Timer()
    {
        Debug.Log(timer);
        yield return new WaitForSeconds(1f);
        timer--;
        StartCoroutine(Timer());
    }
}
