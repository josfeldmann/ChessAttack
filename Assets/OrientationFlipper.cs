using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OrientationFlipper : MonoBehaviour
{

    public TextMeshProUGUI text;
    public bool hardCoded = true;
    public CameraSize current;
    public CameraSize portrait, landScape;
    public Camera cam;



    private DeviceOrientation lastOrientation;

    private void Awake() {
        if (!hardCoded) {
            AdjustBasedOnOrientation();
            lastOrientation = Input.deviceOrientation;
        }
        if(hardCoded)ApplyCameraSize(current);
    }

    private void Update() {
        if (hardCoded) return;
        print(Input.deviceOrientation);
        if (Input.deviceOrientation != lastOrientation) {
            AdjustBasedOnOrientation();
            lastOrientation = Input.deviceOrientation;
        }
    }


    public void AdjustBasedOnOrientation() {
        
       
        
        if (Input.deviceOrientation == DeviceOrientation.LandscapeLeft || Input.deviceOrientation == DeviceOrientation.LandscapeRight) {
            ApplyCameraSize(landScape);
        } else {
            ApplyCameraSize(portrait);
        }


    }

    public void ApplyCameraSize(CameraSize s) {

        current = s;
        //text.text = s.orientation;
        if (s.isWidth) {
            float aspect = (float)Screen.width / (float)Screen.height;
            float height = s.desiredOrthographicSize / aspect;
            cam.orthographicSize = height;
        } else {
            
            cam.orthographicSize = s.desiredOrthographicSize;
        }
        cam.transform.position = s.cameraWorldPosition;
    }

    
}
