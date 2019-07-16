using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MalcomBlink : MonoBehaviour {

	public float blinkTime;
	float timer;
	Animator m_Animator;

	void Awake() {
		m_Animator = gameObject.GetComponent<Animator> ();
		InvokeRepeating("_blink", 0.1f, 0.1f);
	}
	void _blink () {
		m_Animator.SetBool("blink", false); 
		int x = Random.Range (0, 40);
		if (x == 0) {
			m_Animator.SetBool("blink", true);
		}
	}
}
