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
    public float codeProgressMax = 20, artProgressMax = 20, writingProgressMax = 20, coffeeProgressMax = 20, soundProgressMax = 20;

    void Start()
    {
        
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
        progressBar.fillAmount = gameProgress/100;
    }
}
