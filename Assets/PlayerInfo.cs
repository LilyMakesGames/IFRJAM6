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

    public int prog, art, write, coffee, sound;
    public float stress;


    public void SetStatus(int p, int a, int w, int c, int s)
    {
        prog = p;
        art = a;
        write = w;
        coffee = c;
        sound = s;
    }
}
