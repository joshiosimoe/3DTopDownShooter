using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    private float explosionForce = 5000;
    private float radius = 10;
    private int damage = 3;
    public GameObject bombExplosion;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other)
    {
        
        GameObject explosion = Instantiate(bombExplosion, transform.position, transform.rotation);
        knockBack();
        Destroy(gameObject);
    }

    void knockBack()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

        foreach (Collider nearby in colliders)
        {
            Rigidbody nearbyRB = nearby.GetComponent<Rigidbody>();
            if(nearbyRB != null && nearbyRB.gameObject.tag == "Enemy")
            {
                nearbyRB.AddExplosionForce(explosionForce, transform.position, radius);
                nearbyRB.gameObject.GetComponent<EnemyHealthManager>().HurtEnemy(damage);
            }
        }
    }
}
