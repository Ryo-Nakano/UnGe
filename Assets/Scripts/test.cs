using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NCMB;

public class test : MonoBehaviour {

	// Use this for initialization
	void Start () {
		// NCMBObjectクラスのインスタンスを作成→変数playLogsに格納
		NCMBObject unko = new NCMBObject("Unko");//新しいクラス(スプレッドシート作成！)
		Debug.Log(PlayerPrefs.GetString("unkoId"));

		if (PlayerPrefs.HasKey("objectId") == true)//objectId持ってた時(2回目以降のセーブの時)
        {
			NCMBQuery<NCMBObject> query = new NCMBQuery<NCMBObject>("Unko");
			query.WhereEqualTo("objectId", PlayerPrefs.GetString("unkoId"));
			query.FindAsync((List<NCMBObject> objList, NCMBException e) => {
                if (e != null)
                {
					//検索失敗時の処理
					Debug.Log("ミスっとるで！");
                }
                else
                {
					//objectIdが"unkoId"と一致するものを取得
					Debug.Log("=============");
					Debug.Log(objList[0]["message2"]);
					Debug.Log(objList[0]["message"]);
					Debug.Log("=============");
					//ここまでは取得できてる！！
                    
					objList[0]["message2"] = "これはさすがに農林";
					objList[0]["message"] = "もはや漁業";
					objList[0].SaveAsync();
                    //きたあああああああああ！！！！！！！
                }
            });
        }

		// オブジェクトに値を設定

//		testClass["message1"] = "Hello, NAKANO!";//testClassにmessage1っていう列を作って、そこに"Hello,NAKANO"を追加！
		//unko.Add ("message2", "パスタ作ったお前");
		//unko.Add("message", "お前パスタ作ったん？");//testClassにmessage2っていう列を作って、そこに"Good Morning NAKANO"を追加！
		//どうやら、message2のフィールドが存在する時はただ値を追加するだけの役割果たしてくれるらしい！

//		obj.setObjectId("updateTestObjectId");//ObjectIdを更新することができる！
//		playLogs.put("LhY0CnUdnbgafHWj", "Wake Up NAKANO!");//既にある値を、別のモノに変更することができる！←なんかよく知らんけどputできないやん！

	}
	
	// Update is called once per frame
	void Update () {
		
	}

}
