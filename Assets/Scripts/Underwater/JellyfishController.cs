using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JellyfishController : MonoBehaviour {
    public float verticalSpeed;
    public float verticalOscillationCoeff;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        driftingEffect();
	}

    void driftingEffect()
    {
        //Prise en compte de l'oscillation de la position dans l'eau
        var deltaY = Time.deltaTime * verticalSpeed * Mathf.Cos(Time.realtimeSinceStartup) * verticalOscillationCoeff;
        transform.Translate(0, deltaY, 0);
    }
}
