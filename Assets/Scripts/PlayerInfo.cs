using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    public enum PlayerState
    {
        Idle,
        Working,
        Carrying,
        Stressing
    }

    public PlayerState playerState;

    public GameManager manager;

    public Sprite cloth, naked;

    public Funcao workingNow;

    public float prog, art, write, coffee, sound, patience;
    public float stress;
    public bool soundStress;

    private void Update()
    {
        if (stress <= 0)
            stress = 0;
        if(workingNow != null)
        {
            if (stress > patience && workingNow.machineType != Funcao.MachineType.Rest)
            {
                if (!soundStress)
                {
                    soundStress = true;
                    StartCoroutine(StressSound());
                }
                playerState = PlayerState.Stressing;
                workingNow.ChangeCharUsing(null);
                Debug.Log("CHEGA DESSA MERDA!!!!");
            }
        }
    }

    public void SetStatus(int p, int a, int w, int c, int s, int pa)
    {
        prog = p;
        art = a;
        write = w;
        coffee = c;
        sound = s;
        patience = pa;
    }

    public IEnumerator StressSound()
    {
        yield return new WaitForSeconds(0.8f);
        manager.PlaySound(manager.tableSlam);
        StartCoroutine(StressSound());

    }
}
