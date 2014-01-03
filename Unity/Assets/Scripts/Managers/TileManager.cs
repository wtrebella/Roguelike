using UnityEngine;
using System.Collections;

public enum TileTheme {
	Stone,
	Snow,
	Sand,
	Grass,
	NONE
}

public enum TileType {
	Singular,
	Center,
	CenterRounded,
	Left,
	Mid,
	Right,
	CliffLeft,
	CliffLeftAlt,
	CliffRight,
	CliffRightAlt,
	Half,
	HalfLeft,
	HalfMed,
	HalfRight,
	HillLeft,
	HillLeftOpposite,
	HillRight,
	HillRightOpposite,
	NONE
}

public class TileManager : MonoBehaviour {
	public GameObject baseTilePrefab;

	protected Transform tileMap;
	protected SpriteManager spriteManager;

	void Awake() {
		spriteManager = GameObject.Find("Sprite Manager").GetComponent<SpriteManager>();
		tileMap = GameObject.Find("Tile Map").transform;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public Transform GetNewTile(TileTheme tileTheme, TileType tileType) {
		Transform newTile = ((GameObject)Instantiate(baseTilePrefab)).transform;
		newTile.GetComponent<SpriteRenderer>().sprite = spriteManager.GetTileSprite(tileTheme, tileType);
		newTile.gameObject.AddComponent<BoxCollider2D>();
		newTile.gameObject.layer = LayerMask.NameToLayer("Ground");
		newTile.transform.parent = tileMap;
		return newTile;
	}
}
