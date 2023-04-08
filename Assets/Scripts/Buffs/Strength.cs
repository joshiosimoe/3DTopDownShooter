using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Strength : MonoBehaviour, ICollectableBuff
{
    //private float strengthCountdown;
    //private string strengthCDText = "";
    //private string strengthMultiplier = "";
    //private int strengthCount = 0;
    private bool hasStrength = false;
    public BulletController projectileStats;

    // Start is called before the first frame update
    void Start()
    {
        BuffsManager.strengthCountdown = 5;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Buff()
    {
        BuffsManager.strengthCount++;
        Debug.Log("Activated");
        if(hasStrength)
        {
            BuffsManager.strengthCountdown = 5;
        }
        else
        {
            BuffsManager.buffsList.Add("Strength");
            BuffsManager.timersList.Add(BuffsManager.strengthCountdown);
        }
        if(BuffsManager.strengthCount > 1)
        {
            BuffsManager.strengthMultiplier = " (x" + BuffsManager.strengthCount + ")";
        }
        else
        {
            BuffsManager.strengthMultiplier = "";
        }
        projectileStats.damageToGive *= 2;
        BuffsManager.hasStrength = true;
    }
    
}
