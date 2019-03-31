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

    public Image trophy;
    public Text gameOverMessage;

    public float gameProgress;
    public float codeProgress=50f, artProgress = 50f, writeProgress = 50f, coffeeProgress = 50f, soundProgress = 50f;
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
        codeArrow.GetComponent<RectTransform>().anchoredPosition = new Vector3((codeObjective / codeProgressMax * 66) + 7, 5);
        artArrow.GetComponent<RectTransform>().anchoredPosition = new Vector3((artObjective / artProgressMax * 66) + 7, 5);
        writeArrow.GetComponent<RectTransform>().anchoredPosition = new Vector3((writeObjective / writeProgressMax * 66) + 7, 5);
        soundArrow.GetComponent<RectTransform>().anchoredPosition = new Vector3((soundObjective / soundProgressMax * 66) + 7, 5);
        coffeeArrow.GetComponent<RectTransform>().anchoredPosition = new Vector3((coffeeObjective / coffeeProgressMax * 66) + 7, 5);

    }


    void Update()
    {
        ProgressUpdate();

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

    void GameOver()
    {
        float aux;
        aux = Mathf.Abs(codeProgress - codeObjective) + Mathf.Abs(artProgress - artObjective) + Mathf.Abs(writeProgress - writeObjective) + Mathf.Abs(soundProgress - soundObjective) + Mathf.Abs(coffeeProgress - coffeeObjective);
        if(aux <= 20)
        {
            gameOverMessage.text = "You got First Place!" + "\n" + "Congratulations!";
        }
        if (aux > 20 && aux <= 30)
        {
            gameOverMessage.text = "You got Second Place!" +"\n"+"Almost there!";
        }
        if (aux > 30 && aux <= 40)
        {
            gameOverMessage.text = "You got Third Place!" +"\n" + "You can do better next time!";
        }
        if (aux > 40 && aux <= 50)
        {
            gameOverMessage.text = "You got Fourth Place!" + "\n" + "Try to think more about balance!";

        }
        if (aux > 50 && aux <= 60)
        {
            gameOverMessage.text = "You got Fifth Place!" + "\n" + "Don't let your friends burn out!";
        }
        if (aux > 60)
        {
            gameOverMessage.text = "You got Last Place!" + "\n" + "I don't even know what to tell you";
        }
        gameOverPanel.SetActive(true);
        gameStarted = false;

    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(1f);
        if (timer <= 0)
        {
            GameOver();
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
