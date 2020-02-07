using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunManager : MonoBehaviour
{
    public enum GunType { REVOLVER = 0, GRENADE_LAUNCHER = 1 }
    public GameObject revolverGun, revolverModel, grenadeLauncherGun, grenadeLauncherModel;
    public InputManager input;
    public Transform modelTransform;

    public void SwitchGun(GunType newGun) {
        GameObject gun, model;

        switch (newGun) {
            case GunType.GRENADE_LAUNCHER:
                gun = grenadeLauncherGun;
                model = grenadeLauncherModel;
                break;
            default:
                gun = revolverGun;
                model = revolverModel;
                break;
        }

        //Destroy(transform.GetChild(0));
        Instantiate(gun, transform);
        Instantiate(model, modelTransform);
        input.gun = gun.GetComponent<Gun>();
    }
}
