using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClampPositionToIntegers : MonoBehaviour {
	private void FixedUpdate () {	    
        gameObject.transform.position = new Vector3(
            Mathf.RoundToInt(gameObject.transform.position.x),
	        Mathf.RoundToInt(gameObject.transform.position.y),
	        Mathf.RoundToInt(gameObject.transform.position.z)
        );
    }
}
