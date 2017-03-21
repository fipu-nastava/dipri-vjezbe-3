using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawnin : MonoBehaviour {

	private bool stvorenNovi = false;
	private float sirina;

	// Use this for initialization
	void Start () {
		sirina = this.GetComponentInChildren<SpriteRenderer>().bounds.size.x;

		StartCoroutine(Provjera());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	// neka se ovo izvršava svakih 100ms umjesto 60 puta u sekundi
	IEnumerator Provjera ()
	{
		while (true) {

			var x = this.transform.position.x;

			if (!stvorenNovi && x < 0) {
				var novaPozadina = Instantiate (this);
				novaPozadina.transform.position = 
					(Vector2) this.transform.position + Vector2.right * sirina;
				stvorenNovi = true;
			}

			if (x < -sirina) {
				Destroy(this.gameObject);
			}

			// sljedeći put provjeri tek za 100ms
			yield return new WaitForSeconds (0.1f);
		}
	}
}
