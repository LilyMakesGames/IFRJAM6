using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
	public Animator animator;
	
	void Start()
	{
		animator = GetComponent<Animator>();
	}
	
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
				animator.SetBool("isAngry", true);
                Debug.Log("CHEGA DESSA MERDA!!!!");
            }
			else
			{
				animator.SetBool("isAngry", false);
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
