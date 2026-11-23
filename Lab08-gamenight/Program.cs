
/*
* Name: Nathan Akins
* Course: CSCI 1250, Section 002
* Assignment: Lab 08, Game Night
* Date: November 22, 2026
* Description: Keeps a game night leaderboard in three text files. Shows the
* season so far, records tonight's points, saves the new totals,
* and adds tonight's result to a history that grows every week.
*/



const string PLAYERS_FILE = "players.txt";
const string POINTS_FILE = "points.txt";
const string HISTORY_FILE = "history.txt";
const int MAX_POINTS = 20;
// Look for the data files next to Program.cs, however the program is started.
string projectFolder = Path.GetFullPath(
Path.Combine(AppContext.BaseDirectory, "..", "..", ".."));
Directory.SetCurrentDirectory(projectFolder);

System.Console.WriteLine("=== GAME NIGHT LEADERBOARD ===");
string[] history = File.ReadAllLines(HISTORY_FILE);
if (history.Length == 0)
{
    System.Console.WriteLine("No nights played yet.");
}
else
{
    System.Console.WriteLine($"Nights played: {history.Length}");
}


// foreach: I need to display every history entry, but I don't need an index to do it.
foreach (string night in history)
{
    System.Console.WriteLine($"  {night}");
}

int tonightNum = history.Length + 1;
System.Console.WriteLine($"Tonight is Night {tonightNum}.");


System.Console.WriteLine("PLAYER".PadRight(11)  +   "POINTS");

string[] players = File.ReadAllLines(PLAYERS_FILE);
string[] points = File.ReadAllLines(POINTS_FILE);

// PART 1: Read the player and point files and display the season standings.

// for: the player names and points are in corresponding arrays, so I need a shared index to match them.
for (int i = 0; i < points.Length; i++)
{
    System.Console.WriteLine($"{players[i]}".PadRight(11) + points[i]);
}




int pointtotal = TotalPoints(points);
int bestplayer = LeaderIndex(points);




System.Console.WriteLine($"Leader: {players[bestplayer]} with {points[bestplayer]} points");
System.Console.WriteLine($"Season Total: {pointtotal}");


