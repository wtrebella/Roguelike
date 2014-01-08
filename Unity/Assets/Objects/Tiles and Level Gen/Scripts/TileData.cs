using UnityEngine;
using System.Collections;

public class TileData {
	public TileType tileType = TileType.NONE;

	public TileData() {

	}

	public static TileData CharToTileData(char c) {
		TileData td = null;
		
		if (c == '0') {
			td = new TileData();
			td.tileType = TileType.Empty;
		}
		else if (c == '1') {
			td = new TileData();
			td.tileType = TileType.Ground;
		}
		else if (c == 'p') {
			td = new TileDataGun();
			(td as TileDataGun).gunType = GunType.Pistol;
		}
		else if (c == 't') {
			td = new TileDataEnemy();
			(td as TileDataEnemy).enemyType = EnemyType.Turtle;
		}
		
		return td;
	}
}

public class TileDataGun : TileData {
	public GunType gunType = GunType.NONE;
	
	public TileDataGun() {
		tileType = TileType.Gun;
	}
}

public class TileDataEnemy : TileData {
	public EnemyType enemyType = EnemyType.NONE;
	
	public TileDataEnemy() {
		tileType = TileType.Enemy;
	}
}