using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedusaScript : MonoBehaviour
{
    public bool done = false;
    public float timeLeft = 15;
    public float maxDistance = 30;
    public float viewingAngle = 20;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach(GameObject g in GameObject.FindGameObjectsWithTag("Player"))
        {
            if(this != g && g.name != "Head")
            {
                Vector3 direction = g.transform.position - transform.position;
                Vector3 direction2 = transform.position - g.transform.position;
                float angle = Vector3.Angle(direction, transform.forward);
                float angle2 = Vector3.Angle(direction2, g.transform.forward);
                if(angle < viewingAngle && angle2 < viewingAngle && timeLeft > 0)
                {
                    RaycastHit hit;
                    if(Physics.Raycast(transform.position + transform.up, direction.normalized, out hit, maxDistance))
                    {
                        if(hit.collider.gameObject.tag == "Player")
                        {
                            g.GetComponent<PlayerController>().canMove = false;
                            g.GetComponent<PickupsManager>().frozenUI.SetActive(true);
                        }
                    }
                }
                else
                {
                    g.GetComponent<PlayerController>().canMove = true;
                    g.GetComponent<PickupsManager>().frozenUI.SetActive(false);
                }
            }
        }
        if(timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
        }
        else
        {
            done = true;
        }
    }
}
