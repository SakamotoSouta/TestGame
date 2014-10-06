using UnityEngine;
using System.Collections;

public class CharacterStatusGui : MonoBehaviour
{
	public GameObject labelPrefab;
	public GameObject sliderPrefab;
	
	GameObject parent;
	
	// プレイヤーステータスGUI
	GameObject playerSlider;
	GameObject playerLabel;
	// ターゲットステータスGUI
	GameObject enemySlider;
	GameObject enemyLabel;
	
	// 画面の中心
	float baseWidth = Screen.width / 2;
	float baseHeight = Screen.height / 2;
	
	// ステータス.
	CharacterStatus playerStatus;
	Vector2 playerStatusOffset = new Vector2(-140f, 0f);
	Vector2 enemyStatusOffset = new Vector2(-128f / 2f, -50f);	
	// ライフバー.
	float lifeBarTextureWidth = 128f;
	Color playerFrontLifeBarColor = Color.green;
	Color enemyFrontLifeBarColor = Color.red;
	
	void Start(){
		parent = GameObject.Find ("Panel");
	}
	
	// プレイヤーステータスの描画.
	void DrawPlayerStatus()
	{
		float x = baseWidth - playerStatusOffset.x;
		float y = playerStatusOffset.y;
		if (!playerLabel) {
			playerLabel = NGUITools.AddChild (parent, labelPrefab);
		}
		if (!playerSlider) {
			playerSlider = NGUITools.AddChild (parent, sliderPrefab);
		}
		playerLabel.transform.localScale = new Vector2 (28f, 28f);
		
		UIAnchor posSlider = playerSlider.GetComponent("UIAnchor") as UIAnchor;
		UIAnchor posLabel = playerLabel.GetComponent("UIAnchor") as UIAnchor;
		posSlider.side = UIAnchor.Side.Right;
		posSlider.pixelOffset = playerStatusOffset;
		posLabel.side = UIAnchor.Side.Right;
		posLabel.pixelOffset = playerStatusOffset;
		posLabel.pixelOffset.x += 30;
		posLabel.pixelOffset.y += 30;
		
		DrawCharacterStatus(
			playerStatus,
			playerFrontLifeBarColor,
			playerLabel,
			playerSlider);
	}
	
	// 敵ステータスの描画.
	void DrawEnemyStatus()
	{
		if (playerStatus.lastAttackTarget != null) {
			CharacterStatus target_status = playerStatus.lastAttackTarget.GetComponent<CharacterStatus> ();
			if (!enemySlider) {
				enemySlider = NGUITools.AddChild (parent, sliderPrefab);
			}
			if (!enemyLabel) {
				enemyLabel = NGUITools.AddChild (parent, labelPrefab);
			}
			enemyLabel.transform.localScale = new Vector2 (28f, 28f);
			
			UIAnchor posSlider = enemySlider.GetComponent ("UIAnchor") as UIAnchor;
			UIAnchor posLabel = enemyLabel.GetComponent ("UIAnchor") as UIAnchor;
			posSlider.side = UIAnchor.Side.Top;
			posSlider.pixelOffset = enemyStatusOffset;
			posLabel.side = UIAnchor.Side.Top;
			posLabel.pixelOffset = enemyStatusOffset;
			posLabel.pixelOffset.x -= enemyStatusOffset.x;
			posLabel.pixelOffset.y += 30; 
			DrawCharacterStatus (
				target_status,
				enemyFrontLifeBarColor,
				enemyLabel,
				enemySlider);
		}
		else {
			if (enemySlider) {
				Destroy(enemySlider);
			}
			if (enemyLabel) {
				Destroy(enemyLabel);
			}
		}
	}
	
	// キャラクターステータスの描画.
	void DrawCharacterStatus(CharacterStatus status, Color front_color, GameObject label, GameObject slider)
	{
		if (label != null) { 
			UILabel UIlabel = label.GetComponent("UILabel") as UILabel;
			UIlabel.text = status.characterName;
		}
		GameObject fore = slider.transform.FindChild("Foreground").gameObject;
		UISlicedSprite s = fore.GetComponent ("UISlicedSprite") as UISlicedSprite;
		s.color = front_color;
		float life_value = (float)status.HP / status.MaxHP;
		UISlider Slider = slider.GetComponent("UISlider") as UISlider;
		Slider.sliderValue = life_value;
	}
	
	void Awake()
	{
		PlayerCtrl player_ctrl = GameObject.FindObjectOfType(typeof(PlayerCtrl)) as PlayerCtrl;
		playerStatus = player_ctrl.GetComponent<CharacterStatus>();
	}
	
	void OnGUI()
	{
		// 解像度対応.
		GUI.matrix = Matrix4x4.TRS(
			Vector3.zero,
			Quaternion.identity,
			new Vector3(Screen.width / baseWidth, Screen.height / baseHeight, 1f));
		
		// ステータス.
		DrawPlayerStatus();
		DrawEnemyStatus();
	}
}