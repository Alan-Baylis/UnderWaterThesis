using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkedGrid {

	public class GridNode {
		public GridNode North;
		public GridNode South;
		public GridNode West;
		public GridNode East;
		public GameObject Data;
	}

	public GridNode NWCorner;
	public GridNode NECorner;
	public GridNode SWCorner;
	public GridNode SECorner;

	public GridNode[] TopRow {
		get {
			List<GridNode> topRow = new List<GridNode>();
			for(GridNode i = NWCorner; i != NECorner; i = i.East) {
				topRow.Add(i);
			}
			topRow.Add(NECorner);
			return topRow.ToArray();
		}

		set {
			GridNode anchor = this.TopRow[0];
			for(int i = 0; i < value.Length; i++) {
				anchor.North = value[i];
				value[i].South = anchor;
				anchor = anchor.East;
			}
			NWCorner = value[0];
			NECorner = value[value.Length - 1];
		}
	}

	public GridNode[] BottomRow {
		get {
			List<GridNode> bottomRow = new List<GridNode>();
			for(GridNode i = SWCorner; i != SECorner; i = i.East) {
				bottomRow.Add(i);
			}
			bottomRow.Add(SECorner);
			return bottomRow.ToArray();
		}
		set {
			GridNode anchor = this.BottomRow[0];
			for(int i = 0; i < value.Length; i++) {
				anchor.South = value[i];
				value[i].North = anchor;
				anchor = anchor.East;
			}
			SWCorner = value[0];
			SECorner = value[value.Length - 1];
		}
	}

	public GridNode[] LeftColumn {
		get {
			List<GridNode> leftColumn = new List<GridNode>();
			for(GridNode i = NWCorner; i != SWCorner; i = i.South) {
				leftColumn.Add(i);
			}
			leftColumn.Add(SWCorner);
			return leftColumn.ToArray();
		}
		set {
			GridNode anchor = this.LeftColumn[0];
			for(int i = 0; i < value.Length; i++) {
				anchor.West = value[i];
				value[i].East = anchor;
				anchor = anchor.South;
			}
		}
	}

	public GridNode[] RightColumn {
		get {
			List<GridNode> rightColumn = new List<GridNode>();
			for(GridNode i = NECorner; i != SECorner; i = i.South) {
				rightColumn.Add(i);
			}
			rightColumn.Add(SECorner);
			return rightColumn.ToArray();
		}
		set {
			GridNode anchor = this.RightColumn[0];
			for(int i = 0; i < value.Length; i++) {
				anchor.East = value[i];
				value[i].West = anchor;
				anchor = anchor.South;
			}
		}
	}

	public void AddNWCorner(GameObject data) {
		GridNode newNode = new GridNode();

		newNode.Data = data;
		newNode.East = NWCorner;
		NECorner = newNode;
	}

	public void AddNECorner(GameObject data) {
		GridNode newNode = new GridNode();

		newNode.Data = data;
		newNode.West = NECorner;
		NWCorner = newNode;
	}

	public void AddSWCorner(GameObject data) {
		GridNode newNode = new GridNode();

		newNode.Data = data;
		newNode.East = SWCorner;
		SWCorner = newNode;
	}

	public void AddSECorner(GameObject data) {
		GridNode newNode = new GridNode();

		newNode.Data = data;
		newNode.West = SECorner;
		SECorner = newNode;
	}

	public void AddTopRow(GameObject[] newRow) {
		GridNode newCorner = new GridNode();

		newCorner.Data = newRow[0];
		NWCorner = newCorner;
		SWCorner = newCorner;
		Debug.Log("Adding top row");
		for(int i = 1; i < newRow.Length; i++) {
			GridNode newNode = new GridNode();
			newNode.Data = newRow[i];
			Debug.Log(newRow[i]);
			newNode.West = newCorner;
			newCorner.East = newNode;

			newCorner = newNode;
		}
		NECorner = newCorner;
		SECorner = newCorner;
	}

	public void AddRow(GameObject[] newRow) {
		GridNode anchor = SWCorner;

		GridNode lastNode = new GridNode();
		lastNode.Data = newRow[0];
		lastNode.North = anchor;
		anchor.South = lastNode;
		SWCorner = lastNode;
		Debug.Log("Adding row" + newRow.Length.ToString());
		for(int i = 0; i < newRow.Length - 1; i++) {
			anchor = anchor.East;
			GridNode temp = new GridNode();
			temp.Data = newRow[i];
			temp.West = lastNode;
			temp.North = anchor;
			anchor.South = temp;
			lastNode.East = temp;

			lastNode = temp;
		}

		SECorner = lastNode;
	}

	public void AddFirstColumn(GameObject[] newColumn) {
		GridNode newCorner = new GridNode();

		newCorner.Data = newColumn[0];
		NWCorner = newCorner;
		for(int i = 1; i < newColumn.Length; i++) {
			GridNode newNode = new GridNode();
			newNode.Data = newColumn[i];
			newNode.North = newCorner;
			newCorner.South = newNode;

			newCorner = newNode;
			}
		SWCorner = newCorner;
		NECorner = NWCorner;
		}

	public void AddColumn(GameObject[] newColumn) {
		GridNode anchor = NECorner;

		 GridNode lastNode = new GridNode();
		 lastNode.Data = newColumn[0];
		 lastNode.West = anchor;
		 NECorner = lastNode;

		 for(int i = 0; i < newColumn.Length; i++) {
			anchor = anchor.South;
			GridNode temp = new GridNode();
			temp.Data = newColumn[i];
			temp.North = lastNode;
			temp.West = anchor;

			lastNode = temp;
		 }

		 SECorner = lastNode;
	}

	public void MoveTopRowDown() {
		GridNode[] oldTopRow = this.TopRow;
		NWCorner = oldTopRow[0].South;
		NECorner = oldTopRow[oldTopRow.Length - 1].South;
		foreach(GridNode node in this.TopRow) {
			node.North = null;
		}
		this.BottomRow = oldTopRow;
	}

	public void MoveBottomRowUp() {
		GridNode[] oldBottomRow = this.BottomRow;
		SWCorner = oldBottomRow[0].North;
		SECorner = oldBottomRow[oldBottomRow.Length - 1].North;
		foreach(GridNode node in this.BottomRow) {
			node.South = null;
		}

		this.TopRow = oldBottomRow;
	}

	public void MoveLeftColumnRight() {
		GridNode[] oldLeftColumn = this.LeftColumn;
		NWCorner = oldLeftColumn[0].East;
		SWCorner = oldLeftColumn[oldLeftColumn.Length -1].East;
		foreach(GridNode node in this.LeftColumn) {
			node.West = null;
		}

		this.RightColumn = oldLeftColumn;
	}

	public void MoveRightColumnLeft() {
		GridNode[] oldRightColumn = this.RightColumn;
		NECorner = oldRightColumn[0].West;
		SECorner = oldRightColumn[oldRightColumn.Length - 1].West;
		foreach(GridNode node in this.RightColumn) {
			node.East = null;
		}

		this.LeftColumn = oldRightColumn;
	}
}
