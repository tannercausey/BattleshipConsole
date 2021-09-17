using System;
using System.Threading;
namespace battleship {
	class Board {
		public int size = 10;
		public int hits = 0;
		public int misses = 0;
		public Ship carrier;
		public Ship battleship;
		public Ship destroyer;
		public Ship submarine;
		public Ship patrolBoat;
		public char[,] board;
		public char[,] shots;
		public Board oppBoard;
		public Board() {}

		public Board(int size) {
			this.size = size;
			board = new char[size, size];
			shots = new char[size, size];
			for (int i = 0; i < size; i++)
				for (int j = 0; j < size; j++) {
					board[i, j] = '_';
					shots[i, j] = ' ';
				}
		}
		public void SetOppBoard(Board oppBoard) { this.oppBoard = oppBoard; }

		public void PutShipsRandom() {
			PutShip(5, 'C');	// carrier
			PutShip(4, 'B');	// battleship
			PutShip(3, 'D');	// destroyer
			PutShip(3, 'S');	// submarine
			PutShip(2, 'P');	// patrol boat
		}

		public bool IsGameOver() { if (carrier.sunk && battleship.sunk && destroyer.sunk && submarine.sunk && patrolBoat.sunk) return true; else return false; }

		public void PrintShots() {
			Console.ForegroundColor = ConsoleColor.DarkGray;
			Console.Write("\t ");
			for (int i = 0; i < size; i++) Console.Write("{0} ", i+1);	// print column numbers
			Console.WriteLine();
			for (int i = 0; i < size; i++) {
				Console.Write("{0}\t", i+1);
				for (int j = 0; j < size; j++)
					PrintChar(shots[i, j]);
				Console.WriteLine('|');
			}
			Console.ResetColor();
		}

		public void PrintBoard() {
			Console.ForegroundColor = ConsoleColor.DarkGray;
			Console.Write('\t');
			for (int i = 0; i < size; i++) Console.Write("{0} ", i+1);	// print column numbers
			Console.WriteLine();
			for (int i = 0; i < size; i++) {
				Console.Write("{0}\t", i+1);
				for (int j = 0; j < size; j++)
					PrintChar(board[i, j]);
				Console.WriteLine('|');
			}
			Console.ResetColor();
		}
		// helper function
		public void PutShip(int length, char ship) {
			Orientation orientation = new Random().Next(2) == 1 ? Orientation.Vertical : Orientation.Horizontal;
			int row, col;
			if (orientation == Orientation.Horizontal) {
				row = new Random().Next(size);
				col = new Random().Next(size - length);
				while (ShipAlreadyInRange(row, col, length, orientation)) {
					row = new Random().Next(size);
					col = new Random().Next(size - length);
				}
				for (int i = 0; i < length; i++)
					board[row, col + i] = ship;
			}
			else {
				row = new Random().Next(size - length);
				col = new Random().Next(size);
				while (ShipAlreadyInRange(row, col, length, orientation)) {
					row = new Random().Next(size - length);
					col = new Random().Next(size);
				}
				for (int i = 0; i < length; i++)
					board[row + i, col] = ship;
			}
			switch (ship) {
				case 'C':
					carrier = new Ship(ship, "carrier", orientation, row, col, length);
					break;
				case 'B':
					battleship = new Ship(ship, "battleship", orientation, row, col, length);
					break;
				case 'D':
					destroyer = new Ship(ship, "destroyer", orientation, row, col, length);
					break;
				case 'S':
					submarine = new Ship(ship, "submarine", orientation, row, col, length);
					break;
				case 'P':
					patrolBoat = new Ship(ship, "patrol boat", orientation, row, col, length);
					break;
			}
			
		}
		public bool ShipAlreadyInRange(int row, int col, int length, Orientation orientation) {
			if (orientation == Orientation.Horizontal) {
				for (int i = col; i < col + length; i++) {
					if ("CBDSP".Contains(board[row, i]))
						return true;
				}
				return false;
			}
			else {
				for (int i = row; i < row + length; i++) {
					if ("CBDSP".Contains(board[i, col]))
						return true;
				}
				return false;
			}
		}
		public virtual void Shoot(int row, int col) {	// return true if it's a hit, else return false
			if ("XO".Contains(oppBoard.board[row, col])) {	// already shot here 
				Program.PrintColor($"You've already shot at ({row}, {col}).", ConsoleColor.Red);
			}
			else if (Char.IsLetter(oppBoard.board[row, col])) {	// HIT
				Program.PrintColor("HIT!", ConsoleColor.Green);
				oppBoard.shots[row, col] = 'X';
				hits++;
				if (oppBoard.board[row, col].Equals('C')) {
					carrier.Hit();
				}
				else if (oppBoard.board[row, col].Equals('B')) {
					battleship.Hit();
				}
				else if (oppBoard.board[row, col].Equals('D')) {
					destroyer.Hit();
				}
				else if (oppBoard.board[row, col].Equals('S')) {
					submarine.Hit();
				}
				else if (oppBoard.board[row, col].Equals('P')) {
					patrolBoat.Hit();
				}
			}
			else {		// Miss
				Program.PrintColor("Miss!", ConsoleColor.Red);
				oppBoard.shots[row, col] = 'O';
				misses++;
			}
		}

		public void PrintChar(char x) {
			Console.Write('|');
			if (x.Equals('X')) Console.ForegroundColor = ConsoleColor.Red;
			if (x.Equals('O')) Console.ForegroundColor = ConsoleColor.White;
			Console.Write(x);
			Console.ForegroundColor = ConsoleColor.DarkGray;
		}
	}
}