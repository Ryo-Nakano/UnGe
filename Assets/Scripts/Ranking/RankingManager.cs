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

	// Use this for initialization
	void Start () {
		fetchTopRankers();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void fetchTopRankers()
    {
        // データストアの「HighScore」クラスから検索
		NCMBQuery<NCMBObject> query = new NCMBQuery<NCMBObject>("OnlineRanking");
        query.OrderByDescending("HighScore");
        query.Limit = 5;
        query.FindAsync((List<NCMBObject> objList, NCMBException e) => {

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
    }

}
