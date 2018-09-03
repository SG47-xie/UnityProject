using UnityEngine;
using System.Collections;

public class KonnoTool : MonoBehaviour {

    public static void SetV3ZToZero(Transform trans)
    {
        Vector3 oldV3 = trans.localPosition;
        oldV3.z = 0;
        trans.localPosition = oldV3;
    }

	public static void ShowBossComingWindow()
    {
//         ManyGoldEffect effect = new ManyGoldEffect();
//         effect.Init();
    }
    public static void ShowBossCatched(Vector3 v3,int goldCount = 0)
    {
//         ManyGoldEffect effect = new ManyGoldEffect();
//         effect.Init(v3, goldCount);
    }
}
