using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;//←これはなんだ？
using NCMB;

//PlayLogをCSVファイルに貯めておく為のクラス！
public class DataManager : MonoBehaviour
{

	public static DataManager instance;
	public string[,] playLog;//string型の2次元配列playLogを定義→取得したcsvファイルを2次元配列に直してぶち込んでおく！

	//Start関数より早く実行！
	void Awake ()
	{

        //シングルトンデザインパターンにする！
		if (instance == null) {//最初の1回だけ呼ばれる実装！
			instance = this;
			DontDestroyOnLoad (this);
		} else {
			Destroy (this.gameObject);//最初の1回以降は即削除される
		}

		Load ();//CSVファイル"PlayLog"を変数playLogに格納！
	}

	void Start (){
		
	}



    
	//- * - * - * - * - * - * - * - * - * - * - * - * - * - * - * - * - * - * - * - * - * - * - * - * - * - * - * - * - * - * - * - * - *




	//CSVファイルを読み込む関数
    public void Load()
    {
        playLog = csvManager.GetCsvData("CSV/PlayLog");//Resourcesフォルダ内のPlayLogを取得→2次元配列に変換して変数playLogの中に格納
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

        //既存のものより1行だけ多い2次元配列を作成！
		string[,] array = new string[rowCount + 1, colCount];

		//今までのデータを全部ぶち込む！
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

	//===HighScoreを探してくる===
	int highScore = 0;//ハイスコアを格納しておく為の変数(初期値0)

	public void FindHighScore()
	{
		for (int i = 0; i < playLog.GetLength(0) - 1; i++)//playLogの行数-1回だけ回す！
        {
			if (int.Parse(playLog[i + 1, 1]) > highScore)//参照した値が手元に持ってるhighScoreよりも大きかったとき
            {
				highScore = int.Parse(playLog[i + 1, 1]);//highScoreの値を上書き！
            }
        }

		Debug.Log("HighScore : " + highScore);
	}


	//===【完】playCount拾って来る！===
	int playCount;//総Play回数

	public void PlayCount(){
		playCount = playLog.GetLength (0) - 1;//総行数-1=総プレイ回数！
		Debug.Log("playCount : " + playCount);

		//データを送る処理
	}


	//===【完】clearCount拾って来る===
	int clearCount;//総クリア回数

	public void ClearCount(){
		for(int i = 0; i < playLog.GetLength (0) - 1; i++){//playLogの行数-1回だけ回す！
			if(int.Parse(playLog[i + 1, 2]) == 1){//gameClearedの値が1→クリアだった時
				clearCount++;//clearCountに1づつ足していく
			}
		}
		Debug.Log ("clearCount : " + clearCount);

		//データを送る処理
	}


	//==========【完】firstClearPlayCount拾って来る！==========

	int firstClearPlayCount = 0;//初回クリアまでに何回要したか(初期値0)
	bool gameClear = false;//クリアしたかどうか！(普通はfalse)

	public void CulculateFirstClearCount(){
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

		Debug.Log("firstClearPlayCount : " + firstClearPlayCount);
	}


	//==========【完】平均突破Door枚数拾ってくる！==========
	public float ave;//"平均"ドア突破枚数
	public float sum;//"合計"ドア突破枚数

	public void PassedDoorCount(){
		for(int i = 0; i < playLog.GetLength (0) - 1; i++){//playLogの行数-1回だけ回す！
			sum += int.Parse(playLog[i + 1, 1]);//突破ドア枚数の合計！
		}
		sum = sum / 100;//sumの値を100で割る(score→枚数にする為)
		ave = float.Parse((sum / (playLog.GetLength(0) - 1)).ToString("f1"));//平均突破ドア枚数！

		Debug.Log("sum" + sum);
		Debug.Log("ave" + ave); 
	}



	//=====================NCMBと通信、データの保存や取得をする関数=====================

	//-*-*- 使うKey一覧 *-*-*

	//▶︎PlayCount (総プレイ回数)
	//▶︎Ave (平均ドア突破枚数)
	//▶︎Sum (合計ドア突破枚数)
	//▶︎FirstClearPlayCount (初回クリアまでのプレイ回数)
	//▶︎ClearCount (クリア回数)

	//-*-*-*-*-*-*-*-*-*-*-*
    
	//NCMBにplayデータを保存する関数
	public void SaveNCMB()
    {
		//NCMBObjectのインスタンスを作成→変数ncmbObjに格納
        NCMBObject obj = new NCMBObject("OnlineRanking");//OnlineRankingテーブルのインスタンス作成！

        if (PlayerPrefs.HasKey("objectId") == true)//objectId持ってた時(2回目以降のセーブの時)
        {
            //Updateの処理
			NCMBQuery<NCMBObject> query = new NCMBQuery<NCMBObject>("OnlineRanking");
			query.WhereEqualTo("objectId", PlayerPrefs.GetString("objectId"));
            query.FindAsync((List<NCMBObject> objList, NCMBException e) => {
                if (e != null)
                {
                    //検索失敗時の処理
                    Debug.Log("ミスっとるで！");
                }
                else
                {
                    //値の更新
					objList[0]["PlayCount"] = playCount;//プレイ総数
					objList[0]["Ave"] = ave;//平均Door突破枚数
					objList[0]["Sum"] = sum;//合計Door突破枚数
					objList[0]["FirstClearPlayCount"] = firstClearPlayCount;//初回クリアまでのプレイ回数
					objList[0]["ClearCount"] = clearCount;//クリア回数
					objList[0]["HighScore"] = highScore;//ハイスコア
                    objList[0].SaveAsync();//変更内容のsave
                }
            });
        }
        else//objectId持ってない時(初セーブの時)
        {
			//新しく行追加
			obj.Add("PlayCount", playCount);
            obj.Add("Ave", ave);
			obj.Add("Sum", sum);
			obj.Add("FirstClearPlayCount", firstClearPlayCount);
			obj.Add("ClearCount", clearCount);

			obj.SaveAsync((NCMBException e) => {//eには"例外(exeption)"が入ってる→来たらエラー来なかったらok！    
                if (e != null)//eが空でない時→エラーの時！
                {
                    //エラー時の処理
                    Debug.Log("NCMB Save Missed!");
                }
                else//eが空の時→成功した時！
                {
                    //成功時の処理
                    Debug.Log("NCMB Save Completed!!!");

                    //NCMBObjectのObjectIdを"objectId"キーでPlayerPrefsに保存(次回以降のセーブで同一行のデータを更新していく形にしたい為)
                    PlayerPrefs.SetString("objectId", obj.ObjectId);
                }
            });
		}
    }
}


