using UnityEngine;
using System.Collections.Generic;

public class CellScript{

	public bool front = true;
	public bool back = true;
	public bool left = true;
	public bool right = true;
	public bool visited = false;

	public void printType(){
		Debug.Log (front + " " + back + " " + left + " " + right);
	}

	public int WallsUp(){
		int count = 0;

		if (front == true) {
			count++;
		}
		if (back == true) {
			count++;
		}
		if (left == true) {
			count++;
		}
		if (right == true) {
			count++;
		}
		return count;
	}

	// Use this for initialization
	void Start () {
	}


	
	// Update is called once per frame
	void Update () {
	
	}
}
