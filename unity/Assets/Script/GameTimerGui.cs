using UnityEngine;
using System.Collections;

public class GameTimerGui : MonoBehaviour
{
	GameRuleCtrl gameRuleCtrl;
	private GameObject Label;

	float baseWidth = 854f;
	float baseHeight = 480f;

	public Texture timerIcon;
	public GUIStyle timerLabelStyle;
	
	void Awake()
	{
		gameRuleCtrl = GameObject.FindObjectOfType(typeof(GameRuleCtrl)) as GameRuleCtrl;
	}
	
	void OnGUI()
	{
		// 解像度対応.
		GUI.matrix = Matrix4x4.TRS(
			Vector3.zero,
			Quaternion.identity,
			new Vector3(Screen.width / baseWidth, Screen.height / baseHeight, 1f));
		
		// タイマー
		Label = GameObject.Find ("Label");
		if (Label) {
			UILabel labelText = Label.GetComponent("UILabel") as UILabel;
			labelText.text = gameRuleCtrl.timeRemaining.ToString("0");
		}
	}
}
