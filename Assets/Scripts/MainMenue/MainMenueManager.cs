using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;//シーン遷移扱う

public class MainMenueManager : MonoBehaviour {

	public AudioClip buttonSound;//あとでUnityからアタッチ！
	AudioSource audioSource;//取得したコンポーネント格納しておく為の変数定義！
	[SerializeField] GameObject bgm;//Unityからアタッチ

    //=====以下、画面遷移の為の変数=====
	[SerializeField] GameObject view2;//View2をUnityからアタッチ
    Animator animator;//取得したAnimatorを格納しておく為の変数

	// Use this for initialization
	void Start () {
		audioSource = gameObject.GetComponent<AudioSource> ();//AudioSourceのコンポーネント取得、AudioSourceクラスのaudioSource変数に格納！
		animator = view2.gameObject.GetComponent<Animator>();//View2についてるAnimatorを取得→変数animatorに格納
	}
	
	// Update is called once per frame
	void Update () {
		
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


    //=====以下、画面遷移の為の関数=====

	//View2に遷移する為の関数
    public void GoToRankingView()
    {
        animator.SetBool("running", true);
        Debug.Log("GoToView2");
    }

    //View1に戻る為の関数
    public void BackToMainMenu()
    {
        animator.SetBool("running", false);
        Debug.Log("BackToView1");
    }
}
