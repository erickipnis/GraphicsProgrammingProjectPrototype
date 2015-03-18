using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour {

	int health;
	// Use this for initialization
	void Start () {
		health = 100;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 pos = transform.position;
		pos.z -= 1 * Time.deltaTime;
		transform.position = pos;

		if(pos.z < -5)
		{
			ManagerScript.health -= 25;
			Destroy(this.gameObject);
		}
		if(health <= 0)
			Destroy(this.gameObject);
	}

	void OnTriggerEnter(Collider col)
	{
		switch(col.gameObject.tag)
		{
		case "Cannonball":
			health -= 50;
			Destroy(col.gameObject);
			break;
		case "Ship":
			ManagerScript.grid[col.gameObject.GetComponent<ShipScript>().index].Open = true;
			Destroy(col.gameObject);
			Destroy(this.gameObject);
			break;
		}
	}
}
