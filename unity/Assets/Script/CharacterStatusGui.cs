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
    Vector2 playerStatusOffset = new Vector2(8f, 80f);
	
    // ライフバー.
    float frontLifeBarOffsetX = 2f;
    float lifeBarTextureWidth = 128f;
    Color playerFrontLifeBarColor = Color.green;
    Color enemyFrontLifeBarColor = Color.red;

	void Start(){
		parent = GameObject.Find ("Panel");
		// NGUIの生成
		playerSlider = NGUITools.AddChild (parent, sliderPrefab);
		playerLabel = NGUITools.AddChild (parent, labelPrefab);
		enemySlider = NGUITools.AddChild (parent, sliderPrefab);
		enemyLabel = NGUITools.AddChild (parent, labelPrefab);
	}

    // プレイヤーステータスの描画.
    void DrawPlayerStatus()
    {
		float x = baseWidth - playerStatusOffset.x;
		float y = playerStatusOffset.y;
        DrawCharacterStatus(
            x, y,
            playerStatus,
            playerFrontLifeBarColor,
			playerLabel,
			playerSlider);
    }

    // 敵ステータスの描画.
    void DrawEnemyStatus()
    {
		if (playerStatus.lastAttackTarget != null)
        {
			CharacterStatus target_status = playerStatus.lastAttackTarget.GetComponent<CharacterStatus>();
            DrawCharacterStatus(
                baseWidth, 0f,
				target_status,
                enemyFrontLifeBarColor,
				enemyLabel,
				enemySlider);
        }
    }

    // キャラクターステータスの描画.
    void DrawCharacterStatus(float x, float y, CharacterStatus status, Color front_color, GameObject label, GameObject slider)
    {
		// ラベルの取得
		if (label != null) {
			Transform t = label.transform;
			t.position = new Vector3(x, y, 0); 
			UILabel UIlabel = label.GetComponent("UILabel") as UILabel;
			UIlabel.text = status.characterName;
		}
        // 名前.
        /*GUI.Label(
            new Rect(x, y, nameRect.width, nameRect.height),
			status.characterName,
            nameLabelStyle);


		float life_value = (float)status.HP / status.MaxHP;
		if(backLifeBarTexture != null)
		{
			// 背面ライフバー.
			y += nameRect.height;
			GUI.DrawTexture(new Rect(x, y, bar_rect.width, bar_rect.height), backLifeBarTexture);
		}

		Slider = GameObject.Find ("Slider");
		if (Slider) {
			UISlider slider = Slider.GetComponent("UISlider") as UISlider;
			//GameObject Fore = GameObject.Find("Foreground");
			//float resize_front_bar_offset_x = frontLifeBarOffsetX * bar_rect.width / lifeBarTextureWidth;
			//float front_bar_width = bar_rect.width - resize_front_bar_offset_x * 2;
			//var gui_color = GUI.color;
			//GUI.color = front_color;
			//UISlicedSprite s= Fore.GetComponent("UISlicedSprite") as UISlicedSprite;
			slider.sliderValue = life_value;
			//GUI.DrawTexture(new Rect(x + resize_front_bar_offset_x, y, front_bar_width * life_value, bar_rect.height), frontLifeBarTexture);
			//GUI.color = gui_color;			
		}
        // 前面ライフバー.
		if(frontLifeBarTexture != null)
		{
			float resize_front_bar_offset_x = frontLifeBarOffsetX * bar_rect.width / lifeBarTextureWidth;
			float front_bar_width = bar_rect.width - resize_front_bar_offset_x * 2;
			var gui_color = GUI.color;
			GUI.color = front_color;
			GUI.DrawTexture(new Rect(x + resize_front_bar_offset_x, y, front_bar_width * life_value, bar_rect.height), frontLifeBarTexture);
			GUI.color = gui_color;
		}
*/
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