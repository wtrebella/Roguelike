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
	Gun,
	Enemy,
	NONE
}

public enum RoomType {
	RoomType1,
	RoomType2,
	RoomType3,
	NONE
}

public enum GunType {
	Pistol,
	PistolRed,
	PistolGreen,
	PistolBlue,
	NONE
}
