using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;//シーン遷移扱う
using UnityEngine.UI;//Canvas扱う

public class GameOverSceneManager : MonoBehaviour {

	public Text highScoreText;
	public Text lastScoreText;
	int lastScore;
	int highScore;


	// Use this for initialization
	void Start () {
		lastScore = PlayerPrefs.GetInt ("Score");//Scoreの名前で保存してあるデータを取ってきて、lastScoreに格納

		//HighScore選別
		if(PlayerPrefs.HasKey("HighScore") == false){//HighScoreが存在しない時
			highScore = lastScore;//lastScoreをhighScoreにして、
			PlayerPrefs.SetInt ("HighScore", lastScore);//HighScoreデータとして格納
		}else{//HighScore存在する時
			if (highScore < lastScore) {//直近の結果の方がhighScoreよりも高かった時
				highScore = lastScore;//highScoreにlastSccoreを代入して
				PlayerPrefs.SetInt ("HighScore", highScore);
			} else {//普通にhighScoreが最強だった場合
				highScore = PlayerPrefs.GetInt("HighScore");
			}
		}
			

		//スコア表示
		lastScoreText.text = "LastScore : " + lastScore;
		highScoreText.text = "HighScore : " + highScore;

		//highScoreの方がlastScoreよりも大きい時もなぜかlastScoreの値がhighScoreに保存されてしまう
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	//MainMenueに戻るメソッド定義
	public void ReturnToMainMenue(){//ボタンから呼び出すからpublic
		SceneManager.LoadScene("MainMenue");
	}
}
