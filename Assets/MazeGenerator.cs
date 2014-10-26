using UnityEngine;
using System.Collections.Generic;

public class MazeGenerator : MonoBehaviour {


	Stack<int[]> endPaths= new Stack<int[]>();
	int[] END=new int[2];
	int[] START=new int[2];
	//ArrayList deadEnds=new ArrayList();
	static int numRows=10;
	static int numCols=10;

	CellScript[,] grid=new CellScript[numRows,numCols];

	//Debug.Log (count3);
	public GameObject Front;
	public GameObject Back;
	public GameObject Left;
	public GameObject Right;
	public GameObject FrontBack;
	public GameObject FrontLeft;
	public GameObject FrontRight;
	public GameObject BackLeft;
	public GameObject BackRight;
	public GameObject LeftRight;
	public GameObject FrontBackLeft;
	public GameObject FrontBackRight;
	public GameObject FrontLeftRight;
	public GameObject BackLeftRight;
	public GameObject EmptyRoom;
	public GameObject pumpkin;
	GameObject[,] maze= new GameObject[numRows,numCols];

	List<int[]> CheckNeighbors(int[] CUR, CellScript[,] tempGrid){
		List<int[]> answers = new List<int[]> ();
		int[] temp = new int[2];
		if(CUR[0]-1>=0 && tempGrid[CUR[0]-1,CUR[1]].visited==false){
			temp[0]=CUR[0]-1;
			temp[1]=CUR[1];
			answers.Add((int[])temp.Clone ());
		}
		if(CUR[0]+1<numRows && tempGrid[CUR[0]+1,CUR[1]].visited==false){
			temp[0]=CUR[0]+1;
			temp[1]=CUR[1];
			answers.Add((int[])temp.Clone ());
		}
		if(CUR[1]-1>=0 && tempGrid[CUR[0],CUR[1]-1].visited==false){
			temp[0]=CUR[0];
			temp[1]=CUR[1]-1;
			answers.Add((int[])temp.Clone ());
		}
		if(CUR[1]+1<numCols && tempGrid[CUR[0],CUR[1]+1].visited==false){
			temp[0]=CUR[0];
			temp[1]=CUR[1]+1;
			answers.Add((int[])temp.Clone ());
		}
		return answers;
	}

	List<int[]> CheckDirections(int[] CUR, CellScript[,] tempGrid){
		List<int[]> answers = new List<int[]> ();
		int[] temp = new int[2];
		if(CUR[0]-1>=0 && tempGrid[CUR[0]-1,CUR[1]].back==true){
			if(tempGrid[CUR[0]-1,CUR[1]].WallsUp()>=3 || tempGrid[CUR[0]-1,CUR[1]].visited == true){
				temp[0]=CUR[0]-1;
				temp[1]=CUR[1];
				answers.Add((int[])temp.Clone ());
			}
		}
		if(CUR[0]+1<numRows && tempGrid[CUR[0]+1,CUR[1]].front==true){
			if(tempGrid[CUR[0]+1,CUR[1]].WallsUp()>=3 || tempGrid[CUR[0]+1,CUR[1]].visited == true){
				temp[0]=CUR[0]+1;
				temp[1]=CUR[1];
				answers.Add((int[])temp.Clone ());
			}
		}
		if(CUR[1]-1>=0 && tempGrid[CUR[0],CUR[1]-1].right==true){
			if(tempGrid[CUR[0],CUR[1]-1].WallsUp()>=3 || tempGrid[CUR[0],CUR[1]-1].visited == true){
				temp[0]=CUR[0];
				temp[1]=CUR[1]-1;
				answers.Add((int[])temp.Clone ());
			}
		}
		if(CUR[1]+1<numCols && tempGrid[CUR[0],CUR[1]+1].left==true){
			if(tempGrid[CUR[0],CUR[1]+1].WallsUp()>=3 || tempGrid[CUR[0],CUR[1]+1].visited == true){
				temp[0]=CUR[0];
				temp[1]=CUR[1]+1;
				answers.Add((int[])temp.Clone ());
			}
		}
		return answers;
	}

	// Use this for initialization
	void Start () {


		for (int x=0; x<numRows; x++) {
			for(int y=0;y<numCols;y++){
				grid[x,y]=new CellScript();
			}		
		}
		START [0] = 0;
		START [1] = 0;
		END [0] = numRows-1;
		END [1] = numCols-1;
		grid [START[0],START[1]].visited = true;
		endPaths.Push (START);
		int[] next = new int[2];
		next [0] = START [0];
		next [1] = START [1];
		int count3 = 0;
		do{
			List<int[]> validNeighbors = CheckNeighbors((int[]) endPaths.Peek (),grid);
			if (validNeighbors.Count == 0){
				//make hard copy of endPaths and add onto deadEnds
				//pop the last entry of endPaths
				//Debug.Log("before pop, stack size=" + endPaths.Count);
				endPaths.Pop ();
				//Debug.Log ("popped: " + check[0] + " " + check[1]);
				//Debug.Log("after pop, stack size=" + endPaths.Count);
				//Debug.Log ("after pop cur is: " + top[0] + " " + top[1]);

			}
			else{
				//take a random direction to go into
				int hold2 = validNeighbors.Count;
				int hold = Random.Range(0,hold2);
				int[] temp2=(int[])validNeighbors[hold].Clone ();
				next[0]=temp2[0];
				next[1]=temp2[1];
				grid[next[0],next[1]].visited = true;
				//break wall
				int[] diff = new int[2];
				diff[0] = next[0]-((int[])(endPaths.Peek ()))[0];
				diff[1] = next[1]-((int[])(endPaths.Peek ()))[1];
				if (diff[0] == -1){
					int[] temp=(int[])endPaths.Peek ();
					grid[temp[0],temp[1]].front = false;
					grid[next[0],next[1]].back = false;
				}
				if (diff[0] == 1){
					int[] temp=(int[])endPaths.Peek ();
					grid[temp[0],temp[1]].back = false;
					grid[next[0],next[1]].front = false;
				}
				if (diff[1] == -1){
					int[] temp=(int[])endPaths.Peek ();
					grid[temp[0],temp[1]].left = false;
					grid[next[0],next[1]].right = false;
				}
				if (diff[1] == 1){
					int[] temp=(int[])endPaths.Peek ();
					grid[temp[0],temp[1]].right = false;
					grid[next[0],next[1]].left = false;
				}
				endPaths.Push ((int[])next.Clone ());
				count3++;
			}
		}
		while(!(next[0] == END[0] && next[1]==END[1]));
		/*while (endPaths.Count!=0) {
			int[] stuff=(int[])endPaths.Pop ();
			Debug.Log(stuff[0]+" "+stuff[1]);
		}*/
		List<int[]> BWalls=new List<int[]>();
		int[] stuff4=new int[2];
		for (int q=0; q<numRows; q++) {
			for (int w=0; w<numCols; w++) {
				stuff4 [0] = q;
				stuff4 [1] = w;
				if (grid [q, w].visited == false) {
					BWalls = CheckDirections ((int[])stuff4.Clone (), grid);
					int hold2 = BWalls.Count;
					if (hold2 != 0) {
						int hold = Random.Range (0, hold2);
						int[] temp6 = (int[])BWalls[hold].Clone ();
						if (q - temp6 [0] == -1) {
							grid [q, w].back = false;
							grid [temp6 [0], temp6 [1]].front = false;
						}
						if (q - temp6 [0] == 1) {
							grid [q, w].front = false;
							grid [temp6 [0], temp6 [1]].back = false;
						}
						if (w - temp6 [1] == -1) {
							grid [q, w].right = false;
							grid [temp6 [0], temp6 [1]].left = false;
						}
						if (w - temp6 [1] == 1) {
							grid [q, w].left = false;
							grid [temp6 [0], temp6 [1]].right = false;
						}
					}
				}
			}
		}
		List<int[]> pumpkinSpawns = new List<int[]> ();
		for(int r=0;r<numRows;r++){
			for(int c=0;c<numCols;c++){
				if (r == numRows-1 && c == numCols-1) grid[r,c].back = false;

				if (grid[r,c].front == true && grid[r,c].back == false && grid[r,c].left == false && grid[r,c].right == false){
					maze[r,c] = Front;
				}
				else if (grid[r,c].front == false && grid[r,c].back == true && grid[r,c].left == false && grid[r,c].right == false){
					maze[r,c] = Back;
				}
				else if (grid[r,c].front == false && grid[r,c].back == false && grid[r,c].left == true && grid[r,c].right == false){
					maze[r,c] = Left;
				}
				else if (grid[r,c].front == false && grid[r,c].back == false && grid[r,c].left == false && grid[r,c].right == true){
					maze[r,c] = Right;
				}
				else if (grid[r,c].front == true && grid[r,c].back == true && grid[r,c].left == false && grid[r,c].right == false){
					maze[r,c] = FrontBack;
				}
				else if (grid[r,c].front == true && grid[r,c].back == false && grid[r,c].left == true && grid[r,c].right == false){
					maze[r,c] = FrontLeft;
				}
				else if (grid[r,c].front == true && grid[r,c].back == false && grid[r,c].left == false && grid[r,c].right == true){
					maze[r,c] = FrontRight;
				}
				else if (grid[r,c].front == false && grid[r,c].back == true && grid[r,c].left == true && grid[r,c].right == false){
					maze[r,c] = BackLeft;
				}
				else if (grid[r,c].front == false && grid[r,c].back == true && grid[r,c].left == false && grid[r,c].right == true){
					maze[r,c] = BackRight;
				}
				else if (grid[r,c].front == false && grid[r,c].back == false && grid[r,c].left == true && grid[r,c].right == true){
					maze[r,c] = LeftRight;
				}
				else if (grid[r,c].front == true && grid[r,c].back == true && grid[r,c].left == true && grid[r,c].right == false){
					maze[r,c] = FrontBackLeft;
					if (r != 0 && c !=0) {
						int[] t = new int[2];
						t[0] = r;
						t[1] = c;
						pumpkinSpawns.Add (t);
					}
				}
				else if (grid[r,c].front == true && grid[r,c].back == true && grid[r,c].left == false && grid[r,c].right == true){
					maze[r,c] = FrontBackRight;
					if (r != 0 && c !=0) {
						int[] t = new int[2];
						t[0] = r;
						t[1] = c;
						pumpkinSpawns.Add (t);
					}
				}
				else if (grid[r,c].front == true && grid[r,c].back == false && grid[r,c].left == true && grid[r,c].right == true){
					maze[r,c] = FrontLeftRight;
					if (r != 0 && c !=0) {
						int[] t = new int[2];
						t[0] = r;
						t[1] = c;
						pumpkinSpawns.Add (t);
					}
				}
				else if (grid[r,c].front == false && grid[r,c].back == true && grid[r,c].left == true && grid[r,c].right == true){
					maze[r,c] = BackLeftRight;
					if (r != 0 && c !=0) {
						int[] t = new int[2];
						t[0] = r;
						t[1] = c;
						pumpkinSpawns.Add (t);
					}
				}
				else {
					maze[r,c] = EmptyRoom;
				}
				maze[r,c] = Instantiate(maze[r,c],new Vector3(r*5,0,c*5),maze[r,c].transform.rotation) as GameObject;
			}
		}

		while (pumpkinSpawns.Count > 3) {
			int ran = Random.Range (0,pumpkinSpawns.Count);
			pumpkinSpawns.RemoveAt (ran);
		}
		if (pumpkinSpawns.Count > 0) {
			GameObject[] pumpkins = new GameObject[pumpkinSpawns.Count];
			for (int i = 0;i<pumpkinSpawns.Count;i++) {
				pumpkins[i] = Instantiate (pumpkin, new Vector3(pumpkinSpawns[i][0]*5, 2.5f, pumpkinSpawns[i][1]*5), pumpkin.transform.rotation) as GameObject;
				(pumpkins[i] as GameObject).transform.localScale = new Vector3(0.05f,0.05f,0.075f);
				if (grid[pumpkinSpawns[i][0],pumpkinSpawns[i][1]].front == false) pumpkins[i].transform.Rotate (new Vector3(0,0,270));
				else if (grid[pumpkinSpawns[i][0],pumpkinSpawns[i][1]].left == false) pumpkins[i].transform.Rotate (new Vector3(0,0,180));
				else if (grid[pumpkinSpawns[i][0],pumpkinSpawns[i][1]].back == false) pumpkins[i].transform.Rotate (new Vector3(0,0,90));
			}
		}
		
	}
	
	// Update is called once per frame
	void Update () {


	}
}
