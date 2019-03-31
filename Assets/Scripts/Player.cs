using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameManager manager;
    Rigidbody2D rb;
    [SerializeField]
    float speed;
    public PlayerInfo info;
    GameObject currentCol;
    public GameObject carried, npcBehind;
	
	private Animator animator;
	private SpriteRenderer spriteRenderer;

    void Start()
    {
        info = GetComponent<PlayerInfo>();
        rb = GetComponent<Rigidbody2D>();

        
        info.SetStatus(3, 3, 3, 3, 3,999999);
		animator = GetComponent<Animator>();
		spriteRenderer = GetComponent<SpriteRenderer>();


    }


    void FixedUpdate()
    {
		float h = 0f;
		float v = 0f;
        if (manager.gameStarted)
        {
            switch (info.playerState)
            {
                case PlayerInfo.PlayerState.Idle:
                    
					h = Input.GetAxisRaw("Horizontal");
					v = Input.GetAxisRaw("Vertical");
					if ( h == 0f && v == 0f )
					{
						animator.SetBool("isWalking", false);
					}
					else
					{
						animator.SetBool("isWalking", true);
						animator.SetFloat("horizontal", h);
						animator.SetFloat("vertical", v);
						if (h == -1)
						{
							spriteRenderer.flipX = true;
						}
						else if (h == 1)
						{
							spriteRenderer.flipX = false;
						}
					}
					rb.velocity = new Vector3(h, v) * speed;
					
                    if (currentCol != null)
                    {
                        if (Input.GetButtonDown("Action"))
                        {
                            Debug.Log("Entrou?");
							
                            if (currentCol.GetComponent<Funcao>().charUsing == null && npcBehind == null)
                            {
                                manager.PlaySound(manager.startedWorking);
								//animator.SetBool("isWorking", true);
                                rb.velocity = Vector3.zero;
                                info.workingNow = currentCol.GetComponent<Funcao>();
                                transform.position = new Vector3(currentCol.transform.position.x, currentCol.transform.position.y + 0.75f);
                                currentCol.GetComponent<Funcao>().ChangeCharUsing(info);
                                info.playerState = PlayerInfo.PlayerState.Working;
                            }
                        }

                    }
                    if (npcBehind != null)
                    {
                        if (Input.GetButton("Action"))
                        {
                            Debug.Log("GetNPC");
                            manager.PlaySound(manager.catchNPC);
                            carried = npcBehind;
                            info.playerState = PlayerInfo.PlayerState.Carrying;
							animator.SetBool("isLoading", true);
							carried.GetComponent<PlayerInfo>().animator.SetBool("isTaked", true);
                        }
                        if (currentCol != null && Input.GetButton("Action"))
                        {
                            Debug.Log("RemoveNPC");
                            carried.GetComponent<PlayerInfo>().soundStress = false;
                            carried.GetComponent<PlayerInfo>().StopAllCoroutines();
                            carried.GetComponent<PlayerInfo>().workingNow = null;
                            manager.PlaySound(manager.catchNPC);
                            currentCol.GetComponent<Funcao>().ChangeCharUsing(null);
							carried.GetComponent<PlayerInfo>().workingNow = null;
                        }
                    }

                    break;
                case PlayerInfo.PlayerState.Working:
                    if (Input.GetButtonDown("Action"))
                    {
                        Debug.Log("Leave Work");
                        manager.PlaySound(manager.stoppedWorking);
						//animator.SetBool("isWorking", false);
                        info.workingNow.ChangeCharUsing(null);
						info.workingNow = null;
                        info.playerState = PlayerInfo.PlayerState.Idle;
                    }
                    break;
                case PlayerInfo.PlayerState.Carrying:
                    carried.transform.position = new Vector3(transform.position.x, transform.position.y + 0.5f);
                    rb.velocity = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * speed;
					
					h = Input.GetAxisRaw("Horizontal");
					v = Input.GetAxisRaw("Vertical");
					if ( h == 0f && v == 0f )
					{
						animator.SetBool("isWalking", false);
					}
					else
					{
						animator.SetBool("isWalking", true);
					}
					
                    if (Input.GetButtonDown("Action"))
                    {
						animator.SetBool("isLoading", false);
						carried.GetComponent<PlayerInfo>().animator.SetBool("isTaked", false);
                        if (currentCol != null && currentCol.GetComponent<Funcao>().charUsing == null)
                        {
                            npcBehind = null;
                            switch (currentCol.GetComponent<Funcao>().machineType)
                            {
                                case Funcao.MachineType.Rest:
                                    carried.transform.position = new Vector3(currentCol.transform.position.x, currentCol.transform.position.y +0.25f);
                                    break;
                                case Funcao.MachineType.Coffee:
                                    carried.transform.position = new Vector3(currentCol.transform.position.x - 1f, currentCol.transform.position.y);
                                    break;
                                default:
                                    carried.transform.position = new Vector3(currentCol.transform.position.x, currentCol.transform.position.y + 0.75f);
                                    break;

                            }
                            currentCol.GetComponent<Funcao>().ChangeCharUsing(carried.GetComponent<PlayerInfo>());
                            carried.GetComponent<PlayerInfo>().workingNow = currentCol.GetComponent<Funcao>();
                            carried = null;
                            info.playerState = PlayerInfo.PlayerState.Idle;
                            manager.PlaySound(manager.releaseNPC);

                        }
                        else
                        {
                            manager.PlaySound(manager.releaseNPC);
                            npcBehind = null;
                            carried.transform.position = transform.position;
                            carried = null;
                            info.playerState = PlayerInfo.PlayerState.Idle;
                        }
                    }
                    break;

            }
            if (Input.GetButtonDown("Bar"))
            {
                manager.progressPanel.GetComponent<RectTransform>().position = new Vector3(manager.progressPanel.GetComponent<RectTransform>().position.x * -1, manager.progressPanel.GetComponent<RectTransform>().position.y);
            }

        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Machine"))
        {
            currentCol = col.gameObject;
        }
        if (col.CompareTag("NPC"))
        {
            npcBehind = col.gameObject;
        }
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Machine"))
        {
            currentCol = null;
        }
        if (col.CompareTag("NPC"))
        {
            npcBehind = null;
        }

    }
}
