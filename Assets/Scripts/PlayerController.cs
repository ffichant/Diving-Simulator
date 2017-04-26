using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    public GameObject oxygenBar;
    public GameObject palmingBar;
    public float oxygenDecayRate;
    public float staminaUseRate;
    public float staminaRegenerationRate;
    public float horizontalStaminaSpeedBoost;//Représente un coefficient multiplicateur
    public float horizontalSpeed;
    public float verticalSpeed;
    public float verticalOscillationCoeff;
    
    // Update is called once per frame
    void Update()
    {
        //Utilisation de la stamina
        var deltaStamina = 1.0f;
        if (Input.GetKey(KeyCode.Space) && (palmingBar.GetComponent<Scrollbar>().size > 0))
        {
            palmingBar.GetComponent<Scrollbar>().size -= Time.deltaTime * staminaUseRate;
            deltaStamina += horizontalStaminaSpeedBoost;
        }
        else if (palmingBar.GetComponent<Scrollbar>().size < 1 && !Input.GetKey(KeyCode.Space))
            palmingBar.GetComponent<Scrollbar>().size += staminaRegenerationRate;

        //Déplacement du joueur
        var x = Input.GetAxis("Horizontal") * Time.deltaTime * horizontalSpeed;
        var y = Input.GetAxis("Vertical") * Time.deltaTime * verticalSpeed;

        //Animation du joueur
        if(x < 0)
            GetComponent<Animator>().SetTrigger("TurningLeft");
        else if(x > 0)
            GetComponent<Animator>().SetTrigger("TurningRight");


        //Prise en compte de l'oscillation de la position dans l'eau
        var deltaY = Time.deltaTime*verticalSpeed*Mathf.Cos(Time.realtimeSinceStartup) * verticalOscillationCoeff;
        transform.Translate(x*deltaStamina, y+deltaY, 0);


        oxygenBar.GetComponent<Scrollbar>().size -= Time.deltaTime * oxygenDecayRate;
    }
}
