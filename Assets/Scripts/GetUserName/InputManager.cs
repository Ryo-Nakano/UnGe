using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;//シーン遷移扱う
using UnityEngine.UI;//Canvas扱う


public class InputManager : MonoBehaviour {

//	InputField inputField;//InputField型の変数inputFieldを定義！
//	string userName;
//
//
//	/// <summary>
//	/// Startメソッド
//	/// InputFieldコンポーネントの取得および初期化メソッドの実行
//	/// </summary>
//	void Start() {
//
//		inputField = GetComponent<InputField>();//Startでコンポーネント取ってきておいて、変数inputFieldに保持！
//
//		InitInputField();
//	}
//
//
//
//	/// <summary>
//	/// Log出力用メソッド
//	/// 入力値を取得してLogに出力し、初期化
//	/// </summary>
//
//
//	public void InputLogger() {
//
//		string userName = inputField.text;
//
//		PlayerPrefs.SetString ("UserName", userName);//"UserName"というkeyでuserNameの値保存！
//		Debug.Log(PlayerPrefs.GetInt("UserName"));
//
//		InitInputField();
//		
//	}
//
//
//
//	/// <summary>
//	/// InputFieldの初期化用メソッド
//	/// 入力値をリセットして、フィールドにフォーカスする
//	/// </summary>
//
//
//	void InitInputField() {
//
//		// 値をリセット
//		inputField.text = "";
//
//		// フォーカス
//		inputField.ActivateInputField();
//	}


	//==================全く別の実装===================

	string userName;
	public InputField inputField;
	public Text text;

	public void SaveText () {//ボタン押した時に呼ぶ！		userName = inputField.text;//なんでここNullReferenceExeption
		PlayerPrefs.SetString ("UserName", userName);//"UserName"というkeyでuserNameの値保存！

		inputField.text = "保存完了！";
	}
}
