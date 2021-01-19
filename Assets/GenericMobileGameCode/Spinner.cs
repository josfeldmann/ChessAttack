using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinner : MonoBehaviour
{
    private bool spinning;
    private float speed = 5;
    private float targetz = 0;
    bool spinAgainAfter;
    public void SpinAtSpeed(float speed) {
        if (spinning) {
            spinAgainAfter = true;
        } else {
            spinning = true;
            spinAgainAfter = false; 
        }
        this.speed = speed;
    }

    private void Update() {
        if (spinning) {
            if (targetz < 360) {
                targetz = Mathf.MoveTowards(targetz, 360, speed * Time.deltaTime);
            } else {
                if (spinAgainAfter) {
                    spinAgainAfter = false;
                    spinning = true;
                } else {
                    spinning = false;
                }
                targetz = 0;
            }
            transform.eulerAngles = new Vector3(0, 0, targetz);
        }
    }

    public void Stop() {
        spinning = false;
    }
}
