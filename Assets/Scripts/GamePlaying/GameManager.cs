using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;//シーン遷移扱う

public class GameManager : MonoBehaviour {

//	int playCount;//プレイ回数！


	// Use this for initialization
//	void Awake(){
//		if (PlayerPrefs.HasKey ("PlayCount") == true) {//PlayCount存在する時
//			playCount = PlayerPrefs.GetInt("PlayCount");//PlayCountから値を取得
//		}
//	}


	void Start () {
//		playCount++;
//		PlayerPrefs.SetInt ("PlayCount", playCount);//"PlayCount"のkeyでplayCountの値を保存！
	}
	
	// Update is called once per frame
	void Update () {
			
	}

	//GameOverシーンに移動する
//	public void MoveToGameOver(){
//		SceneManager.LoadScene("GameOver");
//	}
		



}
