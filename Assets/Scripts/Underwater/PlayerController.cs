﻿using System.Collections;
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

    public GameObject canvas;
    
    void Start()
    {
        canvas = GameObject.Find("Canvas");
    }
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


       
        transform.Translate(x*deltaStamina, y, 0);
        //On s'assure que le joueur ne fait pas de rotation quand il y a une collision
        transform.rotation = Quaternion.Euler(0, 0, 0) ;
        driftingEffect();

        oxygenBar.GetComponent<Scrollbar>().size -= Time.deltaTime * oxygenDecayRate;
        clampPos();
    }

    void driftingEffect()
    {
        //Prise en compte de l'oscillation de la position dans l'eau
        var deltaY = Time.deltaTime * verticalSpeed * Mathf.Cos(Time.realtimeSinceStartup) * verticalOscillationCoeff;
        transform.Translate(0, deltaY, 0);
    }
    
    void clampPos()
    {
        RectTransform canvasRect = canvas.GetComponent<RectTransform>();
        float minX = -5.75f;
        float minY = -4.5f;
        float maxX = 5.75f;
        float maxY = 4.5f;
        transform.position = new Vector3(
          Mathf.Clamp(transform.position.x, minX, maxX),
          Mathf.Clamp(transform.position.y, minY, maxY),
          transform.position.z);
    }
}