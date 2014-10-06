using UnityEngine;
using System.Collections;

public class TitleScenenCtrl : MonoBehaviour {
    // タイトル画面テクスチャ
    public Texture2D bgTexture;
	private bool StartButton = false;

    void OnGUI()
    {

        // スタートボタンを作成します
		if (StartButton)
        {
			Application.LoadLevel("GameScene");
        }
    }

	// StartButtonが押されたとき
	void StartButtonPressed(){
		StartButton = true;
	}
}
