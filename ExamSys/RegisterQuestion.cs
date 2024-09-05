using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;


namespace ExamSys
{
    public class RegisterQuestion
    {

        QuestionBank questionBank = new QuestionBank();
        public int NumberOfQuestions { get; set; }
        public string? TeacherName { get; set; }
        public void RegisterQuestions()
        {
            #region animation
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            string textExam = "                     _____                     \r\n                    | ____|_  ____ _ _ __ ___  \r\n                    |  _| \\ \\/ / _` | '_ ` _ \\ \r\n ____            _  | |___ >  < (_| | | | | | |\r\n/ ___| _   _ ___| |_|_____/_/\\_\\__,_|_| |_| |_|\r\n\\___ \\| | | / __| __/ _ \\ '_ ` _ \\             \r\n ___) | |_| \\__ \\ ||  __/ | | | | |            \r\n|____/ \\__, |___/\\__\\___|_| |_| |_|            \r\n       |___/                                   ";
            string bookDes = "       .--.                   .---.\r\n   .---|__|           .-.     |~~~|\r\n.--|===|--|_          |_|     |~~~|--.\r\n|  |===|  |'\\     .---!~|  .--|   |--|\r\n|%%|   |  |.'\\    |===| |--|%%|   |  |\r\n|%%|   |  |\\.'\\   |   | |__|  |   |  |\r\n|  |   |  | \\  \\  |===| |==|  |   |  |\r\n|  |   |__|  \\.'\\ |   |_|__|  |~~~|__|\r\n|  |===|--|   \\.'\\|===|~|--|%%|~~~|--|\r\n^--^---'--^    `-'`---^-^--^--^---'--' ";
            Console.WriteLine(bookDes);
            Console.WriteLine(textExam);
            Console.ResetColor();
            Thread.Sleep(1000);
            Console.Clear();
            #endregion
            // The Introduction For The Teacher

            int QOrder = 1;
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Write($"Welcome Teacher Please Enter your name: ");
            Console.ResetColor();
            TeacherName = Console.ReadLine()?.ToUpper();
            while (string.IsNullOrWhiteSpace(TeacherName)) 
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Please enter Name");
                Console.ResetColor();
                TeacherName = Console.ReadLine(); 
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"How Many Question Will be MR.{TeacherName}? ");
            Console.ResetColor();
            int numberOfQuestions;

            while (!int.TryParse(Console.ReadLine(), out numberOfQuestions) || numberOfQuestions <= 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Please enter a valid positive number for the number of questions.");
                Console.ResetColor();
            }
            NumberOfQuestions = numberOfQuestions;
            int questionCounter = 0;

            // Menu of the Questions
            while (NumberOfQuestions > questionCounter)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"╔═══════════════════════════════════════════════════╗");
                Console.WriteLine($"\t[Q-{QOrder}] - What the type of the question? ");
                Console.WriteLine($"\tType [1] for choose one \r\n\tType [2] for true or false \r\n\tType [3] for essay \r\n\tType [4] for fill the blank");
                Console.WriteLine($"╚═══════════════════════════════════════════════════╝");
                Console.ResetColor();
                string? typeOfTheQuestion = Console.ReadLine();

                if (typeOfTheQuestion == "1")
                {
                    MultipleChoice(QOrder);
                }
                else if (typeOfTheQuestion == "2")
                {
                    TrueOrFalse(QOrder);
                }
                else if (typeOfTheQuestion == "3")
                {
                    Essay(QOrder);
                }
                else if (typeOfTheQuestion == "4")
                {
                    FillTheBlank(QOrder);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"please enter one the above!!");
                    Console.ResetColor();
                    QOrder--;
                    questionCounter--;
                }
                QOrder++;
                questionCounter++;

            }
            Exam exam = new Exam(questionBank, NumberOfQuestions, TeacherName);
            exam.ConductExam();
        }
        public void MultipleChoice(int QOrder)
        {
            List<string> listOfAnswers = new List<string>();

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine($"\nWrite the head of the choice Question");
            Console.ResetColor();
            string? question = Console.ReadLine();
            while (string.IsNullOrWhiteSpace(question))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Pleas enter the Question");
                question = Console.ReadLine();
                Console.ResetColor();
            }

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine($"How many answers do you want?");
            int answerNumber;
            Console.ResetColor();
            while (!int.TryParse(Console.ReadLine(), out answerNumber) || answerNumber <= 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Please enter a valid number");
                Console.ResetColor();
            }

            int answersCounter = 0;
            string? answers;
            while (answerNumber > answersCounter)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine($"Enter new answer number /{answersCounter + 1}/");
                Console.ResetColor();
                answers = Console.ReadLine();
                while (string.IsNullOrWhiteSpace(answers))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Please enter the answer:");
                    Console.ResetColor();
                    answers = Console.ReadLine();
                }
                listOfAnswers.Add(answers);
                answersCounter++;
            }

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("Write the right answer");
            Console.ResetColor();
            string? chooses = Console.ReadLine();
            while (string.IsNullOrWhiteSpace(chooses))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Please enter the right answer");
                Console.ResetColor();
                chooses = Console.ReadLine();
            }
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("Grade");
            Console.ResetColor();
            int grade;
            while (!int.TryParse(Console.ReadLine(), out grade) || grade < 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Please enter a valid grade.");
                Console.ResetColor();
            }

            questionBank.AddQuestion(new Question("Multiple Choice Question\n- " + question, listOfAnswers, chooses, grade));
            
        }
        public void TrueOrFalse(int QOrder)
        {
            List<string> listOfAnswers = new List<string>() { "True", "False" };
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine($"\nWrite the head of the ✓,X Question");
            Console.ResetColor();
            string? question = Console.ReadLine();
            while (string.IsNullOrWhiteSpace(question))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Pleas enter question");
                Console.ResetColor();
                question = Console.ReadLine();
            }

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine($"Enter the answer (true / false) ");
            Console.ResetColor();
            string? chooses = Console.ReadLine()?.ToLower();

            while (chooses != "true" && chooses != "false")
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Please enter 'true' or 'false' only");
                Console.ResetColor();
                chooses = Console.ReadLine()?.ToLower();
            }

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("Grade");
            Console.ResetColor();
            int grade;
            while (!int.TryParse(Console.ReadLine(), out grade) || grade < 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Please enter a valid non-negative number for the grade.");
                Console.ResetColor();
            }

            questionBank.AddQuestion(new Question("Enter 'true' or 'false'\n- " + question, listOfAnswers, chooses, grade));
        }
        public void Essay(int QOrder)
        {
            List<string> listOfAnswers = new List<string>();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine($"\nWrite the head of the essay Question");
            Console.ResetColor();
            string? question = Console.ReadLine();
            while (string.IsNullOrWhiteSpace(question))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Pleas enter question");
                Console.ResetColor();
                question = Console.ReadLine();

            }

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine($"Enter the answer");
            Console.ResetColor();
            string? answers = Console.ReadLine();
            while (string.IsNullOrWhiteSpace(answers))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Please enter the answer:");
                Console.ResetColor();
                answers = Console.ReadLine();
            }

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("Grade");
            Console.ResetColor();
            int grade;
            while (!int.TryParse(Console.ReadLine(), out grade) || grade < 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Please enter a valid non-negative number for the grade.");
                Console.ResetColor();
            }
            questionBank.AddQuestion(new Question("Write the answer\n- " + question, listOfAnswers, answers, grade));
        }
        public void FillTheBlank(int QOrder)
        {
            List<string> listOfAnswers = new List<string>();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine($"\nWrite the head of fill the blank Question");
            Console.ResetColor();
            string? tempQuestion = Console.ReadLine();
            while (string.IsNullOrWhiteSpace(tempQuestion))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Pleas enter question");
                Console.ResetColor();
                tempQuestion = Console.ReadLine();

            }

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine($"Enter the Word you want to hide from the head of the question");
            Console.ResetColor();
            string? answers = Console.ReadLine()?.ToLower(); ;
            while (string.IsNullOrWhiteSpace(answers) || !tempQuestion.Contains(answers))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Please enter valid word existing in the head of the question");
                Console.ResetColor();
                answers = Console.ReadLine();
            }

            string question = tempQuestion.Replace(answers, "■■■■");

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("Grade");
            Console.ResetColor();
            int grade;
            while (!int.TryParse(Console.ReadLine(), out grade) || grade < 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Please enter a valid non-negative number for the grade.");
                Console.ResetColor();
            }
            questionBank.AddQuestion(new Question("Fill the blank\n- " + question, listOfAnswers, answers, grade));
        }
    }
}
