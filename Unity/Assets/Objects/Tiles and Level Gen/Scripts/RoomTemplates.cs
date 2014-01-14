﻿using UnityEngine;
using System.Collections;

public class RoomTemplates {

	public static char[,] GetRoomTemplate(RoomType type) {
		char[,] template = null;
//		int variationsPerType = 2;
		int rand = Random.Range(1, 4);

		if (rand == 1) {
			template = new char[,] {
				{'0', '0', '1', '1', '0', '0', '1', '1', '0', '0', '0', '0'},
				{'0', '0', '1', '1', '0', '0', '1', '1', '0', '0', '0', '0'},
				{'0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '1', '1'},
				{'0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '1', '1'},
				{'1', '1', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0'},
				{'1', '1', '0', '0', '0', '0', 't', '0', '0', '0', '0', '0'},
				{'0', '0', '0', '0', '0', '0', '1', '1', '0', '0', '0', '0'},
				{'0', '0', '0', '0', '0', '0', '1', '1', 't', '0', '0', '0'},
				{'0', '0', '1', '1', '0', '0', '1', '1', '1', '1', '0', '0'},
				{'0', '0', '1', '1', '0', '0', '1', '1', '1', '1', '0', '0'},
				{'0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0'},
				{'0', '0', '0', '0', '0', '0', '0', '0', 'r', '0', '0', '0'}
			};
		}
		else if (rand == 2) {
			template = new char[,] {
				{'1', '1', '0', '0', '1', '1', '0', '0', '0', '0', '1', '1'},
				{'1', '1', '0', '0', '1', '1', '0', '0', '0', '0', '1', '1'},
				{'0', '0', '0', '0', '0', '0', '0', '0', '1', '1', '1', '1'},
				{'0', '0', '0', '0', '0', '0', '0', '0', '1', '1', '1', '1'},
				{'1', '1', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0'},
				{'1', '1', '0', '0', '0', '0', 't', '0', '0', '0', '0', '0'},
				{'0', '0', '0', '0', '0', '0', '1', '1', '0', '0', '0', '0'},
				{'0', '0', '0', '0', '0', '0', '1', '1', '0', '0', '0', '0'},
				{'0', '0', '1', '1', '0', '0', '1', '1', '1', '1', '0', '0'},
				{'0', '0', '1', '1', '0', '0', '1', '1', '1', '1', '0', '0'},
				{'0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0'},
				{'0', '0', '0', '0', '0', '0', 'r', '0', '0', '0', '0', '0'}
			};
		}
		else if (rand == 3) {
			template = new char[,] {
				{'0', '0', '0', '0', '0', '0', '0', '0', '1', '1', '0', '0'},
				{'0', '0', '0', '0', '0', '0', '0', '0', '1', '1', '0', '0'},
				{'0', '0', '1', '1', '0', '0', '1', '1', '0', '0', '0', '0'},
				{'0', '0', '1', '1', '0', '0', '1', '1', '0', '0', '0', '0'},
				{'0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0'},
				{'0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '0'},
				{'0', '0', '1', '1', '1', '1', '1', '1', '0', '0', '0', '0'},
				{'0', '0', '1', '1', '1', '1', '1', '1', '0', '0', '0', '0'},
				{'0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '1', '1'},
				{'0', '0', '0', '0', '0', '0', '0', '0', '0', '0', '1', '1'},
				{'1', '1', '0', '0', '0', '0', '0', '0', '1', '1', '0', '0'},
				{'1', '1', '0', 'r', '0', '0', '0', '0', '1', '1', '0', '0'}
			};
		}

//		if (type == RoomType.BottomLeft) {
//			if (rand == 1) {
//				template = new char[,] {
//					{'1', '0', '0', '0', '0'},
//					{'1', '0', '0', '0', '0'},
//					{'1', '0', '0', '0', '0'},
//					{'1', '0', '0', '0', '0'},
//					{'1', '1', '1', '1', '1'}
//				};
//			}
//			else if (rand == 2) {
//				template = new char[,] {
//					{'1', '0', '1', '1', '0'},
//					{'1', '0', '1', '1', '0'},
//					{'1', '0', '0', '0', '0'},
//					{'1', '0', '0', '0', '0'},
//					{'1', '1', '1', '1', '1'}
//				};
//			}
//		}
//		else if (type == RoomType.RoomType2) {
//			if (rand == 1) {
//				template = new char[,] {
//					{'0', '0', '0', '0', '0'},
//					{'0', '1', '1', '0', '0'},
//					{'0', '1', '1', '0', '0'},
//					{'0', '0', '0', '0', '0'},
//					{'0', '0', '0', '1', '1'}
//				};
//			}
//			if (rand == 2) {
//				template = new char[,] {
//					{'1', '0', '0', '1', '1'},
//					{'1', '0', '0', '0', '0'},
//					{'0', '1', '1', '0', '0'},
//					{'0', '1', '1', '0', '0'},
//					{'0', '0', '0', '0', '0'}
//				};
//			}
//			if (rand == 3) {
//				template = new char[,] {
//					{'0', '1', '1', '0', '0'},
//					{'0', '1', '1', '0', '0'},
//					{'0', '0', '0', '0', '1'},
//					{'0', '0', '0', '0', '1'},
//					{'0', '0', '0', '0', '0'}
//				};
//			}
//		}
//		else if (type == RoomType.RoomType3) {
//			if (rand == 1) {
//				template = new char[,] {
//					{'0', '0', '0', '0', '0'},
//					{'1', '1', '1', '1', '0'},
//					{'0', '0', '0', '0', '0'},
//					{'0', '0', '0', '0', '0'},
//					{'0', '0', '0', '0', '0'}
//				};
//			}
//			if (rand == 2) {
//				template = new char[,] {
//					{'0', '0', '0', '0', '0'},
//					{'0', '0', '0', '0', '0'},
//					{'0', '1', '1', '1', '1'},
//					{'0', '0', '0', '0', '0'},
//					{'0', '0', '0', '0', '0'}
//				};
//			}
//			if (rand == 3) {
//				template = new char[,] {
//					{'0', '1', '0', '0', '0'},
//					{'1', '1', '0', '0', '0'},
//					{'0', '1', '0', '1', '0'},
//					{'0', '0', '0', '1', '1'},
//					{'0', '0', '0', '1', '0'}
//				};
//			}
//		}
//		else if (type == RoomType.RoomType4) {
//			if (rand == 1) {
//				template = new char[,] {
//					{'1', '0', '0', '0', '0'},
//					{'0', '1', '0', '0', '0'},
//					{'0', '0', '0', '0', '0'},
//					{'0', '0', '0', '1', '0'},
//					{'0', '0', '0', '0', '1'}
//				};
//			}
//			if (rand == 2) {
//				template = new char[,] {
//					{'0', '0', '0', '0', '1'},
//					{'0', '0', '0', '1', '0'},
//					{'0', '0', '0', '0', '0'},
//					{'0', '1', '0', '0', '0'},
//					{'1', '0', '0', '0', '0'}
//				};
//			}
//			if (rand == 3) {
//				template = new char[,] {
//					{'0', '0', '0', '0', '0'},
//					{'0', '1', '0', '0', '0'},
//					{'1', '0', '0', '0', '0'},
//					{'0', '0', '0', '0', '1'},
//					{'0', '0', '0', '1', '0'}
//				};
//			}
//		}
//		else return null;

		return TranslateTemplate(template);
	}

	static char[,] TranslateTemplate(char[,] template) {
		char[,] newTemplate = new char[template.GetLength(1), template.GetLength(0)];

		for (int x = 0; x < Room.roomWidth; x++) {
			for (int y = 0; y < Room.roomHeight; y++) {
				newTemplate[x, Room.roomHeight - 1 - y] = template[y, x];
			}
		}

		return newTemplate;
	}
}
