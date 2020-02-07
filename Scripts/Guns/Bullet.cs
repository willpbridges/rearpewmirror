using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
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

    // currently broken reflection code
    /*
    private void FixedUpdate() {
        Debug.Log(rb.velocity);
        if (Physics.SphereCast(transform.position, coll.radius, rb.velocity.normalized, out RaycastHit hitInfo, rb.velocity.magnitude, 1 << gameObject.layer)) {
            string hitTag = hitInfo.collider.gameObject.tag;
            if (hitTag == "Reflect") {
                rb.velocity = Vector3.ClampMagnitude(Vector3.Reflect(rb.velocity, hitInfo.normal) * Mathf.Infinity, rb.velocity.magnitude);
            }
        }

        //transform.position += rb.velocity;
        //rb.MovePosition(transform.position + rb.velocity);
    }
    */

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Reflect")
        {
            Destroy(gameObject);
        }
    }
}
