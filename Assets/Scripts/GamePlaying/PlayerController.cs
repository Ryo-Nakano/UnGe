using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;//Canvas扱う
using UnityEngine.SceneManagement;//シーン遷移扱う

public class PlayerController : MonoBehaviour {

	[SerializeField]float speed;
	public bool isMove = true; 
	Animator anim;

	AudioSource audioSource;//取得したコンポーネント格納しておく為の変数定義！
	[SerializeField] AudioClip moveSound;//音素材後でUintyからアタッチ！
	[SerializeField] AudioClip gameOverSound;
	[SerializeField] GameObject bgm;//GameObject型のbgm変数！

	public Text howManyDoorsText;
	public Text nowScore;

	int howManyDoors = 10;//残りDoor枚数表示用
	int count = 0;//スコア格納用

	// Use this for initialization
	void Start () {
		anim = this.gameObject.GetComponent<Animator>();//Animator取得して格納
		audioSource = gameObject.GetComponent<AudioSource> ();//AudioSourceのコンポーネント取得、AudioSourceクラスのaudioSource変数に格納！

		howManyDoorsText.text = "残りDoor枚数 : " + howManyDoors;//先ず最初にドア残り枚数表示
		nowScore.text = "Score : " + 0;
	}
	
	// Update is called once per frame
	void Update () {
		MoveAhead ();//isMove == trueのとき前進 
	}

	//==========Payerの動き==========

	//ボタン押して左に動く
	public void MoveToLeft(){
		audioSource.PlayOneShot (moveSound);
		this.gameObject.transform.position = new Vector3 (1.4f, 0, this.gameObject.transform.position.z); 
	}

	//ボタン押して右に動く
	public void MoveToRight(){
		audioSource.PlayOneShot (moveSound);
		this.gameObject.transform.position = new Vector3 (3.2f, 0, this.gameObject.transform.position.z); 
	}

	//前に進むメソッド
	public void MoveAhead(){
		if(isMove == true){//isMoveがtrueの時だけ動く
			this.gameObject.transform.position += new Vector3 (0, 0, speed * Time.deltaTime);	
		}
	}



	//==========Doorに当たった時==========

	int RandomNumber(){
		int randomNumber = Random.Range (0,2);//0か1返す
		return randomNumber;
	}

	//ドアに当たった時の処理
	void OnTriggerEnter(Collider col){

		if(col.tag == "Door"){//ぶつかった相手がDoorだった時

			//Door通れるとき
			if (RandomNumber () == 0) {
				Debug.Log("GoNextDoor!!");

				//残りDoor枚数-1の処理
				howManyDoors--;//Door枚数-1
				howManyDoorsText.text = "残りDoor枚数 : " + howManyDoors;//残りDoor枚数表示

				count += 100;
				nowScore.text = "Score : " + count;//現在のスコア表示

//				Instantiate ("GoodEffect", this.transform.position, Quaternion.identity);

				//効果音鳴らす
				//パーティクルシステム呼び出し(Instantiate？)

				//Door通れないとき
			} else {
				Debug.Log("Stop!!");
				isMove = false;//動き止めて
				anim.SetBool ("isGo", false);//アニメーションも止める 
				PlayerPrefs.SetInt("Score", count);//Scoreの名前でcountの値を保存！
				Invoke("MoveToGameOver", 3f);//3秒後GameOverシーン移動
				audioSource.PlayOneShot (gameOverSound);
				Destroy (bgm);
				Debug.Log("GoToGameOverScene");

				//効果音鳴らす
			}
		}

		//最初左右選択しないと死ぬ
		if(col.tag == "Die"){
			Debug.Log("Select Right or Left!!");
			isMove = false;//動き止めて
			anim.SetBool ("isGo", false); //アニメーションも止める
			PlayerPrefs.SetInt("Score", count);//Scoreの名前でcountの値を保存！
			Invoke("MoveToGameOver", 3f);//3秒後GameOverシーン移動
			Debug.Log("GoToGameOverScene");
			Destroy (bgm);
			audioSource.PlayOneShot (gameOverSound);
		}
	}

	//GameOverシーンに移動する
	void MoveToGameOver(){
		SceneManager.LoadScene("GameOver");
	}
}
