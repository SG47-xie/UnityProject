using UnityEngine;
using System.Collections;

public class SearchEngine : MonoBehaviour {
	public int [,]Curposition = new int[10,9];
	public int NOCHESS=0;
	public int m_nMaxDepth;
	public static int m_nSearchDepth=2;
	public Blackmove.CHESSMOVE m_cmBestMove;//存放最佳走法的变量
    public Transform m_ItemParent;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	///                                              搜索引擎
	//
	//搏弈接口，为当前局面走出下一步
	//设置最大搜索深度
	//设置估值引擎
	//设置走法产生器
	//根据某一个走法产生走了之后的棋盘
	//恢复某一走法所产生棋盘为走过之前的
	//判断当前局面是否分出胜负
	//搜索时用于当前节点棋盘状态数组 int Curposition[10,9]
	//记录最佳走法的变量 chessmove  m_cmbestMove
	//走法产生器
	//估值核心
	//最大搜索深度
	//当前搜索的最大搜索深度

	
	///////////////////////////////////////////
	///////////////////////////////////////////
	///////////////////////////////////////////
	///////////////////////////////////////////
	/// //知道坐标得到item名字
	//
	Blackmove m = new Blackmove ();
	GameObject obj;
	public string Itemsconname(Blackmove.CHESSMOVE move){//得到点击目标位置gameobject的对象名字

		string s3="";
		for (int i=1; i<=90; i++) {
			obj = GameObject.Find("item"+i.ToString());
			int x=System.Convert.ToInt32((obj.transform.localPosition.x)/130);
			int y = System.Convert.ToInt32(Mathf.Abs((obj.transform.localPosition.y)/128));
			if(x==move.To.x&&y==move.To.y)
				s3=obj.name;
		}
		return s3;
	}
	public string Itemfirname(Blackmove.CHESSMOVE move){//得到开始位置gameobject的对象名字
		string s3="";
		for (int i=1; i<=90; i++) {
			obj = m_ItemParent.Find("item"+i).gameObject;
			int x=System.Convert.ToInt32((obj.transform.localPosition.x)/130);
			int y = System.Convert.ToInt32(Mathf.Abs((obj.transform.localPosition.y)/128));
			if(x==move.From.x&&y==move.From.y)
				s3=obj.name;
		}
		return s3;
	}
	//根据传入的走法                                  改变棋盘
	//move是要进行的走法
	//public int[,] CurPosition = new int[10, 9]; 
	public int MakeMove(Blackmove.CHESSMOVE move){
		int nChessID;
		nChessID = Curposition [move.To.y, move.To.x];//取目标棋子
		//把棋子移动到目标位置
		Curposition [move.To.y, move.To.x] =Curposition [move.From.y, move.From.x];
		//将原位清空
		Curposition [move.From.y, move.From.x] = NOCHESS;//清空原来位置
		//shuzi (Curposition);
		return nChessID;//返回被吃掉的棋子
	}
	//	                                 根据走法恢复棋盘
	//move 是要恢复的走法
	//nChessID是原棋盘上move位置的棋子类型
	void UnMakeMove(Blackmove.CHESSMOVE move,int nChessID){
	//将目标位置和棋子拷贝会原位
		Curposition [move.From.y, move.From.x] = Curposition [move.To.y, move.To.x];
		//恢复目标位置的棋子
		Curposition [move.To.y, move.To.x] = nChessID;
	}
	//检查游戏是否结束
	//如果结束，返回0，否则返回极大极小值
	int IsGameOver(int [,]position,int nDepth){
		int i, j;
		bool RedLive = false, BlackLive = false;
		//检查黑方九宫格是否有帅
		for (i=3; i<6; i++)
			for (j=0; j<3; j++) {
			if(position[j,i]==1)
				BlackLive = true;
		}
		//检查红方九宫格是否有帅
		for (i=3; i<6; i++)
			for (j=7; j<10; j++) {
			if(position[j,i]==8)
				RedLive = true;
		}
		i = (m_nMaxDepth - nDepth+1) % 2;//取当前是奇偶标志
		if (!RedLive)//红将不再了
		if (i != 0)
			return 19990 + nDepth;//奇数层返回极大值
		else
			return -19990 - nDepth;//偶数层返回极小值
		if (!BlackLive)//黑将不再了
		if (i != 0)
			return -19990 - nDepth;//奇数层返回极小值
		else
			return 19990 + nDepth;//偶数层返回极大值
		return 0;//两个将都在返回0

	}
	public int Falphabeta(int depth,int alpha,int beta){
		int type;
		int score;
		int Count, i;
		int current = -20000;
		i = IsGameOver (Curposition, depth);
		if (i != 0)
			return i;
		if (depth <= 0)
			return Eveluate (Curposition, (m_nMaxDepth - depth) % 2 != 0);
		Count = m.CreatePossibleMove (Curposition, depth, (m_nMaxDepth - depth) % 2 != 0);
			for(i=0;i<Count;i++){
			//Blackmove.CHESSMOVE a = m.m_MoveList[depth,i];
				type=MakeMove(m.m_MoveList[depth,i]);
				score = -Falphabeta(depth-1,-beta,-alpha);
			//Debug.Log("当前搜索层数是"+m_nSearchDepth+"层 "+board.chess[a.From.y,a.From.x]+"   "+a.From.x+","+a.From.y+"--"+a.To.x+","+a.To.y+":"+score);
				UnMakeMove(m.m_MoveList[depth,i],type);
				if(score>current){
					current = score;
				if(depth==m_nMaxDepth)
					m_cmBestMove = m.m_MoveList[depth,i];
					if(score>=alpha)
						alpha = score;
					if(score>=beta)
						break;
				}
		
			}
		//CeShi+="\n";
		return current;
		}
		//极小窗口算法
	//blackclick bl = new blackclick();
//	IEnumerator Robot(){
//		yield return new WaitForSeconds (0.2f);
//		blackclick.threm();
//	}
	int PrincipalVariation(int depth,int alpha,int beta)
	{
		int score;
		int Count, i;
		int type;
		int best;
		i = IsGameOver (Curposition, depth);
		if (i != 0)
			return i;
		if (depth <= 0)
			return Eveluate (Curposition, (m_nMaxDepth - depth) % 2 != 0);
		Count = m.CreatePossibleMove (Curposition, depth, (m_nMaxDepth - depth) % 2 != 0);
		type=MakeMove(m.m_MoveList[depth,0]);
		best = -PrincipalVariation (depth - 1, -beta, -alpha);
		UnMakeMove(m.m_MoveList[depth,0],type);
		if (depth == m_nMaxDepth)
			m_cmBestMove = m.m_MoveList [depth, i];
		for (i=1; i<Count; i++) {
		//	StartCoroutine(Robot);
			if(best<beta){
				//如果不能被beta 剪枝
				if(best<beta)
					if(best>alpha)
						alpha=best;
					type = MakeMove(m.m_MoveList[depth,i]);
					score = -PrincipalVariation(depth-1,-alpha-1,-alpha);
					if(score>alpha&&score<beta){
						best = -PrincipalVariation(depth-1,-beta,-score);
						if(depth==m_nMaxDepth)
							m_cmBestMove = m.m_MoveList[depth,i];
					}
					else if(score>best){
						best=score;
						if(depth==m_nMaxDepth)
							m_cmBestMove = m.m_MoveList[depth,i];
					}
					UnMakeMove(m.m_MoveList[depth,i],type);
			}
		}
		return best;
	}
	public	Blackmove.CHESSMOVE SearchAGoodMove(int [,]position){
		//设置搜索层数
		m_nMaxDepth = m_nSearchDepth;
		//将传入的棋盘复制到成员变量中
		for (int i=0; i<position.GetLength(0); i++)
			for (int j=0; j<position.GetLength(1); j++)
				Curposition [i, j] = position [i, j];
		/*string str = "";
		for(int c =0;c< Curposition.GetLength(0);c++){
			for(int d=0;d<Curposition.GetLength(1);d++)
				str+=Curposition[c,d];
			str+="\n";
		}
		print (str);*/
		//先进行浅层搜索，猜测目标值范围
		//极小窗口算法
		PrincipalVariation (m_nMaxDepth, -20000, 20000);
		MakeMove (m_cmBestMove);//此行经过测试  可有可无
	/*	int x, y;//渴望算法
		m_nMaxDepth = m_nSearchDepth-1;
		x = Falphabeta (m_nMaxDepth, -20000, 20000);
		//对目标小床进行搜索
		m_nMaxDepth = m_nSearchDepth;
		y = Falphabeta (m_nMaxDepth, x - 50, x + 50);
		if (y < x - 50)
			Falphabeta (m_nMaxDepth, -20000, y);
		if (y > x + 50)
			Falphabeta (m_nMaxDepth, y, 20000);*/
		//wu.........
		//调用负极大值搜索函数找最佳走法
		//NegaMax (m_nMaxDepth);
		blackclick.posthread = true;//线程的内容已经全部执行完毕
		  return m_cmBestMove ;
	}
	//负极大值函数
	//depth表示节点离叶子节点的层数

	int NegaMax(int depth){
		int current = -20000;
		int score;
		int Count, i;
		int type;
		i = IsGameOver (Curposition, depth);//检查棋局是否结束
		if (i != 0)
			return i;//棋局没有结束，返回极大极小值
		if (depth <= 0)
			return Eveluate (Curposition,((m_nMaxDepth - depth) % 2) != 0);//
		//返回估值
		//列举出当前局面下一步所有可能的走法
		Count = m.CreatePossibleMove (Curposition, depth,((m_nMaxDepth - depth) % 2) != 0 );//
		for (i=0; i<Count; i++) {
			Blackmove.CHESSMOVE a = m.m_MoveList[depth,i];
		//根据走法产生新局面
			type = MakeMove(a);

			//print (a.From.x+","+a.From.y+"--"+a.To.x+","+a.To.y);

	//递归调用负极大值搜索下一层节点
			score = -NegaMax(depth-1); 
			//Debug.Log("当前搜索层数是"+m_nSearchDepth+"层 "+board.chess[a.From.y,a.From.x]+"   "+a.From.x+","+a.From.y+"--"+a.To.x+","+a.To.y+":"+score);
			//恢复当前局面
			UnMakeMove(m.m_MoveList[depth,i],type);
			if(score>current){//如果score大于一直的最大值
				current = score;
				if(depth==m_nMaxDepth){
					//靠近根部保存最佳走法
					m_cmBestMove = m.m_MoveList[depth,i];
				}
			}
		}
		return current;//返回极大值
	}
	//伪代码，alpha-beta算法

	////-********************************-----------------------------------------------
	/// 估值核心
	/// 
	/// 
	//棋子基本价值
	public int[]BaseValue = new int[15]{0,10000,500,350,350,250,250,100,10000,500,350,350,250,250,100};
	//棋子灵活性分数数组
	public int []FlexValue = new int[15]{
		//che 6 ma 12 xiang 1 shi 1 pao 6 jiang 0 bing 15
		0,0,6,12,6,1,1,15,0,6,12,6,1,1,15
	}; 
	//每个位置威胁信息
	public int [,] AttackPos = new int[10,9];
	//存放每个位置被保护的信息
	public int [,] GuardPos = new int[10,9];
	//存放每个位置上的棋子灵活性
	public int [,]FlexibilityPos = new int[10,9];
	//存放每个位置的棋子总价值
	public int [,]chessValue = new int[10,9];
	//记录棋子的相关位置个数
	public int nPosCount;
	//记录一个棋子相关位置的数组
	public Blackmove.CHESSMANPOS[]RelatePos = new Blackmove.CHESSMANPOS[30];
	//用来统计调用了估值函数的也子节点次数
	public int count = 0;
	//实例化
	Blackmove bl = new Blackmove();
	//红兵的附加值数组
	public int [,] BA0 = new int [10,9]
	{
		{0,0,0,0,0,0,0,0,0} ,
		{90,90,110,120,120,120,110,90,90} ,
		{90,90,110,120,120,120,110,90,90} ,
		{70,90,110,110,110,110,110,90,70} ,
		{70,70,70,70,70,70,70,70,70} ,
		{0,0,0,0,0,0,0,0,0} ,
		{0,0,0,0,0,0,0,0,0} ,
		{0,0,0,0,0,0,0,0,0} ,
		{0,0,0,0,0,0,0,0,0} ,
		{0,0,0,0,0,0,0,0,0} ,
	} ;
	//黑兵附加值数组
	public int [,] BA1 = new int [10,9]
	{
		{0,0,0,0,0,0,0,0,0} ,
		{0,0,0,0,0,0,0,0,0} ,
		{0,0,0,0,0,0,0,0,0} ,
		{0,0,0,0,0,0,0,0,0} ,
		{0,0,0,0,0,0,0,0,0} ,
		{70,70,70,70,70,70,70,70,70} ,
		{70,90,110,110,110,110,110,90,70} ,
		{90,90,110,120,120,120,110,90,90} ,
		{90,90,110,120,120,120,110,90,90} ,
		{0,0,0,0,0,0,0,0,0} ,
	} ;
	//为每一个兵返回附加值
	//x是横，y纵，cursituation棋盘
	//不是兵返回0
	int GetBingValue(int x,int y,int [,]CurSituation){
	//如果是红卒返回其位置附加价值
		if (CurSituation [y, x] == 14) 
			return BA0[y,x];
		//如果是黑兵返回棋位置附加价值
		if (CurSituation [y, x] == 7)
			return BA1[y,x];
		return 0;
	}
	Blackmove me = new Blackmove();
	//position是要估值的棋盘
	//bIsRedTurn轮倒水走棋的标志，true red  false black
	//列举与指定位置棋子相关的棋子
	//int asd;
	public int Eveluate(int [,]position,bool bIsRedTum)
	{
		int i, j, k;
		int nChessType, nTargetType;
		count++;
		chessValue = new int[10,9];
		AttackPos = new int[10, 9];
		GuardPos = new int[10, 9];
		FlexibilityPos = new int[10, 9];
		for (i = 0; i < 10; i++)
			for (j = 0; j < 9; j++)
		{
			if (position [i,j]!=0 )
			{
				nChessType = position [i,j];
				GetRelatePiece (position ,j,i);//找出该棋子的所有相关位置
				for (k =0;k<nPosCount;k++)//对每一目标位置
				{
					//取目标位置棋子类型
					nTargetType = position [RelatePos [k].y,RelatePos [k].x];
					if (nTargetType == 0 )//如果是空白
						FlexibilityPos [i,j]++;//灵活性增加
					else
					{
						//是棋子
						if (bl.IsSameSide (nChessType ,nTargetType ))
						{
							//如果是己方棋子，目标受到保护
							GuardPos [RelatePos [k].y ,RelatePos [k].x]++;
						}
						else 
						{
							//如果是敌方棋子，目标受到威胁
							AttackPos [RelatePos [k].y,RelatePos [k].x]++;
							FlexibilityPos [i,j]++;//灵活性增加
							//string sss = "";
							switch (nTargetType)
							{
							case 8://如果是红将
								if (!bIsRedTum)
									return 18888;//返回失败极值
								break;
							case 1: //如果是黑将
								if (bIsRedTum)//如果是黑将
									return 18888;//返回失败极值
								break;

							default ://不是将的其他棋子
								//根据威胁的棋子加上威胁分值
							{
								AttackPos [RelatePos[k].y,RelatePos [k].x]+=(30+(BaseValue [nTargetType]-BaseValue [nChessType])/10)/10;							
								break ;
							}
							
							}
						}
					}
				}
				
			}
		}
		//以上扫描棋盘部分
		//一下循环统计扫描到的数据
		for (i = 0; i<10; i++)
			for (j =0; j<9; j++) 
		{
			if (position [i,j]!=0 )
			{
				nChessType = position [i,j];//棋子类型
				chessValue [i,j]++;			//如果存在其价值不为0
				//把每个棋子的灵活性价值加进棋子价值
				chessValue [i,j]+=FlexValue [nChessType ]*FlexibilityPos  [i,j];
				//加上兵的位置附加值
				chessValue [i,j]+=GetBingValue (j,i,position );
				
			}
		}
		//下面循环继续统计扫描到的数据
		int nHalfvalue;
		for (i = 0; i<10; i++)
			for (j =0; j<9; j++)
		{
			if (position [i,j]!=0 )//如果不是空白
			{
				nChessType = position [i,j];//取棋子类型
				//棋子基本价值的1/16作为威胁/保护增量
				nHalfvalue = BaseValue [nChessType]/16;
				//把每个棋子的基本价值介入其总价值
				chessValue [i,j]+=BaseValue [nChessType ];
				if (bl.IsRed (nChessType ))//如果是红旗
				{
					if (AttackPos [i,j]!=0)//当前红棋如果被威胁
					{
						if (bIsRedTum)//如果轮到红旗走
						{
							if (nChessType == 8)//如果是红将
							{
								chessValue [i,j]-=20;//价值降低20
							}
							else
							{
								//价值减去2倍nHalfvalue
								chessValue [i,j]-=nHalfvalue *2;
								if (GuardPos [i,j]!=0)//是否被己方棋子保护
								 chessValue [i,j]+=nHalfvalue ;/*被保护再加上nHalfvalue*/
							}
						}
						else  //红棋被威胁，轮到黑棋走
						{
							if (nChessType ==8)//是否是红将
								return 18888;//返回失败的极值
							chessValue [i,j]-=nHalfvalue *10;//减去10倍的nHalfvalue，表示威胁程度高
							if(GuardPos [i,j]!=0)//如果被保护
							{
								chessValue [i,j]+=nHalfvalue *9;//被保护再加上9倍nHalfvalue
							}
						}
						//被威胁的棋子加上威胁差，防止一个并威胁
						//一个被保护的車，而估值函数没有反映这类问题
						chessValue [i,j]-=AttackPos [i,j];
					}
					else //没受到威胁
					{
						if(GuardPos [i,j]!=0)
							chessValue [i,j]+=5;//受保护，加一分
					}
				}
				else  //如果是黑棋
				{
					if(AttackPos [i,j]!=0)//黑棋受威胁
					{
						if(!bIsRedTum )//轮到黑棋走
						{
							if (nChessType ==1)//如果是黑将
								chessValue [i,j]-=20;//棋子价值降低20
							else
							{
								chessValue [i,j]-=nHalfvalue *2;
								if (GuardPos [i,j]!=0)//如果受保护
									chessValue [i,j]+=nHalfvalue ;
							}
						}
						else //轮到红旗走
						{
							if(nChessType == 1)
								return 18888;
							chessValue [i,j]-=nHalfvalue *10;
							if (GuardPos [i,j]!=0)
								chessValue [i,j]+=nHalfvalue *9;
						}
						chessValue [i,j]-=AttackPos [i,j];
					}
					else //黑棋没有被威胁
					{
						if(GuardPos [i,j]!=0)
							chessValue [i,j]+=5;
					}
				}
			}
		}
		/*string str="保护性：\n";
		for (int c=0; c<GuardPos.GetLength(0); c++) {
			for(int d=0;d<GuardPos.GetLength(1);d++)
				str+=" "+GuardPos[c,d];
				str+="\n";
		}
		print (str);
		str="灵活性:\n";
		for (int c=0; c<FlexibilityPos.GetLength(0); c++) {
			for(int d=0;d<FlexibilityPos.GetLength(1);d++)
				str+=" "+FlexibilityPos[c,d];
				str+="\n";
		}
		print (str);
		str="威胁性：\n";
		for (int c=0; c<AttackPos.GetLength(0); c++) {
			for(int d=0;d<AttackPos.GetLength(1);d++)
				str+=" "+AttackPos[c,d];
				str+="\n";
		}
		print (str);
		str="棋子价值：\n";
		for (int c=0; c<chessValue.GetLength(0); c++) {
			for(int d=0;d<chessValue.GetLength(1);d++)
				str+=","+chessValue[c,d];
			str+="\n";
		}
		print (str);
*/
		//以上生成每个棋子的总价值
		//以下统计红黑方的总分
		int nRedValue = 0;
		int nBlackValue = 0;
		for (i=0; i<10; i++)
			for (j =0; j<9; j++) 
		{
			nChessType = position [i,j];
			if (nChessType !=0 )//如果不是空白
			{
				if(bl.IsRed (nChessType ))  //如果是红旗
					nRedValue +=chessValue [i,j];//将这格棋子的价值加入到红旗价值当中
				else
					nBlackValue += chessValue [i,j];
			}
		}
		if (bIsRedTum) {
			//print (nRedValue - nBlackValue);
			return  nRedValue - nBlackValue;//如果轮到红旗走返回估值
		} else {
			//print (nRedValue - nBlackValue);
			return nBlackValue - nRedValue;//轮到黑棋走返回估值的相反数
		}

		
	}
	
	
	//列举与指定位置的棋子相关的棋子
	//这个函数枚举了给定位上棋子的所有相关位置
	public int GetRelatePiece(int [,]position,int j,int i ){
		nPosCount = 0;
		int nChessID;
		//RelatePos = new Blackmove.CHESSMANPOS[30];
		bool flag = false;
		int x, y;
		nChessID = position [i, j];
		switch (nChessID) {
		case 1:
			for (y=0; y<3; y++)
				for (x=3; x<6; x++)
					if (CanTouch (position, j, i, x, y)) {
						AddPoint (x, y);
					}
			break;
		case 8:
			for (y=7; y<10; y++)
				for (x=3; x<6; x++)
					if (CanTouch (position, j, i, x, y))
						AddPoint (x, y);
			break;
		case 12:
			for (y=7; y<10; y++)
				for (x=3; x<6; x++)
					if (CanTouch (position, j, i, x, y))
						AddPoint (x, y);
			break;
		case 5:
			for (y=0; y<3; y++)
				for (x=3; x<6; x++)
					if (CanTouch (position, j, i, x, y))
						AddPoint (x, y);
			break;
		case 6:
		case 13:
			x = j + 2;
			y = i + 2;
			if (x < 9 && y < 10 && CanTouch (position, j, i, x, y))
				AddPoint (x, y);
			//右下
			x = j + 2;
			y = i - 2;
			if (x < 9 && y >= 0 && CanTouch (position, j, i, x, y))
				AddPoint (x, y);
			//左下
			x = j - 2;
			y = i + 2;
			if (x >= 0 && y < 10 && CanTouch (position, j, i, x, y))
				AddPoint (x, y);
			//左下
			x = j - 2;
			y = i - 2;
			if (x >= 0 && y >= 0 && CanTouch (position, j, i, x, y))
				AddPoint (x, y);
			break;
		case 3:
		case 10:
			x = j + 2;
			y = i + 1;
			if ((x < 9 && y < 10) && CanTouch (position, j, i, x, y))
				AddPoint (x, y);
			//检查right top
			x = j + 2;
			y = i - 1;
			if ((x < 9 && y >= 0) && CanTouch (position, j, i, x, y))
				AddPoint (x, y);
			//left down
			x = j - 2;
			y = i + 1;
			if ((x >= 0 && y < 10) && CanTouch (position, j, i, x, y))
				AddPoint (x, y);
			//left top
			x = j - 2;
			y = i - 1;
			if ((x >= 0 && y >= 0) && CanTouch (position, j, i, x, y))
				AddPoint (x, y);
			//right down
			x = j + 1;
			y = i + 2;
			if ((x < 9 && y < 10) && CanTouch (position, j, i, x, y))
				AddPoint (x, y);
			//left down
			x = j - 1;
			y = i + 2;
			if ((x >= 0 && y < 10) && CanTouch (position, j, i, x, y))
				AddPoint (x, y);
			//right top
			x = j + 1;
			y = i - 2;
			if ((x < 9 && y >= 0) && CanTouch (position, j, i, x, y))
				AddPoint (x, y);
			//left top
			x = j - 1;
			y = i - 2;
			if ((x >= 0 && y >= 0) && CanTouch (position, j, i, x, y))
				AddPoint (x, y);
			break;
		case 2:
		case 9:
			x = j + 1;
			y = i;
			while (x<9) {
				if (position [y, x] == 0)
					AddPoint (x, y);
				else {
					AddPoint (x, y);
					break;
				}
				x++;
			}
			x = j - 1;
			y = i;
			while (x>=0) {
				if (position [y, x] == 0)
					AddPoint (x, y);
				else {
					AddPoint (x, y);
					break;
				}
				x--;
			}
			x = j;
			y = i + 1;
			while (y<10) {
				if (position [y, x] == 0)
					AddPoint (x, y);
				else {
					AddPoint (x, y);
					break;
				}
				y++;
			}
			x = j;
			y = i - 1;
			while (y>=0) {
				if (position [y, x] == 0)
					AddPoint (x, y);
				else {
					AddPoint (x, y);
					break;
				}
				y--;
			}
			break;
		case 14:
			y = i - 1;
			x = j;
			if (y >= 0)
				AddPoint (x, y);
			if (i < 5) {
				y = i;
				x = j + 1;
				if (x < 9)
					AddPoint (x, y);
				x = j - 1;
				if (x >= 0)
					AddPoint (x, y);
			}

			break;
		case 7:
			y = i + 1;
			x = j;
			if (y < 10)
				AddPoint (x, y);
			if (i > 4) {
				y = i;
				x = j + 1;
				if (x < 9)
					AddPoint (x, y);
				x = j - 1;
				if (x >= 0)
					AddPoint (x, y);
			}
			break;
		case 4:
		case 11:
			x = j + 1;
			y = i;
			flag = false;
			while (x<9) {
				if (position [y, x] == 0) {
					if (!flag)
						AddPoint (x, y);
				} else {
					if (!flag)
						flag = true;
					else {
						AddPoint (x, y);
						break;
					}
				}
				x++;
			}
			x = j - 1;
			flag = false;
			while (x>=0) {
				if (position [y, x] == 0) {
					if (!flag)
						AddPoint (x, y);
				} else {
					if (!flag)
						flag = true;
					else {
						AddPoint (x, y);
						break;
					}
				}
				x--;
			}
			x = j;
			y = i + 1;
			flag = false;
			while (y<10) {
				if (position [y, x] == 0) {
					if (!flag)
						AddPoint (x, y);
				} else {
					if (!flag)
						flag = true;
					else {
						AddPoint (x, y);
						break;
					}
				}
				y++;
			}
			y = i - 1;
			flag = false;
			while (y>=0) {
				if (position [y, x] == 0) {
					if (!flag)
						AddPoint (x, y);
				} else {
					if (!flag)
						flag = true;
					else {
						AddPoint (x, y);
						break;
					}
				}
				y--;
			}
			break;
		default:
			break;
		}
	/*	string str = "";
		for (int c=0; c<nPosCount; c++) {
			str +="棋子位置是："+j+","+i + "-------" +"他的相关位置是:"+ RelatePos [c].x + "," + RelatePos [c].y + "**********";
			str+="\n";
		}
		print (str);*/
		return nPosCount;
	}

	//判断from 位置能否走到to
	bool CanTouch(int [,]position,int nFromX,int nFromY,int nToX,int nToY){
		//print (nFromX + " " + nFromY + " " + nToX + " " + nToY);
		int i=0, j=0;
		int nMoveChessID, nTargetID;
		if (nFromX == nToX && nFromY == nToY)
			return false;//目的与原相同
		if (!rules.KingBye (position, nFromX, nFromY, nToX, nToY))
			return false;
		nMoveChessID = position [nFromY, nFromX];
		nTargetID = position [nToY, nToX];
		switch (nMoveChessID) {
		case 1://将
				if(nToY>2||nToX>5||nToX<3)
					return false;
				if(Mathf.Abs(nFromY-nToY)+Mathf.Abs(nToX-nFromX)>1)
					return false;//将智能走一步
			break;
		case 5://黑士
			if(nToY>2||nToX>5||nToX<3)
				return false;//士出九宫格
			if(Mathf.Abs(nFromY-nToY)!=1||Mathf.Abs(nToX-nFromX)!=1)
				return false;//走斜线
			break;
		case 6://黑相
			if(nToY>4)
				return false;
			if(Mathf.Abs(nFromX-nToX)!=2||Mathf.Abs(nFromY-nToY)!=2)
				return false;
			if(position[(nFromY+nToY)/2,(nFromX+nToX)/2]!=0)
				return false;
			break;
		case 7:
			if(nToY<nFromY)
				return false;
			if(nFromY<5&&nFromY==nToY)
				return false;
			if(nToY-nFromY+Mathf.Abs(nToX-nFromX)>1)
				return false;
			break;
		case 8:
				if(nToY<7||nToX>5||nToX<3)
					return false;
				if(Mathf.Abs(nFromY-nToY)+Mathf.Abs(nToX-nFromX)>1)
					return false;
			//if(position[nToY,nToX]!=0 && position[nToY,nToX]>7)
			//	return false;
			break;
		case 12://红士
			if(nToY<7||nToX>5||nToX<3)
				return false;
			if(Mathf.Abs(nFromY-nToY)!=1||Mathf.Abs(nToX-nFromX)!=1)
				return false;
			break;
		case 13://红相
			if(nToY<5)
				return false;//相不能过河
			if(Mathf.Abs(nFromX-nToX)!=2||Mathf.Abs(nFromY-nToY)!=2)
				return false;
			if(position[(nFromY+nToY)/2,(nFromX+nToX)/2]!=0)
				return false;
			break;
		case 14:
			if(nToY>nFromY)
				return false;
			if(nFromY>4&&nFromY==nToY)
				return false;
			if(nFromY-nToY+Mathf.Abs(nToX-nFromX)>1)
				return false;
			break;
		case 2:
		case 9://黑车和红车
			if(nFromY!=nToY&&nFromX!=nToX)
				return false;
			if(nFromY==nToY){
				if(nFromX<nToX){
					//right
					for(i=nFromX+1;i<nToX;i++)
						if(position[nFromY,i]!=0)
							return false;
				}
				else{
					for(i=nToX+1;i<nFromX;i++)
						if(position[nFromY,i]!=0)
							return false;
				}
			}
			else{
				//纵向
				if(nFromY<nToY){
					//down
					for(j=nFromY+1;j<nToY;j++)
						if(position[j,nFromX]!=0)
							return false;
				}
				else{
					//top
					for(j=nToY+1;j<nFromY;j++)
						if(position[j,nFromX]!=0)
							return false;
				}
			}
			break;
		case 3:
		case 10://两个马的走法
			if(!((Mathf.Abs(nToX-nFromX)==1&&Mathf.Abs(nToY-nFromY)==2)||(Mathf.Abs(nToX-nFromX)==2&&Mathf.Abs(nToY-nFromY)==1)))
				return false;
			if(nToX-nFromX==2){
				i=nFromX+1;
				j=nFromY;
			}
			else if(nFromX-nToX==2){
				i=nFromX-1;
				j=nFromY;
			}
			else if(nToY-nFromY==2){
				i=nFromX;
				j=nFromY+1;
			}
			else if(nFromY-nToY==2){
				i=nFromX;
				j=nFromY-1;

			}
			if(position[j,i]!=0)
				return false;
			break;
		case 4:
		case 11:
			if(nFromY!=nToY&&nFromX!=nToX)
				return false;
			//炮不吃子结果的路线中不能有棋子
			if(position[nToY,nToX]==0)
			{
				if(nFromY==nToY){
					//横向
					if(nFromX<nToX){
						//right
						for(i=nFromX+1;i<nToX;i++)
							if(position[nFromY,i]!=0)
								return false;
					}
					else{
						//left
						for(i=nToX+1;i<nFromX;i++)
							if(position[nFromY,i]!=0)
								return false;
					}
				}
				else{
					if(nFromY<nToY){
						//down
						for(j=nFromY+1;j<nToY;j++)
							if(position[j,nFromX]!=0)
								return false;
					}
					else{
						for(j=nToY+1;j<nFromY;j++)
							if(position[j,nFromX]!=0)
								return false;
					}
				}
			}
			//炮吃子时候
			else{
				int count=0;
				if(nFromY==nToY){
					if(nFromX<nToX){
						for(i=nFromX+1;i<nToX;i++)
							if(position[nFromY,i]!=0)
								count++;
						if(count!=1)
							return false;
					}
					else{
						//left
						for(i=nToX+1;i<nFromX;i++)
							if(position[nFromY,i]!=0)
								count++;
						if(count!=1)
							return false;
					}
				}
				else{
					//纵相
					if(nFromY<nToY){
						for(j=nFromY+1;j<nToY;j++)
							if(position[j,nFromX]!=0)
								count++;
						if(count!=1)
							return false;
					}
					else{
						//top
						for(j=nToY+1;j<nFromY;j++)
							if(position[j,nFromX]!=0)
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
		return true;
	
	}
	//将一个位置加入相关队列
	void AddPoint(int x,int y){
		//这个函数将一个位置加入数组relatepas中
		RelatePos [nPosCount].x = x;
		RelatePos [nPosCount].y = y;
		nPosCount++;
	}
	/*public void shuzi(int [,]Curpositio){
		string str = "";
		for (int i=0; i<Curpositio.GetLength(0); i++) {
			for(int j=0;j<Curpositio.GetLength(1);j++)
				str+=Curpositio[i,j];
			str+="\n";
		}
		print (str);
	}*/
}
