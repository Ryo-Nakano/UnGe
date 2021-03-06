﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;//シーン遷移扱う！

public class TitleSceneManager : MonoBehaviour {
	public AudioClip buttonSound;//あとでUnityからアタッチ！
	AudioSource audioSource;//取得したコンポーネント格納しておく為の変数定義！


	// Use this for initialization
	void Start () {
		audioSource = gameObject.GetComponent<AudioSource> ();//先ずAudioSourceを取得しておく！
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	//MainMenueに移動するメソッド定義
	public void MoveToMainMenue(){//ボタン押した時呼び出し
//		if (PlayerPrefs.HasKey ("UserName") == true) {//UserNameが存在する時
			StartCoroutine("MoveSceneWithTimer", "MainMenue");//普通にシーン遷移(MainMenueへ)
//		} else {//UserName存在しない時→初プレイの時
//			StartCoroutine("MoveSceneWithTimer", "GetUserName");
//		}
	}

	//シーン遷移するメソッド(stringの引数渡してそのシーンい移動！)
	IEnumerator MoveSceneWithTimer(string sceneName){
		audioSource.PlayOneShot(buttonSound);//音出します
		yield return new WaitForSeconds(2.3f);//2秒待ちます
		SceneManager.LoadScene (sceneName);//シーン移動します
	}
		
}
