using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour {

	public static DataManager instance;
	public string[,] playLog;

	// Use this for initialization
	void Awake () {//Startより早い
		if (instance == null) {
			instance = this;
		} else {
			Destroy (this.gameObject);
		}

		DontDestroyOnLoad (this);
	}
	
	public void Load(){
		playLog = csvManager.GetCsvData ("CSV/test");
	}

	public void Save(){
		csvManager.WriteData ("CSV/test.csv", playLog);
	}
}
