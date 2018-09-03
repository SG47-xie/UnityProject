using UnityEngine;
using System.Collections;

public class yemiantiaozhuang : MonoBehaviour {
	public static int ChessPeople;//判断当前是人人象棋 还是人机
	// Use this for initialization
	public GameObject lab,sprite;
	void Start () {
	//把难度选择 和 new label  隐藏  人人对战的时候
		lab = GameObject.Find ("xshi");
		sprite = GameObject.Find("nandu");
		//
		UISprite sprite1,sprite2,sprite3;
		GameObject game = GameObject.Find ("daxiao");
		GameObject game2 = GameObject.Find ("bg");//找到开始游戏的背景
		GameObject game3 = GameObject.Find("uiroot");
		sprite1 = game.GetComponent<UISprite> ();//得到了uiroot 的高和宽
		sprite2 = game2.GetComponent<UISprite> ();
		sprite3 = game3.GetComponent<UISprite> ();
		sprite2.width = sprite1.width;
		sprite2.height = sprite1.height;
		sprite3.width = sprite1.width;
		sprite3.height = sprite1.height;

	}
	
	// Update is called once per frame
	void Update () {
	
	}
	ChessChongzhi chess = new ChessChongzhi ();
	public TweenPosition startPanelTween;
	public TweenPosition optionPanelTween;
	public void OnOptionButtonClick(){//单人游戏
		ChessPeople = 1;
		startPanelTween.PlayForward ();
		optionPanelTween.PlayForward ();
		lab.transform.localPosition = new Vector3 (616, 807, 0);
		sprite.transform.localPosition = new Vector3 (622, 692, 0);
	}
	public void OnCompleteSettingButtonClik(){//返回
		//返回界面  数组重新初始化
	
		GameObject what = GameObject.Find ("chessRENJI");
		what.transform.localPosition = new Vector3 (8888, 8888, 0);
		board.start = true;
		startPanelTween.PlayReverse ();
		optionPanelTween.PlayReverse ();
		//ChessChongzhi chess = new ChessChongzhi ();
		for (int c=ChessChongzhi.Count; c>1; c--) {
			chess.IloveHUIQI();
		}
		for (int i=1; i<=90; i++) {
			GameObject game = GameObject.Find("item"+i.ToString());
			GameObject Clear = GameObject.Find("prefabs"+i.ToString());
			Destroy(game);
			Destroy(Clear);
		}
		what = GameObject.Find ("Musiccup");
		Destroy (what);
	}
	public void ShuangrenAnclick(){//双人

		ChessPeople = 2;
		startPanelTween.PlayForward ();
		optionPanelTween.PlayForward ();
		lab.transform.localPosition = new Vector3 (8888, 8888, 0);
		sprite.transform.localPosition = new Vector3 (8888, 8888, 0);
	}
}
