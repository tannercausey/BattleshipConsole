using System;
using System.Threading;

namespace battleship {
	sealed class Program {
		static void Main(string[] args) {
			int size = 10;
			Console.WriteLine("Welcome to Battleship!\nEnter 1 for Player v Player, or enter 2 for Player v Computer:");
			if (Console.ReadKey(true).KeyChar == '1') {	// PvP
				UserBoard player1 = new UserBoard(size, 1);
				UserBoard player2 = new UserBoard(size, 2);
				player1.SetOppBoard(player2);
				player2.SetOppBoard(player1);
				
				player1.PutShipsRandom();
				player2.PutShipsRandom();
				
				while (!player1.IsGameOver() && !player2.IsGameOver()) {
					Console.Clear();
					player1.TakeTurn();
					player2.TakeTurn();
				}
				if (player1.IsGameOver()) Console.WriteLine("Player 2 wins!");
				else Console.WriteLine("Player 1 wins!");
				return;
			}
			
			else {	// PvE
				UserBoard user = new UserBoard(size, 1);
				CompBoard comp = new CompBoard(size);
				user.SetOppBoard(comp);
				comp.SetOppBoard(user);
				
				user.PutShipsRandom();
				comp.PutShipsRandom();
				//comp.PrintBoard(); //print comp board for easy testing
				while (user.IsGameOver() == false && comp.IsGameOver() == false) {
					Console.Clear();
					user.TakeTurn();
					comp.TakeTurn();
				}
				if (user.IsGameOver()) Console.WriteLine("Computer wins!");
				else Console.WriteLine("User wins!");
			}
		}
		public static string ReadString(string prompt) {	// this helper function saves a lot of LOC
			string input = String.Empty;	// declare var
			do {	// do once
				Console.WriteLine(prompt);	// write prompt to console
				input = Console.ReadLine();	// read input to variable
			} while (input.Equals(string.Empty) || input == "\n");	// while user enters empty string
			return input;	// return user input
		}
		public static void PrintColor(string message, System.ConsoleColor textColor) {	// method to print message in textColor :)
			Console.ForegroundColor = textColor;	// set text color to textColor
			Console.WriteLine(message);			// write message to Console
			Console.ResetColor();		// reset
			if (textColor == ConsoleColor.DarkYellow)	Thread.Sleep(1500);	// wait 1.5 seconds
			else if (textColor == ConsoleColor.Green)	Thread.Sleep(1000);	// wait 1 seconds
			else if (textColor == ConsoleColor.Red)		Thread.Sleep(3500);	// wait 3.5 seconds
		}
	}
}