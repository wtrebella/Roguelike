using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpriteManager : MonoBehaviour {
	public Sprite[] allSprites;

	void Start() {

	}
	
	void Update() {
	
	}

	public Sprite GetSprite(string spriteName) {
		foreach (Sprite s in allSprites) {
			if (s.name == spriteName) return s;
		}

		Debug.Log("no sprite with name: " + spriteName);

		return null;
	}

	public Sprite GetTileSprite(TileTheme tileTheme, TileType tileType) {
		return GetSprite(tileTheme.ToString() + tileType.ToString());
	}
}
