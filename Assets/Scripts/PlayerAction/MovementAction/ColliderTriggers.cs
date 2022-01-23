using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderTriggers : MonoBehaviour
{

    public CollisionController cc;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        cc.UpdateCollider(name, other.gameObject);
    }
    private void OnTriggerExit(Collider other) {
        cc.UpdateCollider(name, null);
    }
}
