using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopManager : MonoBehaviour
{
    public static int totalMoney;
    public TextMeshProUGUI moneyText;
    public TMP_Text shopMoneyUI;
    public GameObject playerInterface;
    public GameObject shopInterface;
    public bool shopActive;

    private float moneyTimer = 1.0f;
    private float moneyInterval = 1.0f;
    private int passiveGain = 5;


    public ShopItemSO[] shopItemsSO;
    public ShopTemplate[] shopPanels;
    public Button[] myPurchaseBtns;

    // Start is called before the first frame update
    void Start()
    {
        shopInterface.gameObject.SetActive(false);
        for(int i = 0; i < shopItemsSO.Length; i++){
            shopItemsSO[i].hasPurchased = false;
        }
        //LoadPanels();
    }

    // Update is called once per frame
    void Update()
    {
        //Open Shop
        if(Input.GetKeyDown(KeyCode.E)){
            if(!shopActive){
                shopActive = true;
            }
            else if(shopActive){
                shopActive = false;
            }
            activateShop();
        }
        
        //Passive Income
        moneyTimer -= Time.deltaTime;
        if(moneyTimer <= 0){
            moneyTimer = moneyInterval;
            totalMoney += passiveGain;
        }
        CheckPurchaseable();
        moneyText.text = "Money: $" + totalMoney;
        shopMoneyUI.text = "Money: $" + totalMoney;


    }

    void activateShop (){
        if(shopActive){
            playerInterface.gameObject.SetActive(false);
            shopInterface.gameObject.SetActive(true);
            LoadPanels();
        }
        else if (!shopActive){
            playerInterface.gameObject.SetActive(true);
            shopInterface.gameObject.SetActive(false);
        }
    }

    public void LoadPanels(){
        for(int i = 0; i < shopItemsSO.Length; i++){
            shopPanels[i].titleTxt.text = shopItemsSO[i].title;
            shopPanels[i].descriptionTxt.text = shopItemsSO[i].description;
            shopPanels[i].costTxt.text = "$" + shopItemsSO[i].baseCost.ToString();
            if(shopItemsSO[i].hasPurchased){
                shopPanels[i].unlockTxt.text = "Unlocked";
            }
            else{
                shopPanels[i].unlockTxt.text = "Unlock";
            }
        }
    }

    public void CheckPurchaseable(){
        for(int i = 0; i < shopItemsSO.Length; i++){
            if(totalMoney >= shopItemsSO[i].baseCost && shopItemsSO[i].hasPurchased == false){ //if I have enough money
                myPurchaseBtns[i].interactable = true;
            }
            else{
                myPurchaseBtns[i].interactable = false;
            }
        }
    }

    public void PurchaseItem(int btnNo){
        if(totalMoney >= shopItemsSO[btnNo].baseCost){
            totalMoney -= shopItemsSO[btnNo].baseCost;
        }
        shopItemsSO[btnNo].hasPurchased = true;
        LoadPanels();
    }

    public void UnlockDash(){
        PlayerController.dashUnlocked = true;
    }

    public void UnlockBomb(){
        SpawnManager.bombUnlocked = true;
    }

    public void UnlockRapidFire(){
        SpawnManager.rapidFireUnlocked = true;
    }

    public void UnlockExplosiveBullets(){
        GunController.explosiveBulletUnlocked = true;
    }

}
