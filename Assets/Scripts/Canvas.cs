using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canvas : MonoBehaviour
{
	Animator animator;
	bool isOpen = true;
	bool isAlertOn = false;
	
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKeyDown("tab"))
		{
			if (isOpen)
			{
				closePanel();
			}
			else
			{
				openPanel();
			}
		}
		if (Input.GetKeyDown("q"))
		{
			activeAlert();
		}
    }
	
	void closePanel()
	{
		animator.SetTrigger("closeTrigger");
		isOpen = false;
	}
	
	void openPanel()
	{
		animator.SetTrigger("openTrigger");
		animator.SetBool("isAlertOn", false);
		isOpen = true;
	}
	
	void activeAlert()
	{
		if (!isOpen)
		{
			animator.SetBool("isAlertOn", true);
		}
	}
	
	
}
