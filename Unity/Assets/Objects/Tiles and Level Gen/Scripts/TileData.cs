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
			td = new TileDataWeapon();
			(td as TileDataWeapon).weaponType = WeaponType.Pistol;
		}
		else if (c == 't') {
			td = new TileDataEnemy();
			(td as TileDataEnemy).enemyType = EnemyType.Turtle;
		}
		else if (c == 'r') {
			td = new TileDataEnemy();
			(td as TileDataEnemy).enemyType = EnemyType.Rat;
		}
		
		return td;
	}
}

public class TileDataWeapon : TileData {
	public WeaponType weaponType = WeaponType.NONE;
	
	public TileDataWeapon() {
		tileType = TileType.Weapon;
	}
}

public class TileDataEnemy : TileData {
	public EnemyType enemyType = EnemyType.NONE;
	
	public TileDataEnemy() {
		tileType = TileType.Enemy;
	}
}