using UnityEngine;
using System.Collections;
using System.Threading;
public class blackclick : MonoBehaviour {
	public static int FromX=-1, ToX=-1, ToY=-1, FromY=-1;
	public static GameObject ObjBlack=null,ObjRed=null;//红色对象，和黑色对象
	public UILabel Lab;
	public static bool bdfdd = true;//测试
	public static string str="红方走";
	public static bool ChessMove =true;//true   redMove   false BlackMove
	public static bool TrueOrFalse=true;//判断这个时候输赢状态能否走棋  //重新开始记得该true
	public static string RedName=null,BlackName=null,ItemName;//blackchessname  and   redchessname
	rules re = new rules ();
	public int bestmove;
	public UIToggle tog;
	public Blackmove.CHESSMOVE chere;
	public GameObject renji;//识别人机走到哪里
	public static bool posthread=true;//判断线程里面的内容是否执行完毕
	Canmovetishi can = new Canmovetishi();
// Use this for initialization
	//控制窗口，不让窗口乱动

	void Start () {
		GameObject obj = GameObject.Find("tishi");
		Lab= obj.GetComponent<UILabel> ();
		}
	
	// Update is called once per frame
	void Update () {
		Lab.text = str;

	}
	//得到点击的象棋名字
	//判断点击到的是什么？
	//0是空   1 是黑色   2 是红色
	public int IsBlackOrRed(int x,int y){
		int Count = board.chess [y, x];
		if (Count == 0)
			return 0;
		else if (Count > 0 && Count < 8)//是黑色
			return 1;
		else  //是红色 
			return 2;
	}
	public void BlackNameOrRedName(GameObject obj){//得到棋子的名字
	if (obj.name.Substring (0, 1) == "r")
			RedName = obj.name;//得到red名字
		else if (obj.name.Substring (0, 1) == "b")
			BlackName = obj.name;//得到black名字
		else 
			ItemName = obj.name;//得到item名字
	}
	//移动类
	public void IsMove(string One,GameObject game ,int x1,int y1,int x2,int y2){
	    GameObject parent1 = GameObject.Find (One);
		parent1.transform.parent = game.transform;
		parent1.transform.localPosition = Vector3.zero;
		board.chess [y2, x2] = board.chess[y1,x1];
		board.chess [y1, x1] = 0;
	}
	//吃子类
	public void IsEat(string Frist,string sconde,int x1,int y1,int x2,int y2){
	GameObject Onename = GameObject.Find (Frist);//得到第一个
		GameObject Twoname = GameObject.Find (sconde);//得到第二个名字
		GameObject Twofather = Twoname.gameObject.transform.parent.gameObject;//得到第二个的父亲
		Onename.gameObject.transform.parent = Twofather.transform;
		Onename.transform.localPosition = Vector3.zero;
	//	Destroy (Twoname);
		//Twoname.transform.localPosition = new Vector3 (1000, 10000, 0);
		board.chess [y2, x2] = board.chess [y1, x1];
		board.chess [y1, x1] = 0;
		GameObject a = GameObject.Find ("xiaoshi");
		Twoname.transform.parent = a.transform;
		Twoname.transform.localPosition = new Vector3(5000,5000,0);
	}
	//用来悔棋功能
	//点击事件
	//播放音乐
	 ChessChongzhi chzh = new ChessChongzhi();
	SearchEngine see = new SearchEngine();
	Blackmove mo = new Blackmove();
	public void IsClickCheck(){	
		renji = GameObject.Find("chessRENJI");
		if (TrueOrFalse == false)
			return;
		GameObject obj = UICamera.hoveredObject;
		BlackNameOrRedName (obj);//是否点击到棋子  如果是  就得到棋子
		if (obj.name.Substring (0, 1) != "i")
			obj = obj.gameObject.transform.parent.gameObject;//得到他的父容器
		int x=System.Convert.ToInt32((obj.transform.localPosition.x)/130);
		int y = System.Convert.ToInt32(Mathf.Abs((obj.transform.localPosition.y)/128));
		int Result = IsBlackOrRed (x, y);//判断点击到了什么
		switch (Result) {
		case 0://点击到了空  是否要走棋
			//如果点击到了空格  就把对象清空
			//p.MusicPlay();
			for(int i=1;i<=90;i++)
			{
				GameObject Clear = GameObject.Find("prefabs"+i.ToString());
				Destroy(Clear);
			}
			if(posthread ==false)
				return ;
			//can.ClickChess(FromX,FromY);
			ToY = y;
			ToX = x;
			if(ChessMove){//红色走
				if(RedName == null)
					return;
				string sssRed = RedName;//记录红色棋子的名字
			bool ba = rules.IsValidMove(board.chess,FromX,FromY,ToX,ToY);
			if(!ba)
					return;

				int a = board.chess[FromY,FromX];
				int b = board.chess[ToY,ToX];
				chzh.AddChess(ChessChongzhi.Count,FromX,FromY,ToX,ToY,true,a,b);
				IsMove(RedName,obj,FromX,FromY,ToX,ToY);//走了
				str = "黑方走";
				KingPosition.JiangJunCheck();
				ChessMove = false;
				//getString();
				if(str=="红色棋子胜利")
					return ;//因为没有携程关系  每次进入黑色走棋的时候都判断 棋局是否结束
				if(yemiantiaozhuang.ChessPeople==2)
				{//如果现在是双人对战模式
					BlackName = null;
					RedName = null;
					return;
				}
				if (ChessMove == false)
					StartCoroutine("Robot");
			//执行走棋

			}
			else{//黑色走
				if(BlackName==null)
					return;
				bool ba = rules.IsValidMove(board.chess,FromX,FromY,ToX,ToY);
				if(!ba)
					return;
				//ChessChongzhi chzh = new ChessChongzhi();
				int a = board.chess[FromY,FromX];
				int b = board.chess[ToY,ToX];
				chzh.AddChess(ChessChongzhi.Count,FromX,FromY,ToX,ToY,true,a,b);
				//看看是否能播放音乐
				IsMove(BlackName,obj,FromX,FromY,ToX,ToY);
			
				//黑色走棋
				ChessMove = true;
				str="红方走";
				KingPosition.JiangJunCheck();
			}
			break;
		case 1://点击到了黑色  是否选中  还是  红色要吃子
			if(posthread ==false)
				return ;
			if(!ChessMove){
				FromX = x;
				FromY = y;
			//	Canmovetishi can = new Canmovetishi();

				for(int i=1;i<=90;i++)
				{
					GameObject Clear = GameObject.Find("prefabs"+i.ToString());
					Destroy(Clear);
				}
				can.ClickChess(FromX,FromY);
			}
			else{
				for(int i=1;i<=90;i++)
				{
					GameObject Clear = GameObject.Find("prefabs"+i.ToString());
					Destroy(Clear);
				}
			if(RedName ==null)
					return;
				ToX = x;
				ToY = y;
				bool ba = rules.IsValidMove(board.chess,FromX,FromY,ToX,ToY);
				if(!ba)
					return;
				//ChessChongzhi chzh = new ChessChongzhi();
				int a = board.chess[FromY,FromX];
				int b = board.chess[ToY,ToX];
				chzh.AddChess(ChessChongzhi.Count,FromX,FromY,ToX,ToY,true,a,b);
				//看看是否能播放音乐
				IsEat(RedName,BlackName,FromX,FromY,ToX,ToY);
			
				//print(RedName+"  "+BlackName+" "+FromX+" "+FromY+" "+ToX+" "+ToY);
					ChessMove = false;
				//getString();
				//红色吃子  变黑色走
				str="黑方走";
				KingPosition.JiangJunCheck();
				if(str=="红色棋子胜利")
					return ;//因为没有携程关系  每次进入黑色走棋的时候都判断 棋局是否结束
				if(yemiantiaozhuang.ChessPeople==2){
					RedName=null;
					BlackName=null;
					return;
				}
				if (ChessMove == false)
					StartCoroutine("Robot");
			}
			break;
		case 2://点击到了红色   是否选中  还是黑色要吃子
			if(posthread ==false)
				return ;
			if(ChessMove){
				FromX=x;
				FromY = y;
				//Canmovetishi can = new Canmovetishi();
				for(int i=1;i<=90;i++)
				{
					GameObject Clear = GameObject.Find("prefabs"+i.ToString());
					Destroy(Clear);
				}
				can.ClickChess(FromX,FromY);
			}
			else{
				for(int i=1;i<=90;i++)
				{
					GameObject Clear = GameObject.Find("prefabs"+i.ToString());
					Destroy(Clear);
				}
				if(BlackName==null)
					return;
					ToX = x;
					ToY = y;
				bool ba = rules.IsValidMove(board.chess,FromX,FromY,ToX,ToY);
				if(!ba)
					return;
				//ChessChongzhi chzh = new ChessChongzhi();
				int a = board.chess[FromY,FromX];
				int b = board.chess[ToY,ToX];
				chzh.AddChess(ChessChongzhi.Count,FromX,FromY,ToX,ToY,true,a,b);
				//看看是否能播放音乐
				IsEat(BlackName,RedName,FromX,FromY,ToX,ToY);

				RedName = null;
				BlackName = null;
				ChessMove = true;
				str="红方走";
				KingPosition.JiangJunCheck();
			}
			break;
	
		}
	
	}
	IEnumerator Robot(){
		yield return new WaitForSeconds (0.2f);
		posthread = false;
		Thread th = new Thread (new ThreadStart (threm));
		th.Start ();
	}
	public void threm(){
		//str="对方正在思考";
		if (ChessMove == false) {
			//print("yes");
			//ChessChongzhi chzh = new ChessChongzhi();
			//SearchEngine see = new SearchEngine();
			//Blackmove mo = new Blackmove();
			chere = see.SearchAGoodMove(board.chess);
				string s1 = "";
				string s2 = "";
				string s3 = "";
				string s4 = "";
				s1 = see.Itemfirname (chere);
				s2 = see.Itemsconname (chere);
			//print(s1+"  "+s2);
				GameObject one = GameObject.Find (s1);
				GameObject two = GameObject.Find (s2);
				foreach (Transform child in one.transform)
					s3 = child.name;//第一个象棋名字
				foreach (Transform child in two.transform)
					s4 = child.name;//吃到的子的象棋名字
				if (s4 == "") {
					int a = board.chess [chere.From.y, chere.From.x];
					int b = board.chess [chere.To.y, chere.To.x];
					chzh.AddChess (ChessChongzhi.Count, chere.From.x, chere.From.y, chere.To.x, chere.To.y, false, a, b);
					IsMove (s3, two, chere.From.x, chere.From.y, chere.To.x, chere.To.y);
					renji.transform.localPosition = one.transform.localPosition;

				} else {
					int a = board.chess [chere.From.y, chere.From.x];
					int b = board.chess [chere.To.y, chere.To.x];
					chzh.AddChess (ChessChongzhi.Count, chere.From.x, chere.From.y, chere.To.x, chere.To.y, false, a, b);
					IsEat (s3, s4, chere.From.x, chere.From.y, chere.To.x, chere.To.y);
					renji.transform.localPosition = one.transform.localPosition;

				}
		
				RedName = null;
				BlackName = null;
				GameObject obj1 = GameObject.Find (s3);
				tog = obj1.GetComponent<UIToggle> ();
				tog.value = true;
				str = "红方走";
				KingPosition.JiangJunCheck ();
				ChessMove = true;
			}

		}

}
