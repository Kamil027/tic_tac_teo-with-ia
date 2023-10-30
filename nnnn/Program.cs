using System;

class TicTacToe
{
	static char[] board = { '1', '2', '3', '4', '5', '6', '7', '8', '9' };

	static void Main()
	{
		int player = 1; // Player 1 (insan) 
		int choice = 0;
		int moveCount = 0;
		bool validInput = false;

		do
		{
			Console.Clear();
			Console.WriteLine("Player 1: X and Player 2: O");
			Console.WriteLine("\n");
			DrawBoard();

			if (player == 1)
			{
				do
				{
					Console.WriteLine($"Player {player}'s turn. Choose an empty spot (1-9): ");
					validInput = int.TryParse(Console.ReadLine(), out choice);

					if (!validInput || choice < 1 || choice > 9 || board[choice - 1] == 'X' || board[choice - 1] == 'O')
					{
						Console.WriteLine("Invalid input. Please try again.");
						validInput = false;
					}
				} while (!validInput);
			}
			else
			{
				// komputer istifade edir minMax
				choice = GetComputerMove();
			}

			if (player == 1)
				board[choice - 1] = 'X';
			else
				board[choice - 1] = 'O';

			moveCount++;
			player = player == 1 ? 2 : 1;

		} while (!CheckForWin() && moveCount < 9);

		Console.Clear();
		DrawBoard();
		if (moveCount == 9 && !CheckForWin())
			Console.WriteLine("It's a draw!");
		else
			Console.WriteLine($"Player {player} wins!");

		Console.ReadLine();
	}

	static int GetComputerMove()
	{
		int bestMove = -1;
		int bestScore = int.MinValue;

		for (int i = 0; i < board.Length; i++)
		{
			if (board[i] != 'X' && board[i] != 'O')
			{
				board[i] = 'O';
				int score = Minimax(board, false);
				board[i] = (i + 1).ToString()[0]; // novbeti iterasiya ucun lovheni yenilemek

				if (score > bestScore)
				{
					bestScore = score;
					bestMove = i;
				}
			}
		}

		return bestMove + 1;
	}

	static int Minimax(char[] board, bool isMaximizing)
	{
		int score = Evaluate(board);

		if (score == 10)
			return score;

		if (score == -10)
			return score;

		if (!IsMovesLeft(board)) //hereket ucun imkan yoxdursa
			return 0;

		if (isMaximizing)
		{
			int best = int.MinValue;

			for (int i = 0; i < board.Length; i++)
			{
				if (board[i] != 'X' && board[i] != 'O')
				{
					board[i] = 'O';
					best = Math.Max(best, Minimax(board, !isMaximizing));
					board[i] = (i + 1).ToString()[0];
				}
			}
			
			return best;
		}
		else
		{
			int best = int.MaxValue;

			for (int i = 0; i < board.Length; i++)
			{
				if (board[i] != 'X' && board[i] != 'O')
				{
					board[i] = 'X';
					best = Math.Min(best, Minimax(board, !isMaximizing));
					board[i] = (i + 1).ToString()[0];
				}
			}

			return best;
		}
	}

	static int Evaluate(char[] board)
	{
		for (int i = 0; i < 8; i++)
		{
			int a, b, c;

			switch (i)
			{
				case 0:
					a = 0; b = 1; c = 2;
					break;
				case 1:
					a = 3; b = 4; c = 5;
					break;
				case 2:
					a = 6; b = 7; c = 8;
					break;
				case 3:
					a = 0; b = 3; c = 6;
					break;
				case 4:
					a = 1; b = 4; c = 7;
					break;
				case 5:
					a = 2; b = 5; c = 8;
					break;
				case 6:
					a = 0; b = 4; c = 8;
					break;
				case 7:
					a = 2; b = 4; c = 6;
					break;
				default:
					a = b = c = 0;
					break;
			}

			if (board[a] == board[b] && board[b] == board[c])
			{
				if (board[a] == 'O')
					return 10;
				else if (board[a] == 'X')
					return -10;
			}
		}

		return 0;
	}

	static bool IsMovesLeft(char[] board)
	{
		for (int i = 0; i < board.Length; i++)
		{
			if (board[i] != 'X' && board[i] != 'O')
				return true;
		}
		return false;
	}

	static bool CheckForWin()
	{
		for (int i = 0; i < 8; i++)
		{
			int a, b, c;

			switch (i)
			{
				case 0:
					a = 0; b = 1; c = 2;
					break;
				case 1:
					a = 3; b = 4; c = 5;
					break;
				case 2:
					a = 6; b = 7; c = 8;
					break;
				case 3:
					a = 0; b = 3; c = 6;
					break;
				case 4:
					a = 1; b = 4; c = 7;
					break;
				case 5:
					a = 2; b = 5; c = 8;
					break;
				case 6:
					a = 0; b = 4; c = 8;
					break;
				case 7:
					a = 2; b = 4; c = 6;
					break;
				default:
					a = b = c = 0;
					break;
			}

			if (board[a] == board[b] && board[b] == board[c])
			{
				if (board[a] == 'O')
					return true;
				else if (board[a] == 'X')
					return true;
			}
		}

		return false;
	}

	static void DrawBoard()
	{
		Console.WriteLine("     |     |      ");
		Console.WriteLine($"  {board[0]}  |  {board[1]}  |  {board[2]}");
		Console.WriteLine("_____|_____|_____ ");
		Console.WriteLine("     |     |      ");
		Console.WriteLine($"  {board[3]}  |  {board[4]}  |  {board[5]}");
		Console.WriteLine("_____|_____|_____ ");
		Console.WriteLine("     |     |      ");
		Console.WriteLine($"  {board[6]}  |  {board[7]}  |  {board[8]}");
		Console.WriteLine("     |     |      ");
	}
}
