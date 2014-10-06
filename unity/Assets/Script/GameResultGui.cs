using UnityEngine;
using System.Collections;

public class GameResultGui : MonoBehaviour
{
	GameRuleCtrl gameRuleCtrl;

	float baseWidth = 800f;
	float baseHeight = 100f;

	public Texture2D gameOverTexture;
	public Texture2D gameClearTexture;
	public GameObject texturePrefab;
	private GameObject parent;
	private GameObject texture;
	
	void Awake()
	{
		gameRuleCtrl = GameObject.FindObjectOfType(typeof(GameRuleCtrl)) as GameRuleCtrl;
	}

	void Start(){
		parent = GameObject.Find ("Panel");
	}

	void Update(){
		Texture2D aTexture;
		if( gameRuleCtrl.gameClear )
		{
			aTexture = gameClearTexture;
			texture = NGUITools.AddChild (parent, texturePrefab);
		}
		else if( gameRuleCtrl.gameOver )
		{
			aTexture = gameOverTexture;
			texture = NGUITools.AddChild (parent, texturePrefab);
		}
		else
		{
			return;
		}
		if (texture) {
			texture.transform.localScale = new Vector3 (baseWidth, baseHeight, 0f);
			UITexture tex = texture.GetComponent ("UITexture") as UITexture;
			tex.mainTexture = aTexture;
		}
	}


}
