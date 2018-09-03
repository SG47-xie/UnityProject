using UnityEngine;
using System.Collections;

public class Huadong : MonoBehaviour {

    // Use this for initialization

    private GameObject obj;
    private float timer1=0f;
    private float timer2=0.02f;
	void Start () {
        
        if (this.gameObject.name== "CenterLiftBtn")
        {
           // this.gameObject.SetActive(false);
        }
       
	}
	
	// Update is called once per frame
	void Update ()
    {
        timer1 += Time.deltaTime;
        if(timer1>=timer2)
        {
            // transform.Translate(transform.right* Time.deltaTime);
            // if (this.gameObject.name == "Fish15")
             // {
              //  print(this.gameObject.transform.position.x);
            //  }
           // if (this.gameObject.name == "Fish15")
           // {
                Vector3 pos = this.gameObject.transform.position;
                pos.x+=0.005f;
                this.gameObject.transform.position = pos;
            if (this.gameObject.name == "Fish15")
            {
                print(this.gameObject.transform.position.x);

            }
                //  }

                timer1 = 0;
        }
	}
}
