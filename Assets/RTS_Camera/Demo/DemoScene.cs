using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DemoScene : MonoBehaviour 
{
    public Button btn45;
    public Button btn90;

    private void Start()
    {
        Transform camT = Camera.main.transform;
        SetXRotation(camT, 45f);    
        SetZRotation(camT, 45f);    
    }

    private void SetXRotation(Transform t, float angle)
    {
        t.localEulerAngles = new Vector3(angle, t.localEulerAngles.y, t.localEulerAngles.z);
    }
    private void SetZRotation(Transform t, float angle)
    {
        t.localEulerAngles = new Vector3(t.localEulerAngles.x, angle, t.localEulerAngles.z);
    }
}
