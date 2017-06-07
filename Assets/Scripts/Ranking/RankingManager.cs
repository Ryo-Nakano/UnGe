using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;//シーン遷移扱う

public class RankingManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	//MainMenueに戻るメソッド定義
	public void BackToMainMenue(){//ボタンから呼び出すからpublic
		SceneManager.LoadScene("MainMenue");
	}
}
