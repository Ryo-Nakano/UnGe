using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabBarScript : MonoBehaviour {

	Toggle toggle;
	private GameObject _background;
	private GameObject _label;

	private Image iBackground;
	private Text tLabel;

	Color white = new Color(255f / 255f, 255f / 255f, 250f / 255f, 150f/250f);
	Color black = new Color(0f / 255f, 0f / 250f, 0f / 255f);

	// Use this for initialization
	void Start () {
		toggle = this.gameObject.GetComponent<Toggle>();
		_background = transform.Find("Background").gameObject;
		_label = _background.transform.Find("Label").gameObject;

		iBackground = _background.GetComponent<Image>();
		tLabel = _label.GetComponent<Text>();

		ChangeTabColor();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ChangeTabColor()
	{
		if (toggle.isOn == true)
        {
            iBackground.color = white;
			tLabel.color = Color.black;
        }
        else//isOn == falseの時
        {
            iBackground.color = black;
			tLabel.color = Color.white;
        }
	}
}
