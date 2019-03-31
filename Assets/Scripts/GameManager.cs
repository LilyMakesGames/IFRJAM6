using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    int count, countTimer = 0;
    public Image codeProgressBar, artProgressBar, writeProgressBar, coffeeProgressBar, soundProgressBar;
    public GameObject progressPanel, gameOverPanel;
    bool showPanel = false;

    public Image codeArrow, artArrow, writeArrow, coffeeArrow, soundArrow;

    public AudioSource sound,music;
    public AudioClip catchNPC, releaseNPC, tableSlam, startedWorking, stoppedWorking;

    public Animator clockAnim;

    public float gameProgress;
    [HideInInspector]
    public float codeProgress, artProgress, writeProgress, coffeeProgress, soundProgress;
    float codeProgressMax = 100f, artProgressMax = 100f, writeProgressMax = 100f, coffeeProgressMax = 100f, soundProgressMax = 100f;
    public float codeObjective, artObjective, writeObjective, coffeeObjective, soundObjective;
    float totalProgress;

    public float decay;

    [SerializeField]
    float timer = 30;

    float div;

    public bool gameStarted;

    void Start()
    {
        div = timer / 30;
        StartCoroutine(Timer());
        totalProgress = codeObjective + artObjective + writeObjective + coffeeObjective + soundObjective;
    }


    void Update()
    {
        ProgressUpdate();
        codeArrow.GetComponent<RectTransform>().anchoredPosition = new Vector3((codeObjective / codeProgressMax* 66) + 7, 5);

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

    public void PlaySound(AudioClip clip)
    {
        sound.clip = clip;
        sound.Play();
    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(1f);
        if (timer <= 0)
        {
            gameOverPanel.SetActive(true);
            gameStarted = false;
            StopAllCoroutines();
        }
        countTimer++;
        if (countTimer >= div)
        {
            clockAnim.SetInteger("Animation", count);
            count++;
            countTimer = 0;

        }
        timer--;
        Debug.Log(timer);
        codeProgress -= decay;
        artProgress -= decay;
        writeProgress -= decay;
        coffeeProgress -= decay;
        soundProgress -= decay;
        StartCoroutine(Timer());

    }
}
