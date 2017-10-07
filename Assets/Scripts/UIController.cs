using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour {

	public void closeView () {
		GameObject currentModel = GameObject.FindGameObjectWithTag ("CurrentModel");
		if (currentModel != null) {
			GameObject.Destroy (currentModel);
		}
		GameManager.modelInstantiated = false;
	}
}
