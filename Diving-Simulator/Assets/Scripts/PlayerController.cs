using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    public GameObject oxygenBar;
    public GameObject palmingBar;
    public float oxygenDecayRate = 0.02f;
    public float horizontalSpeed = 5.0f;
    public float verticalSpeed = 5.0f;

    public float horizontalOscillationCoeff = 0.8f;
    public float verticalOscillationCoeff = 0f;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) && (palmingBar.GetComponent<Scrollbar>().size > 0))
        {
            palmingBar.GetComponent<Scrollbar>().size -= Time.deltaTime * oxygenDecayRate;
        }
        else if (palmingBar.GetComponent<Scrollbar>().size < 1 && !Input.GetKeyDown(KeyCode.P))
            palmingBar.GetComponent<Scrollbar>().size += 0.05f;

        //Déplacement du joueur
        var x = Input.GetAxis("Horizontal") * Time.deltaTime * horizontalSpeed;
        var y = Input.GetAxis("Vertical") * Time.deltaTime * verticalSpeed;

        if(x < 0)
            GetComponent<Animator>().SetTrigger("TurningLeft");
        else if(x > 0)
            GetComponent<Animator>().SetTrigger("TurningRight");


        //Prise en compte de l'oscillation de la position dans l'eau
        //var deltaX = Time.deltaTime*horizontalSpeed*Mathf.Cos(Time.realtimeSinceStartup) * horizontalOscillationCoeff;
        var deltaY = Time.deltaTime*verticalSpeed*Mathf.Cos(Time.realtimeSinceStartup) * verticalOscillationCoeff;
        transform.Translate(x, y+deltaY, 0);


        oxygenBar.GetComponent<Scrollbar>().size -= Time.deltaTime * oxygenDecayRate;
    }
}
