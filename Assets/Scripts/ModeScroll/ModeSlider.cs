using UnityEngine;
using System.Collections;

public class ModeSlider : MonoBehaviour {

	public Camera cam;//移动的摄像机
	private Vector3 touchPosition;//触摸一帧内移动距离
    void Update()
    {
       // cam=
        DragMoveMode();
    }
    private void DragMoveMode()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            touchPosition = Input.GetTouch(0).deltaPosition;
            Camera.main.transform.Translate(new Vector3(-touchPosition.x * 0.1f, 0, 0));
        }
    }
}
