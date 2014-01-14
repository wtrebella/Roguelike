using UnityEngine;
using System.Collections;

public enum TileTheme {
	Stone,
	Snow,
	Sand,
	Grass,
	NONE
}

public enum ControlState {
	WasPressed,
	IsPressed,
	WasReleased,
	NONE
}

public enum EnemyType {
	Turtle,
	Rat,
	NONE
}

public enum GroundTileType {
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

public enum BulletType {
	Pistol,
	PistolBlue,
	PistolGreen,
	PistolRed,
	NONE
}

public enum TileType {
	Empty,
	Ground,
	Weapon,
	Enemy,
	NONE
}

public enum RoomType {
	BottomLeft,
	BottomRight,
	TopLeft,
	TopRight,
	MidLeft,
	MidRight,
	MidTop,
	MidBottom,
	Mid,
	NONE
}

public enum WeaponType {
	Pistol,
	NONE
}
