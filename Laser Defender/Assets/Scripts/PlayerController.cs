using System;
using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public GameObject projectile;
    public float speed;
    public float speedProjectile;
    public float fireRate;
    public float health;
    
    private float xMin;
    private float xMax;
    private float yMin;
    private float yMax;
    private float paddingHorizontal;
    private float paddingVertical;
    private float fireNext;


    void Awake() {
        speed = 5.0f;
        speedProjectile = 8.0f;
        paddingHorizontal = 0.5f;
        paddingVertical = 0.4f;
        fireRate = 0.2f;
        health = 1000f;
    }

	void Start ()
	{
        CalculateWorldEdges();
    }
	
	void Update () {
        
        CheckInput();

	    RestrictPosition();
	}

    void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log("Player was hit by laser.");
        Laser laser = collider.gameObject.GetComponent<Laser>();
        if (laser != null)
        {
            laser.Hit();
            health -= laser.Damage;
            if (health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    void CalculateWorldEdges()
    {
        float distance = transform.position.z - Camera.main.transform.position.z;
        Vector3 topEdge = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, distance));
        Vector3 bottomEdge = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance));
        Vector3 leftEdge = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance));
        Vector3 rightEdge = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distance));

        xMin = leftEdge.x + paddingHorizontal;
        xMax = rightEdge.x - paddingHorizontal;
        yMin = bottomEdge.y + paddingVertical;
        yMax = topEdge.y - paddingVertical;
    }

    void CheckInput() {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) {
            transform.position += Vector3.up * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) {
            transform.position += Vector3.down * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) {
            transform.position += Vector3.left * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) {
            transform.position += Vector3.right * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0)) {
            if (Time.time > fireNext) {
                fireNext = Time.time + fireRate;
                Fire();
            }
        }
    }

    void Fire() {
        Vector3 laserStart = transform.position + new Vector3(0, 0.6f, 0);
        GameObject laser = Instantiate(projectile, laserStart, Quaternion.identity) as GameObject;
        laser.GetComponent<Rigidbody2D>().velocity = new Vector3(0, speedProjectile);
    }

    void RestrictPosition() {
        float newX = Mathf.Clamp(transform.position.x, xMin, xMax);
        float newY = Mathf.Clamp(transform.position.y, yMin, yMax);
        transform.position = new Vector3(newX, newY, transform.position.z);
    }
    
}
