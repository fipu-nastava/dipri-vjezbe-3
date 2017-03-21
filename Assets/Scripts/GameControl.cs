using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour {

	public float scrollSpeed = 1f;
	public bool gameOver = false;

	public static GameControl instance;

	// Još ranije od Starta
	void Awake ()
	{
		if (instance == null) {
			instance = this;
		} else {
			Destroy(this.gameObject); // višak, već postoji
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (gameOver && Input.anyKeyDown) {
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}
	}
}
