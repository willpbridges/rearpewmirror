using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenManager : MonoBehaviour
{
    private int numberOfPlayers;
    private int currentNumberOfPlayers;
    private Camera[] cams;

    public void SetNumberOfPlayers(int numberOfPlayers) {
        this.numberOfPlayers = numberOfPlayers;
    }

    public void SetCams(Camera[] cams) {
        this.cams = cams;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentNumberOfPlayers != numberOfPlayers) {
            currentNumberOfPlayers = numberOfPlayers;

            foreach (Camera cam in cams) {
                cam.depth = -1;
            }

            if (currentNumberOfPlayers == 1) {
                Camera p1Cam = cams[0];

                p1Cam.rect = new Rect(Vector2.zero, Vector2.one);

                p1Cam.depth = 0;
            } else if (currentNumberOfPlayers == 2) {
                Camera p1Cam = cams[0];
                Camera p2Cam = cams[1];

                p1Cam.rect = new Rect(new Vector2(0f, 0.5f), new Vector2(1f, 0.5f));
                p2Cam.rect = new Rect(Vector2.zero, new Vector2(1f, 0.5f));

                p1Cam.depth = 0;
                p2Cam.depth = 0;
            } else if (currentNumberOfPlayers == 3) {
                Camera p1Cam = cams[0];
                Camera p2Cam = cams[1];
                Camera p3Cam = cams[2];

                p1Cam.rect = new Rect(new Vector2(0f, 0.5f), new Vector2(1f, 0.5f));
                p2Cam.rect = new Rect(new Vector2(0.5f, 0f), new Vector2(0.5f, 0.5f));
                p3Cam.rect = new Rect(Vector2.zero, new Vector2(0.5f, 0.5f));

                p1Cam.depth = 0;
                p2Cam.depth = 0;
                p3Cam.depth = 0;
            } else if (currentNumberOfPlayers == 4) {
                Camera p1Cam = cams[0];
                Camera p2Cam = cams[1];
                Camera p3Cam = cams[2];
                Camera p4Cam = cams[3];

                p1Cam.rect = new Rect(new Vector2(0f, 0.5f), new Vector2(0.5f, 0.5f));
                p2Cam.rect = new Rect(Vector2.zero, new Vector2(0.5f, 0.5f));
                p3Cam.rect = new Rect(new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f));
                p4Cam.rect = new Rect(new Vector2(0.5f, 0f), new Vector2(0.5f, 0.5f));

                p1Cam.depth = 0;
                p2Cam.depth = 0;
                p3Cam.depth = 0;
                p4Cam.depth = 0;
            } else {
                Debug.Log("something went wrong");
            }
        }
    }
}
