using UnityEngine;
using System.Collections;

public class CannonballScript : MonoBehaviour {

	const float LIFESPAN = 2.0f;
	float timer;
	// Use this for initialization
	void Start () {
		timer = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		if(timer > LIFESPAN)
			Destroy(this.gameObject);

		Vector3 pos = transform.position;
		pos.z += 3.5f * Time.deltaTime;
		transform.position = pos;
	}
}
