using UnityEngine;
using System.Collections;

public class board : MonoBehaviour {
	//public static ArrayList All;
	public UILabel sprite1;
	public static string s="简单";
	public static string sss1="";
	public static int[,]chess = new int[10, 9]{
	
		{2,3,6,5,1,5,6,3,2},
		{0,0,0,0,0,0,0,0,0},
		{0,4,0,0,0,0,0,4,0},
		{7,0,7,0,7,0,7,0,7},
		{0,0,0,0,0,0,0,0,0},
		{0,0,0,0,0,0,0,0,0},
		{14,0,14,0,14,0,14,0,14},
		{0,11,0,0,0,0,0,11,0},
		{0,0,0,0,0,0,0,0,0},
		{9,10,13,12,8,12,13,10,9}

	};
	public static bool start=true;
	void Start () {
		GameObject obj = GameObject.Find("newnandu");
		sprite1 = obj.GetComponent<UILabel> ();


	}
	
	// Update is called once per frame
	void Update () {
	
	}
	//GameObject ite = (GameObject)Instantiate(Resources.Load("item"));//找到预设体
	public	void text(){                                  //动态添加了90个sprite 并且给它们位置等信息

		UIAtlas atlas;
		int xx=0, yy=0;
		GameObject a = GameObject.Find ("chess");
		atlas = Resources.Load ("")as UIAtlas;   //让他不能找到图片集合
		for (int i=1; i<=90; i++) {
			GameObject ite = (GameObject)Instantiate(Resources.Load("item"));//找到预设体
			ite.transform.parent = a.transform;           //给预设体指定添加到什么地方
			GameObject b = GameObject.Find(ite.name);    //找到这个预设体的名字，给他做一些操作
			b.transform.localScale = new Vector3(1,1,1);
			b.name = "item" + i.ToString ();                                           //suoyou所有的深度 都是5
			b.transform.localPosition = new Vector3(xx,yy,0);
			xx+=130;
			if(xx>=1170){
				yy-=128;
				xx=0;
			}
		}
	}
	public static void xiangqizi(string sql,GameObject game,string name,int count){    
		/// P/	/// </summary>引用prefab 生成象棋的棋子
		/// 
		/// 
		GameObject a = (GameObject)Instantiate(Resources.Load(sql));
		a.transform.parent = game.transform;
		GameObject b = GameObject.Find(a.name);
		b.name = name+count.ToString();
		b.transform.localPosition = new Vector3(0,0,0);
		b.transform.localScale = new Vector3(1,1,1);
	}
	public void newgame(){
		if (start == true) {
			return;
		}
		start = true;
	}
	public void chessposition(){

        Debug.Log(yemiantiaozhuang.ChessPeople + "人数");
		board.chess = new int[10, 9]{  //此注释要取消
			{2,3,6,5,1,5,6,3,2},
			{0,0,0,0,0,0,0,0,0},
			{0,4,0,0,0,0,0,4,0},
			{7,0,7,0,7,0,7,0,7},
			{0,0,0,0,0,0,0,0,0},
			{0,0,0,0,0,0,0,0,0},
			{14,0,14,0,14,0,14,0,14},
			{0,11,0,0,0,0,0,11,0},
			{0,0,0,0,0,0,0,0,0},
			{9,10,13,12,8,12,13,10,9}
		};
		//初始化其他对象
		blackclick.ChessMove = true;
		blackclick.str = "红方走";
		blackclick.TrueOrFalse = true;
		if (start == false) {
			return;
		}
		//print (yemiantiaozhuang.ChessPeople);
	    s = sprite1.text;
		if (s == "简单")
			SearchEngine.m_nSearchDepth = 1;
		if (s == "一般")
			SearchEngine.m_nSearchDepth = 2;
		if (s == "困难")
			SearchEngine.m_nSearchDepth = 3;
	ChessChongzhi.Count = 0;//重置悔棋记录对象
		ChessChongzhi.pos = new ChessChongzhi.QIZI[400];
		start=false;
		//All = new ArrayList ();//初始化
		text ();
		int count=1;
		for(int i=1;i<=90;i++){
			GameObject obj = GameObject.Find("item"+i.ToString());
		//	All.Add(obj);
			int x=System.Convert.ToInt32((obj.transform.localPosition.x)/130);
			int y = System.Convert.ToInt32(Mathf.Abs((obj.transform.localPosition.y)/128));
			switch(chess[y,x]){
			case 1:
				count++;
				xiangqizi ("black_jiang", obj, "b_jiang",count);
				break;
			case 2:
				count++;
				xiangqizi ("black_ju", obj, "b_ju",count);
				break;
			case 3:
				count++;
				xiangqizi ("black_ma", obj, "b_ma",count);
				break;
			case 4:
				count++;
				xiangqizi ("black_pao", obj, "b_pao",count);
				break;
			case 5:
				count++;
				xiangqizi ("black_shi", obj, "b_shi",count);
				break;
			case 6:
				count++;
				xiangqizi ("black_xiang", obj, "b_xiang",count);
				break;
			case 7:
				count++;
				xiangqizi ("black_bing", obj, "b_bing",count);
				break;
			case 8:
				count++;
				xiangqizi ("red_shuai", obj, "r_shuai",count);
				break;
			case 9:
				count++;
				xiangqizi ("red_ju", obj, "r_ju",count);
			break;
			case 10:
				count++;
				xiangqizi ("red_ma", obj, "r_ma",count);
				break;
			case 11:
				count++;
				xiangqizi ("red_pao", obj, "r_pao",count);
			break;	
			case 12:
				count++;
				xiangqizi ("red_shi", obj, "r_shi",count);
			break;	
			case 13:
				count++;
				xiangqizi ("red_xiang", obj, "r_xiang",count);
			break;	
			case 14:
				count++;
				xiangqizi ("red_bing", obj, "r_bing",count);
				break;
			}
	}
	}
	public void gameover(){
		//Application.Quit ();
        Destroy(transform.parent.parent.parent.gameObject);
	}
}
