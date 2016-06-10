using UnityEngine;
using System.Collections;

public class Laser : MonoBehaviour {
    private float damage = 100f;

    public float Damage
    {
        get { return damage; }
        //set { damage = value; }
    }

    public void Hit() {
        Destroy(gameObject);
    }

    void OnBecameInvisible() {
        Destroy(gameObject);
    }
}
