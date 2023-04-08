using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtPlayer : MonoBehaviour
{
    public List<GameObject> colliderList = new List<GameObject>();
    public int damageToGive;
    private float timeBetweenDamage = 1;
    private float timer = 0;
    private bool isPlayerContact;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        isPlayerContact = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(isPlayerContact)
        {
            timer -= Time.deltaTime;
            if(timer <= 0)
            {
                /*for(int i = 0; i < colliderList.Count; i++)
                {
                    if(colliderList[i].tag == "Player")
                    {*/
                        player.GetComponent<PlayerHealthManager>().HurtPlayer(damageToGive);
                    /*}
                }*/
                timer = timeBetweenDamage;
            }
        }
    }

    // Trigger to inflict damage to player
    public void OnTriggerEnter(Collider other){
        if(!colliderList.Contains(other.gameObject))
        {   
            if(other.gameObject.tag == "Player")
            {
                player = other.gameObject;
                colliderList.Add(other.gameObject);
                isPlayerContact = true;
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if(colliderList.Contains(other.gameObject))
        {
            colliderList.Remove(other.gameObject);
            isPlayerContact = false;
        }
    }
}
