using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NCMB;

public class test : MonoBehaviour {

	// Use this for initialization
	void Start () {
		// NCMBObjectクラスのインスタンスを作成→変数playLogsに格納
		NCMBObject playLogs = new NCMBObject("PlayLogs");//新しいクラス(スプレッドシート作成！)


		// オブジェクトに値を設定

//		testClass["message1"] = "Hello, NAKANO!";//testClassにmessage1っていう列を作って、そこに"Hello,NAKANO"を追加！
		playLogs.Add ("message2", "What's up NAKANO!");//testClassにmessage2っていう列を作って、そこに"Good Morning NAKANO"を追加！
		//どうやら、message2のフィールドが存在する時はただ値を追加するだけの役割果たしてくれるらしい！

//		obj.setObjectId("updateTestObjectId");//ObjectIdを更新することができる！
//		playLogs.put("LhY0CnUdnbgafHWj", "Wake Up NAKANO!");//既にある値を、別のモノに変更することができる！←なんかよく知らんけどputできないやん！

		// データストアへの登録
		playLogs.SaveAsync ((NCMBException e) => {//eには"例外(exeption)"が入ってる→来たらエラー来なかったらok！    
			if (e != null) {//eが空でない時→エラーの時！
				//エラー時の処理
				Debug.Log("Missed!");
			} else {//eが空の時→エラーの時！
					//成功時の処理
				Debug.Log("SaveCompleted!!!");
			}                   
		});

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
