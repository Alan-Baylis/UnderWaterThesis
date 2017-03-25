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
	LinkedListNode<GameObject> currentCube;
	private GameObject[] visibleGrid;
	private GameObject[] trueGrid;
	private float cubeWidth = 2;
	// Use this for initialization
	void Start () {
		gridLine = new LinkedList<GameObject>();
		visibleGrid = new GameObject[visibleGridSize];
		playerPos = startingPosition;
		for(int i = 0 - (visibleGridSize / 2); i< visibleGridSize / 2; i++) {
			GameObject temp = Instantiate(cube, new Vector3(startingPosition + (i * cubeWidth), 0, 0), Quaternion.identity);
			temp.GetComponent<BasicGroundBehavior>().effectivePosition = new BasicGroundBehavior.EffectivePositionPoint(i, 0);
			gridLine.AddLast(temp);
			if(i == 0){
				currentCube = gridLine.Last;
			}
		}

		LinkedGrid testGrid = new LinkedGrid();
/*		testGrid.AddFirstColumn(visibleGrid);
		List<GameObject> testList = new List<GameObject>();
		for(int i = 0 - (visibleGridSize / 2); i< visibleGridSize / 2; i++) {
			GameObject temp = Instantiate(cube, new Vector3(cubeWidth * 2, 0, startingPosition + (i * cubeWidth)), Quaternion.identity);
			temp.GetComponent<BasicGroundBehavior>().effectivePosition = new BasicGroundBehavior.EffectivePositionPoint(1, i);
			testList.Add(temp);
		}
		testGrid.AddColumn(testList.ToArray());*/
		gridLine.CopyTo(visibleGrid, 0);
		testGrid.AddTopRow(visibleGrid);
		List<GameObject> testList = new List<GameObject>();
		for(int i = 0 - (visibleGridSize / 2); i < visibleGridSize / 2; i++) {
			GameObject temp = Instantiate(cube, new Vector3(startingPosition + (i * cubeWidth), 0, cubeWidth), Quaternion.identity);
			temp.GetComponent<BasicGroundBehavior>().effectivePosition = new BasicGroundBehavior.EffectivePositionPoint(i, 1);
			testList.Add(temp);
		}
		testGrid.AddRow(testList.ToArray());
		testGrid.MoveBottomRowUp();
		Debug.Log("bottomRowmembers " + testGrid.BottomRow.Length.ToString());
		foreach(LinkedGrid.GridNode node in testGrid.TopRow) {
			Debug.Log(node.Data);
			node.Data.transform.position -= new Vector3(0, 0, -cubeWidth * 2);
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
			temp.GetComponent<BasicGroundBehavior>().ModifyPosition((int)first.transform.position.x - (int)cubeWidth, x - visibleGridSize / 2, 1);
			gridLine.AddFirst(temp);
			currentCube = currentCube.Previous; 
		}
		else if(x > playerPos) {
			currentCube = currentCube.Next;
			GameObject temp = gridLine.First.Value;
			gridLine.RemoveFirst();
			GameObject last = gridLine.Last.Value;
			temp.transform.position = new Vector3(last.transform.position.x + cubeWidth, temp.transform.position.y, 0);
			temp.GetComponent<BasicGroundBehavior>().ModifyPosition((int)last.transform.position.x + (int)cubeWidth, x + Mathf.CeilToInt(visibleGridSize / 2) + 2, 1);
			gridLine.AddLast(temp);
		}
		RotateCubesTowardsPlayer();
		playerPos = x;
	}

	void RotateCubesTowardsPlayer() {
		currentCube.Next.Value.transform.rotation = Quaternion.AngleAxis(5, Vector3.forward);
		currentCube.Previous.Value.transform.rotation = Quaternion.AngleAxis(-5, Vector3.forward);
		currentCube.Value.transform.rotation = Quaternion.identity;
	}
}
