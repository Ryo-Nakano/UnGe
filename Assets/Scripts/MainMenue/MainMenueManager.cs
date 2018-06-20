using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;//シーン遷移扱う
using UnityEngine.UI;

public class MainMenueManager : MonoBehaviour {

	public AudioClip buttonSound;//あとでUnityからアタッチ！
	AudioSource audioSource;//取得したコンポーネント格納しておく為の変数定義！
	[SerializeField] GameObject bgm;//Unityからアタッチ

    //=====以下、画面遷移の為の変数=====
	[SerializeField] GameObject view2;//View2をUnityからアタッチ
	Animator slidingHorizontalAnimator;//取得したAnimatorを格納しておく為の変数

	[SerializeField] GameObject view3;//View3をUnityからアタッチ
	Animator slidingVerticalAnimator;//取得したAnimatorを格納しておく為の変数

	[SerializeField] InputField inputField;
    [SerializeField] Text alertText;

	// Use this for initialization
	void Start () {
		audioSource = gameObject.GetComponent<AudioSource> ();//AudioSourceのコンポーネント取得、AudioSourceクラスのaudioSource変数に格納！
		slidingHorizontalAnimator = view2.gameObject.GetComponent<Animator>();//View2についてるAnimatorを取得→変数animatorに格納
		slidingVerticalAnimator = view3.GetComponent<Animator>();//View3についてるAnimatorを取得→変数animatorに格納
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

	//View1→View2に遷移する為の関数
    public void GoToRankingView()
    {
        slidingHorizontalAnimator.SetBool("runningH", true);
        Debug.Log("View1 → View2");
    }

    //View2→View1に戻る為の関数
    public void BackToMainMenu()
    {
        slidingHorizontalAnimator.SetBool("runningH", false);
		Debug.Log("View2 → View1");
    }

	//View1→View3に遷移する為の関数
    public void GoToProfileView()
    {
		slidingVerticalAnimator.SetBool("runningV", true);
		Debug.Log("View1 → View3");
    }

    //View3→View1に戻る為の関数
    public void BackToMainMenu2()
    {
		slidingVerticalAnimator.SetBool("runningV", false);
		inputField.text = "";
		alertText.text = "";
		Debug.Log("View3 → View1");
    }
}
