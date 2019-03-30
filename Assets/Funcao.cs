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
                        manager.artProgress -= charUsing.art / 4;
                        charUsing.stress++;
                        break;
                    case MachineType.Art:
                        manager.artProgress += charUsing.art;
                        manager.soundProgress -= charUsing.sound / 4;
                        charUsing.stress++;
                        break;
                    case MachineType.Coffee:
                        manager.coffeeProgress += charUsing.coffee;
                        charUsing.stress++;
                        break;
                    case MachineType.Sound:
                        manager.soundProgress += charUsing.sound;
                        manager.writeProgress -= charUsing.write / 4;
                        charUsing.stress++;
                        break;
                    case MachineType.Writing:
                        manager.writeProgress += charUsing.write;
                        manager.codeProgress -= charUsing.prog / 4;
                        charUsing.stress++;
                        break;
                    case MachineType.Rest:
                        charUsing.stress--;
                        break;
                }
            }
        }

    }

    public void ChangeCharUsing(PlayerInfo c)
    {
        StopAllCoroutines();
        charUsing = c;
        if(c != null)
        {
            StartCoroutine(Tick());
        }
    }

    IEnumerator Tick()
    {
        yield return new WaitForSeconds(1f);
        UpdateValues();
        if(manager.gameStarted)
            StartCoroutine(Tick());
    }

}
