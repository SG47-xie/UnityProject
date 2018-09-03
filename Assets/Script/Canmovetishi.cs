using UnityEngine;
using System.Collections;

public class Canmovetishi : MonoBehaviour {
	Blackmove IsChess = new Blackmove();
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//点击棋子能判断得出来他的那些位置是能走的呢？

	//1、先得到他能走到的位置
	//2* 获取位置坐标
	//3.得到item对象
	//把预设体添加到场景
	//5.找到预设体的富容器变且添加， 
	//6.设置预设体的位置   tox， toy
	/*-----------------------------------------*/
	//判断点击到的是什么棋子
	public void ClickChess(int fromx,int fromy){
		//print (board.chess[fromy,fromx]+" "+fromx + " " + fromy);
		int ChessID = board.chess [fromy, fromx];
		switch(ChessID){
		case 1:
		case 8:
			Gen_KingMove(board.chess,fromx,fromy);
			break;
		case 2:
		case 9:
			Gen_CarMove(board.chess,fromx,fromy);
			break;
		case 3:
		case 10:
			Gen_HorseMove(board.chess,fromx,fromy);
			break;
		case 4:
		case 11:
			Gen_CanonMove(board.chess,fromx,fromy);
			break;
		case 5://黑士
			Gen_BBishopMove(board.chess,fromx,fromy);
			break;
		case 6://黑象
		case 13://红相
			Gen_ElephantMove(board.chess,fromx,fromy);
			break;
		case 7://黑兵
			Gen_BPawnMove(board.chess,fromx,fromy);
			break;
		case 12://红shi
			Gen_RBishopMove(board.chess,fromx,fromy);
			break;
		
		case 14://红兵
			Gen_RPawnMove(board.chess,fromx,fromy);
			break;
		}
	}
	//把传入进来的可走位置全部画出来
	public void GetPrefabs(int [,]position,int c,int d,int x,int y){//得到相关位置的item坐标  tox  toy
	//先进行与社体清空
	//	for (int i=1; i<=90; i++) {
		//	GameObject obj = GameObject.Find("prefabs"+i.ToString());
		//	Destroy(obj);
		//}
		/*string str = "";
		for(int t=0;t<board.chess.GetLength(0);t++){
			for(int f=0;f<board.chess.GetLength(1);f++)
				str+=" "+board.chess[t,f];
			str+="\n";
		}
		print (str);*/
		if (!rules.KingBye (position, c, d, x, y))
			//print(!rules.KingBye(position,c,d,x,y));
//			print (c + "," + d + "-" + x + "," + y);
			return;
			int wid = x * 130;
		int heit = y * (-128);

		for (int i=1; i<=90; i++) {
			GameObject game = GameObject.Find("item"+i.ToString());
			if(game.transform.localPosition.x==wid&&game.transform.localPosition.y==heit){
				//得到了game  对象    了解game对象坐标 
				GameObject obj = GameObject.Find("chess");//找到预设体参照物
				GameObject ite;
				if(board.chess[y,x]==0)
				 ite = (GameObject)Instantiate(Resources.Load("canmove"));//找到预设体
				else
				 ite = (GameObject)Instantiate(Resources.Load("nengchi"));//找到预设体
				ite.transform.parent = obj.transform;
				GameObject b = GameObject.Find(ite.name);    //找到这个预设体的名字，给他做一些操作
				b.name = "prefabs"+i.ToString();
				b.transform.localPosition = new Vector3(wid,heit,0);
				b.transform.localScale = new Vector3(1,1,1);
				GameObject objecta = GameObject.Find(b.name);
			}
		}
	}
	//得到将的走法
	void Gen_KingMove(int [,]position,int j,int i){//两个参数 fromx  和fromy
		int x, y;
		for (y=0; y<3; y++)
			for (x=3; x<6; x++)
				if (rules.IsValidMove (position, j, i, x, y)) //走法是否合法
					GetPrefabs (position,j,i,x, y);
		for (y=7; y<10; y++)
			for (x=3; x<6; x++)
				if (rules.IsValidMove (position, j, i, x, y))//走法是否合法
					GetPrefabs (position,j,i,x, y);

	}
	//红士的走法
	void Gen_RBishopMove(int [,]position,int j,int i){
		//i j棋子位置   x y目标位置
		int x, y;
		for(y=7;y<10;y++)
			for(x=3;x<6;x++)
				if (rules.IsValidMove (position,j,i, x, y))//走法是否合法
					GetPrefabs (position,j,i,x, y);
	}
	//黑士走法
	void Gen_BBishopMove(int [,]position,int j,int i){
		int x, y;
		for(y=0;y<3;y++)
			for(x=3;x<6;x++)
				if (rules.IsValidMove (position,j,i, x, y))//走法是否合法
					GetPrefabs (position,j,i,x, y);
	}
	//相象走法
	void Gen_ElephantMove(int [,]position,int j,int i){
		int x, y;
		//向右下方走步
		x = j + 2;
		y = i + 2;
		if (x < 9 && y < 10 && rules.IsValidMove (position, j, i, x, y))
			GetPrefabs (position,j,i,x, y);
		//向右上方走步
		x = j + 2;
		y = i - 2;
		if (x <9&&y>=0&&rules.IsValidMove (position, j, i, x, y))
			GetPrefabs (position,j,i,x, y);
		//向左下方走步
		x = j - 2;
		y = i + 2;
		if (x >=0&&y<10&&rules.IsValidMove (position, j, i, x, y))
			GetPrefabs (position,j,i,x, y);
		//向左上方走步
		x = j - 2;
		y = i - 2;
		if (x>=0&&y >=0&&rules.IsValidMove (position, j, i, x, y))
			GetPrefabs (position,j,i,x, y);
	}
	//马的走法
	void Gen_HorseMove(int [,]position,int j,int i){
		int x, y;
		//插入右下方的有效走法
		x = j + 2;
		y = i + 1;
		if ((x < 9 && y < 10) && rules.IsValidMove (position, j, i, x, y))
			GetPrefabs (position,j,i,x, y);
		//插入右上方的有效走法
		x = j + 2;
		y = i - 1;
		if((x<9&&y>=0)&&rules.IsValidMove(position,j,i,x,y))
			GetPrefabs (position,j,i,x, y);
		//左下
		x = j - 2;
		y = i + 1;
		if((x>=0&&y<10)&&rules.IsValidMove(position,j,i,x,y))
			GetPrefabs (position,j,i,x, y);
		//左上
		x = j - 2;
		y = i - 1;
		if((x>=0&&y>=0)&&rules.IsValidMove(position,j,i,x,y))
			GetPrefabs (position,j,i,x, y);
		//右下
		x = j + 1;
		y = i + 2;
		if((x<9&&y<10)&&rules.IsValidMove(position,j,i,x,y))
			GetPrefabs (position,j,i,x, y);
		//left down
		x = j - 1;
		y = i + 2;
		if((x>=0&&y<10)&&rules.IsValidMove(position,j,i,x,y))
			GetPrefabs (position,j,i,x, y);
		//right down
		x = j + 1;
		y = i - 2;
		if((x<9&&y>=0)&&rules.IsValidMove(position,j,i,x,y))
			GetPrefabs (position,j,i,x, y);
		//left top
		x = j - 1;
		y = i - 2;
		if((x>=0&&y>=0)&&rules.IsValidMove(position,j,i,x,y))
			GetPrefabs (position,j,i,x, y);
		
	}
	//车的走法
	void Gen_CarMove(int [,]position,int j,int i){
		int x, y;
		int nChessID;
		nChessID = position [i, j];
		//右边
		x = j + 1;
		y = i;
		while (x<9) {
			if(position[y,x]==0)
				GetPrefabs (position,j,i,x, y);
			else{
				if(!IsChess.IsSameSide(nChessID,position[y,x]))
					GetPrefabs (position,j,i,x, y);
				break;
			}
			x++;
		}
		//left
		x = j - 1;
		y = i;
		while (x>=0) {
			if(position[y,x]==0)
				GetPrefabs (position,j,i,x, y);
			else{
				if(!IsChess.IsSameSide(nChessID,position[y,x]))
					GetPrefabs (position,j,i,x, y);
				break;
			}
			x--;
		}
		x = j;
		y = i + 1;
		//down
		while (y<10) {
			if(position[y,x]==0)
				GetPrefabs (position,j,i,x, y);
			else{
				if(!IsChess.IsSameSide(nChessID,position[y,x]))
					GetPrefabs (position,j,i,x, y);
				break;
			}
			y++;
		}
		//top
		x = j;
		y = i - 1;
		while (y>=0) {
			if(position[y,x]==0)
				GetPrefabs (position,j,i,x, y);
			else{
				if(!IsChess.IsSameSide(nChessID,position[y,x]))
					GetPrefabs (position,j,i,x, y);
				break;
			}
			y--;
		}
	}
	//红卒的走法
	void Gen_RPawnMove(int [,]position,int j,int i){
		int x, y;
		int nChessID;
		nChessID = position [i, j];
		y = i - 1;
		x = j;
		if(y>0&&!IsChess.IsSameSide(nChessID,position[y,x]))
			GetPrefabs (position,j,i,x, y);
		if (i < 5) {
			y=i;
			x=j+1;//right
			if(x<9&&!IsChess.IsSameSide(nChessID,position[y,x]))
				GetPrefabs (position,j,i,x, y);
			x=j-1;//right
			if(x>=0&&!IsChess.IsSameSide(nChessID,position[y,x]))
				GetPrefabs (position,j,i,x, y);
		}
	}
	//黑兵走法
	void Gen_BPawnMove(int [,]position,int j,int i){
		int x, y;
		int nChessID;
		nChessID = position [i, j];
		y = i + 1;//前
		x = j;
		if(y<10&&!IsChess.IsSameSide(nChessID,position[y,x]))
			GetPrefabs (position,j,i,x, y);
		if (i > 4) {
			y=i;
			x=j+1;
			if(x<9&&!IsChess.IsSameSide(nChessID,position[y,x]))
				GetPrefabs (position,j,i,x, y);
			x=j-1;
			if(x>=0&&!IsChess.IsSameSide(nChessID,position[y,x]))
				GetPrefabs (position,j,i,x, y);
		}
		
	}
	//炮走法
	void Gen_CanonMove(int [,]position,int j,int i){
		int x, y;
		bool flag;
		int nChessID;
		nChessID = position [i, j];
		//right
		x = j + 1;
		y = i;
		flag = false;
		while (x<9) {
			if(position[y,x]==0){
				if(!flag)
					GetPrefabs (position,j,i,x, y);
			}
			else{
				if(!flag)
					flag = true;
				else{
					if(!IsChess.IsSameSide(nChessID,position[y,x]))
						GetPrefabs (position,j,i,x, y);
					break;
				}
			}
			x++;
		}
		x = j - 1;
		flag = false;
		while (x>=0) {
			if (position [y, x] == 0) {
				if(!flag)
					GetPrefabs (position,j,i,x, y);
			}
			else{
				if(!flag)
					flag=true;
				else{
					if(!IsChess.IsSameSide(nChessID,position[y,x]))
						GetPrefabs (position,j,i,x, y);
					break;
				}
			}
			x--;
		}
		x = j;
		y = i + 1;
		flag = false;
		while (y<10) {
			if(position[y,x]==0){
				if(!flag)
					GetPrefabs (position,j,i,x, y);
			}
			else{
				if(!flag)
					flag = true;
				else{
					if(!IsChess.IsSameSide(nChessID,position[y,x]))
						GetPrefabs (position,j,i,x, y);
					break;
				}
			}
			y++;
		}
		y = i - 1;
		flag = false;
		while (y>=0) {
			if(position[y,x]==0){
				if(!flag)
					GetPrefabs (position,j,i,x, y);
			}
			else{
				if(!flag)
					flag = true;
				else{
					if(!IsChess.IsSameSide(nChessID,position[y,x]))
						GetPrefabs (position,j,i,x, y);
					break;
				}
			}
			y--;
		}
	}

}
