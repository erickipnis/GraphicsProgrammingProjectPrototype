using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ManagerScript : MonoBehaviour {

	// Use this for initialization
	public GameObject ship;
	public GameObject enemy;
	public GameObject gatherer;
	public GameObject node;

	private GameObject curSelection;

	public static List<NodeScript> grid;

	public Material lineMaterial;

	const int GRID_HEIGHT = 6;
	const int GRID_WIDTH = 8;
	const float GRID_SIZE = 2.0f;
	const float GRID_TOP = -2.5f;
	const float GRID_LEFT = -8.5f;
	const float GRID_ABOVE = -1.4f;

	public static int resources;
	public static int health;

	float spawnTimer;
	float timeToSpawn;

	void Start () {

		spawnTimer = 0.0f;
		timeToSpawn = 10.0f;
		curSelection = null;
		resources = 125;
		health = 100;
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

	}
	
	// Update is called once per frame
	void Update () {
		SelectObject();
		if(curSelection != null)
			PlaceObject();

		SpawnEnemy();
		spawnTimer += Time.deltaTime;

		if(health <= 0)
			Application.LoadLevel("Bubbles");
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

	private void SelectObject()
	{
		if(Input.GetKeyDown(KeyCode.Alpha1))
		{
			if(curSelection != null)
				Destroy(curSelection);
			curSelection = (GameObject)GameObject.Instantiate(gatherer, Vector3.zero, Quaternion.identity);;
		}
		else if(Input.GetKeyDown(KeyCode.Alpha2))
		{
			if(curSelection != null)
				Destroy(curSelection);
			curSelection = (GameObject)GameObject.Instantiate(ship, Vector3.zero, Quaternion.identity);;
		}
	}

	private void PlaceObject()
	{
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
		if(Input.GetMouseButtonDown(0) && grid[index].Open)
		{
			if(curSelection.tag == "Ship" && index % GRID_WIDTH != 0 && resources >= 50)
			{
				curSelection.GetComponent<ShipScript>().placed = true;
				curSelection.GetComponent<ShipScript>().index = index;
				curSelection = null;
				resources -= 50;
				grid[index].Open = false;
				return;
			}
			if(curSelection.tag == "Gatherer" && index % GRID_WIDTH == 0 && resources >= 75)
			{
				curSelection.GetComponent<GathererScript>().placed = true;
				curSelection = null;
				resources -= 75;
				grid[index].Open = false;
				return;
			}
		}
		if(curSelection != null)
			curSelection.transform.position = grid[index].Position;
	}

	private void SpawnEnemy()
	{
		if(spawnTimer > timeToSpawn)
		{
			int lane = Random.Range(0, GRID_HEIGHT);
			GameObject.Instantiate(enemy, new Vector3(GRID_TOP + GRID_SIZE * lane + GRID_SIZE /2, GRID_ABOVE, 10.0f), Quaternion.Euler(0.0f, 180.0f, 0.0f));
			spawnTimer = 0.0f;
			if(timeToSpawn > 0.25f)
				timeToSpawn *= 0.9f;
		}
	}

	void OnGUI()
	{
		GUIStyle s = new GUIStyle();
		s.normal.textColor = Color.black;
		GUI.Label(new Rect(20, 20, 100, 20), "Resources: " + resources, s);
	}
}
