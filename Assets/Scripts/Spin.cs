using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour {
	[SerializeField]
	private float _spinSpeed;

	[SerializeField]
	private Vector3 _axis;
	
	void Update () {
		transform.Rotate(_axis, _spinSpeed);
	}
}
