using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour {

	public int visibleGridSize;
	public int trueGridSize;
	public int startingPosition;
	public GameObject cube;
	private int playerPos;
	LinkedList<GameObject> gridLine;
	private GameObject[] visibleGrid;
	private GameObject[] trueGrid;
	private float cubeWidth;
	// Use this for initialization
	void Start () {
		gridLine = new LinkedList<GameObject>();
		visibleGrid = new GameObject[visibleGridSize];
		playerPos = startingPosition;
		cubeWidth = cube.GetComponent<BoxCollider>().bounds.size.x;
		GameObject temp = Instantiate(cube, new Vector3(startingPosition, 0, 0), Quaternion.identity);
		temp.GetComponent<BasicGroundBehavior>().effectivePosition = 0;
		gridLine.AddFirst(temp);
		for(int i = 1; i< visibleGridSize; i++) {
			temp = Instantiate(cube, new Vector3(startingPosition + (i * cubeWidth), 0, 0), Quaternion.identity);
			temp.GetComponent<BasicGroundBehavior>().effectivePosition = i;
			gridLine.AddLast(temp);
		}
		BasicGroundBehavior.gridManager = this;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void UpdatePlayerPosition(int x, GameObject standingCube) {
		if(x < playerPos) {
			GameObject temp = gridLine.Last.Value;
			gridLine.RemoveLast();
			GameObject first = gridLine.First.Value;
			temp.transform.position = new Vector3(first.transform.position.x - cubeWidth, temp.transform.position.y, 0);
			temp.GetComponent<BasicGroundBehavior>().ModifyPosition((int)first.transform.position.x - (int)cubeWidth);
			gridLine.AddFirst(temp);
		}
		else if(x > playerPos) {
			GameObject temp = gridLine.First.Value;
			gridLine.RemoveFirst();
			GameObject last = gridLine.Last.Value;
			temp.transform.position = new Vector3(last.transform.position.x + cubeWidth, temp.transform.position.y, 0);
			//GameObject temp = Instantiate(cube, new Vector3(last.transform.position.x + cubeWidth, 0, 0), Quaternion.identity);
			temp.GetComponent<BasicGroundBehavior>().ModifyPosition((int)last.transform.position.x + (int)cubeWidth);
			gridLine.AddLast(temp);
		}
		playerPos = x;
	}
}
