using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamSpawnbox : MonoBehaviour
{
    public Color gizmosColor = new Color(0.5f, 0.5f, 0.5f, 0.5f);
    public Vector3 boundsSize = new Vector3(5f, 1f, 5f);

    private Vector3 playerSize = Vector3.zero;
    private List<Vector3> spawnLocations = new List<Vector3>();

    public GameObject SpawnPlayer(GameObject playerPrefab) {
        if (playerSize == Vector3.zero) {
            playerSize = playerPrefab.GetComponent<Collider>().bounds.size;
        }

        Bounds spawnBounds = new Bounds(transform.position, boundsSize);
        Vector3 range = spawnBounds.extents;

        Vector3 randomCoordinate;
        bool validLocationFound = false;
        do {
            Vector3 randomRange = new Vector3(Random.Range(-range.x, range.x),
                                              0f,
                                              Random.Range(-range.z, range.z));
            randomCoordinate = spawnBounds.center + randomRange;

            Bounds randomBounds = new Bounds(randomCoordinate, playerSize);

            if (spawnLocations.Count == 0) {
                break;
            }

            foreach (Vector3 pos in spawnLocations) {
                Bounds posBounds = new Bounds(pos, playerSize * 1.1f);
                if (!randomBounds.Intersects(posBounds)) {
                    validLocationFound = true;
                    break;
                }
            }
        } while (!validLocationFound);

        spawnLocations.Add(randomCoordinate);

        Quaternion rotation = new Quaternion();
        rotation.SetLookRotation(transform.forward);

        return Instantiate(playerPrefab, randomCoordinate, rotation);
    }
}
