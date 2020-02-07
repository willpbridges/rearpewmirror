using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    public float despawnTime;

    private float timeAlive = 0f;

    // Update is called once per frame
    void Update()
    {
        if (timeAlive >= despawnTime) {
            Destroy(gameObject);
        }
        timeAlive += Time.deltaTime;
    }
}
