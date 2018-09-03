using UnityEngine;
using System.Collections.Generic;
public enum BackAnimType
{
    BACK_ANIM_TZ,
    BACK_ANIM_BD,
    BACK_ANIM_SD,
    BACK_ANIM_JG,
    BACK_ANIM_JF,
    BACK_ANIM_BOMB,
    BACK_ANIM_BOSS
}
public enum ShowFlagType
{
    SHOW_NONE,
    SHOW_VERSION,
    SHOW_PING,
}
public class SceneObjMgr:Singleton<SceneObjMgr>
{
    public static int PlayHashName = Animator.StringToHash("Play");
    public static int BackAnimHashNameTZ = Animator.StringToHash("Base Layer.Anim_Meteorite");
    public static int BackAnimHashNameBD = Animator.StringToHash("Base Layer.Anim_Frozen");
    public static int BackAnimHashNameSD = Animator.StringToHash("Base Layer.Anim_ChainLightning");
    public static int BackAnimHashNameJG = Animator.StringToHash("Base Layer.Anim_Default");
    public static int BackAnimHashNameJF = Animator.StringToHash("Base Layer.Anim_Cyclone");
    public static int BackAnimHashNameBomb = Animator.StringToHash("Base Layer.Paotaizhengdongbg");
    public static int BackAnimHashNameBossFish = Animator.StringToHash("Base Layer.Anim_Boss");
    GameObject   m_UIRoot;
    Camera       m_UICamera;
    GameObject   m_SceneCamera;
    GameObject   m_UIRootPanel;
    GameObject   m_Background;
    Transform    m_UIPanelTransform;
    Transform    m_UIContainerTransform;
    byte         m_BackImageIdx;
    Animator     m_BackAnim;
    Texture2D    m_BackTex;
    UILabel      m_LogLabel;
    UILabel      m_VerLabel;
    string       m_VerText;
    byte         m_ShowType;

    public Transform UIContainerTransform
    {
        get { return m_UIContainerTransform; }
        set { m_UIContainerTransform = value; }
    }
    public GameObject UIRoot
    {
        get { return m_UIRoot; }
        set { m_UIRoot = value; }
    }
    public void GlobalInit()
    {
        GetSceneObj();
        m_ShowType = (byte)ShowFlagType.SHOW_VERSION;
        m_BackImageIdx = 255;
    }
    public void SetShowType(ShowFlagType t)
    {
        switch(t)
        {
            case ShowFlagType.SHOW_NONE:
                m_VerLabel.text = "";
                break;
            case ShowFlagType.SHOW_PING:
                break;
            case ShowFlagType.SHOW_VERSION:
                m_VerLabel.text = m_VerText;
                m_VerLabel.color = Color.white;
                break;
        }
        m_ShowType = (byte)t;
    }
    public void Update(float delta)
    {
        //if (ServerSetting.SHOW_PING == false)
        //     return;
        //if(m_ShowType == (byte)ShowFlagType.SHOW_PING)
        //{
        //    uint d = SceneMain.Instance.bulletTime.CurTime;
        //    if (d > 999)
        //        d = 999;
        //    if (d < 60)
        //        m_VerLabel.color = Color.green;
        //    else if (d < 90)
        //        m_VerLabel.color = new Color(1.0f, 0.8f, 0);
        //    else if (d < 120)
        //        m_VerLabel.color = new Color(1.0f, 0.4f, 0);
        //    else
        //        m_VerLabel.color = Color.red;
        //    m_VerLabel.text = "延迟:" + d + "ms";
        //}
    }
    public void SetVerText(string ver)
    {
        m_VerText = ver;
        m_VerLabel.text = ver;
    }
    public void ShowLog(string str)
    {
        if (m_LogLabel != null)
        {
            m_LogLabel.text = str;
        }
        else
            GUIHelper.ShowLabel(str, Color.black);
    }
    public Texture2D BackgroundTex
    {
        get
        {
            if (m_BackTex == null)
                m_BackTex = (Texture2D)Resources.Load("InnerRes/UpdateBackground", typeof(Texture2D));
            return m_BackTex;
        }
    }
    public void PlayBack(BackAnimType eType)
    {
        switch (eType)
        {
            case BackAnimType.BACK_ANIM_TZ:
                m_BackAnim.Play(BackAnimHashNameTZ, 0, 0.001f);
                break;
            case BackAnimType.BACK_ANIM_BD:
                m_BackAnim.Play(BackAnimHashNameBD, 0, 0.001f);
                break;
            case BackAnimType.BACK_ANIM_SD:
                m_BackAnim.Play(BackAnimHashNameSD, 0, 0.001f);
                break;
            case BackAnimType.BACK_ANIM_JG:
                m_BackAnim.Play(BackAnimHashNameJG, 0, 0.001f);
                break;
            case BackAnimType.BACK_ANIM_JF:
                m_BackAnim.Play(BackAnimHashNameJF, 0, 0.001f);
                break;
            case BackAnimType.BACK_ANIM_BOMB:
                m_BackAnim.Play(BackAnimHashNameBomb, 0, 0.001f);
                break;
            case BackAnimType.BACK_ANIM_BOSS:
                m_BackAnim.Play(BackAnimHashNameBossFish, 0, 0.001f);
                break;
        }
    }
    public void ResetBackTex()
    {
        m_BackImageIdx = 255;
        ReplaceBackImg(BackgroundTex);
    }
    public void SwapBackgroundImage(byte imgIdx)
    {
        ChangeScrollBackGround(imgIdx);

        if (m_BackImageIdx == imgIdx)
            return;
    
        m_BackImageIdx = imgIdx;
        Texture2D tex = (Texture2D)ResManager.Instance.LoadObject("BackImg" + m_BackImageIdx, "SceneRes/BackImages/", ResType.SceneRes, typeof(Texture2D), false);
        if (tex == null)
        {
            m_BackImageIdx = 0;
            tex = (Texture2D)ResManager.Instance.LoadObject("BackImg" + m_BackImageIdx, "SceneRes/BackImages/", ResType.SceneRes, typeof(Texture2D), false);
        }
        ReplaceBackImg(tex);
    }
    private void ChangeScrollBackGround(int imgIdx)
    {
        int tmpIdx = imgIdx;
        if (imgIdx == 2 || imgIdx == 3)
        {
            tmpIdx = 0;
        }

        if (imgIdx == 0 ||imgIdx == 1)
        {
            m_Background.transform.GetChild(0).gameObject.SetActive(true);
            m_Background.transform.GetChild(1).gameObject.SetActive(true);
            m_Background.transform.GetChild(2).gameObject.SetActive(true);

            ReplaceAllScrollScene(tmpIdx);
        }
        else
        {
            m_Background.transform.GetChild(0).gameObject.SetActive(false);
            m_Background.transform.GetChild(1).gameObject.SetActive(false);
            m_Background.transform.GetChild(2).gameObject.SetActive(false);
        }
    }
    void ReplaceMoveBgByIdx(int objIdx,string textureNamePre,int sceneIdx,int renderQ)
    {
        Transform moveBgObj = m_Background.transform.GetChild(objIdx);

        string path = "NewRes/ScrollBg/NewScroll/scene" + sceneIdx + "/" + textureNamePre;

        Debug.Log("ReplaceMoveBgByIdx  " + path);

        moveBgObj.GetChild(0).gameObject.GetComponent<MeshRenderer>().material.mainTexture = (Texture)Resources.Load(path);
        moveBgObj.GetChild(1).gameObject.GetComponent<MeshRenderer>().material.mainTexture = (Texture)Resources.Load(path + "R");
        moveBgObj.GetChild(0).gameObject.GetComponent<MeshRenderer>().material.renderQueue = renderQ;
        moveBgObj.GetChild(1).gameObject.GetComponent<MeshRenderer>().material.renderQueue = renderQ;
    }
    void ReplaceAllScrollScene(int sceneIdx)
    {
        Debug.Log("SceneObjMgr:ChangeScrollBackGround: " + sceneIdx);

        //int renderQ = m_Background.transform.GetChild(0).GetChild(0).gameObject.GetComponent<MeshRenderer>().material.renderQueue;
        int renderQ = 2000;

        ReplaceMoveBgByIdx(2, "scrollbg_back", sceneIdx, renderQ);
        ReplaceMoveBgByIdx(1, "scrollbg_mid", sceneIdx, renderQ+ 1);
        ReplaceMoveBgByIdx(0, "scrollbg_front", sceneIdx, renderQ+2);
    }
    void ReplaceBackImg(Texture2D tex)
    {

        Debug.Log("是第几张背景......:  " + tex.width+"    jjjj    "+ m_BackImageIdx);
        Texture2D texold = (Texture2D)m_Background.GetComponent<Renderer>().material.mainTexture;
        if (texold != null && texold != m_BackTex)
            Resources.UnloadAsset(texold);
        m_Background.GetComponent<Renderer>().material.mainTexture = tex;
    }

    //konno
    public void StopFishBg()
    {
        m_Background.transform.GetChild(0).gameObject.SetActive(false);
        m_Background.transform.GetChild(1).gameObject.SetActive(false);
        m_Background.transform.GetChild(2).gameObject.SetActive(false);
    }
    public GameObject SceneBackground
    {
        get
        {
            return m_Background;
        }
    }
    public GameObject SceneCamera
    {
        get
        {
            return m_SceneCamera;
        }
    }
    void GetSceneObj()
    {
        m_SceneCamera   = GameObject.Find("SceneCamera");
        m_UIRoot        = GameObject.Find("SceneUIRoot");
        m_Background    = GameObject.Find("SceneBackground");
        InitUIRoot();
        return;
    }
    public bool HasPopupWindow
    {
        get
        {
            return m_UIContainerTransform.childCount > 4;
        }
    }
    void InitUIRoot()
    {
        m_BackAnim = m_Background.GetComponent<Animator>();
        m_UIRootPanel = m_UIRoot.transform.GetChild(1).gameObject;
        m_UICamera = m_UIRoot.transform.GetChild(0).GetComponent<Camera>();
        m_UIPanelTransform = m_UIRootPanel.transform;
        m_UIContainerTransform = m_UIRootPanel.transform.GetChild(0);
        m_VerLabel = m_UIContainerTransform.GetChild(0).GetComponent<UILabel>();
        m_LogLabel = m_UIContainerTransform.GetChild(1).GetComponent<UILabel>();
        Resolution.CheckResolution();

        m_UICamera.aspect = Camera.main.aspect;
        m_UICamera.rect = Camera.main.rect;
    }
    public void LoadInnerUpdateBackMat()
    {
        Material backMat = (Material)Resources.Load("InnerRes/UpdateBackgroundMat", typeof(Material));
        if (backMat != null)
        {
            m_Background.GetComponent<Renderer>().material = backMat;
            m_BackTex = (Texture2D)backMat.mainTexture;
        }
    }
    public void LoadSceneObj()
    {
        return;
        //加载背景和摄像机，UIROOT
        Object objback = ResManager.Instance.LoadObject("SceneBackground", "ExtraRes/ScenePrefab/", ResType.ExtraRes);
        Object objcam = ResManager.Instance.LoadObject("SceneCamera", "ExtraRes/ScenePrefab/", ResType.ExtraRes);
        Object objui = ResManager.Instance.LoadObject("SceneUIRoot", "ExtraRes/ScenePrefab/", ResType.ExtraRes);
        if (objback != null &&
            objcam != null &&
            objui != null)
        {

            GameObject back = GameObject.Find("SceneBackground");
            GameObject camera = GameObject.Find("SceneCamera");
            GameObject uiroot = GameObject.Find("SceneUIRoot");
            if (back != null)
                GameObject.Destroy(back);
            if (camera != null)
                GameObject.Destroy(camera);
            if (uiroot != null)
                GameObject.Destroy(uiroot);

            GameObject goback = (GameObject)GameObject.Instantiate(objback);
            GameObject gocam = (GameObject)GameObject.Instantiate(objcam);
            GameObject goui = (GameObject)GameObject.Instantiate(objui);
            goback.name = "SceneBackground";
            gocam.name = "SceneCamera";
            goui.name = "SceneUIRoot";

            ResManager.Instance.UnloadObject(objback);
            ResManager.Instance.UnloadObject(objcam);
            ResManager.Instance.UnloadObject(objui);
            

            m_Background = goback;
            m_UIRoot = goui;
            InitUIRoot();
        }
        else
            GetSceneObj();
    }
    public Transform UIPanelTransform
    {
        get { return m_UIPanelTransform; }
        set { m_UIPanelTransform = value; }
    }
    public Camera UICamera
    {
        get { return m_UICamera; }
        set { m_UICamera = value; }
    }
}
