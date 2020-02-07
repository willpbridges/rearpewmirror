using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialSetter : MonoBehaviour
{
    public Material teamA;
    public Material teamB;

    public Material[] mirrorMaterials;
    public RenderTexture[] mirrorTextures;

    public void SetMaterials(GameObject player, PlayerNumber playerNumber) {
        int pNum = (int)playerNumber;
        Material teamMaterial = pNum % 2 == 0 ? teamB : teamA;

        MeshRenderer[] meshes = player.GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer m in meshes) {
            if (m.gameObject.name.Equals("MirrorFace")) {
                m.material = mirrorMaterials[pNum - 1];
            } else if (!m.gameObject.name.Contains("Solid")) {
                m.material = teamMaterial;
            }
        }

        Camera[] cams = player.GetComponentsInChildren<Camera>();
        foreach (Camera c in cams) {
            if (c.gameObject.name.Equals("Mirror Camera")) {
                c.targetTexture = mirrorTextures[pNum - 1];
            }
        }
    }
}
