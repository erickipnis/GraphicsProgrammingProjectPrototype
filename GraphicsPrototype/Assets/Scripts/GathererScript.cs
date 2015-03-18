using UnityEngine;
using System.Collections;

public class GathererScript : MonoBehaviour {

	float timer;

	public bool placed;
	// Use this for initialization
	void Start () {
		placed = false;
		timer = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
		if(placed)
		{
			timer += Time.deltaTime;
			if(timer > 1.0f)
			{
				ManagerScript.resources+=5;
				timer = 0.0f;
			}
		}
	
	}
}
