using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerBuffs : MonoBehaviour
{
    private Camera mainCamera;

    public BulletController projectileStats;

    public GunController theGun;

    //Bomb
    public GameObject bomb;
    public GameObject bombPickup;
    public TextMeshProUGUI bombCDText;
    public TextMeshProUGUI bombCounterText;
    public int bombCount = 0;
    public float bombCountdown = 0.5f;
    public bool isBombReady = true;
    private string bombTimerText;

    //Buffs
    public TextMeshProUGUI buffsText;
    private List<string> buffsList = new List<string>();
    private List<float> timersList = new List<float>();
    
    // Rapid Fire Buff
    private float rapidFireCountdown = 5;
    private string RFCDText = "";
    private string RPMultiplier = "";
    private int rapidFireCount = 0;
    private bool hasRapidFire = false;
    private float tempRapidFireRate;

    // Strength Buff
    private float strengthCountdown = 5;
    private string strengthCDText = "";
    private string strengthMultiplier = "";
    private int strengthCount = 0;
    public static bool hasStrength = false;

    // Health Buff

    // SpinGun Buff
    /*public SpinnerGunController spinGun;
    public GameObject Gun;
    private Transform parent;
    [SerializeField] private Vector3 rotation;
    private bool spinGunActive;
    private float spinGunTimer = 3;
    private float spinGunDuration = 3;*/

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = FindObjectOfType<Camera>();
        buffsText.enabled = false;
        bombCDText.enabled = false;
        bombCounterText.enabled = false;
        tempRapidFireRate = theGun.timeBetweenShots;
        //parent = this.transform;
        //spinGunActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        //RapidFireBuff
        CheckRapidFire();
        
        //StrengthBuff
        CheckStrength();

        //Controls Bomb Pickup
        CheckBomb();

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
    }

    private void OnTriggerEnter(Collider other) {

        //Rapid Fire
        if (other.CompareTag("RapidFireBuff")){
            Destroy(other.gameObject);
            rapidFireCount++;
            if(hasRapidFire)
            {
                rapidFireCountdown = 5;
            }
            else
            {
                tempRapidFireRate = theGun.timeBetweenShots;
                buffsList.Add("Rapid Fire");
                timersList.Add(rapidFireCountdown);
            }
            if(rapidFireCount > 1)
            {
                RPMultiplier = " (x" + rapidFireCount + ")";
            } 
            else 
            {
                RPMultiplier = "";
            }
            theGun.timeBetweenShots *= 0.5f;
            hasRapidFire = true;
        }

        //Strength
        if(other.CompareTag("StrengthBuff")){
            Destroy(other.gameObject);
            strengthCount++;
            if(hasStrength)
            {
                strengthCountdown = 5;
            }
            else
            {
                buffsList.Add("Strength");
                timersList.Add(strengthCountdown);
            }
            if(strengthCount > 1)
            {
                strengthMultiplier = " (x" + strengthCount + ")";
            }
            else
            {
                strengthMultiplier = "";
            }
            projectileStats.damageToGive *= 2;
            hasStrength = true;
        }

        //if(other.CompareTag("StrengthBuff")){
            /*ICollectableBuff collectableBuff = other.GetComponent<ICollectableBuff>();
            if(collectableBuff != null) {
                collectableBuff.Buff();
            }
            Destroy(other.gameObject);*/
        //}

        //Pick Up Bomb
        if(other.CompareTag("BombPickup"))
        {
            Destroy(other.gameObject);
            bombCount++;
            isBombReady = true;
        }
    }

    void CheckRapidFire() {
        //buffsText.text = "Current Buffs:\nRapid Fire" + RPMultiplier + "\n" + RFCDText;
        if(hasRapidFire)
        {
            //buffsText.enabled = true;
            rapidFireCountdown -= Time.deltaTime;
            var rapidFireIndex = 0;
            for(var i = 0; i < buffsList.Count; i++){
                if(buffsList[i] == "Rapid Fire")
                {
                    rapidFireIndex = i;
                    timersList[i] = rapidFireCountdown;
                }
            }

            RFCDText = (rapidFireCountdown % 60).ToString("f2");
            if(rapidFireCountdown <= 0)
            {
                //buffsText.enabled = false;
                hasRapidFire = false;
                rapidFireCountdown = 5;
                theGun.timeBetweenShots = tempRapidFireRate;
                rapidFireCount = 0;
                buffsList.RemoveAt(rapidFireIndex);
                timersList.RemoveAt(rapidFireIndex);
            }
        }
    }

    void CheckStrength() {
        //buffsText.text = "Current Buffs:\nStrength" + strengthMultiplier + "\n" + strengthCDText;
        if(hasStrength)
        {
            //buffsText.enabled = true;
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
                //buffsText.enabled = false;
                hasStrength = false;
                strengthCountdown = 5;
                projectileStats.damageToGive = 1;
                //strengthCount = 0;
                buffsList.RemoveAt(strengthIndex);
                timersList.RemoveAt(strengthIndex);
            }
        }
    }

    void CheckBomb() {
        if(SpawnManager.bombUnlocked){
            bombCounterText.enabled = true;
            bombCDText.text = "Bomb Cooldown:\n" + bombTimerText;
            bombCounterText.text = "Bomb Counter: " + bombCount;
            if(isBombReady == false)
            {
                bombCountdown -= Time.deltaTime;
                bombTimerText = (bombCountdown % 60).ToString("f2");
                bombCDText.enabled = true;
                if(bombCountdown <= 0)
                {
                    isBombReady = true;
                    bombCountdown = 1;
                    bombCDText.enabled = false;
                }
            }
            if(Input.GetMouseButtonDown(1) && isBombReady && bombCount > 0)
            {
                SpawnBomb();
                bombCount--;
                isBombReady = false;
            }
        }
    }

    private void SpawnBomb()
    {
        Ray cameraRay = mainCamera.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayLength;

        if(groundPlane.Raycast(cameraRay, out rayLength)){
            Vector3 pointToLook = cameraRay.GetPoint(rayLength);
            //Debug.DrawLine(cameraRay.origin, pointToLook, Color.blue);
            Instantiate(bomb, new Vector3(pointToLook.x, transform.position.y + 5, pointToLook.z), bomb.transform.rotation);
        }
    }
}
