using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentController : MonoBehaviour {
    public float verticalSpeed;
    public float horizontalSpeed;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag != "Terrain")
        {
            var deltaX = Time.deltaTime * horizontalSpeed;
            var deltaY = Time.deltaTime * verticalSpeed;
            other.transform.Translate(deltaX, deltaY, 0);
        }
    }
}
