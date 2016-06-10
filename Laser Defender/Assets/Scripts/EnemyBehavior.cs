using UnityEngine;
using System.Collections;

public class EnemyBehavior : MonoBehaviour
{
    public GameObject projectile;
    public float health;
    public float fireRate;
    
    private float speedProjectile;

    void Start() {
        health = 300f;
        speedProjectile = 6.0f;
        fireRate = 1.0f;
    }

    void Update()
    {
        float probability = Time.deltaTime * fireRate;
        if (Random.value < probability) {
            Fire();
        }

        
    }

    void OnTriggerEnter2D(Collider2D collider) {
        Laser laser = collider.gameObject.GetComponent<Laser>();
        if (laser != null) {
            laser.Hit();
            health -= laser.Damage;
            if (health <= 0) {
                Destroy(gameObject);
            }
        }
    }

    void Fire() {
        Vector3 laserStart = transform.position + new Vector3(0, -0.7f, 0);
        GameObject laser = Instantiate(projectile, laserStart, Quaternion.identity) as GameObject;
        
        laser.GetComponent<Rigidbody2D>().velocity = new Vector3(0, -speedProjectile);
    }
}
