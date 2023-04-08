using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBullet : MonoBehaviour
{
    public int explosionDamage;

    private float radius = 10;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision other)
    {
        // Controls bullet collision with enemy
        if(other.gameObject.tag == "Enemy")
        {
            ExplosionDamage();
        }
    }

    private void ExplosionDamage(){
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

        foreach (Collider nearby in colliders)
        {
            Rigidbody nearbyRB = nearby.GetComponent<Rigidbody>();
            if(nearbyRB != null && nearbyRB.gameObject.tag == "Enemy")
            {
                nearbyRB.gameObject.GetComponent<EnemyHealthManager>().HurtEnemy(explosionDamage);
            }
        }
    }
}
