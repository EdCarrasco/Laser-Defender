using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float speed = 2f;
    public float width = 5f;
    public float height = 3f;
    
    private bool moveRight = true;
    private float xMin;
    private float xMax;

    public void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(width, height));
    }
    
	void Start ()
	{
	    foreach (Transform child in transform)
	    {
            GameObject enemy = Instantiate(enemyPrefab, child.transform.position, Quaternion.identity) as GameObject;
            enemy.transform.parent = child;
        }

	    float distance = transform.position.z - Camera.main.transform.position.z;
	    Vector3 leftBoundary = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance));
        Vector3 rightBoundary = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distance));
	    xMin = leftBoundary.x;
	    xMax = rightBoundary.x;
	}
    
	void Update () {
        if (moveRight)
        {
            //transform.position += new Vector3(speed*Time.deltaTime, 0);
            transform.position += Vector3.right * speed * Time.deltaTime;
        }
        else
        {
            //transform.position += new Vector3(-speed*Time.deltaTime, 0);
            transform.position += Vector3.left * speed * Time.deltaTime;
        }

        float leftEdgeOfFormation = transform.position.x - (width * 0.5f);
        float rightEdgeOfFormation = transform.position.x + (width * 0.5f);
        if (leftEdgeOfFormation < xMin)
        {
            moveRight = true;
        }
        else if (rightEdgeOfFormation > xMax)
        {
            moveRight = false;
        }
    }
}
