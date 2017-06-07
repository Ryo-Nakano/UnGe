using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;//シーン遷移扱う

public class MainMenueManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	//Rankingに移動するメソッド定義
	public void MoveToRanking(){//ボタンに割り当ての為public
		SceneManager.LoadScene ("Ranking");
	}

	//GamePlayingに移動するメソッド定義
	public void MoveToGamePlaying(){//ボタンに割り当ての為public
		SceneManager.LoadScene ("GamePlaying");
	}
}
