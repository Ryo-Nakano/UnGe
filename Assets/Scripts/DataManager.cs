using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;//←これはなんだ？

public class DataManager : MonoBehaviour
{

	public static DataManager instance;
	public string[,] playLog;
//string型の2次元配列playLogを定義！

	// Use this for initialization
	void Awake ()
	{//Start関数より早く実行！
		if (instance == null) {//最初の1回だけ呼ばれる実装！
			instance = this;
			DontDestroyOnLoad (this);
		} else {
			Destroy (this.gameObject);//最初の1回以降は即削除される
		}

		Load ();//CSVファイル"PlayLog"をplayLogに格納！
	}

	void Start (){

	}

	//CSVファイルを読み込む関数
	public void Load ()
	{
		playLog = csvManager.GetCsvData ("CSV/PlayLog");
	}

	//CSVファイルに書き込みを行う関数
	public void Save ()
	{
		csvManager.WriteData ("CSV/PlayLog.csv", playLog);
	}

	//playLogの要素数追加→要は行数追加！
	public void AddRow (int score, bool gameCleared)
	{
		int rowCount = playLog.GetLength (0);//行数取得！
		int colCount = playLog.GetLength (1);//列数獲得！
		//コピ
		string[,] array = new string[rowCount + 1, colCount];

		//今までのデータをコピー
		for (int i = 0; i < rowCount; i++) {
			for (int j = 0; j < colCount; j++) {
				array [i, j] = playLog [i, j];
			}
		}

		//新しいデータを追加
		array [rowCount, 0] = rowCount.ToString();
		array [rowCount, 1] = score.ToString();
		if (gameCleared == true) {
			array [rowCount, 2] = "1";
		} else {
			array [rowCount, 2] = "0";
		}

		playLog = array;//playLogにarrayを代入→playLog内容の更新！

	}


	//=====================playLogいい感じに拾って来て、いい感じに加工して、いい感じの変数に入れる関数！=====================

	//===playCount拾って来る！===
	int playCount;

	void PlayCount(){
		playCount = playLog.GetLength (0) - 1;//総行数-1=総プレイ回数！
		Debug.Log(playCount);

		//データを送る処理
	}


	//===clearCount拾って来る===
	int clearCount;

	void ClearCount(){
		for(int i = 0; i < playLog.GetLength (0) - 1; i++){//playLogの行数-1回だけ回す！
			if(int.Parse(playLog[i + 1, 2]) == 1){//gameClearedの値が1→クリアだった時
				clearCount++;//clearCountに1づつ足していく
			}
		}
		Debug.Log (clearCount);

		//データを送る処理
	}


	//===firstClearCount拾って来る！===

	int firstClearPlayCount;
	bool gameClear = false;//クリアしたかどうか！(普通はfalse)

	void FirstClearCount(){
		//初回クリアまでの回数計測
		for(int i = 0; i < playLog.GetLength (0) - 1; i++){//playLogの行数-1回だけ回す！
			if(int.Parse(playLog[i + 1, 2]) != 1){//gameClearedの値が1でない時→クリアでない時
				firstClearPlayCount++;//1ずつ足していく！
			}else{//gameClearedの値が1の時→クリアの時
				gameClear = true;//クリアしたよ！フラグを立てる
				break;//クリアの時はfor文ぬける！
			}
		}

		//ゲーム未クリアの場合のケア
		if(gameClear == true){//ゲームクリアしてる時
			//データを送る処理
		}
		//ゲームクリアが保証されている場合にのみデータの送信を行えば、取り敢えずあ大丈夫かな？
	}




}
