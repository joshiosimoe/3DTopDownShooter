using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{

    // Movement
    private float moveSpeed = 15.0f;
    private Rigidbody myRigidbody;
    private Vector3 moveInput;
    private Vector3 moveVelocity;

    // Camera
    private Camera mainCamera;

    // Gun
    public GunController theGun;
    private Vector3 bulletOffset = new Vector3(0, 0, 2);

    //Dash
    public float dashSpeed;
    private bool isDashing;
    private int dashCounter = 3;
    private float dashRechargeTime = 3;
    public TextMeshProUGUI dashCounterText;
    public TextMeshProUGUI dashRechargeText;
    public static bool dashUnlocked;

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
        mainCamera = FindObjectOfType<Camera>();
        dashCounterText.enabled = false;
        dashRechargeText.enabled = false;
        dashUnlocked = false;
    }

    // Update is called once per frame
    void Update()
    {

        // Controls Player movement
        Move();

        // Dash Controls
        Dash();

        // Receives position of mouse cursor 
        LookAtCursor();

        //Fires Gun
        FireGunOnClick();
        
    }

    void FixedUpdate () {
        myRigidbody.velocity = moveVelocity;
        if(isDashing)
            Dashing();
    }

    // Triggers Rapid Fire after Colliding


    void Move() {
        moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        moveVelocity = moveInput * moveSpeed;
    }

    void Dash() {
        if(dashUnlocked){
            dashCounterText.enabled = true;
            dashCounterText.text = "Dash Counter: " + dashCounter;
            if(Input.GetKeyDown(KeyCode.LeftShift) && dashCounter > 0)
            {
                dashCounter--;
                isDashing = true;
            }
            if(dashCounter < 3)
            {
                dashRechargeTime -= Time.deltaTime;
                dashRechargeText.text = "Recharge in " + (int)(dashRechargeTime + 1);
                dashRechargeText.enabled = true;
                
                if(dashRechargeTime<=0)
                {
                    dashCounter++;
                    dashRechargeTime = 3;
                }
            }
            else
            {
                dashRechargeText.enabled = false;
            }
        }
    }

    void LookAtCursor() {
        if(SpinnerTest.spinGunActive == false){
            Ray cameraRay = mainCamera.ScreenPointToRay(Input.mousePosition);
            Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
            float rayLength;

            if(groundPlane.Raycast(cameraRay, out rayLength)){
                Vector3 pointToLook = cameraRay.GetPoint(rayLength);
                //Debug.DrawLine(cameraRay.origin, pointToLook, Color.blue);
                transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));
            }
        }
    }

    void FireGunOnClick() {
        if(Input.GetMouseButtonDown(0))
            theGun.isFiring = true;

        if(Input.GetMouseButtonUp(0))
            theGun.isFiring = false;
    }

    

    // Dash Function
    private void Dashing()
    {
        if(isMoving(moveInput))
        {
            myRigidbody.AddForce(moveVelocity * dashSpeed, ForceMode.Impulse);
            isDashing = false;
        }
        else
        {
            myRigidbody.AddForce(transform.forward * moveSpeed * dashSpeed, ForceMode.Impulse);
            isDashing = false;
        }
    }

    // Checks if Player is moving
    private bool isMoving(Vector3 movement)
    {
        if(movement.x > 0 || movement.x < 0 || movement.y > 0 || movement.y < 0 || movement.z > 0 || movement.z < 0)
        {
            return true;
        }
        return false;
    }

    public void UnlockDash(){
        dashUnlocked = true;
    }

}