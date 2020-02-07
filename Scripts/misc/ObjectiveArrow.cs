using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveArrow : MonoBehaviour
{
    float speed = 3f;
    float height = 1f;
    Vector3 pos;
    void Start()
    {
        pos = transform.position;
    }

    void Update()
    {
        transform.Rotate(0, 1, 0);
        float newY = Mathf.Sin(Time.time * speed) + pos.y;
        transform.position = new Vector3(pos.x, newY, pos.z) * height;
    }
}
