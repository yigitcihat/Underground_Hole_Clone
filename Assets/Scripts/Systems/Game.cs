using System.Collections.Generic;

public static class Game
{
	public static bool IsGameOver = false;
	public static bool IsGameStart = false;
	public static bool IsHole = true;
	public static bool IsMoving = false;
	public static List<Resource> CreatedResources = new List<Resource>();
	public static int RequiredIron;
	public static int RequiredWood;
	public static int RequiredPlastic;
}
