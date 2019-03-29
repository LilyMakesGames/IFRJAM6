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

    void Start()
    {
        info = GetComponent<PlayerInfo>();
        rb = GetComponent<Rigidbody2D>();

        info.SetStatus(1, 1, 1, 1, 1);

    }

    void FixedUpdate()
    {
        switch (info.playerState)
        {
            case PlayerInfo.PlayerState.Idle:
                rb.velocity = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * speed;
                if(currentCol != null)
                {
                    if (Input.GetButtonDown("Action") && currentCol.CompareTag("Machine"))
                    {
                        if (currentCol.GetComponent<Funcao>().charUsing == null)
                        {
                            rb.velocity = Vector3.zero;
                            info.workingNow = currentCol.GetComponent<Funcao>();
                            currentCol.GetComponent<Funcao>().ChangeCharUsing(info);
                            info.playerState = PlayerInfo.PlayerState.Working;
                        }
                    }

                }
                break;
            case PlayerInfo.PlayerState.Working:
                if (Input.GetButtonDown(("Action")))
                {
                    info.workingNow.ChangeCharUsing(null);
                    info.playerState = PlayerInfo.PlayerState.Idle;
                }
                break;
        }
        if (Input.GetButtonDown("Bar"))
        {
            manager.panel.GetComponent<RectTransform>().position = new Vector3(manager.panel.GetComponent<RectTransform>().position.x * -1, manager.panel.GetComponent<RectTransform>().position.y);
        }


    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        currentCol = col.gameObject;
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        currentCol = null;
    }
}
