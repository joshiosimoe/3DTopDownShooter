using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject player;

    // Camera position 
    private Vector3 offset = new Vector3(0, 25, 0);
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Camera follows player from above
        transform.position = player.transform.position + offset;
    }
}
