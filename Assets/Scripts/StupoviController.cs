using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StupoviController : MonoBehaviour {

	public int brojStupova = 5;
	public GameObject stupoviPrefab;
	public float brzinaStvaranja = 6f; // svake 3 sekunde
	public float columnMin = -8f;
	public float columnMax = -2.5f; 

	private float xPozicija = 10f;
	private int trenutniStup = 0;
	private GameObject TrenutniStup {
		get {
			return stupovi[trenutniStup];
		}
	}
	private List<GameObject> stupovi;
	private Vector2 vanEkrana = new Vector2 (-15,-25);

	// Use this for initialization
	void Start () {
		stupovi = new List<GameObject>();

		for(int i = 0; i < brojStupova; i++)
        {
            //...and create the individual columns.
			stupovi.Add((GameObject)Instantiate(stupoviPrefab, vanEkrana, 
				Quaternion.identity));
        }

		StartCoroutine(StvoriNove());
	}
	
	// Update is called once per frame
	IEnumerator StvoriNove ()
	{
		while (true) {
			yield return new WaitForSeconds(brzinaStvaranja);

			float yPozicija = Random.Range(columnMin, columnMax);

			TrenutniStup.transform.position = new Vector2(xPozicija, yPozicija);

			trenutniStup++;
			trenutniStup %= brojStupova;
		}
	}
}
