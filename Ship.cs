using System;
namespace battleship {
	enum Orientation {	Vertical, Horizontal	}
	class Ship {
		public char id;
		public string name;
		public Orientation orientation;
		public int row;
		public int col;
		public int length;
		public bool hit = false;
		public bool sunk = false;
		public int numHits = 0;
		public Ship(char id, string name, Orientation orientation, int row, int col, int length) {
			this.id = id;
			this.name = name;
			this.orientation = orientation;
			this.row = row;
			this.col = col;
			this.length = length;
		}
		public void Hit() {
			hit = true;
			numHits++;
			if (numHits == length) {
				sunk = true;
				Console.WriteLine("You sunk my {0}!", name);
				Console.Write("Press any key to continue: ");
				Console.ReadKey(true);
				Console.WriteLine();
			}
		}
		public bool Sunk() {	return sunk;	}
	}
}