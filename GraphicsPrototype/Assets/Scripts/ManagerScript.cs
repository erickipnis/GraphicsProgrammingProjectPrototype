using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ManagerScript : MonoBehaviour {

	// Use this for initialization
	public GameObject ship;
	public GameObject node;

	List<NodeScript> grid;

	public Material lineMaterial;

	const int GRID_HEIGHT = 6;
	const int GRID_WIDTH = 12;
	const float GRID_SIZE = 2.0f;
	const float GRID_TOP = -2.5f;
	const float GRID_LEFT = -5.5f;
	const float GRID_ABOVE = -1.4f;

	private GameObject testShip;

	void Start () {

		grid = new List<NodeScript>();
		// set up the grid
		for(int i = 0; i < GRID_HEIGHT; i++)
		{
			CreateLine(new Vector3(i * GRID_SIZE + GRID_TOP, GRID_ABOVE, GRID_LEFT),
			           new Vector3(i * GRID_SIZE + GRID_TOP, GRID_ABOVE, GRID_LEFT + GRID_WIDTH * GRID_SIZE));
			
			for(int j = 0; j < GRID_WIDTH;j++)
			{
				if(i == 0)
				{
					CreateLine(new Vector3(GRID_TOP, GRID_ABOVE, j * GRID_SIZE + GRID_LEFT), 
					           new Vector3(GRID_TOP + GRID_SIZE * GRID_HEIGHT, GRID_ABOVE, j * GRID_SIZE + GRID_LEFT));
				}

				GameObject n = (GameObject)GameObject.Instantiate(node,
				                                                  new Vector3(GRID_TOP + i * GRID_SIZE + GRID_SIZE/2.0f, -1.6f, GRID_LEFT + j * GRID_SIZE + GRID_SIZE/2.0f), 
				                                                  Quaternion.identity);
				grid.Add(n.GetComponent<NodeScript>()); 
			}
		}

		CreateLine(new Vector3(GRID_HEIGHT * GRID_SIZE + GRID_TOP, GRID_ABOVE, GRID_LEFT), 
		           new Vector3(GRID_HEIGHT * GRID_SIZE + GRID_TOP, GRID_ABOVE, GRID_LEFT + GRID_WIDTH * GRID_SIZE));
		CreateLine(new Vector3(GRID_TOP, GRID_ABOVE, GRID_WIDTH * GRID_SIZE + GRID_LEFT), 
		           new Vector3(GRID_TOP + GRID_HEIGHT * GRID_SIZE, GRID_ABOVE, GRID_WIDTH * GRID_SIZE + GRID_LEFT));

		testShip = (GameObject)GameObject.Instantiate(ship, Vector3.zero, Quaternion.identity);

	}
	
	// Update is called once per frame
	void Update () {
		Vector3 mouse = Input.mousePosition;
		mouse.z = 16.5f;
		mouse = Camera.main.ScreenToWorldPoint(mouse);

		int index = 0;
		float smallest = 100000;
		for(int i = 0; i < grid.Count;i++)
		{
			if(Vector3.Distance(mouse, grid[i].Position) < smallest)
			{
				index = i;
				smallest = Vector3.Distance(mouse, grid[i].Position);
			}
		}

		// place a ship on an open grid
		if(Input.GetMouseButtonUp(0) && grid[index].Open)
		{
			grid[index].Open = false;
			GameObject.Instantiate(ship, grid[index].Position, Quaternion.identity);
		}
		testShip.transform.position = grid[index].Position;
	}

	private void CreateLine(Vector3 start, Vector3 end)
	{
		GameObject g = new GameObject();
		g.name = "Line";
		LineRenderer line = g.AddComponent<LineRenderer>();
		line.SetVertexCount(2);
		line.material = lineMaterial;
		line.SetWidth(0.1f, 0.1f);
		line.SetColors(Color.black, Color.black);
		line.SetPosition(0, start);
		line.SetPosition(1, end);
		line.gameObject.layer = 5;
	}
}
