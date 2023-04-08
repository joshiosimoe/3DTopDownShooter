using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BuffsManager : MonoBehaviour
{
    public BulletController projectileStats;
    public TextMeshProUGUI buffsText;
    public static List<string> buffsList = new List<string>();
    public static List<float> timersList = new List<float>();

    public static float strengthCountdown = 5;
    private string strengthCDText = "";
    public static string strengthMultiplier = "";
    public static int strengthCount = 0;
    public static bool hasStrength = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(buffsList.Count > 0){
            var str = "Current Buffs\n";
            for(var i = 0; i < buffsList.Count; i++)
            {
                str += buffsList[i] + "\n" + (timersList[i] % 60).ToString("f2") + "\n";
            }
            buffsText.text = str;
            buffsText.enabled = true;
        }
        else
        {
            buffsText.enabled = false;
        }

        CheckStrength();
    }

    void CheckStrength() {
        if(hasStrength)
        {
            buffsText.enabled = true;
            strengthCountdown -= Time.deltaTime;
            var strengthIndex = 0;
            for(var i = 0; i < buffsList.Count; i++){
                if(buffsList[i] == "Strength")
                {
                    strengthIndex = i;
                    timersList[i] = strengthCountdown;
                }
            }
            strengthCDText = (strengthCountdown % 60).ToString("f2");
            if(strengthCountdown <= 0)
            {
                buffsText.enabled = false;
                hasStrength = false;
                strengthCountdown = 5;
                projectileStats.damageToGive = 1;
                strengthCount = 0;
                buffsList.RemoveAt(strengthIndex);
                timersList.RemoveAt(strengthIndex);
            }
        }
    }

    /*void AddBuff(String Type){
        
    }*/
}
