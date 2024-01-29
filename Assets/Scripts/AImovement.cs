using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class AImovement : MonoBehaviour
{
    Rigidbody rb;
    Vector3 aiposition;
    Collider[] colliders;
    Collider[] filtercollider;
    Collider[] orderedByProximity;
    Collider nearest;
    public float forceToAI;
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        
    }

    // Update is called once per frame
    void Update()
    {
        aiposition = this.transform.position;
        colliders = Physics.OverlapSphere(aiposition, 100f);
        filtercollider = new Collider[colliders.Length - 2];
        orderedByProximity = new Collider[colliders.Length - 2];
        for(int i = 0; i<colliders.Length; i++)
        {
            int j = 0;
            if (colliders[i].gameObject.name != "r" && colliders[i].gameObject.name != "ai")   
            {
                filtercollider[j] = colliders[i];
                j++;
            }
            i++;
        }
        orderedByProximity = filtercollider.OrderBy(c => (aiposition - c.transform.position).sqrMagnitude).ToArray();
        if (orderedByProximity.Length > 2)
        {
            nearest = orderedByProximity[2];
            Vector3 forcedirection = new Vector3(nearest.transform.position.x - aiposition.x, 0, nearest.transform.position.z - aiposition.z);
            rb.AddForce(forcedirection.normalized * forceToAI);
        }

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("ball"))
        {
            Destroy(collision.gameObject);
        }
    }
}
