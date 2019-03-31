using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
	private Animator animator;
	
	void Start()
	{
		animator = GetComponent<Animator>();
	}
	
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
	
	void FixedUpdate()
	{
		if (workingNow == null)
		{
			animator.SetBool("isWorking", false);
		}
		else
		{
			animator.SetBool("isWorking", true);
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
}
