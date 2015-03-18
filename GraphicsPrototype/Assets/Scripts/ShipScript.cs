using UnityEngine;
using System.Collections;

public class ShipScript : MonoBehaviour {

	public bool placed;

	public GameObject cannonball;
	public int index;

	float shootTimer;
	// Use this for initialization
	void Start () {
		shootTimer = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
		if(placed)
		{
			shootTimer += Time.deltaTime;
			if(shootTimer > 3.0f)
			{
				GameObject.Instantiate(cannonball, transform.position, Quaternion.identity);
				shootTimer = 0.0f;
			}
		}
	}
}
