using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamSys
{
    internal class Leaderboard
    {
        public List<StudentScore> StudentScores { get; set; } = new List<StudentScore>();

        public void AddScore(string studentName, string score)
        {
            StudentScores.Add(new StudentScore { StudentName = studentName, Score = score });
        }
        public void DisplayLeaderboard()
        {
            StudentScores.Sort(CompareScores);
            Console.WriteLine();
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("        ---=====Leaderboard====---         ");
            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine($"+----+---------------------+--------------+\r\n|    |                     |              |\r\n+ Nr +         Name        +    Degree    +\r\n|    |                     |              |");
            for (int i = 0; i < StudentScores.Count; i++)
            {
                Console.WriteLine($"+----+---------------------+---+---+---+--+\r\n| {i + 1}  |{StudentScores[i].StudentName}|{StudentScores[i].Score}|\r\n+----+---------------------+---+---+---+--+");
            }
            Console.ResetColor();
        }
        // To make sure whose the best will remain number 1
        public static int CompareScores(StudentScore s1, StudentScore s2)
        {
                return s2.Score.CompareTo(s1.Score);   
        }

        // DataType tp use it to transfer data with List
        public class StudentScore
        {
            public string? StudentName { get; set; }
            public string? Score { get; set; }
        }
    }
}

