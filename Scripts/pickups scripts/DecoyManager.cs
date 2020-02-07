using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecoyManager : MonoBehaviour
{
    private GameObject decoyPrefab;
    public bool done;
    private float timeLeft = 15;
    private GameObject livingDecoy;
    // Start is called before the first frame update
    void Start()
    {
        decoyPrefab = GetComponent<PickupsManager>().decoyPrefab;
        Renderer decoyRend = decoyPrefab.GetComponent<Renderer>();
        decoyRend.material = GetComponent<Renderer>().material;
        livingDecoy = Instantiate(decoyPrefab, this.transform.position - transform.forward, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        if(timeLeft <= 0)
        {
            Destroy(livingDecoy);
            done = true;
        }
        else
        {
            timeLeft -= Time.deltaTime;
        }
    }
}
