using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;//シーン遷移扱う！

public class TitleSceneManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	//MainMenueに移動するメソッド定義
	public void MoveToMainMenue(){//ボタン押した時呼び出し
		SceneManager.LoadScene ("MainMenue");//MainMenueに移動
	}
}
