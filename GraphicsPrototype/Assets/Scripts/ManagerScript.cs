using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ManagerScript : MonoBehaviour {

	// Use this for initialization
	public GameObject ship;

	Vector3 mousePos;

	List<Vector3> grid;

	public Material lineMaterial;

	const int GRID_HEIGHT = 6;
	const int GRID_WIDTH = 12;
	const float GRID_SIZE = 2.0f;
	const float GRID_TOP = -2.5f;
	const float GRID_LEFT = -5.5f;

	void Start () {

		grid = new List<Vector3>();
		// set up the grid
		for(int i = 0; i < GRID_HEIGHT; i++)
		{
			CreateLine(new Vector3(i * GRID_SIZE + GRID_TOP, 3.0f, GRID_LEFT),
			           new Vector3(i * GRID_SIZE + GRID_TOP, 3.0f, GRID_LEFT + GRID_WIDTH * GRID_SIZE));
			
			for(int j = 0; j < GRID_WIDTH;j++)
			{
				if(i == 0)
				{
					CreateLine(new Vector3(GRID_TOP, 3.0f, j * GRID_SIZE + GRID_LEFT), 
					           new Vector3(GRID_TOP + GRID_SIZE * GRID_HEIGHT, 3.0f, j * GRID_SIZE + GRID_LEFT));
				}

				grid.Add(new Vector3(GRID_TOP + i * GRID_SIZE + GRID_SIZE/2.0f, 0.0f, GRID_LEFT + j * GRID_SIZE + GRID_SIZE/2.0f)); 
			}
		}

		CreateLine(new Vector3(GRID_HEIGHT * GRID_SIZE + GRID_TOP, 3.0f, GRID_LEFT), 
		           new Vector3(GRID_HEIGHT * GRID_SIZE + GRID_TOP, 3.0f, GRID_LEFT + GRID_WIDTH * GRID_SIZE));
		CreateLine(new Vector3(GRID_TOP, 3.0f, GRID_WIDTH * GRID_SIZE + GRID_LEFT), 
		           new Vector3(GRID_TOP + GRID_HEIGHT * GRID_SIZE, 3.0f, GRID_WIDTH * GRID_SIZE + GRID_LEFT));

	}
	
	// Update is called once per frame
	void Update () {
		Vector3 mouse = Input.mousePosition;

		for(int i = 0; i< grid.Count;i++)
		{
			Vector3 start = grid[i];
			start.y = 3.0f;
			Debug.DrawLine(start, grid[i]);
		}
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
	}
}
