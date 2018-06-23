﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickExample : MonoBehaviour {

    public Vector3 rot = Vector3.zero;

    [HideInInspector] public bool doRot = false;

    private BasicController ctl;
    private HoloLensHandTracking.HandsTrackingController handMgr;

    private void Awake() {
        ctl = Camera.main.GetComponent<BasicController>();
        handMgr = GameObject.FindGameObjectWithTag("HandManager").GetComponent<HoloLensHandTracking.HandsTrackingController>();
    }

	private void Update() {
		if (ctl.isLookingAt == gameObject.name && handMgr.handPressed) {
            doRot = true;
        }

        if (!handMgr.handPressed) {
            doRot = false;
        }

        if (doRot) transform.Rotate(rot);
    }

}
