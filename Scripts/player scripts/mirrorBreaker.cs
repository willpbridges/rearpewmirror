using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mirrorBreaker : MonoBehaviour
{
    public GameObject mirrorFace;
    public bool broken;
    public float timeBroken;
    private float timeLeft;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(broken)
        {
            mirrorFace.SetActive(false);
            timeLeft = timeBroken;
        }
        if(!broken)
        {
            if (timeLeft <= 0)
            {
                mirrorFace.SetActive(true);
            }
            else
            {
                timeLeft -= Time.deltaTime;
            }
        }

    }
}
