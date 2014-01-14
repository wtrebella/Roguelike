﻿using UnityEngine;
using System.Collections;

public class RoomTemplates {

	public static char[,] GetRoomTemplate(int type) {
		char[,] template = null;
		
		if (type == 1) {
			int rand = Random.Range(0, 5);

			if (rand == 0) {
				template = new char[,] {
					{'0', '0'},
					
					{'0', '0'},

					{'0', '0'},
					
					{'0', '0'},

					{'0', '0'},

					{'0', '0'},
					
					{'0', '0'},
					
					{'0', 'r'},
					
					{'1', '1'},

					{'1', '1'}
				};
			}
			else if (rand == 1) {
				template = new char[,] {
					{'1', '1'},
					
					{'0', '0'},
					
					{'0', '0'},
					
					{'0', '0'},
					
					{'0', '0'},
					
					{'0', '0'},
					
					{'0', '0'},
					
					{'0', '0'},
					
					{'1', '1'},
					
					{'1', '1'}
				};
			}
			else if (rand == 2) {
				template = new char[,] {
					{'1', '1'},
					
					{'1', '1'},
					
					{'0', '0'},
					
					{'0', '0'},
					
					{'0', '0'},
					
					{'0', '0'},
					
					{'0', '0'},
					
					{'1', '1'},
					
					{'1', '1'},
					
					{'1', '1'}
				};
			}
			else if (rand == 3) {
				template = new char[,] {
					{'1', '1'},
					
					{'1', '1'},
					
					{'1', '1'},
					
					{'0', '0'},
					
					{'0', '0'},
					
					{'0', '0'},
					
					{'1', '1'},
					
					{'1', '1'},
					
					{'1', '1'},
					
					{'1', '1'}
				};
			}
			else if (rand == 4) {
				template = new char[,] {
					{'1', '1'},
					
					{'1', '1'},
					
					{'1', '1'},
					
					{'1', '1'},
					
					{'0', '0'},
					
					{'0', 't'},
					
					{'1', '1'},
					
					{'1', '1'},
					
					{'1', '1'},
					
					{'1', '1'}
				};
			}
		}

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
