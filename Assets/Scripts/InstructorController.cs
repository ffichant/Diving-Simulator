using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructorController : MonoBehaviour {
    public float verticalSpeed;
    public float verticalOscillationCoeff;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //Animation du moniteur
        if (GetComponent<WaypointSystem>().horizontalMovementDirection < 0)
            GetComponent<Animator>().SetTrigger("TurningLeft");
        else if (GetComponent<WaypointSystem>().horizontalMovementDirection > 0)
            GetComponent<Animator>().SetTrigger("TurningRight");

        driftingEffect();
    }

    void driftingEffect()
    {
        //Prise en compte de l'oscillation de la position dans l'eau
        var deltaY = Time.deltaTime * verticalSpeed * Mathf.Cos(Time.realtimeSinceStartup) * verticalOscillationCoeff;
        transform.Translate(0, deltaY, 0);
    }
}
