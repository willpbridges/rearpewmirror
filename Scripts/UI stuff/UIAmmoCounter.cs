using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIAmmoCounter : MonoBehaviour
{
    public float ammoFlashDelay;        // minimum time allowed between flashing the ammo counter

    private Text ammoCounter;
    private float timeSinceAmmoFlashed; // time since the ammo counter was last flashed

    // Start is called before the first frame update
    void Awake() {
        ammoCounter = GetComponent<Text>();
        ammoCounter.color = Color.white;
        timeSinceAmmoFlashed = 0f;
    }

    // Update is called once per frame
    void Update() {
        timeSinceAmmoFlashed += Time.deltaTime;
    }

    public void UpdateAmmo(int count) {
        ammoCounter.text = string.Format("Ammo: {0:00}", count);
    }

    public void ShowReloading() {
        ammoCounter.text = "Reloading...";
    }

    public void FlashAmmoCount() {
        if (timeSinceAmmoFlashed >= ammoFlashDelay) {
            timeSinceAmmoFlashed = 0f;
            StartCoroutine(FlashAmmoCountRoutine());
        }
    }

    IEnumerator FlashAmmoCountRoutine() {
        ammoCounter.color = Color.red;
        yield return new WaitForSeconds(0.4f);
        ammoCounter.color = Color.black;
    }
}
