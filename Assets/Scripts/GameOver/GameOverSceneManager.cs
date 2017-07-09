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

	AudioSource audioSource;//取得したコンポーネント格納しておく為の変数定義！
	public AudioClip buttonSound;//あとでUnityからアタッチ！


	// Use this for initialization
	void Start () {
		audioSource = gameObject.GetComponent<AudioSource> ();//AudioSourceのコンポーネント取得、AudioSourceクラスのaudioSource変数に格納！
		lastScore = PlayerPrefs.GetInt ("Score");//Scoreの名前で保存してあるデータを取ってきて、lastScoreに格納

		//HighScore選別
		if(PlayerPrefs.HasKey("HighScore") == false){//HighScoreが存在しない時
			highScore = lastScore;//lastScoreをhighScoreにして、
			PlayerPrefs.SetInt ("HighScore", lastScore);//HighScoreデータとして格納
		}else{//HighScore存在する時
			if (highScore < lastScore) {//直近の結果の方がhighScoreよりも高かった時
				highScore = lastScore;//highScoreにlastSccoreを代入して
				PlayerPrefs.SetInt ("HighScore",lastScore);//lastScoreの値をHighScoreとして保存
			} else {//普通にhighScoreが最強だった場合
				highScore = PlayerPrefs.GetInt("HighScore");
			}
		}
			

		//スコア表示
		lastScoreText.text = "さっきのスコア : " + lastScore;
		highScoreText.text = "全盛期のスコア : " + highScore;

		//highScoreの方がlastScoreよりも大きい時もなぜかlastScoreの値がhighScoreに保存されてしまう
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	//MainMenueに戻るメソッド定義
	public void ReturnToMainMenue(){//ボタンから呼び出すからpublic
		StartCoroutine("MoveMenueWithTimer");
	}

	IEnumerator MoveMenueWithTimer(){
		audioSource.PlayOneShot(buttonSound);//音出します
		yield return new WaitForSeconds(2.3f);//2秒待ちます
		SceneManager.LoadScene ("MainMenue");//シーン移動します
	}
}
