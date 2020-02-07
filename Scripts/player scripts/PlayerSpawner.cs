using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public int numberOfPlayers = 4;
    public GameObject playerPrefab;
    public TeamSpawnbox teamASpawnbox, teamBSpawnbox;
    public ScreenManager screenManager;
    public MaterialSetter materialSetter;

    private void OnEnable() {
        Camera[] cams = new Camera[numberOfPlayers];

        for (int ii = 1; ii <= numberOfPlayers; ++ii) {
            PlayerNumber playerNumber = (PlayerNumber)ii;

            GameObject instance;

            if (ii % 2 == 0) {
                instance = teamBSpawnbox.SpawnPlayer(playerPrefab);
            } else {
                instance = teamASpawnbox.SpawnPlayer(playerPrefab);
            }

            int playerLayer = LayerMask.NameToLayer("Player" + ii);
            instance.layer = LayerMask.NameToLayer("Player" + ii);
            Transform head = instance.transform.Find("Head");
            head.gameObject.layer = LayerMask.NameToLayer("Player" + ii);
            instance.GetComponent<InputManager>().playerNumber = playerNumber;
            materialSetter.SetMaterials(instance, playerNumber);

            int notThisPlayerMask = ~(1 << LayerMask.NameToLayer("Player" + ii));
            int noDecoyMask = ~(1 << LayerMask.NameToLayer("Decoy"));
            Camera forwardCam = head.Find("Forward Camera").gameObject.GetComponent<Camera>();
            forwardCam.cullingMask = notThisPlayerMask & noDecoyMask;
            cams[ii - 1] = forwardCam;
        }

        screenManager.SetCams(cams);
        screenManager.SetNumberOfPlayers(numberOfPlayers);
    }
}
