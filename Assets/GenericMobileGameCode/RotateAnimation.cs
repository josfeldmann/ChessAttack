using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAnimation : MonoBehaviour, IActivate
{
    public bool startLeft = true;

    public float zAmount;
    public float speed;

    private float currentZTarget;
    private float currentZ;

    private void Awake() {
        if (startLeft) currentZ = -zAmount;
        else currentZ = zAmount;
    }

    private void Update() {
        if (active) {
            currentZ = Mathf.MoveTowards(currentZ, currentZTarget, speed * Time.deltaTime);
            transform.eulerAngles = new Vector3(0,0,currentZ);
            if (currentZ == currentZTarget) {
                if (currentZTarget == zAmount) {
                    currentZTarget = -zAmount;
                } else {
                    currentZTarget = zAmount;
                }
            }
        }
    }

    private bool active = true;
    public void Activate() {
        active = true;
    }

    public void DeActivate() {
        active = false;
        transform.eulerAngles = Vector3.zero;
    }

    public bool isActive() {
        return active;
    }
}
