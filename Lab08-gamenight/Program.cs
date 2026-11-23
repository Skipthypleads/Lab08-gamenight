
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


// PART 2: Collect tonight's scores, update the season totals, and find tonight's winner.

int tonightscore;
int season;
string Mostvaluableplayer = "";
int Highestpoints = -1;

// for: I need to go through each player in order and use the same index to access their name and season points.
for (int i = 0; i < players.Length; i++)
{
    System.Console.Write($"How many points did {players[i]} get tonight: ");
    tonightscore = Convert.ToInt32(Console.ReadLine());

// while: I don't know how many attempts it will take for a player to enter a valid score, so im keeping them in prompt purgatory until they follow my rules 
    while (tonightscore < 0 || tonightscore > MAX_POINTS)
    {
        System.Console.WriteLine("Points must be from 0 to 20.");
        System.Console.Write($"How many points did {players[i]} get tonight: ");
        tonightscore = Convert.ToInt32(Console.ReadLine());
    }
    System.Console.WriteLine($"Points for {players[i]} tonight: {tonightscore}");
    season = Convert.ToInt32(points[i]) + tonightscore;
    points[i] = season.ToString();
    if (tonightscore > Highestpoints)
    {
        Highestpoints = tonightscore;
        Mostvaluableplayer = players[i];
    }

}


File.WriteAllLines(POINTS_FILE, points);
System.Console.WriteLine($"Tonight's winner: {Mostvaluableplayer} with {Highestpoints}");

// Part 3: Read previous game nights and add tonight's result to the history.

string[] entry = { HistoryEntry(tonightNum, Mostvaluableplayer, Highestpoints) };
File.AppendAllLines(HISTORY_FILE, entry);
System.Console.WriteLine("Saved points.txt");
System.Console.WriteLine($"Added to history.txt: {entry[0]}");



// Creates the formatted history entry for a game night.
static string HistoryEntry(int night, string winner, int points)
{
    return $"Night {night}: {winner} won with {points} points";
}

// Returns the index of the player with the highest season score.
static int LeaderIndex(string[] points)
{
    int highscore = 0;

    int bestplayer = 0;

// for: I need to compare every score and keep track of the index belonging to the highest one.
        for (int i = 0; i < points.Length; i++)
    {
        if (Convert.ToInt32(points[i]) > highscore)
        {
            highscore = Convert.ToInt32(points[i]);
            bestplayer = i;
        }
    }

    return bestplayer;
}

// Adds all player scores together and returns the season total.
static int TotalPoints(string[] points)
{
    int pointtotal = 0;

// foreach: I need to add every score to the total, and I don't need to track their positions.
    foreach (string point in points)
    {
    pointtotal += Convert.ToInt32(point);
    }
    return pointtotal;
}