using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour {

	public void closeView () {
		GameObject[] currentModels = GameObject.FindGameObjectsWithTag ("CurrentModel");
		foreach (GameObject currentModel in currentModels) {
			if (currentModel != null) {
				GameObject.Destroy (currentModel);
			}
		}
		GameManager.modelInstantiated = false;
	}
}
