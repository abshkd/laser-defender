using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float Speed = 0.1f;
    private float x;
    private float y;
    private float minX = 1f;
    private float maxX = 15f;
    private float minY = 0.7f;
    private float maxY = 2.0f;
	// Use this for initialization
	void Start () {
        x = 8.0f;
        y = 0.5f;

        //Start position of the ship
        this.transform.position = new Vector3(x,y,0f);
    }
	
	// Update is called once per frame
	void Update () {

        x += Input.GetAxis("Horizontal")* Speed;
        y += Input.GetAxis("Vertical") * Speed;
        x = Mathf.Clamp(x, minX, maxX);
        y = Mathf.Clamp(y, minY, maxY);
        Vector3 shipPosition = new Vector3(x, y, 0f);
        this.transform.position = shipPosition;
	}
}
