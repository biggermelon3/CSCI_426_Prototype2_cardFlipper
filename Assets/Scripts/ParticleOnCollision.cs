using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleOnCollision : MonoBehaviour
{
    public ParticleSystem myParticle;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("hit");
        Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.CompareTag("StrikeBall"))
        {
            ParticleSystem newParticle = Instantiate(myParticle, this.transform.parent.GetChild(2).transform.position, Quaternion.identity, transform);
            newParticle.transform.localScale = new Vector3(0, 0, 0);
        }
    }
}
