using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NCMB;//NCMB使う為の準備

public class ProfileManager : MonoBehaviour {

	[SerializeField] InputField inputField;
	[SerializeField] Text alertText;//InputFieldへの入力状況に応じてアラートを表示！
    
	MainMenueManager mainMenueManager;

	// Use this for initialization
	void Start () {
		alertText.text = "";//最初はalertTextは空
		mainMenueManager = GameObject.Find("MainMenueManager").GetComponent<MainMenueManager>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //『完了』ボタン押した時に呼ばれる処理
	public void DoneButton()
	{
		Debug.Log("PUSHED!!");

		if(inputField.text.Length > 0)//文字数0より多い時
		{
			if(inputField.text.Length <= 6)//文字数6文字以下の時
			{
				Debug.Log("文字数は適正");

                //InputFieldへの入力内容をNCMBに保存
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
						objList[0]["UserName"] = inputField.text;//プレイ総数
                        objList[0].SaveAsync();//変更内容のsave
                    }
                });

				mainMenueManager.BackToMainMenu2();//View3→View1に画面遷移
				inputField.text = "";//空白に直す
			}
			else//文字数6文字より多い時
			{
				Debug.Log("文字多すぎるよ！");
			}
		}
		else//文字入力してない時
		{
			Debug.Log("文字入力されてないよ！");
		}
		//1.入力文字数チェック
        //2'.範囲外→アラート出す
        //2.範囲内→NCMBに入力内容保存
        //3.保存完了後、自動でView1に戻る

		//Debug.Log()でちゃんと条件分岐する事は確認！
	}
}
