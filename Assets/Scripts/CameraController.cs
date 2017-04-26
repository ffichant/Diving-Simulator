using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    public GameObject player;
    public GameObject canvas;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        canvas = GameObject.Find("Canvas");
	}
	
	// Update is called once per frame
	void Update () {
        RectTransform canvasRect = canvas.GetComponent<RectTransform>();
        float minX = canvas.transform.position.x - (canvasRect.rect.width/2*canvasRect.localScale.x);
        float minY = canvas.transform.position.y - (canvasRect.rect.height/2*canvasRect.localScale.y);
        float maxX = canvas.transform.position.x + (canvasRect.rect.width/2*canvasRect.localScale.x);
        float maxY = canvas.transform.position.y + (canvasRect.rect.height/2*canvasRect.localScale.y);
        Debug.Log("Test : minX= " + minX + " maxX= " + maxX);
        //transform.Translate(movement * speed * Time.deltaTime, Space.Self);
        transform.position = new Vector3(
          Mathf.Clamp(player.transform.position.x, minX, maxX),
          Mathf.Clamp(player.transform.position.y, minY, maxY),
          player.transform.position.z);
    }
}
