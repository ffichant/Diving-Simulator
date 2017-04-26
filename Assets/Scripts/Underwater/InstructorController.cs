using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructorController : MonoBehaviour {
    public float verticalSpeed;
    public float verticalOscillationCoeff;

    private bool turnedLeft = true;
    private bool turnedRight = false;
    // Use this for initialization
    void Start () {
        		
	}
	
	// Update is called once per frame
	void Update () {
        //Animation du moniteur
        if (GetComponent<WaypointSystem>().horizontalMovementDirection < 0 && turnedRight)
        {
            GetComponent<Animator>().SetTrigger("TurningLeft");
            turnedLeft = true;
            turnedRight = false;
        }
        else if (GetComponent<WaypointSystem>().horizontalMovementDirection > 0 && turnedLeft)
        {
            GetComponent<Animator>().SetTrigger("TurningRight");
            turnedRight = true;
            turnedLeft = false;
        }
        driftingEffect();
    }

    void driftingEffect()
    {
        //Prise en compte de l'oscillation de la position dans l'eau
        var deltaY = Time.deltaTime * verticalSpeed * Mathf.Cos(Time.realtimeSinceStartup) * verticalOscillationCoeff;
        transform.Translate(0, deltaY, 0);
    }
}
