using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class FPSCamera : MonoBehaviour
{
    public enum RotationAxes { XandY = 0, X = 1, Y = 2 }
    public RotationAxes axes = RotationAxes.XandY;
    public float sensitivityX = 15F;
    public float sensitivityY = 15F;

    public float minimumX = -360F;
    public float maximumX = 360F;

    public float minimumY = -60F;
    public float maximumY = 60F;

    float rotationY = 0F;

    public void Look(float lookX, float lookY) {
        if (axes == RotationAxes.XandY) {
            float rotationX = transform.localEulerAngles.y + lookX * sensitivityX;

            rotationY += lookY * sensitivityY;
            rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

            transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
        } else if (axes == RotationAxes.X) {
            transform.Rotate(0, lookX * sensitivityX, 0);
        } else {
            rotationY += lookY * sensitivityY;
            rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

            transform.localEulerAngles = new Vector3(-rotationY, transform.localEulerAngles.y, 0);
        }
    }
}

[CustomEditor(typeof(FPSCamera))]
public class FPSCameraEditor : Editor 
{
    public override void OnInspectorGUI() {
        var fPSCamera = target as FPSCamera;

        GUI.enabled = false;
        EditorGUILayout.ObjectField("Script", MonoScript.FromMonoBehaviour((FPSCamera)target), typeof(FPSCamera), false);
        GUI.enabled = true;

        fPSCamera.axes = (FPSCamera.RotationAxes)EditorGUILayout.EnumPopup("Axes", fPSCamera.axes);

        switch(fPSCamera.axes) {
            case (FPSCamera.RotationAxes.XandY):
                fPSCamera.sensitivityX = EditorGUILayout.FloatField("SensitivityX", fPSCamera.sensitivityX);
                fPSCamera.sensitivityY = EditorGUILayout.FloatField("SensitivityY", fPSCamera.sensitivityY);
                fPSCamera.minimumX = EditorGUILayout.FloatField("MinimumX", fPSCamera.minimumX);
                fPSCamera.maximumX = EditorGUILayout.FloatField("MaximumX", fPSCamera.maximumX);
                fPSCamera.minimumY = EditorGUILayout.FloatField("MinimumY", fPSCamera.minimumY);
                fPSCamera.maximumY = EditorGUILayout.FloatField("MaximumY", fPSCamera.maximumY);
                break;
            case (FPSCamera.RotationAxes.X):
                fPSCamera.sensitivityX = EditorGUILayout.FloatField("SensitivityX", fPSCamera.sensitivityX);
                fPSCamera.minimumX = EditorGUILayout.FloatField("MinimumX", fPSCamera.minimumX);
                fPSCamera.maximumX = EditorGUILayout.FloatField("MaximumX", fPSCamera.maximumX);
                break;
            case (FPSCamera.RotationAxes.Y):
                fPSCamera.sensitivityY = EditorGUILayout.FloatField("SensitivityY", fPSCamera.sensitivityY);
                fPSCamera.minimumY = EditorGUILayout.FloatField("MinimumY", fPSCamera.minimumY);
                fPSCamera.maximumY = EditorGUILayout.FloatField("MaximumY", fPSCamera.maximumY);
                break;
        }
    }
}
