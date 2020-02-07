using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickupSpawner : MonoBehaviour
{
    public enum powerupType { Medusa, Decoy, Boombox, GrenadeLauncher };
    [SerializeField]
    public powerupType thisType;
    public GameObject medusaModel;
    public GameObject decoyModel;
    public GameObject boomboxModel;
    public GameObject grenadeLauncherModel;
    private GameObject pickup;
    public float respawnTime;
    private float timeRem;
    private bool dead;
    private void Awake()
    {
        if(thisType == powerupType.Medusa)
        {
            this.tag = "Medusa";
        }
        if(thisType == powerupType.Decoy)
        {
            this.tag = "Decoy";
        }
        if(thisType == powerupType.Boombox)
        {
            this.tag = "Boombox";
        }
        if(thisType == powerupType.GrenadeLauncher) {
            this.tag = "GrenadeLauncher";
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        if(thisType == powerupType.Medusa)
        {
            pickup = Object.Instantiate(medusaModel, this.transform);
        }
        if (thisType == powerupType.Decoy)
        {
            pickup = Object.Instantiate(decoyModel, this.transform);
        }
        if (thisType == powerupType.Boombox)
        {
            pickup = Object.Instantiate(boomboxModel, this.transform.position, Quaternion.Euler(new Vector3(-22.5f, -3.12f, -36f)));
        }
        if (thisType == powerupType.GrenadeLauncher) {
            pickup = Object.Instantiate(grenadeLauncherModel, this.transform);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if(dead && timeRem > 0)
        {
            timeRem -= Time.deltaTime;
        }
        if(dead && timeRem <= 0)
        {
            reactivate();
        }
    }
    public void deactivate()
    {
        dead = true;
        timeRem = respawnTime;
        this.GetComponent<BoxCollider>().enabled = false;
        pickup.SetActive(false);
    }
    void reactivate()
    {
        dead = false;
        this.GetComponent<BoxCollider>().enabled = true;
        pickup.SetActive(true);
    }
}
