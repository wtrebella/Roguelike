using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TileData {
	public TileType tileType = TileType.NONE;

	public TileData() {

	}

	public static TileData CharToTileData(char c, Dictionary<char, float> randValsDict = null) {
		TileData td = new TileData();
		td.tileType = TileType.Empty;

		if (c == '1') {
			td = new TileData();
			td.tileType = TileType.Ground;
		}
		else if (c == '2' || c == '3' || c == '4' || c == '5' || c == '6' || c == '7' || c == '8' || c == '9') {
			td = new TileData();
			if (randValsDict[c] < 0.5f) td.tileType = TileType.Ground;
		}
		else if (c == 'p') {
			td = new TileDataWeapon();
			(td as TileDataWeapon).weaponType = WeaponType.Pistol;
		}
		else if (c == 't') {
			if (randValsDict[c] < 0.5f) {
				td = new TileDataEnemy();
				(td as TileDataEnemy).enemyType = EnemyType.Turtle;
			}
		}
		else if (c == 'r') {
			if (randValsDict[c] < 0.5f) {
				td = new TileDataEnemy();
				(td as TileDataEnemy).enemyType = EnemyType.Rat;
			}
		}
		
		return td;
	}

	public static char[] GetAllChars() {
		string s = "0123456789ptr";
		return s.ToCharArray();
	}

	public static Dictionary<char, float> GetDictionaryOfCharsAndRandomValues() {
		Dictionary<char, float> newDict = new Dictionary<char, float>();

		char[] ca = GetAllChars();

		foreach (char c in ca) newDict.Add(c, Random.value);

		return newDict;
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