using UnityEngine;

[CreateAssetMenu(menuName = "MobileKit/CameraSize")]
public class CameraSize : ScriptableObject {

    public string orientation = "Landscape";
    public bool isWidth;
    public float desiredOrthographicSize;
    public Vector3 cameraWorldPosition;

}
