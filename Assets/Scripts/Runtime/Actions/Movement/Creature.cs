using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature : MonoBehaviour
{
    [SerializeField] private int health = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Damage(int delta) {
        health -= delta;
        if (health <= 0) {
            Die();
        }
    }

    protected virtual void Die() {
        GameObject.Destroy(gameObject);
    }
}
