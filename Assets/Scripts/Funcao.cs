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
                        manager.coffeeProgress -= charUsing.coffee/4;
                        manager.artProgress -= charUsing.art / 3;
                        charUsing.stress++;
                        break;
                    case MachineType.Art:
                        manager.artProgress += charUsing.art;
                        manager.soundProgress -= charUsing.sound /4;
                        manager.coffeeProgress -= charUsing.coffee/3;
                        charUsing.stress++;
                        break;
                    case MachineType.Coffee:
                        manager.coffeeProgress += charUsing.coffee;
                        charUsing.stress++;
                        break;
                    case MachineType.Sound:
                        manager.soundProgress += charUsing.sound;
                        manager.coffeeProgress -= charUsing.coffee/4;
                        manager.writeProgress -= charUsing.write / 3;
                        charUsing.stress++;
                        break;
                    case MachineType.Writing:
                        manager.writeProgress += charUsing.write;
                        manager.coffeeProgress -= charUsing.coffee/4;
                        manager.codeProgress -= charUsing.prog / 3;
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
