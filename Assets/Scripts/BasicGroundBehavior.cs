using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicGroundBehavior : MonoBehaviour {

	public static GridManager gridManager;
	static AnimationCurve movementFunc;
	static float totalPeriod = 3;
	private Vector3 startPos;
	public int effectivePosition;
	public int realPosition;
	// Use this for initialization
	void Start () {
		startPos = transform.position;
		if(movementFunc == null) {
			movementFunc = new AnimationCurve();
			float t = 0;
			while(t < Mathf.PI * 2) {
				movementFunc.AddKey(new Keyframe(t, Mathf.Sin(t)));
				t += Time.deltaTime;
			}
			movementFunc.postWrapMode = WrapMode.Loop;
		}
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3(startPos.x, startPos.y + movementFunc.Evaluate(Time.time + effectivePosition), startPos.z);
	}

	void OnTriggerEnter() {
		gridManager.UpdatePlayerPosition(effectivePosition, gameObject);
	}

	public void ModifyPosition(int newX, int newEffectivePosition) {
		startPos.x = newX;
		effectivePosition = newEffectivePosition;
	}

	public void TiltCube(float angle, Vector3 axis){
		StartCoroutine(Tilt(angle, axis));
	}

	IEnumerator Tilt(float angle, Vector3 axis) {
		float t = 0;
		yield return null;
	}
}
