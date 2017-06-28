using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

		Load ();
	}

	void Start ()
	{
		DataManager.instance.AddRow (100, true);//行の追加 && 新らしいデータ追加！
		Debug.Log("SAVE");
		DataManager.instance.Save();
		
	}

	//CSVファイルを読み込む関数
	public void Load ()
	{
		playLog = csvManager.GetCsvData ("CSV/test");
	}

	//CSVファイルに書き込みを行う関数
	public void Save ()
	{
		csvManager.WriteData ("CSV/test.csv", playLog);
	}

	//playLogの要素数追加→要は行数追加！
	public void AddRow (int score, bool gameCleared)
	{
//		string[] rows = playLog.ToString ().Split ("\n" [0]);//\nで切る→改行で切る→横に切る→行数取得！
//		string[] cols = rows [0].Split ("," [0]);//どれかをてきとーに","で切る→縦に切る→列数獲得！
//
//		string[,] work = playLog;//playLogをwork変数にコピー
//		playLog = new string[rows.Length + 1, cols.Length];//playLogに1列追加
//
//		//workから戻す処理(newで初期化されちゃってるから！)
//		for (int i = 0; i < rows.Length; i++) {
//			for (int j = 0; j < cols.Length; j++) {
//				playLog [i, j] = work [i, j];
//			}
//		}

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
}
