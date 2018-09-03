using UnityEngine;
using System.Collections;

public class ChessChongzhi : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	// Update is called once per frame
	void Update () {
	
	}
	public static QIZI[]pos = new QIZI[400];//最大话，避免象棋走棋出现重叠
	public static int Count;//统计的步数初始化为0
	public struct HUIQIposition{
		public int x;
		public int y;
	}
	public struct QIZI{
		public HUIQIposition From;
		public HUIQIposition To;
		public GameObject obj1;//第一个框
		public GameObject obj2;//第二个框
		public GameObject objfrist;//第一个名字
		public GameObject objsconde;//第二个棋子名字
		public bool BlackOrRed;//当前是红旗吃黑棋  还是黑棋吃红旗//true 红吃黑   false 黑吃红
		public HUIQIposition ChessID;//吃子时候的两个ID
	}
	//下面用来悔棋
	//开始位置的坐标和终点位置的坐标   
	public void AddChess(int count,int fromx,int fromy,int tox,int toy,bool IsTrueOrfalse,int ID1,int ID2){//每次走棋都把棋子位置添加进去
		GameObject item1 = ItemOne (fromx, fromy, tox, toy);//得到第一个框名字
		GameObject item2 = ItemTow (fromx, fromy, tox, toy);//得到第二个框名字
		//如果是吃子
		GameObject firstChess = chessOne (item1);//得到第一个旗子名字
		GameObject scondeChess = ChessTwo (item2);//得到第二个棋子名字
		pos [count].From.x = fromx;
		pos [count].From.y = fromy;
		pos [count].To.x = tox;
		pos [count].To.y = toy;
		pos [count].obj1 = item1;
		pos [count].obj2 = item2;
		pos [count].objfrist = firstChess;
		pos [count].objsconde = scondeChess;
		pos [count].BlackOrRed = IsTrueOrfalse;//判断当前是红吃黑还是黑吃红
		pos [count].ChessID.x = ID1;
		pos [count].ChessID.y = ID2;
		Count++;
	}
	//得到第一个旗子名字
	public GameObject chessOne(GameObject obj){
		string s = "";
		GameObject game = null;
		foreach(Transform child in obj.transform)
			s=child.name;//第一个象棋名字
		game = GameObject.Find (s);
		return game;
	}
	//得到第二个旗子名字
	public GameObject ChessTwo(GameObject obj){
		string s = "";
		GameObject game = null;
		foreach(Transform child in obj.transform)
			s=child.name;//第二个象棋名字
		game = GameObject.Find (s);
		return game;
	}
	//得到第二个旗子名字
	//得到第二个对象名字
	public GameObject ItemTow(int fromx,int fromy,int tox,int toy){//得到点击目标位置gameobject的对象名字
		GameObject obj;
		string s3="";
		for (int i=1; i<=90; i++) {
			obj = GameObject.Find("item"+i.ToString());
			int x=System.Convert.ToInt32((obj.transform.localPosition.x)/130);
			int y = System.Convert.ToInt32(Mathf.Abs((obj.transform.localPosition.y)/128));
			if(x==tox&&y==toy)
				s3=obj.name;
		}
		obj = GameObject.Find (s3);
		return obj;
	}
	//得到第一个对象名字
	public GameObject ItemOne(int fromx,int fromy,int tox,int toy){//得到开始位置gameobject的对象名字
		GameObject obj;
		string s3="";
		for (int i=1; i<=90; i++) {
			obj = GameObject.Find("item"+i.ToString());
			int x=System.Convert.ToInt32((obj.transform.localPosition.x)/130);
			int y = System.Convert.ToInt32(Mathf.Abs((obj.transform.localPosition.y)/128));
			if(x==fromx&&y==fromy)
				s3=obj.name;
		}
		obj = GameObject.Find (s3);
		return obj;
	}
	//开始悔棋功能了
		int jiluCont = 0;//如果现在是用户胜利，点击悔棋
	public void IloveHUIQI(){
	
		GameObject obj = GameObject.Find ("chessRENJI");
		obj.transform.localPosition = new Vector3 (8888, 8888, 0);
		for(int i=1;i<=90;i++)
		{
			GameObject Clear = GameObject.Find("prefabs"+i.ToString());
			Destroy(Clear);
		}
		if (blackclick.ChessMove == false && yemiantiaozhuang.ChessPeople == 1) {
			if(blackclick.str=="红色棋子胜利"){
				//人机状态时候红色棋子胜利悔棋
				yemiantiaozhuang.ChessPeople = 2;//先把他等于2
				jiluCont++;
			}
			return;
		}if (yemiantiaozhuang.ChessPeople == 1) {//如果是人机 就让他悔棋两部
			if (Count <= 1)
				return;
			int f = Count - 1;
			int s = Count - 2;
			int oneID = pos [f].ChessID.x;//黑色旗子原来位置ID
			int twoID = pos [f].ChessID.y;//黑色旗子移动到的位置的ID
			int threeID = pos [s].ChessID.x;//红色旗子原来位置的ID
			int forID = pos [s].ChessID.y;//红色旗子移动到的位置ID 
			//print (pos [f].From.x + "," + pos [f].From.y + "--" + pos [f].To.x + "," + pos [f].To.y);
			//print (board.chess [0, 0]);
			GameObject obj1, obj2, obj3, obj4;
			bool IsfalseOrtrue;
			obj1 = pos [f].obj1;//第一个款
			obj2 = pos [f].obj2;//第二个框
			obj3 = pos [f].objfrist;//第一个旗子
			obj4 = pos [f].objsconde;//第二个旗子
			IsfalseOrtrue = pos [f].BlackOrRed;//现在红色还是黑色
			GameObject o1, o2, o3, o4;
			bool IstrueOrfalse;
			o1 = pos [s].obj1;//第一个款
			o2 = pos [s].obj2;//第二个框
			o3 = pos [s].objfrist;//第一个旗子
			o4 = pos [s].objsconde;//第二个旗子
			IstrueOrfalse = pos [s].BlackOrRed;

			if (obj4 != null) {//黑色等于吃子
				obj3.transform.parent = obj1.transform;
				obj4.transform.parent = obj2.transform;
				obj3.transform.localPosition = Vector3.zero;
				obj4.transform.localPosition = Vector3.zero;
				board.chess [pos [f].From.y, pos [f].From.x] = oneID;
				board.chess [pos [f].To.y, pos [f].To.x] = twoID;
				if (o4 != null) {//红色吃子状态
					o3.transform.parent = o1.transform;
					o4.transform.parent = o2.transform;
					o3.transform.localPosition = Vector3.zero;
					o4.transform.localPosition = Vector3.zero;
					board.chess [pos [s].From.y, pos [s].From.x] = threeID;
					board.chess [pos [s].To.y, pos [s].To.x] = forID;
				} else {
					o3.transform.parent = o1.transform;
					o3.transform.localPosition = Vector3.zero;
					board.chess [pos [s].From.y, pos [s].From.x] = threeID;
					board.chess [pos [s].To.y, pos [s].To.x] = 0;
				}
			} else {//黑色等于走棋
				obj3.transform.parent = obj1.transform;
				obj3.transform.localPosition = Vector3.zero;
				board.chess [pos [f].From.y, pos [f].From.x] = oneID;
				board.chess [pos [f].To.y, pos [f].To.x] = 0;
				if (o4 != null) {
					o3.transform.parent = o1.transform;
					o4.transform.parent = o2.transform;
					o3.transform.localPosition = Vector3.zero;
					o4.transform.localPosition = Vector3.zero;
					board.chess [pos [s].From.y, pos [s].From.x] = threeID;
					board.chess [pos [s].To.y, pos [s].To.x] = forID;
				} else {
					o3.transform.parent = o1.transform;
					o3.transform.localPosition = Vector3.zero;
					board.chess [pos [s].From.y, pos [s].From.x] = threeID;
					board.chess [pos [s].To.y, pos [s].To.x] = 0;
				}
		
			}
			blackclick.ChessMove = true;
			Count -= 2;
			blackclick.TrueOrFalse = true;//在将帅被吃了的情况下 给他能走棋
			blackclick.str = "红方走";
			KingPosition.JiangJunCheck ();


		} else if (yemiantiaozhuang.ChessPeople == 2) {
			if(jiluCont>=1){
				yemiantiaozhuang.ChessPeople=1;
				jiluCont=0;
			}
			if(Count<=0)
				return;
			int f = Count - 1;
			//int s = Count - 2;
			int oneID = pos [f].ChessID.x;//黑色旗子原来位置ID
			int twoID = pos [f].ChessID.y;//黑色旗子移动到的位置的ID
			GameObject obj1, obj2, obj3, obj4;
			bool IsfalseOrtrue;
			obj1 = pos [f].obj1;//第一个款
			obj2 = pos [f].obj2;//第二个框
			obj3 = pos [f].objfrist;//第一个旗子
			obj4 = pos [f].objsconde;//第二个旗子
			IsfalseOrtrue = pos [f].BlackOrRed;//现在红色还是黑色
			GameObject o1, o2, o3, o4;
			bool IstrueOrfalse;
			if (obj4 != null) {//最后一步等于吃子
				obj3.transform.parent = obj1.transform;
				obj4.transform.parent = obj2.transform;
				obj3.transform.localPosition = Vector3.zero;
				obj4.transform.localPosition = Vector3.zero;
				board.chess [pos [f].From.y, pos [f].From.x] = oneID;
				board.chess [pos [f].To.y, pos [f].To.x] = twoID;

			}
			else{
				obj3.transform.parent = obj1.transform;
				obj3.transform.localPosition = Vector3.zero;
				board.chess [pos [f].From.y, pos [f].From.x] = oneID;
				board.chess [pos [f].To.y, pos [f].To.x] = 0;
			}
			if(blackclick.ChessMove==false){
				blackclick.str = "红方走";
				blackclick.ChessMove = true;
			}
			else{
				blackclick.ChessMove=false;
				blackclick.str = "黑方走";
			}Count -= 1;
			blackclick.TrueOrFalse = true;//在将帅被吃了的情况下 给他能走棋
			KingPosition.JiangJunCheck ();
		} 
		else {
			print("return");
			return;
		}
	}



}
