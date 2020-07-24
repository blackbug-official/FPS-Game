using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class WeaponZoom : MonoBehaviour
{
    [SerializeField] Camera fpsCamera;
    [SerializeField] float zoomedOutFOV = 60f;
    [SerializeField] float zoomedInFOV = 20f;

    RigidbodyFirstPersonController fpsController;
    [SerializeField] float zoomInMouseSensitivity = 2f;
    [SerializeField] float zoomOutMouseSensitivity = 0.5f;

    bool zoomedInToggle = false;

    private void Start()
    {
        fpsController = FindObjectOfType<RigidbodyFirstPersonController>();
    }


    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if(zoomedInToggle == false)
            {
                zoomedInToggle = true;
                fpsCamera.fieldOfView = zoomedInFOV;
                fpsController.mouseLook.XSensitivity = zoomInMouseSensitivity;
                fpsController.mouseLook.YSensitivity = zoomInMouseSensitivity;
            }
            else
            {
                zoomedInToggle = false;
                fpsCamera.fieldOfView = zoomedOutFOV;
                fpsController.mouseLook.XSensitivity = zoomOutMouseSensitivity;
                fpsController.mouseLook.YSensitivity = zoomOutMouseSensitivity;
            }
        }
    }



}
