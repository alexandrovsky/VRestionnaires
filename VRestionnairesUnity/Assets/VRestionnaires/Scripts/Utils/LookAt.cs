using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{
	public Transform target;
	public GameObject arrow;
    // Start is called before the first frame update
    void Start()
    {
		

		Hashtable ht = new Hashtable();

		//ht.Add(iT.ScaleBy.looptype,iTween.LoopType.pingPong);
		ht.Add(iT.ScaleTo.time, 1);
		ht.Add(iT.ScaleTo.looptype,"pingPong");
		ht.Add(iT.ScaleTo.delay, 0);
		ht.Add(iT.ScaleTo.x, arrow.transform.localScale.x * 1.25f);
		ht.Add(iT.ScaleTo.y, arrow.transform.localScale.y * 1.25f);
		ht.Add(iT.ScaleTo.z, arrow.transform.localScale.z * 1.25f);
		iTween.ScaleTo(arrow, ht);
	}

    // Update is called once per frame
    void Update()
    {
		if(target) {
			transform.LookAt(target,Vector3.up);
		}
    }
}
