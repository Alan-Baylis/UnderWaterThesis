using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour {

	public int visibleGridSize;
	public int trueGridSize;
	public int startingPosition;
	public GameObject cube;
	private int playerPos;
	private GameObject[] visibleGrid;
	private GameObject[] trueGrid;
	// Use this for initialization
	void Start () {
		visibleGrid = new GameObject[visibleGridSize];
		playerPos = startingPosition;
		float width = cube.GetComponent<BoxCollider>().bounds.size.x;
		for(int i = 0; i< visibleGridSize; i++) {
			visibleGrid[i] = Instantiate(cube, new Vector3(startingPosition + (i * width), 0, 0), Quaternion.identity);
			visibleGrid[i].GetComponent<BasicGroundBehavior>().effectivePosition = i;
		}
		BasicGroundBehavior.gridManager = this;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void UpdatePlayerPosition(int x, GameObject standingCube) {
		playerPos = x;
		Debug.Log("moving around objects");
		List<GameObject> temp = new List<GameObject>();
		float width = cube.GetComponent<BoxCollider>().bounds.size.x;
		for(int i = 0; i < visibleGrid.Length / 2; i++) {
			if(i != 2 && visibleGrid[i] != standingCube) {
				visibleGrid[i].transform.position = new Vector3(standingCube.transform.position.x - (i * width), 0, 0);
				temp.Add(visibleGrid[i]);
			}
		}
		for(int i = (visibleGrid.Length / 2) + 1; i < visibleGrid.Length; i++) {
			if(i != 2 && visibleGrid[i] != standingCube) {
				visibleGrid[i].transform.position = new Vector3(standingCube.transform.position.x + (i * width), 0, 0);
				temp.Add(visibleGrid[i]);
			}
		}

		visibleGrid[2] = standingCube;
	}
}
