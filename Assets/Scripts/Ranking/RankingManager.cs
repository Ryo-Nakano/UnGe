using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NCMB;//NCMB使う！

public class RankingManager : MonoBehaviour {
    
    //HighScoreランキング表示の為のラベル
	[SerializeField] Text[] higehScoreText = new Text[5];
	[SerializeField] Text[] userText = new Text[5];
	[SerializeField] Text[] aveText = new Text[5];

	[SerializeField] Text deathCountYou;
	[SerializeField] Text doorSumYou;
	[SerializeField] Text doorAveYou;
	[SerializeField] Text clearYou;

	[SerializeField] Text deathCountWorld;
	[SerializeField] Text doorSumWorld;
	[SerializeField] Text doorAveWorld;
	[SerializeField] Text clearWorld;

	DataManager dm;//DataManagerのインスタンスを格納しておく為の変数

	// Use this for initialization
	void Start () {
		dm = GameObject.Find("DataManager").GetComponent<DataManager>();//DataManagerを取得→変数dmに格納

		MakeRankingView();//RankingViewを作る関数
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //RankingViewを作る関数
	public void MakeRankingView()
    {
        //==========TopRankのViewを作成==========

        // データストアの「HighScore」クラスから検索
		NCMBQuery<NCMBObject> queryTopRank = new NCMBQuery<NCMBObject>("OnlineRanking");
        queryTopRank.OrderByDescending("HighScore");
        queryTopRank.Limit = 5;
		queryTopRank.FindAsync((List<NCMBObject> objList, NCMBException e) => {

            if (e != null)//エラーあった時
            {
                //検索失敗時の処理
            }
            else//うまく行ったとき
            {
				for (int i = 0; i < objList.Count; i++)//objListの要素数と同じだけfor回す
				{
					//順次ランキング表示！
					userText[i].text = System.Convert.ToString(objList[i]["UserName"]);
					higehScoreText[i].text = System.Convert.ToString(objList[i]["HighScore"]);
					aveText[i].text = System.Convert.ToString(objList[i]["Ave"]);
				}
            }
        });


		//==========RecordsのViewを作成==========

		//===DeathCount(You)===
		dm.PlayCount();
		deathCountYou.text = dm.playCount.ToString() + " 回";

		//===DoorSum(You)===
		dm.PassedDoorCount();
		doorSumYou.text = dm.sum.ToString() + " 枚";

		//===DoorAve(You)===
		doorAveYou.text = dm.ave.ToString() + " 枚";

		//===Clear(You)===
		dm.ClearCount();
		clearYou.text = dm.clearCount.ToString() + " 回";



		int deathCount = 0;
		int doorSum = 0;
		float doorAve = 0f;
		int clear = 0;


		NCMBQuery<NCMBObject> query = new NCMBQuery<NCMBObject>("OnlineRanking");//Queryの取得

		query.FindAsync((List<NCMBObject> objList, NCMBException e) => {

			//===DeathCount(World)===
			for (int i = 0; i < objList.Count; i++)//objListの要素数だけfor回す
			{
				deathCount += int.Parse(objList[i]["PlayCount"].ToString());
			}
			deathCountWorld.text = deathCount.ToString("f0") + " 回";


			//===DoorSum(World)===
			for (int i = 0; i < objList.Count; i++)//objListの要素数だけfor回す
            {
                doorSum += int.Parse(objList[i]["Sum"].ToString());
            }
            doorSumWorld.text = doorSum.ToString("f0") + " 枚";


			//===DoorAve(World)===
			for (int i = 0; i < objList.Count; i++)//objListの要素数だけfor回す
            {
                doorAve += float.Parse(objList[i]["Ave"].ToString());//hogeにがしがし足してく
            }
            doorAve /= objList.Count;
            doorAveWorld.text = doorAve.ToString("f2") + " 枚";


			//===Clear(World)===
			for (int i = 0; i < objList.Count; i++)//objListの要素数だけfor回す
            {
                clear += int.Parse(objList[i]["ClearCount"].ToString());//hogeにがしがし足してく
            }
            clearWorld.text = clear.ToString("f0") + " 回";
		});
    }
}
