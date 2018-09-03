using UnityEngine;
using System.Collections;

public class ModeScroolDemo : MonoBehaviour
{
    private float time1 = 0f;
    private float time2 = 0.002f;

    private float time4 = 0f;
    private float time3 = 0.1f;


    private int isadd = -1;

    //private int isHaveChanedniu = -1;
    //private int isHaveChanedbuyu = -1;
    //private int isHaveChanedwuhui = -1;
    //private int isHaveChanedbaoma = -1;

    private Vector2 m_loctionOld ;
    private Vector2 m_loctionCur ;
    //private int m_niuniu = -1;
    //private int m_buyu = -1;
    //private int m_silengwuhui = -1;
    //private int m_benchibaoma = -1;
    private Vector3 touchPosition;//触摸一帧内移动距离

    // HallLoginUI_Btns m_BtnWind = new HallLoginUI_Btns();
    // HallLoginUI_Room m_RoomWind = new HallLoginUI_Room();
    // Use this for initialization
    void Start()
    {

    }
    private void DragMoveMode()
    {
        if (GameObject.Find("VipPrivilege(Clone)") != null && GameObject.Find("VipPrivilege(Clone)").activeInHierarchy)
        {
            return;
        }
        if (GameObject.Find("ShopSysWndUI2(Clone)") != null && GameObject.Find("ShopSysWndUI2(Clone)").activeInHierarchy)
        {
            return;
        }

#if UNITY_STANDALONE_WIN || UNITY_EDITOR
        if (Input.GetMouseButton(0))
        {
            float offsetX = Input.GetAxis("Mouse X");
            this.gameObject.transform.Translate(new Vector3(offsetX * 0.1f, 0, 0));
        }
#elif UNITY_IOS
         if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
         {
             touchPosition = Input.GetTouch(0).deltaPosition;
             this.gameObject.transform.Translate(new Vector3(touchPosition.x * 0.0018f, 0, 0));
         }
#elif UNITY_ANDROID
         if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
         {
             touchPosition = Input.GetTouch(0).deltaPosition;
             this.gameObject.transform.Translate(new Vector3(touchPosition.x * 0.006f, 0, 0));
         }
#endif

    }
    // Update is called once per frame
    float _eventInt = 0f;
    void Update()
    {

        if(Input.GetMouseButtonUp(0))
        {
            if (GameObject.Find("Niuniu(Clone)") != null && GameObject.Find("Niuniu(Clone)").activeInHierarchy)
            {
                return;

            }
            if (GameObject.Find("Car(Clone)") != null && GameObject.Find("Car(Clone)").activeInHierarchy)
            {
                return;
            }// ||
            
            if(GameObject.Find("FriendSysWidget0(Clone)") != null && GameObject.Find("FriendSysWidget0(Clone)").activeInHierarchy)
            {
                return;
            }
            if(GameObject.Find("ShopSysWndUI2(Clone)") != null && GameObject.Find("ShopSysWndUI2(Clone)").activeInHierarchy)
            {
                return;
            }
            if (GameObject.Find("ShopSysWndUI3(Clone)") != null && GameObject.Find("ShopSysWndUI3(Clone)").activeInHierarchy)

            {
                return;
            }
            if (GameObject.Find("ActivityWnd(Clone)") != null && GameObject.Find("ActivityWnd(Clone)").activeInHierarchy)
            {
                return;
            }
            if (GameObject.Find("SettingWind(Clone)") != null && GameObject.Find("SettingWind(Clone)").activeInHierarchy)
            {
                return;
            }
           
           
           // if (GameObject.Find("AboutOurWnd(Clone)") != null && GameObject.Find("AboutOurWnd(Clone)").activeInHierarchy)
          //  {
               // return;
          //  }
            if (GameObject.Find("ShopSysWndUI0(Clone)") != null && GameObject.Find("ShopSysWndUI0(Clone)").activeInHierarchy)
            {
                return;
            }
            if (GameObject.Find("ShopSysWndUI1(Clone)") != null && GameObject.Find("ShopSysWndUI1(Clone)").activeInHierarchy)
            {
                return;
            }
            if (GameObject.Find("ShopSysWndUI2(Clone)") != null && GameObject.Find("ShopSysWndUI2(Clone)").activeInHierarchy)
            {
                return;
            }
            if (GameObject.Find("ShopSysWndUI3(Clone)") != null && GameObject.Find("ShopSysWndUI3(Clone)").activeInHierarchy)
            {
                return;
            }
            if (GameObject.Find("ShopSysWndUI4(Clone)") != null && GameObject.Find("ShopSysWndUI4(Clone)").activeInHierarchy)
            {
                return;
            }
            if (GameObject.Find("ShopSysWndUI5(Clone)") != null && GameObject.Find("ShopSysWndUI5(Clone)").activeInHierarchy)
            {
                return;
            }
            if (GameObject.Find("ShopSysWndUI6(Clone)") != null && GameObject.Find("ShopSysWndUI6(Clone)").activeInHierarchy)
            {
                return;
            }
            if (GameObject.Find("ShopSysWndUI8(Clone)") != null && GameObject.Find("ShopSysWndUI8(Clone)").activeInHierarchy)
            {
                return;
            }
            if (GameObject.Find("ShopSysWndUI7(Clone)") != null && GameObject.Find("ShopSysWndUI7(Clone)").activeInHierarchy)
            {
                return;
            }
            if (GameObject.Find("VipPrivilege(Clone)") != null && GameObject.Find("VipPrivilege(Clone)").activeInHierarchy)
            {
                return;
            }

            m_loctionCur = new Vector2(Input.mousePosition.x,Input.mousePosition.y);// this.gameObject.transform.localPosition.x;
            //Debug.Log("m_loctionCur" + m_loctionCur);
            float _value = Vector2.Distance(m_loctionOld, m_loctionCur); //Mathf.Abs(m_loctionCur - m_loctionOld);
            if(_value<0.01f)
            {
                Camera cam = GameObject.Find("SceneUIRoot/UiCamera").GetComponent<Camera>();
                Ray ray = cam.ScreenPointToRay(Input.mousePosition);//从摄像机发出到点击坐标的射线
                RaycastHit hitInfo;
                if (Physics.Raycast(ray, out hitInfo))
                {
                    Debug.DrawLine(ray.origin, hitInfo.point);//划出射线，只有在scene视图中才能看到
                    GameObject gameObj = hitInfo.collider.gameObject;
                    Debug.Log("click object name is " + gameObj.name);
                    if (gameObj.name == "longhuabuyu")//当射线碰撞目标为boot类型的物品 ，执行拾取操作
                    {
                        Debug.Log("CenterLiftBtn");
                        HallRunTimeInfo.Instance_UI.m_loginUi.ChangeHallWind(HallLogicUIStatue.Hall_State.Hall_SelectRoom);
                        //// GlobalHallUIMgr.Instance.ShowNiuniuWnd();
                    }
                    else if (gameObj.name == "bairenniuniu")
                    {
                        PlayerRole.Instance.RoleMiNiGame.m_NiuNiuGame.OnJoinRoom();
                        Debug.Log("bairenniuniu");
                        // HallRunTimeInfo.Instance_UI.m_loginUi.ChangeHallWind(HallLogicUIStatue.Hall_State.Hall_SelectRoom);
                    }
                    else if (gameObj.name == "senlinwuhui")
                    {
                        PlayerRole.Instance.RoleMiNiGame.m_DialGame.OnJoinRoom();
                        //Debug.Log("pick up!");
                        ///HallRunTimeInfo.Instance_UI.m_loginUi.ChangeHallWind(HallLogicUIStatue.Hall_State.Hall_SelectRoom);
                    }
                    else if (gameObj.name == "benchibaoma" && "Car" == this.gameObject.name)       
                    {
                        PlayerRole.Instance.RoleMiNiGame.m_CarGame.OnJoinRoom();
                        Debug.Log("benchibaoma");

                        //HallRunTimeInfo.Instance_UI.m_loginUi.ChangeHallWind(HallLogicUIStatue.Hall_State.Hall_SelectRoom);
                    }
                }
            }

        }

        // Input.GetMouseButton
        if (Input.GetMouseButtonDown(0))
        {

            m_loctionOld = new Vector2(Input.mousePosition.x,Input.mousePosition.y);
            //Debug.Log("m_loctionOld" + m_loctionOld);
            return;
        }

        //DragMoveMode();

//         Vector3 pos = this.gameObject.transform.localPosition;
//         time1 += Time.deltaTime;
//         time4 += Time.deltaTime;
//         if (time1 >= time2)
//         {
// 
//             pos.x -= 0.5f;
//             this.gameObject.transform.localPosition = pos;
//             time1 = 0;
//         }
        //TurnAround();
        //ChangeBig();

//         if (this.gameObject.transform.localPosition.x<=-662)
//         {
//             pos.x = 800;
//             this.gameObject.transform.localPosition= pos;
// 
//         }
//         if(this.gameObject.transform.localPosition.x>=802)
//         {
//             pos.x = -660;
//             this.gameObject.transform.localPosition = pos;
//         }

    }
    //     private GameObject FindLeftObj(bool isSelfLeft)//找到目前最左边的Obj,如果自己是第一个就找第二个
    //     {
    //         Transform parentPanel = SceneObjMgr.Instance.UIRoot.transform.Find("ParentPanel");
    //         GameObject mianHallWind = parentPanel.Find("MianHallWind(Clone)").gameObject;
    //         if (mianHallWind.name != "MianHallWind(Clone)")
    //         {
    //             return null;
    //         }
    // 
    //         int minIdx = 6;
    //         Transform minXTrans = mianHallWind.transform.GetChild(minIdx);
    //         for (int i = 6; i < 10; i++)
    //         {
    //             Transform nowTrans = mianHallWind.transform.GetChild(i);
    //             if (minXTrans.localPosition.x > nowTrans.localPosition.x)
    //             {
    //                 if (isSelfLeft)
    //                 {
    // //                     if (GetMyIdx() == 9)
    // //                     {
    // //                         minIdx = i;
    // //                         minXTrans = nowTrans;
    // //                     }
    //                     if (nowTrans == this.transform) continue;
    //                 }
    // 
    //                 minIdx = i;
    //                 minXTrans = nowTrans;
    //             }
    //         }
    // 
    //         Debug.Log("minIdx "+ minIdx);
    //         return minXTrans.gameObject;
    //     }
    //     //参数true表示从左边超出屏幕范围
    //     private Vector3 GetCurPos(bool leftOrRIght)
    //     {
    //         Transform parentPanel = SceneObjMgr.Instance.UIRoot.transform.Find("ParentPanel");
    //         GameObject mianHallWind = parentPanel.Find("MianHallWind(Clone)").gameObject;
    //         if (mianHallWind.name != "MianHallWind(Clone)")
    //         {
    //             return new Vector3(0, 0, 0);
    //         }
    // 
    //         GameObject leftObj = mianHallWind.transform.GetChild(GameManager.Instance.HallLeftLogoIdx).gameObject;
    //         if (leftObj == null) return new Vector3(0, 0, 0);
    // 
    //         int myIdx = GetMyIdx();
    //         float offsetX = 360;
    //         Vector3 myV3 = mianHallWind.transform.GetChild(myIdx).localPosition;
    // 
    //         int offsetIdx = 3;//myIdx - leftIdx
    //         myV3.x = leftObj.transform.localPosition.x + offsetIdx * offsetX;
    //         return myV3;
    //     }
    //     private int GetMyIdx()
    //     {
    //         if (this.gameObject.name == "benchibaoma")
    //         {
    //             return 6;
    //         }
    //         else if (this.gameObject.name == "senlinwuhui")
    //         {
    //             return 7;
    //         }
    //         else if (this.gameObject.name == "bairenniuniu")
    //         {
    //             return 8;
    //         }
    //         else
    //         {
    //             return 9;
    //         }
    //     }
    
    private void ChangeBig()
    {
        Vector3 _scale = this.gameObject.transform.localScale;
        if (time4 >= time3)
        {
            if (this.gameObject.name == "bairenniuniu")
            {

                if (this.gameObject.transform.localPosition.x >= 280)
                {
                    this.gameObject.transform.localScale = new Vector3(90, 90, 90);
                }
                else if (this.gameObject.transform.localPosition.x > 0)
                {
                    float temp = 140 - 50 * this.gameObject.transform.localPosition.x / 280;
                    _scale.x = temp;
                    _scale.y = temp;
                    _scale.z = temp;
                    //_scale.x += 0.6f;
                    //_scale.y += 0.6f;
                    //_scale.z += 0.6f;
                    this.gameObject.transform.localScale = _scale;
                }
                else if (this.gameObject.transform.localPosition.x > -280)
                {
                    float temp = 140 + 50 * this.gameObject.transform.localPosition.x / 280;
                    _scale.x = temp;
                    _scale.y = temp;
                    _scale.z = temp;
                    //_scale.x -= 0.6f;
                    //_scale.y -= 0.6f;
                    //_scale.z -= 0.6f;
                    this.gameObject.transform.localScale = _scale;
                }
            }

            if (this.gameObject.name == "CenterLiftBtn")
            {

                if (this.gameObject.transform.localPosition.x >= 280)
                {
                    this.gameObject.transform.localScale = new Vector3(90, 90, 90);
                }
                else if (this.gameObject.transform.localPosition.x > 0)
                {
                    float temp = 140 - 50 * this.gameObject.transform.localPosition.x / 280;
                    _scale.x = temp;
                    _scale.y = temp;
                    _scale.z = temp;
                    //_scale.x += 0.6f;
                    //_scale.y += 0.6f;
                    //_scale.z += 0.6f;
                    this.gameObject.transform.localScale = _scale;
                }
                else if (this.gameObject.transform.localPosition.x > -280)
                {
                    float temp = 140 + 50 * this.gameObject.transform.localPosition.x / 280;
                    _scale.x = temp;
                    _scale.y = temp;
                    _scale.z = temp;
                    //_scale.x -= 0.6f;
                    //_scale.y -= 0.6f;
                    //_scale.z -= 0.6f;
                    this.gameObject.transform.localScale = _scale;
                }
            }




            if (this.gameObject.name == "senlinwuhui")
            {
                if (this.gameObject.transform.localPosition.x >= 280)
                {
                    this.gameObject.transform.localScale = new Vector3(90, 90, 90);
                }
                else if (this.gameObject.transform.localPosition.x > 0)
                {
                    float temp = 140 - 50 * this.gameObject.transform.localPosition.x / 280;
                    _scale.x = temp;
                    _scale.y = temp;
                    _scale.z = temp;
                    //_scale.x += 0.6f;
                    //_scale.y += 0.6f;
                    //_scale.z += 0.6f;
                    this.gameObject.transform.localScale = _scale;
                }
                else if (this.gameObject.transform.localPosition.x > -280)
                {
                    float temp = 140 + 50 * this.gameObject.transform.localPosition.x / 280;
                    _scale.x = temp;
                    _scale.y = temp;
                    _scale.z = temp;
                    //_scale.x -= 0.6f;
                    //_scale.y -= 0.6f;
                    //_scale.z -= 0.6f;
                    this.gameObject.transform.localScale = _scale;
                }
            }


            if (this.gameObject.name == "benchibaoma")
            {
                if (this.gameObject.transform.localPosition.x >= 280)
                {
                    this.gameObject.transform.localScale = new Vector3(90, 90, 90);
                }
                else if (this.gameObject.transform.localPosition.x > 0)
                {
                    float temp = 130 - 40 * this.gameObject.transform.localPosition.x / 280;
                    _scale.x = temp;
                    _scale.y = temp;
                    _scale.z = temp;
                    //_scale.x += 0.6f;
                    //_scale.y += 0.6f;
                    //_scale.z += 0.6f;
                    this.gameObject.transform.localScale = _scale;
                }
                else if (this.gameObject.transform.localPosition.x > -280)
                {
                    float temp = 130 + 40 * this.gameObject.transform.localPosition.x / 280;
                    _scale.x = temp;
                    _scale.y = temp;
                    _scale.z = temp;
                    //_scale.x -= 0.6f;
                    //_scale.y -= 0.6f;
                    //_scale.z -= 0.6f;
                    this.gameObject.transform.localScale = _scale;
                }
            }




            time4 = 0f;
        }
    }
}