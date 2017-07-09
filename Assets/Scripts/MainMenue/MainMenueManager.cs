using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;//シーン遷移扱う

public class MainMenueManager : MonoBehaviour {

	public AudioClip buttonSound;//あとでUnityからアタッチ！
	AudioSource audioSource;//取得したコンポーネント格納しておく為の変数定義！
	[SerializeField] GameObject bgm;//Unityからアタッチ

	// Use this for initialization
	void Start () {
		audioSource = gameObject.GetComponent<AudioSource> ();//AudioSourceのコンポーネント取得、AudioSourceクラスのaudioSource変数に格納！
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	//Rankingに移動するメソッド定義
	public void MoveToRanking(){//ボタンに割り当ての為public
//		audioSource.Stop(titleMusic);
		StartCoroutine("MoveRankingWithTimer");
	}

	//GamePlayingに移動するメソッド定義
	public void MoveToGamePlaying(){//ボタンに割り当ての為public
//		audioSource.Stop(titleMusic);
		StartCoroutine("MovePlayingWithTimer");
	}

	IEnumerator MoveRankingWithTimer(){
		audioSource.PlayOneShot(buttonSound);//音出します
		yield return new WaitForSeconds(2.3f);//2秒待ちます
		Destroy(bgm);
		SceneManager.LoadScene ("Ranking");//シーン移動します
	}

	IEnumerator MovePlayingWithTimer(){
		audioSource.PlayOneShot(buttonSound);//音出します
		yield return new WaitForSeconds(2.3f);//2秒待ちます
		Destroy(bgm);
		SceneManager.LoadScene ("GamePlaying");//シーン移動します
	}
}
