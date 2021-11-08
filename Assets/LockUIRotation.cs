using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockUIRotation : MonoBehaviour {
    public RectTransform UITransform;

    // Update is called once per frame
    void Update() {
        UITransform.rotation.Set(0,0, -transform.parent.rotation.z, 0);
    }
}
