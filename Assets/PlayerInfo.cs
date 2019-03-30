using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    public enum PlayerState
    {
        Idle,
        Working,
        Carrying
    }

    public PlayerState playerState;

    public Funcao workingNow;

    public float prog, art, write, coffee, sound, patience;
    public float stress;


    public void SetStatus(int p, int a, int w, int c, int s, int pa)
    {
        prog = p;
        art = a;
        write = w;
        coffee = c;
        sound = s;
        patience = pa;
    }
}
