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
		Debug.Log("inputField1 : " + inputField.text);
		if(inputField.text.Length > 0)//文字数0より多い時
		{
			if(inputField.text.Length <= 6)//文字数6文字以下の時
			{
				if(inputField.text == "うんこ" ||
				   inputField.text == "ちんこ" ||
				   inputField.text == "まんこ" ||
				   inputField.text == "おっぱい" ||
				   inputField.text == "死ね" )
				{
					alertText.text = "『秩序が乱れる。やめい。』";
				}
				else if (inputField.text == "セリヌン")
                {
                    alertText.text = "『セリヌン...？ お前まさか...！！』";
                }
				else if (inputField.text == "青野" ||
				         inputField.text == "りょーすけ")
                {
                    alertText.text = "『それは顔面。』";
                }
				else//秩序を乱さない入力内容だった時
				{
					//InputFieldへの入力内容をNCMBに保存
                    NCMBQuery<NCMBObject> query = new NCMBQuery<NCMBObject>("OnlineRanking");
                    query.WhereEqualTo("objectId", PlayerPrefs.GetString("objectId"));
                    query.FindAsync((List<NCMBObject> objList, NCMBException e) => {
                        if (e != null)
                        {
                            //検索失敗時の処理
                            Debug.Log("検索ミス！");
                        }
                        else
                        {
                            //値の更新
                            objList[0]["UserName"] = inputField.text;//プレイ総数
                            Debug.Log("inputField2 : " + inputField.text);
                            objList[0].SaveAsync();//変更内容のsave
                            inputField.text = "";//空白に直す
                        }
                    });

                    mainMenueManager.BackToMainMenu2();//View3→View1に画面遷移
                    alertText.text = "";//Alertを空白に戻す
				}
			}
			else//文字数6文字より多い時
			{
				if(inputField.text == "セリヌンティウス" ||
				   inputField.text == "せりぬんてぃうす")//セリヌンティウスの時
				{
					alertText.text = "『お前まさか...！！』";
				}
				else if(inputField.text == "パスタ作ったお前")
				{
					alertText.text = "『まさか...湘南乃風...！？！？』";
				}
				else
				{
					alertText.text = "『文字数が多すぎるでごわす！』";
				}
			}
		}
		else//文字入力してない時
		{
			alertText.text = "『文字が入力されていないでごわす！』";
		}
	}
}
