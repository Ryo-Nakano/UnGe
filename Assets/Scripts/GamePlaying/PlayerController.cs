using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;//Canvas扱う
using UnityEngine.SceneManagement;//シーン遷移扱う

public class PlayerController : MonoBehaviour {

	[SerializeField]float speed;
	Animator anim;

	AudioSource audioSource;//取得したコンポーネント格納しておく為の変数定義！
	[SerializeField] AudioClip moveSound;//音素材後でUintyからアタッチ！
	[SerializeField] AudioClip gameOverSound;
	[SerializeField] AudioClip successSound;
	[SerializeField] GameObject bgm;//GameObject型のbgm変数！
	[SerializeField] GameObject goodEffect;

	public Text howManyDoorsText;
	public Text nowScore;
	bool gameClear = false;//ゲームクリアしたか、してないか(通常false)

	int howManyDoors = 10;//残りDoor枚数表示用 & クリアしたかしてないか判断
	int count = 0;//スコア格納用

	bool canPlay = true;//Playerが操作可能かどうか判定するフラグ(死んだらfalseになる→Player操作できなくなる)

	DataManager dm;//DataManagerのインスタンスを格納しておく為の変数

	// Use this for initialization
	void Start () {
		anim = this.gameObject.GetComponent<Animator>();//Animator取得して格納
		audioSource = gameObject.GetComponent<AudioSource> ();//AudioSourceのコンポーネント取得、AudioSourceクラスのaudioSource変数に格納！

		howManyDoorsText.text = "残りDoor枚数 : " + howManyDoors;//先ず最初にドア残り枚数表示
		nowScore.text = "Score : " + 0;

		dm = GameObject.Find("DataManager").GetComponent<DataManager>();

//		if(PlayerPrefs.HasKey("ClearCount") == true){//"ClearCount"が存在する時！
//			clearCount = PlayerPrefs.GetInt("ClearCount");//"ClearCount"をclearCountに保持！
//		}
	}
	
	// Update is called once per frame
	void Update () {
		MoveAhead ();//isMove == trueのとき前進 
	}

	//==========Payerの動き==========

	//ボタン押して左に動く
	public void MoveToLeft(){
		if(canPlay == true)//canPlayがtrueの時だけ操作可能
		{
			audioSource.PlayOneShot (moveSound);
            this.gameObject.transform.position = new Vector3 (1.4f, 0, this.gameObject.transform.position.z); 
		}
	}

	//ボタン押して右に動く
	public void MoveToRight(){
		if (canPlay == true)//canPlayがtrueの時だけ操作可能
		{
			audioSource.PlayOneShot(moveSound);
            this.gameObject.transform.position = new Vector3(3.2f, 0, this.gameObject.transform.position.z);
		}
	}

	//前に進むメソッド
	public void MoveAhead(){
		if(canPlay == true){//canPlayがtrueの時だけ自動前進
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
				if(howManyDoors == 0){//残りドア枚数が0の時→クリアした時！
//					clearCount++;//クリアカウントに1足す
//					PlayerPrefs.SetInt("ClearCount", clearCount);//"ClearCount"のkeyでclearCountで保存！
					gameClear = true;//gameClearをtrueにする！
				}

				howManyDoorsText.text = "残りDoor枚数 : " + howManyDoors;//残りDoor枚数表示

				count += 100;
				nowScore.text = "Score : " + count;//現在のスコア表示

				audioSource.PlayOneShot (successSound);//扉突破時効果音鳴らす
				Instantiate (goodEffect, this.transform.position, this.transform.rotation * Quaternion.Euler(-90, 0, 0));//Playerの位置にInstantiate
                
			//Door通れないとき
			} else {
				canPlay = false;//Playerを操作不可に
				Debug.Log("Stop!!");
				anim.SetBool ("isGo", false);//アニメーションも止める 
				PlayerPrefs.SetInt("Score", count);//Scoreの名前でcountの値を保存！
				audioSource.PlayOneShot (gameOverSound);
				Destroy (bgm);
				Debug.Log("GoToGameOverScene");

				SaveNCMB();//『CSVにPlayデータのセーブ→各種値を計算→NCMBに適応』の全ての工程を実行

				Invoke("MoveToGameOver", 3f);//3秒後GameOverシーン移動
			}
		}

		//最初左右選択しないと死ぬ
		if(col.tag == "Die"){
			canPlay = false;//Playerを操作不可に
			Debug.Log("Select Right or Left!!");
			anim.SetBool ("isGo", false); //アニメーションも止める
			PlayerPrefs.SetInt("Score", count);//Scoreの名前でcountの値を保存！
			Destroy (bgm);
			audioSource.PlayOneShot (gameOverSound);

			SaveNCMB();//『CSVにPlayデータのセーブ→各種値を計算→NCMBに適応』の全ての工程を実行

			Invoke("MoveToGameOver", 3f);//3秒後GameOverシーン移動
            Debug.Log("GoToGameOverScene");
		}
	}

	//GameOverシーンに移動する
	void MoveToGameOver(){
		//dm.AddRow (count, gameClear);//行足して...
		//dm.Save();//更新内容をCSVファイルに適用！

		////更新されたCSVの情報を元に各種値を計算
		//dm.PlayCount();//総プレイ回数を計算(変数：playCount)
		//dm.ClearCount();//総クリア回数を計算(変数：clearCount)
		//dm.CulculateFirstClearCount();//初回クリアまでに何回要したか計算(変数：firstClearPlayCount)
		//dm.PassedDoorCount();//突破ドア枚数の平均・合計を計算(変数：ave, sum)


		SceneManager.LoadScene("GameOver");
	}

    //『CSVにPlayデータのセーブ→各種値を計算→NCMBに適応』の全ての工程を実行！

	void SaveNCMB()
	{
		dm.AddRow(count, gameClear);//行足して...
        dm.Save();//更新内容をCSVファイルに適用！

        //更新されたCSVの情報を元に各種値を計算
        dm.PlayCount();//総プレイ回数を計算(変数：playCount)
        dm.ClearCount();//総クリア回数を計算(変数：clearCount)
        dm.CulculateFirstClearCount();//初回クリアまでに何回要したか計算(変数：firstClearPlayCount)
        dm.PassedDoorCount();//突破ドア枚数の平均・合計を計算(変数：ave, sum)
		dm.FindHighScore();//PlayLogの中からHighScoreを見つけてくる(変数：highScore)

        dm.SaveNCMB();//上で計算したデータをNCMBに上げる！
	}
}
