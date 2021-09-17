using System;
namespace battleship {
	class UserBoard : Board {
		public int playerNum;
		public UserBoard(int size, int playerNum) {
			this.size = size;
			board = new char[size, size];
			shots = new char[size, size];
			for (int i = 0; i < size; i++)
				for (int j = 0; j < size; j++) {
					board[i, j] = '_';
					shots[i, j] = ' ';
				}
			this.playerNum = playerNum;
		}

		public void TakeTurn() {
			Console.WriteLine($"Player {this.playerNum}'s turn:");
			oppBoard.PrintShots();
			Console.WriteLine("Where would you like to shoot? Enter the row(1-10) followed by a space and then the column(1-10):");
			int row, col;
			string input = Console.ReadLine();
			string num1 = input.Split(' ')[0];
			string num2 = input.Split(' ')[1];
			while (!(int.TryParse(num1, out row) && int.TryParse(num2, out col)/* || row > 10 || col > 10*/)) {
				Program.PrintColor("Invalid entry.", ConsoleColor.Red);
				Console.WriteLine("Where would you like to shoot? Enter the row(1-10) followed by a space and then the column(1-10):");
				input = Console.ReadLine();
				num1 = input.Split(' ')[0];
				num2 = input.Split(' ')[1];
			}
			row--;// = int.Parse(input.Split(' ')[0]) - 1;
			col--;// = int.Parse(input.Split(' ')[1]) - 1;
			if (row >= size || col >= size) {
				misses++;
				return;	// return to avoid IndexOutOfRange Exception
			}
			Shoot(row, col);
			oppBoard.PrintShots();
		}
	}
}