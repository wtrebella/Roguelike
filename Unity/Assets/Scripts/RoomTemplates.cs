using UnityEngine;
using System.Collections;

public class RoomTemplates {

	public static char[,] GetRoomTemplate(RoomType type) {
		char[,] template;

		if (type == RoomType.RoomType1) {
			template = new char[,] {
				{'1', '1', '1', '1', '0', '0', '1', '1', '1', '1'},
				{'1', '0', '0', '0', '0', '0', '0', '0', '0', '1'},
				{'1', '0', '0', '0', '0', '0', '0', '0', '0', '1'},
				{'1', '0', '0', '0', '0', '0', '0', '0', '0', '1'},
				{'1', '0', '0', '0', '0', 'p', '0', '0', '0', '1'},
				{'1', '0', '0', '0', '0', '1', '0', '0', '0', '1'},
				{'1', '1', '1', '0', '0', '1', '1', '1', '1', '1'}
			};
		}
		else if (type == RoomType.RoomType2) {
			template = new char[,] {
				{'1', '1', '1', '1', '1', '1', '1', '1', '1', '1'},
				{'1', '0', '0', '0', '0', '0', '0', '0', '0', '1'},
				{'0', '0', '0', '0', '0', '0', '0', '0', '0', '1'},
				{'0', '0', '0', '0', '0', '0', '0', '0', '0', '0'},
				{'0', '0', '0', 'p', '0', '0', '0', '0', '0', '0'},
				{'1', '0', '0', '1', '0', '0', '0', '0', '0', '0'},
				{'1', '1', '1', '1', '1', '1', '1', '1', '1', '1'}
			};
		}
		else if (type == RoomType.RoomType3) {
			template = new char[,] {
				{'1', '1', '1', '1', '0', '0', '1', '1', '1', '1'},
				{'1', '0', '0', '0', '0', '0', '0', '0', '0', '0'},
				{'0', '0', '0', '0', '0', '0', '0', '0', '0', '0'},
				{'0', '0', '0', '0', '0', '0', '0', '0', '0', '0'},
				{'0', '0', '0', '0', 'p', '0', '0', '0', '0', '1'},
				{'1', '0', '0', '0', '1', '0', '0', '0', '0', '1'},
				{'1', '1', '1', '1', '1', '1', '1', '1', '1', '1'}
			};
		}
		else return null;

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
