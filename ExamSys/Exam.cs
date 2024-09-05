using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamSys
{
    internal class Exam
    {
        public Exam(QuestionBank questionBank, int numberOfQuestions, string teacherName) 
        {
            QuestionBank = questionBank;
            NumberOfQuestions = numberOfQuestions;
            TeacherName = teacherName;
        }
        public QuestionBank QuestionBank { get; set; }

        public int NumberOfQuestions { get; set; }

        public string TeacherName { get; set; }

        public Leaderboard leaderboard = new Leaderboard();
        public void ConductExam()
        {
            string? StudentName;
            int TotalScore;
            while (true) 
            {    
                #region animation
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                Console.Clear();
                Console.WriteLine($"█████▒▒▒▒▒50%");
                Thread.Sleep(250);
                Console.Clear();

                Console.WriteLine($"████████▒▒80%");
                Thread.Sleep(250);
                Console.Clear();

                Console.WriteLine($"██████████100%");
                Thread.Sleep(250);
                Console.Clear();
                Console.ResetColor();
                #endregion
                // The Introduction For The Student
                int questionCounter = 1;
                TotalScore = 0;
                List<string> studentAnswers = new List<string>();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("╒═════════════════════════════════════════════╕");
                DateTime time = DateTime.Now;
                Console.WriteLine($"\t   {time}");
                Console.WriteLine("╘═════════════════════════════════════════════╛");
                Console.ResetColor();
                Console.Write($"Hello student please Enter your Name: ");
                StudentName = Console.ReadLine();
                while (string.IsNullOrWhiteSpace(StudentName))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Please enter Name");
                    Console.ResetColor();
                    StudentName = Console.ReadLine();
                }
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine();
                Console.WriteLine($"Welcome {StudentName}\r\n"
                    + $"when you ready prese enter to start the exam...  ");
                Console.WriteLine($"Note: if you complete the exam within 10 minutes you will get extra 10 degree as bounce"
                   + $"\r\nThis Exam from MR.{TeacherName},and it Contain of {NumberOfQuestions} Question");
                Console.ReadLine();
                Console.WriteLine($"═════════════════════════════════════════════");
                Console.ResetColor();

                // Print the Questions and The Answers
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                foreach (var question in QuestionBank.Questions)
                {
                    Console.WriteLine($"\n[Q{questionCounter}] - {question.QuestionText}");
                    Console.WriteLine($"─────────────────────────");
                    questionCounter++;
                    for (int i = 0; i < question.AnswerOptions.Count; i++)
                    {
                        Console.WriteLine($"[{i + 1}]. {question.AnswerOptions[i]}");
                    }
                    Console.Write("Your answer: ");
                    string? answer = Console.ReadLine();
                    while (string.IsNullOrWhiteSpace(answer))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"Please enter valid answer");
                        Console.ResetColor();
                        answer = Console.ReadLine();
                    }
                    
                    studentAnswers.Add(answer);
                }

                stopwatch.Stop();
                TimeSpan elapsedTime = stopwatch.Elapsed;
                #region animation
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                Console.Clear();
                Console.WriteLine($"|||||| 25%");
                Thread.Sleep(250);
                Console.Clear();
                Console.WriteLine($"||||||||||| 50%");
                Thread.Sleep(250);
                Console.Clear();
                Console.WriteLine($"||||||||||||||||||||| 100%");
                Thread.Sleep(250);
                Console.Clear();
                Console.ResetColor();
                #endregion

                // The result of the student
                for (int i = 0; i < QuestionBank.Questions.Count; i++)
                {
                    var question = QuestionBank.Questions[i];
                    if (studentAnswers[i].Trim().Equals(question.CorrectAnswer, StringComparison.OrdinalIgnoreCase))
                    {
                        TotalScore += question.Grade;
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        Console.WriteLine($"Question {i + 1}: Correct");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"Question {i + 1}: Incorrect. The correct answer is: {question.CorrectAnswer}");
                        Console.ResetColor();
                    }
                }

                Console.WriteLine($"\r\nExam duration: {elapsedTime}\r\n");
                if (elapsedTime.TotalMinutes <= 10)
                {
                    TotalScore += 10;
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine($"Congratulation you got the Bounce +10 degrees");
                    Console.WriteLine($"Congrats {StudentName} Your total score is: {TotalScore}");
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"you passed 10 minutes you won't get the bounce ");
                    Console.WriteLine($"Congrats {StudentName} Your total score is: {TotalScore}");
                    Console.ResetColor();
                }
                // Send the data for the leaderboard class
                string padStudentName = PadString(StudentName, 21);
                string padTotalScore = GetPaddedTotalScore(TotalScore);

                leaderboard.AddScore(padStudentName, padTotalScore);
                leaderboard.DisplayLeaderboard();

                Console.WriteLine();
                Console.Write($"Is there another Student to practice in the Exam? (y/n)");
                string? anotherStudent = Console.ReadLine()?.ToLower();
                while (anotherStudent != "y" && anotherStudent != "n")
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Please enter Valid choose");
                    Console.ResetColor();
                    anotherStudent = Console.ReadLine()?.ToLower();
                }
                if (anotherStudent == "n")
                {
                    break;
                }
            }
        }
        // Just a methods to format and align the data in the Leaderboard table
        public string PadString(string input, int totalLength)
        {
            return input.PadRight(totalLength);
        }
        public string GetPaddedTotalScore(int totalScore)
        {
            return totalScore.ToString().PadLeft(14, ' ');
        }

    }
}
