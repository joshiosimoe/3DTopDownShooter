using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinnerTest : MonoBehaviour
{
    public GameObject mainGun;
    public GameObject Gun;
    private Transform parent;

    [SerializeField] private Vector3 rotation;
    public static bool spinGunActive;
    private float spinGunTimer = 3;
    private float spinGunDuration = 3;
    // Start is called before the first frame update
    void Start()
    {
        parent = this.transform;
        spinGunActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Z) && spinGunActive == false){
            SpawnGuns();
        }

        if(spinGunActive){
            mainGun.SetActive(false);
            transform.Rotate(rotation * Time.deltaTime);
            spinGunTimer -= Time.deltaTime;
            if(spinGunTimer < 0){
                spinGunTimer = spinGunDuration;
                spinGunActive = false;
                mainGun.SetActive(true);
            }
        }
    }

    public void SpawnGuns(){
        Vector3 gunPosition = new Vector3(transform.position.x, transform.position.y - 1.5f, transform.position.z);
        for(int i = 0; i < 8; i++){
            Gun.transform.rotation = Quaternion.Euler(0, transform.localRotation.eulerAngles.y + i*45, 0);
            GameObject temp = Instantiate(Gun, gunPosition, Gun.transform.rotation, parent) as GameObject;
            Destroy(temp, spinGunDuration);
        }
        spinGunActive = true;
    }
}
