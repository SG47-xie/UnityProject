using UnityEngine;
using System.Collections;

public class LogoRound : MonoBehaviour {

    private float RoundInterval = 0.1f;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        this.gameObject.transform.Rotate(new Vector3(0, RoundInterval, 0));
    }
}
