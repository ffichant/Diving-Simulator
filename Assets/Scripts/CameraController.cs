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
        float minX = -4.8f;
        float minY = -3.8f;
        float maxX = 4.8f;  
        float maxY = 3.8f;
        transform.position = new Vector3(
          Mathf.Clamp(player.transform.position.x, minX, maxX),
          Mathf.Clamp(player.transform.position.y, minY, maxY),
          player.transform.position.z);
    }
}
