using System;
namespace battleship {
	class CompBoard : Board {
		public int lastHitRow;
		public int lastHitCol;
		public CompBoard(int size) {
			this.size = size;
			board = new char[size, size];
			shots = new char[size, size];
			for (int i = 0; i < size; i++)
				for (int j = 0; j < size; j++) {
					board[i, j] = '_';
					shots[i, j] = ' ';
				}
		}

		public void TakeTurn() {
			Shoot(new Random().Next(size), new Random().Next(size));
			oppBoard.PrintShots();
			return;
		}
		public override void Shoot(int row, int col) {
			if (lastHitCol != 0) {
				if (carrier.hit && !carrier.sunk) {
					if (carrier.orientation == Orientation.Horizontal) {	// row is the same
						col = ++lastHitCol;
						row = lastHitRow;
					}
					else {	// col is the same
						col = lastHitCol;
						row = ++lastHitRow;
					}
				}
				else if (battleship.hit && !battleship.sunk) {
					if (battleship.orientation == Orientation.Horizontal) {	// row is the same
						col = ++lastHitCol;
						row = lastHitRow;
					}
					else {	// col is the same
						col = lastHitCol;
						row = ++lastHitRow;
					}
				}
				else if (destroyer.hit && !destroyer.sunk) {
					if (destroyer.orientation == Orientation.Horizontal) {	// row is the same
						col = ++lastHitCol;
						row = lastHitRow;
					}
					else {	// col is the same
						col = lastHitCol;
						row = ++lastHitRow;
					}
				}
				else if (submarine.hit && !submarine.sunk) {
					if (submarine.orientation == Orientation.Horizontal) {	// row is the same
						col = ++lastHitCol;
						row = lastHitRow;
					}
					else {	// col is the same
						col = lastHitCol;
						row = ++lastHitRow;
					}
				}
				else if (patrolBoat.hit && !patrolBoat.sunk) {
					if (patrolBoat.orientation == Orientation.Horizontal) {	// row is the same
						col = ++lastHitCol;
						row = lastHitRow;
					}
					else {	// col is the same
						col = lastHitCol;
						row = ++lastHitRow;
					}
				}
			}
			if (Char.IsLetter(oppBoard.board[row, col])) {
				Program.PrintColor("HIT!", ConsoleColor.Green);
				if (oppBoard.board[row, col].Equals('C')) {
					if (oppBoard.board[this.lastHitRow, this.lastHitCol] != 'C') {
						row = carrier.row;
						col = carrier.col;
						lastHitRow = row;
						lastHitCol = col;
					}
					else {
						if (carrier.orientation == Orientation.Horizontal) {	// row is the same
							lastHitCol = col;
						}
					}
					carrier.Hit();
				}
			}
			else { Program.PrintColor("Miss...", ConsoleColor.Red); }
		}
	}
}