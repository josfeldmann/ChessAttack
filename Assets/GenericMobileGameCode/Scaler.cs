using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scaler : MonoBehaviour
{

    public float speed = 5f;
    public float waitBeforeDeactivate = 2f;

    WaitForSeconds w;

    private void Awake() {
        w = new WaitForSeconds(waitBeforeDeactivate);
    }

    public void ScaleDownToNothing() {
        StopAllCoroutines();
        StartCoroutine(ScaleDownNum());
    }

    IEnumerator ScaleDownNum() {
        while (transform.localScale != Vector3.zero) {
            transform.localScale = Vector3.MoveTowards(transform.localScale, Vector3.zero, speed * Time.deltaTime);
            yield return null;
        }

        yield return w;

        transform.localScale = Vector3.one;
        gameObject.SetActive(false);
    }

}
