using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InputManager : MonoBehaviour
{
    public PlayerNumber playerNumber;

    // script references
    public PlayerController playerController;
    public Gun gun;
    public FPSCamera bodyCam, headCam;
    public HeadBob headBob;
    public float sprintCooldown;
    public Transform gunTransform;
    public PickupsManager pickupsManager;

    private float speed;
    private float nextShoot;

    // input strings
    private string horizontal, vertical, lookX, lookY, jump, shoot, reload, sprint, pickup, cancel;

    // Start is called before the first frame update
    void Start()
    {
        nextShoot = 0;
        speed = playerController.speed;
        int pNum = (int)playerNumber;

        horizontal = "Horizontal_P" + pNum;
        vertical = "Vertical_P" + pNum;
        lookX = "LookX_P" + pNum;
        lookY = "LookY_P" + pNum;
        jump = "Jump_P" + pNum;
        shoot = "Shoot_P" + pNum;
        reload = "Reload_P" + pNum;
        sprint = "Sprint_P" + pNum;
        pickup = "Pickup_P" + pNum;
        cancel = "Cancel_P" + pNum;
    }

    // Update is called once per frame
    void Update()
    {
        float horiz = Input.GetAxis(horizontal);
        float vert = Input.GetAxis(vertical);
        playerController.Move(horiz, vert);
        headBob.Bob(horiz, vert);

        float lX = Input.GetAxis(lookX);
        float lY = Input.GetAxis(lookY);
        bodyCam.Look(lX, lY);
        headCam.Look(lX, lY);

        if (Objective.gameOver) {
            // Restart game
            if (Input.GetButtonDown(jump)) {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }

            // Quit game
            if (Input.GetButtonDown(cancel)) {
                #if UNITY_EDITOR
                    UnityEditor.EditorApplication.isPlaying = false;
                #else
                    Application.Quit();
                #endif
            }
        }

        if (Input.GetButtonDown(jump)) {
            playerController.Jump();
        }

        if (Input.GetButtonDown(pickup)) {
            pickupsManager.UsePickup();
        }

        if (Input.GetButtonDown(reload)) {
            gun.Reload();
        } else if (Input.GetButtonDown(sprint)) {
            RotateGunDown();
            playerController.speed = speed * 1.5f;
        } else if (Input.GetButton(sprint)) {
            playerController.speed = speed * 1.5f;
        } else if (Input.GetButtonUp(sprint)) {
            RotateGunUp();
            playerController.speed = speed;
            nextShoot = Time.time + sprintCooldown;
        } else if ((Input.GetButton(shoot) || Input.GetAxis(shoot) > 0f) && Time.time > nextShoot) {
            gun.Shoot();
        }
    }

    private void RotateGunDown() {
        // TODO: activate gun sprinting animation
    }

    private void RotateGunUp() {
        // TODO: deactivate gun sprinting animation
    }
}
