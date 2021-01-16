using UnityEngine;

public class InputManager : MonoBehaviour {
    public float x, y;
    public bool inputTouch;

    private void Update() {
        inputTouch = Input.GetMouseButtonDown(0);
    }
}


