using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestroy : MonoBehaviour
{
    ParticleSystem particleCreated;
    public float lifeTime;
    // Start is called before the first frame update
    void Start()
    {
        lifeTime = 5f;
    }

    // Update is called once per frame
    void Update()
    {
        lifeTime -= Time.deltaTime;
        if (lifeTime < 0f)
        {
            Destroy(this.gameObject);
        }
    }
}
