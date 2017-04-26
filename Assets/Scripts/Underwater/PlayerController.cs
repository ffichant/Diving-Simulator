using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    public GameObject oxygenBar;
    public GameObject palmingBar;
    public Scrollbar oxygen;
    public Scrollbar stamina;
    public float oxygenDecayRate;
    public float staminaUseRate;
    public float staminaRegenerationRate;
    public float horizontalStaminaSpeedBoost; //Représente un coefficient multiplicateur
    public float horizontalSpeed;
    public float verticalSpeed;
    public float verticalOscillationCoeff;


    public GameObject canvas;
    public ScoreManager Score;
    private bool hasDamagedTerrain = false;
    private bool firstTimeTouchingJellyfish = true;

    public GameObject endButton;

    void Start()
    {
        canvas = GameObject.Find("Canvas");
        Score = GameObject.FindWithTag("Score").GetComponent<ScoreManager>();
        endButton.SetActiveRecursively(false);
        stamina = palmingBar.GetComponent<Scrollbar>();
        oxygen = oxygenBar.GetComponent<Scrollbar>();
    }
    // Update is called once per frame
    void Update()
    {
        //Utilisation de la stamina
        var deltaStamina = 1.0f;
        if (Input.GetKey(KeyCode.Space) && (stamina.size > 0))
        {
            stamina.size -= Time.deltaTime * staminaUseRate;
            deltaStamina += horizontalStaminaSpeedBoost;
        }
        else if (stamina.size < 1 && !Input.GetKey(KeyCode.Space))
            stamina.size += staminaRegenerationRate;

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

        oxygen.size = Mathf.Clamp(oxygen.size - Time.deltaTime * oxygenDecayRate,0,1);
        clampPos();


        //Conditions de fin prématurée
        if (oxygen.size == 0f)
        {
            Score.RegisterLossOfPointsDive(60, "Il faut toujours faire attention à son niveau d'oxygène quand on plonge !");
            endButton.SetActiveRecursively(true);
        }

        oxygen.value = 0f;
        stamina.value = 0f;
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

    private void OnTriggerEnter2D (Collider2D other)
    {
        if(other.tag == "Harmful")
        {
          oxygen.size = Mathf.Clamp(oxygen.size - 15 * oxygenDecayRate,0,1);
            if(firstTimeTouchingJellyfish)
            {
                Score.RegisterLossOfPointsDive(0, "Il faut éviter de toucher les méduses et les autres animaux, cela peut être dangereux !");
                firstTimeTouchingJellyfish = false;
            }
        }
        if(other.tag == "Terrain" && !hasDamagedTerrain)
        {
            Score.RegisterLossOfPointsDive(10, "Il ne faut pas trop s'approcher des fonds marins pour ne pas les abîmer !");
            hasDamagedTerrain = true;
        }

    }
}
