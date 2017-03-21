using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kontrola : MonoBehaviour {

	public float snagaGore = 1;
	public bool isDead = false;
	public Sprite flapSprite;
	public Sprite deadSprite;

	private SpriteRenderer sr;
	private Rigidbody2D rb;
	private Sprite normalSprite;

	// Use this for initialization
	void Start ()
	{
		rb = GetComponent<Rigidbody2D> ();
		sr = GetComponent<SpriteRenderer> ();

		normalSprite = this.sr.sprite;

		var test = Test();
		while (test.MoveNext()) {
			Debug.Log(test.Current);
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (!isDead & Input.anyKeyDown) {

			// poništi brzinu padanja
			rb.velocity = Vector2.zero;

			// primjeni impuls prema gore
			rb.AddForce(Vector2.up * snagaGore, ForceMode2D.Impulse);

			StartCoroutine(Animiraj());
		}
	}

	IEnumerator Test() {
		yield return 1;
		yield return 2;
	}

	IEnumerator Animiraj() {
		sr.sprite = flapSprite;

		// čekaj 100 millisekundi
		yield return new WaitForSeconds(0.1f);

		sr.sprite = normalSprite;
	}

	IEnumerator OnCollisionEnter2D(Collision2D other) {
		rb.velocity = Vector2.zero;

		isDead = true;
		GameControl.instance.gameOver = true;

		yield return Smrt();
	}

	IEnumerator Smrt() 
	{
		// Paralelno izvođenje
		StartCoroutine(Shake(0.5f, 0.2f));
		StartCoroutine(AnimirajSmrt());

		return null;
	}
	/*
	IEnumerator Smrt() 
	{
		// Slijedno izvođenje
		yield return Shake(0.5f, 0.2f);
		StartCoroutine(AnimirajSmrt());
	}
	*/

	/* Rutine */

	IEnumerator AnimirajSmrt ()
	{
		for (int i = 0; i < 3; i++) {
			// prikaži sličicu smrti na pola sekunde
			sr.sprite = deadSprite;
			yield return new WaitForSeconds(0.5f);
			// makni sličicu smrti na pola sekunde
			sr.sprite = null;
			yield return new WaitForSeconds(0.5f);
		}
		// kad završimo vrati sličicu smrti
		sr.sprite = deadSprite;
	}

	// Preuzeto sa http://unitytipsandtricks.blogspot.hr/2013/05/camera-shake.html
	IEnumerator Shake(float duration, float magnitude) {

	    float elapsed = 0.0f;
	    
	    Vector3 originalCamPos = Camera.main.transform.position;
	    
	    while (elapsed < duration) {

			// uvećaj za vrijeme koje je prošlo od prošlog izvođenja (framea)
	        elapsed += Time.deltaTime;          

	        // na koliko smo posto dovršenosti animacije?
	        float percentComplete = elapsed / duration;    

			// https://docs.unity3d.com/ScriptReference/Mathf.Clamp.html     
	        float damper = 1.0f - Mathf.Clamp(4.0f * percentComplete - 3.0f, 0.0f, 1.0f);
	        
	        // map value to [-1, 1]
	        float x = Random.value * 2.0f - 1.0f;
	        float y = Random.value * 2.0f - 1.0f;
	        x *= magnitude * damper;
	        y *= magnitude * damper;
	        
	        Camera.main.transform.position = new Vector3(x, y, originalCamPos.z);
	            
	        yield return null;
	    }
	    
	    Camera.main.transform.position = originalCamPos;
	}
}
