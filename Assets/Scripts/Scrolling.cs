using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scrolling : MonoBehaviour {

	private Rigidbody2D rb;

	// Use this for initialization
	void Start () {

		rb = GetComponent<Rigidbody2D>();

		rb.velocity = Vector2.left * GameControl.instance.scrollSpeed;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (GameControl.instance.gameOver) {
			rb.velocity = Vector2.zero;
		}
	}
}
