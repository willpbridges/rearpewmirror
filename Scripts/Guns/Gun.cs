using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public bool isRaycastWeapon;    // does this weapon fire projectiles or use instant raycast hits? if raycast, projectile and muzzleVelocity are not used
    public GameObject projectile;   // projectile to be fired
    public float muzzleVelocity;    // initial velocity of the projectile
    public float rateOfFire;        // minimum time between shots, in seconds
    public int maxAmmo;             // number of times the gun may be fired before needing to reload
    public float reloadDuration;    // time it takes to reload the gun, in seconds
    public AudioClip[] pewSounds;
    public Animator gunAnimator;

    // pew pew sounds
    private AudioSource audioSource;

    // shooting/reloading
    private UIAmmoCounter ammoCounter;
    private float timeSinceLastShot = 0f;
    private int currentAmmo;
    private bool isReloading;
    private IEnumerator reloadRoutine;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        currentAmmo = maxAmmo;
        ammoCounter = transform.root.gameObject.GetComponentInChildren<UIAmmoCounter>();
        ammoCounter.UpdateAmmo(currentAmmo);
    }

    // Update is called once per frame
    void Update() {
        timeSinceLastShot += Time.deltaTime;
    }

    public void Shoot()
    {
        if (currentAmmo == 0) {
            ammoCounter.FlashAmmoCount();
            return;
        } else if (isReloading) {
            StopCoroutine(reloadRoutine);
            isReloading = false;
            ammoCounter.UpdateAmmo(currentAmmo);
        }

        if (rateOfFire > timeSinceLastShot) {
            return;
        }

        gunAnimator.SetTrigger("Shoot");
        audioSource.PlayOneShot(pewSounds[Random.Range(0, pewSounds.Length)]);

        timeSinceLastShot = 0f;
        currentAmmo--;
        ammoCounter.UpdateAmmo(currentAmmo);

        // ignore raycast hitting colliders in the same layer (i.e. bullets spawned by this player, this player itself)
        int layerMask = 1 << transform.root.gameObject.layer;
        layerMask = ~layerMask;

        // Look at where we're shooting
        Physics.Raycast(new Ray(transform.position, transform.forward), out RaycastHit hitInfo, Mathf.Infinity, layerMask);
        Debug.DrawRay(transform.position, transform.forward, Color.red);

        /*
        // Log where we're shooting
        if (hitInfo.point != Vector3.zero)
        {
            Debug.Log(hitInfo.point);
        }
        else
        {
            Debug.Log("Shooting into space");
        }
        */

        if (isRaycastWeapon)
        {
            // TODO: do stuff for raycast weapons
            if (hitInfo.point != Vector3.zero) {
                Debug.DrawLine(transform.position, hitInfo.point, Color.red, 0.2f);
            }
            else 
            {
                Debug.DrawRay(transform.position, transform.forward * 1000, Color.red, 0.2f);
            }
        }
        else
        {
            GameObject instantiatedProjectile = Instantiate(projectile, transform.position, transform.rotation);
            instantiatedProjectile.layer = transform.root.gameObject.layer;

            Rigidbody ipRB = instantiatedProjectile.GetComponent<Rigidbody>();
            ipRB.velocity = (transform.forward).normalized * muzzleVelocity;
            //ipRB.rotation = Quaternion.LookRotation(ipRB.velocity);
        }
    }

    public void RefillAmmoInstantly() {
        currentAmmo = maxAmmo;
        ammoCounter.UpdateAmmo(currentAmmo);
    }

    public void Reload() {
        if (!isReloading && currentAmmo < maxAmmo) {
            reloadRoutine = ReloadRoutine();
            StartCoroutine(reloadRoutine);
            gunAnimator.SetTrigger("Reload");
        }
    }

    IEnumerator ReloadRoutine()
    {
        isReloading = true;
        ammoCounter.ShowReloading();

        float reloadTime = 0f;
        while (reloadTime < reloadDuration)
        {
            reloadTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        currentAmmo = maxAmmo;
        isReloading = false;
        ammoCounter.UpdateAmmo(currentAmmo);
    }
}
