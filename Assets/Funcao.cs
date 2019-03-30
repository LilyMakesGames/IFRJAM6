using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Funcao : MonoBehaviour
{
    public GameManager manager;

    bool isUsed;
    public PlayerInfo charUsing = null;

    public enum MachineType
    {
        Code,
        Art,
        Writing,
        Coffee,
        Sound,
        Rest
    }

    public MachineType machineType;

    void Start()
    {

    }


    void Update()
    {

    }

    void UpdateValues()
    {
        if (manager.gameStarted)
        {
            if (charUsing != null)
            {
                switch (machineType)
                {
                    case MachineType.Code:
                        manager.codeProgress += charUsing.prog;
                        break;
                    case MachineType.Art:
                        manager.artProgress += charUsing.art;
                        break;
                    case MachineType.Coffee:
                        manager.coffeeProgress += charUsing.coffee;
                        break;
                    case MachineType.Sound:
                        manager.soundProgress += charUsing.sound;
                        break;
                    case MachineType.Writing:
                        manager.writingProgress += charUsing.write;
                        break;
                    case MachineType.Rest:
                        break;
                }
            }
        }

    }

    public void ChangeCharUsing(PlayerInfo c)
    {
        charUsing = c;
        if(c == null)
        {
            StopCoroutine(Tick());
        }
        else
        {
            StartCoroutine(Tick());
        }
    }

    IEnumerator Tick()
    {
        yield return new WaitForSeconds(0.4f);
        UpdateValues();
        StartCoroutine(Tick());
    }

}
