using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PickupsManager : MonoBehaviour
{
    private string currItem = "";
    public GameObject boomboxPrefab;
    public GameObject frozenUI;
    public GameObject medusaIcon;
    public GameObject decoyIcon;
    public GameObject boomboxIcon;
    private bool medusaActive;
    private bool decoyActive;
    public GameObject decoyPrefab;
    public GunManager gunManager;

    public void UsePickup() {
        if (currItem == "Decoy") {
            currItem = "";
            decoyIcon.SetActive(false);
            DecoyManager decoy = gameObject.AddComponent<DecoyManager>() as DecoyManager;
            decoyActive = true;
        }
        if (currItem == "Medusa") {
            currItem = "";
            medusaIcon.SetActive(false);
            MedusaScript powerup = gameObject.AddComponent<MedusaScript>() as MedusaScript;
            medusaActive = true;
        }
        if (currItem == "Boombox") {
            currItem = "";
            boomboxIcon.SetActive(false);
            activateBoombox();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(medusaActive)
        {
            if(GetComponent<MedusaScript>().done)
            {
                MedusaScript powerup = GetComponent<MedusaScript>();
                Destroy(powerup);
                medusaActive = false;
                frozenUI.SetActive(false);
            }

        }
        if(decoyActive)
        {
            if(GetComponent<DecoyManager>().done)
            {
                DecoyManager decoy = GetComponent<DecoyManager>();
                Destroy(decoy);
                decoyActive = false;
            }
        }
    }
    void activateBoombox()
    {
        Object.Instantiate(boomboxPrefab, this.transform.position + transform.forward, Quaternion.identity);
    }

    private void OnTriggerEnter(Collider other)
    {
       if(other.tag == "Medusa" && currItem == "")
        {
            currItem = "Medusa";
            medusaIcon.SetActive(true);
            other.GetComponent<pickupSpawner>().deactivate();
        }
       if(other.tag == "Decoy" && currItem == "")
        {
            currItem = "Decoy";
            decoyIcon.SetActive(true);
            other.GetComponent<pickupSpawner>().deactivate();
        }
        if (other.tag == "Boombox" && currItem == "")
        {
            currItem = "Boombox";
            boomboxIcon.SetActive(true);
            other.GetComponent<pickupSpawner>().deactivate();
        }
        if (other.tag == "GrenadeLauncher") {
            gunManager.SwitchGun(GunManager.GunType.GRENADE_LAUNCHER);
        }

    }
}
