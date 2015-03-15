using UnityEngine;
using System.Collections;

public class NodeScript : MonoBehaviour {

	private Vector3 position;
	private bool open;

	public Vector3 Position {get{return position;} set{position = value;}}
	public bool Open {get{return open;} set {open = value;}}
	// Use this for initialization
	void Start () {
		position = transform.position;
		open = true;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
