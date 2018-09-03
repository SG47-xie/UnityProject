using UnityEngine;
using System.Collections;
public class rules : MonoBehaviour {
	void Start () {

	}
	// Update is called once per frame
	void Update () {
	}
	//让王不能会面
	public static bool Isblack(int x){
		if (x > 0 && x < 8)
			return true;
		else
			return false;
	}
	
	//判断一个棋子是不是红色
	public static bool Isred(int x){
		if (x >= 8 && x < 15)
			return true;
		else
			return false;
	}
	
	//判断两个棋子是不是同颜色
	public static bool IssameSide(int x,int y){
		if (Isblack (x) && Isblack (y) || Isred (x) && Isred (y))
			return true;
		else
			return false;
	}
	//public int [,] CHESS1 = new int[10, 9];//定义一个数组
	public static bool KingBye(int [,]CHESS,int fromx,int fromy,int tox,int toy){
		//假设法
		int Count=0, jiangx=0, jiangy=0, shuaix=0, shuaiy=0;
		//引用类型循环赋值
		int [,] CHESS1 = new int[10, 9];
		for (int i=0; i<CHESS.GetLength(0); i++)
			for(int j=0;j<CHESS.GetLength(1);j++)
				CHESS1[i,j]=CHESS[i,j];
		CHESS1 [toy, tox] = CHESS1 [fromy, fromx];
		CHESS1 [fromy, fromx] = 0;//清空原来位置
		for (int i=0; i<3; i++) {
			for (int j=3; j<6; j++)
				if (CHESS1 [i, j] == 1) {
					jiangx = j;
					jiangy = i;
				}
		}
			for(int i=7;i<10;i++){
			for(int j=3;j<6;j++)
				if(CHESS1[i,j]==8){
					shuaix = j;
					shuaiy = i;
				}
			}
		//上面找到了将 和帅的坐标
		if (jiangx == shuaix) {
			//将和帅的x  相等
			for (int i=jiangy+1; i<shuaiy; i++) {
				if (CHESS1 [i, jiangx] != 0)
					Count++;
			}
		} else
			Count = -1;
		//print (shuaix + "," + shuaiy + "." + jiangx + "," + jiangy);
		if (Count == 0)
			return false;
		return true;
	
	}
	//所有棋子的走棋规则
	//type =false 黑色走棋,true 红色走
	public static bool IsValidMove(int [,]position, int Fromx,int Fromy,int Tox,int toY){	
		int i=0, j=0;

		int nMoveChessID, nTargetID;
		nMoveChessID = position [Fromy, Fromx];//得到移动前的棋
		nTargetID = position [toY, Tox];//得到移动后的棋子
		if (Fromy == toY && Fromx == Tox) //目的与原相同
			return false;
		int ChessOne = board.chess[Fromy,Fromx];
		int ChessTwo = board.chess [toY, Tox];
		if (IssameSide (nMoveChessID, nTargetID)) 
			return false;
		if (!KingBye (position,Fromx, Fromy, Tox, toY))
			return false;
		switch (nMoveChessID) {
		case 1://如果现在是黑将走棋
				if(toY>2||Tox>5||Tox<3)//出了九宫格
					return false;
				if(Mathf.Abs(Fromy-toY)+Mathf.Abs(Tox-Fromx)>1)
					return false;//将只能走一步
			break;
		case 8://如果现在是红将走棋
		
				if(toY<7||Tox>5||Tox<3)
					return false;//目标出了九宫格
				if(Mathf.Abs(Fromy-toY)+Mathf.Abs(Tox-Fromx)>1)
					return false;
			break;
		case 12://红士
			if(toY<7||Tox>5||Tox<3)
				return false;//出了九宫格
			if(Mathf.Abs(Fromy-toY)!=1||Mathf.Abs(Tox-Fromx)!=1)
				return false;//士走斜线
			break;
		case 5:
			if(toY>2||Tox>5||Tox<3)
				return false;//士出九宫格
			if(Mathf.Abs(Fromy-toY)!=1||Mathf.Abs(Tox-Fromx)!=1)
				return false;//士走斜线
			break;
		case 13://红相走棋
			if(toY<5)
				return false;//相不能过河
			if(Mathf.Abs(Fromx-Tox)!=2||Mathf.Abs(Fromy-toY)!=2)
				return false;//相走田字
			if(position[(Fromy+toY)/2,(Fromx+Tox)/2]!=0)
				return false;//相眼被独
			break;
		case 6://黑相
			if(toY>4)
				return false;//象不能过河
			if(Mathf.Abs(Fromx-Tox)!=2||Mathf.Abs(Fromy-toY)!=2)
				return false;//象走田字
			if(position[(Fromy+toY)/2,(Fromx+Tox)/2]!=0)
				return false;//象眼被堵
			break;
		case 7://黑兵
			if(toY<Fromy)
				return false;//兵不能回头
			if(Fromy<5&&Fromy==toY)
				return false;//兵过河前智能走直线
			if(toY-Fromy+Mathf.Abs(Tox-Fromx)>1)
				return false;//兵只能走一步
			break;
		case 14:
			if(toY>Fromy)
				return false;//卒不回头
			if(Fromy>4&&Fromy==toY)
				return false;//兵过河前只能走直线
			if(Fromy-toY+Mathf.Abs(Tox-Fromx)>1)
				return false;
			break;
		case 2:
		case 9:
			if(Fromy!=toY&&Fromx!=Tox)
				return false;//车走直线
			if(Fromy==toY){
				if(Fromx<Tox){//right
					for(i=Fromx+1;i<Tox;i++)
						if(position[Fromy,i]!=0)
							return false;//中间有棋子
				}
				else{
					for(i=Tox+1;i<Fromx;i++)
						if(position[Fromy,i]!=0)
							return false;
				}
			}
			else{
				if(Fromy<toY){
					for(j=Fromy+1;j<toY;j++)
						if(position[j,Fromx]!=0)
							return false;
				}
				else{
					for(j=toY+1;j<Fromy;j++)
						if(position[j,Fromx]!=0)
							return false;
				}
			}
			break;
		case 3://黑马
		case 10:
			if(!((Mathf.Abs(Tox-Fromx)==1&&Mathf.Abs(toY-Fromy)==2)||(Mathf.Abs(Tox-Fromx)==2&&Mathf.Abs(toY-Fromy)==1)))
				return false;//马走日字
			if(Tox-Fromx==2){
				i=Fromx+1;
				j=Fromy;
			}
			else if(Fromx-Tox==2){
				i=Fromx-1;
				j=Fromy;
			}
			else if(toY-Fromy==2){
				i=Fromx;
				j=Fromy+1;
			}
			else if(Fromy-toY==2){
				i=Fromx;
				j=Fromy-1;
			}
			if(position[j,i]!=0)
				return false;//斑马脚
			break;
		case 4://黑炮
		case 11:
			if(Fromy!=toY&&Fromx!=Tox)
				return false;//炮走直线
			//炮吃子时经过的路线中不能有棋子
			if(position[toY,Tox]==0){
				if(Fromy==toY){
					if(Fromx<Tox){
						for(i=Fromx+1;i<Tox;i++)
							if(position[Fromy,i]!=0)
								return false;

					}
					else{
						for(i=Tox+1;i<Fromx;i++)
							if(position[Fromy,i]!=0)
								return false;
					}
				}
				else{
					if(Fromy<toY){
						for(j=Fromy+1;j<toY;j++)
							if(position[j,Fromx]!=0)
								return false;
					}
					else{
						for(j=toY+1;j<Fromy;j++)
							if(position[j,Fromx]!=0)
								return false;
					}
				}
			}
			else{//炮吃子时
				int count = 0;
				if(Fromy==toY){
					if(Fromx<Tox){
						for(i=Fromx+1;i<Tox;i++)
							if(position[Fromy,i]!=0)
								count++;
						if(count!=1)
							return false;
					}
					else{
						for(i=Tox+1;i<Fromx;i++)
							if(position[Fromy,i]!=0)
								count++;
						if(count!=1)
							return false;
					}
				}
				else{
					if(Fromy<toY){
						for(j=Fromy+1;j<toY;j++)
							if(position[j,Fromx]!=0)
								count++;
						if(count!=1)
							return false;
					}
					else{
						for(j=toY+1;j<Fromy;j++)
							if(position[j,Fromx]!=0)
								count++;
						if(count!=1)
							return false;
					}
				}
			}
			break;
		default:
			return false;
		}
		//print ("棋子类型："+position[Fromy,Fromx]+"    ："+Fromx +","+ Fromy + "--" + Tox + "," + toY);
		return true;

	}

}

