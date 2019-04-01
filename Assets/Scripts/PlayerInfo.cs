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
    bool spawnBaloooooooon = false;

    public GameObject prefabSweat, prefabRage, prefabLuv;
	
	void FixedUpdate()
	{
        if (manager.gameStarted)
        {
            if (workingNow != null)
            {
                switch (workingNow.machineType)
                {
                    case Funcao.MachineType.Rest:
                        animator.SetBool("isTaked", true);
                        GetComponent<SpriteRenderer>().flipX = true;
                        break;
                    //case Funcao.MachineType.Coffee:
                    //    animator.SetBool("isTaked", true);
                    //    break;
                    default:
                        animator.SetBool("isWorking", true);
                        break;
                }
            }

        }
        else
        {
            StopAllCoroutines();
        }
    }

    private void Update()
    {
        if (manager.gameStarted)
        {
            if (stress <= 0)
                stress = 0;
            if (workingNow != null)
            {
                if (stress >= patience && workingNow.machineType != Funcao.MachineType.Rest)
                {
                    if (!soundStress)
                    {
                        soundStress = true;
                        StartCoroutine(StressSound());
                    }
                    playerState = PlayerState.Stressing;
                    workingNow.ChangeCharUsing(null);
                    animator.SetBool("isAngry", true);
                    workingNow = null;
                    Debug.Log("CHEGA DESSA MERDA!!!!");
                }
                if (stress < (patience / 10) && workingNow.machineType == Funcao.MachineType.Rest && !spawnBaloooooooon)
                {
                    StartCoroutine(SpawnLuv());
                    spawnBaloooooooon = true;
                }
            }
            if (stress < patience)
            {
                animator.SetBool("isAngry", false);
            }
            if (stress > (patience / 2) && !spawnBaloooooooon)
            {
                if (stress > ((patience * 3) / 4))
                {
                    StartCoroutine(SpawnRage());
                    spawnBaloooooooon = true;
                }
                else
                {
                    StartCoroutine(SpawnSweat());
                    spawnBaloooooooon = true;

                }
            }
        }
        else
        {
            if (workingNow != null)
            {
                workingNow.ChangeCharUsing(null);
                workingNow = null;
                StopAllCoroutines();

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

    public IEnumerator SpawnSweat()
    {
        GameObject baloon = Instantiate(prefabSweat,transform);
        baloon.GetComponent<Transform>().localPosition = new Vector3(0, 1.3f, 0);
        yield return new WaitForSeconds(0.8f);
        Destroy(baloon);
        yield return new WaitForSeconds(1f);
        spawnBaloooooooon = false;
    }
    public IEnumerator SpawnRage()
    {
        GameObject baloon = Instantiate(prefabRage,transform);
        baloon.GetComponent<Transform>().localPosition = new Vector3(0, 1.3f, 0);
        yield return new WaitForSeconds(0.8f);
        Destroy(baloon);
        yield return new WaitForSeconds(1f);
        spawnBaloooooooon = false;
    }
    public IEnumerator SpawnLuv()
    {
        GameObject baloon = Instantiate(prefabLuv,transform);
        baloon.GetComponent<Transform>().localPosition = new Vector3(0, 1.3f, 0);
        yield return new WaitForSeconds(0.8f);
        Destroy(baloon);
        yield return new WaitForSeconds(1f);
        spawnBaloooooooon = false;
    }
}
