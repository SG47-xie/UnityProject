using UnityEngine;
using System.Collections;

public class FishPopWindow
{
    GameObject m_GameObject = null;
	public void Init(float duration)
    {
        Object obj = Resources.Load("ExtraRes/UI/NewWindow/FishPopWindow");
        m_GameObject = GameObject.Instantiate(obj) as GameObject;
        m_GameObject.transform.SetParent(SceneObjMgr.Instance.UIPanelTransform, false);

        SceneMain.Instance.StartInnerCoroutine(wait(duration));
    }

    IEnumerator wait(float time)
    {
        yield return new WaitForSeconds(time);
        GameObject.Destroy(m_GameObject);
    }
}
