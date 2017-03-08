using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float horizontalSpeed = 5.0f;
    public float verticalSpeed = 5.0f;

    public float horizontalOscillationCoeff = 0.8f;
    public float verticalOscillationCoeff = 0f;
    // Update is called once per frame
    void Update()
    {
        //Déplacement du joueur
        var x = Input.GetAxis("Horizontal") * Time.deltaTime * horizontalSpeed;
        var y = Input.GetAxis("Vertical") * Time.deltaTime * verticalSpeed;

        if(x < 0)
            GetComponent<Animator>().SetTrigger("TurningLeft");
        else if(x > 0)
            GetComponent<Animator>().SetTrigger("TurningRight");


        //Prise en compte de l'oscillation de la position dans l'eau
        var deltaX = Time.deltaTime*horizontalSpeed*Mathf.Cos(Time.realtimeSinceStartup) * horizontalOscillationCoeff;
        var deltaY = Time.deltaTime*verticalSpeed*Mathf.Cos(Time.realtimeSinceStartup) * verticalOscillationCoeff;
        transform.Translate(x+deltaX, y+deltaY, 0);

    }
}
