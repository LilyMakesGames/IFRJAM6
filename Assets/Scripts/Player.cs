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

        info.SetStatus(1, 1, 1, 1, 1,100);
		
		animator = GetComponent<Animator>();
		spriteRenderer = GetComponent<SpriteRenderer>();

    }
	
	void Update()
	{
		
		//int h = Input.GetAxisRaw("Horizontal");
		//int v = Input.GetAxisRaw("Vertical");
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
							animator.SetBool("isWorking", true);
                            if (currentCol.GetComponent<Funcao>().charUsing == null && npcBehind == null)
                            {
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
                            carried = npcBehind;
                            info.playerState = PlayerInfo.PlayerState.Carrying;
							animator.SetBool("isLoading", true);
                        }
                        if (currentCol != null && Input.GetButton("Action"))
                        {
                            Debug.Log("RemoveNPC");
                            currentCol.GetComponent<Funcao>().ChangeCharUsing(null);
                        }
                    }

                    break;
                case PlayerInfo.PlayerState.Working:
                    if (Input.GetButtonDown("Action"))
                    {
                        Debug.Log("Leave Work");
						animator.SetBool("isWorking", false);
                        info.workingNow.ChangeCharUsing(null);
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
                        if (currentCol != null && currentCol.GetComponent<Funcao>().charUsing == null)
                        {
                            npcBehind = null;
                            carried.transform.position = new Vector3(currentCol.transform.position.x, currentCol.transform.position.y + 0.75f);
                            currentCol.GetComponent<Funcao>().ChangeCharUsing(carried.GetComponent<PlayerInfo>());
                            carried = null;
                            info.playerState = PlayerInfo.PlayerState.Idle;
                        }
                        else
                        {
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
