using UnityEngine;
using System.Collections;

public class ManyGoldEffect{
    GameObject m_GameObject;

    ParticleSystem m_ExplodeParticle;
    ParticleSystem m_CircleParticle;
    GameObject m_Window;
    UILabel m_GoldLabel;

    float m_ExplodeTime = 1.2f;
    float m_CircleTime = 4.2f;

    Vector3 m_TargetPosition;//特效显示位置
    Vector3 m_WindowPosition;//特效显示位置
    int m_GoldCount;
    // Use this for initialization
    public void Init (Vector3 v3,int goldCount) {
        m_TargetPosition = v3;
        m_TargetPosition.z = 0.0f;
        m_WindowPosition = v3;
        m_GoldCount = goldCount;

        Object obj = Resources.Load("NewRes/ManyGold");
        m_GameObject = GameObject.Instantiate(obj) as GameObject;
        m_GameObject.transform.localPosition = m_TargetPosition;

        m_ExplodeParticle = m_GameObject.transform.GetChild(0).gameObject.GetComponent<ParticleSystem>();
        m_CircleParticle = m_GameObject.transform.GetChild(1).gameObject.GetComponent<ParticleSystem>();

        SceneMain.Instance.StartInnerCoroutine(RunCircle());
    }

    IEnumerator RunCircle()
    {
        m_ExplodeParticle.Play();
        yield return new WaitForSeconds(m_ExplodeTime);
        m_CircleParticle.Play();
        ShowWindow();
        yield return new WaitForSeconds(m_CircleTime);
        DestroyAll();
    }
    private void ShowWindow()
    {
        Object obj = Resources.Load("ExtraRes/UI/NewWindow/FishPopWindow");
        m_Window = GameObject.Instantiate(obj) as GameObject;
        m_Window.transform.localPosition = m_WindowPosition;
        //m_Window.transform.SetParent(SceneObjMgr.Instance.UIPanelTransform, false);

        m_GoldLabel = m_Window.transform.GetChild(0).GetChild(0).GetComponent<UILabel>();
        if (m_GoldCount == 0)
        {
            m_GoldLabel.text = "";
        }
        else
        {
            m_GoldLabel.text = m_GoldCount.ToString();
        }
    }
    private void DestroyAll()
    {
        GameObject.Destroy(m_GameObject);
        GameObject.Destroy(m_Window);
    }
}
