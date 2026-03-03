using ExaminationSystem.Classes;
using System;
using System.Collections.Generic;
using System.IO;
using static System.Console;

namespace ExaminationSystem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /*
             WriteLine(@"
                  = = = = = = = =⌉
                 //
                //                 ===        ===    =====           ===               ===||
                ||                   \\      //     ||    \\        ||  \\            //  ||
                ||                    \\    //      ||     \\       ||   \\          //   ||
                |⊢ = = = = = = =       \\  //       ||      \\      ||    \\        //    ||
                ||                      \\//        ||== == =\\     ||     \\      //     ||
                ||                      //\\        ||        \\    ||      \\    //      ||
                ||                     //  \\       ||         \\   ||       \\  //       ||
                ||= = = = = = = =    ===    ===     ||          \\  ||        \\//        ||
        
");
             */
            // ensure folder structure exists
            FilesOperations.CreateMainFolders();

            // build a subject and a persistent question list
            Subject subject = new Subject(1, "Mathematics", "Dr. Ada Lovelace");
            string questionsFile = Path.Combine(FilesOperations.GetExamsFolder(), "math_questions.txt");
            QuestionList qList = new QuestionList(questionsFile);
            EnsureSampleQuestions(qList);

            // two exam objects with the same questions
            PracticeExam practiceExam = new PracticeExam(TimeSpan.FromMinutes(30), subject)
            {
                Questions = qList,
                NumberOfQuestions = qList.Count
            };

            FinalExam finalExam = new FinalExam(TimeSpan.FromMinutes(60), subject)
            {
                Questions = qList,
                NumberOfQuestions = qList.Count
            };

            // enroll students so notifications fire
            Student alice = new Student(1, "Alice", "alice@example.com");
            Student bob   = new Student(2, "Bob",   "bob@example.com");
            practiceExam.EnrollStudent(alice);
            practiceExam.EnrollStudent(bob);
            finalExam.EnrollStudent(alice);
            finalExam.EnrollStudent(bob);

            // let user choose exam type
            WriteLine("Select exam type to display:");
            WriteLine("1) Practice Exam");
            WriteLine("2) Final Exam");
            Write("Choice: ");
            char choice = ReadKey(true).KeyChar;
            WriteLine();

            switch (choice)
            {
                case '1':
                    practiceExam.ChangeMode(ExamMode.Starting);
                    practiceExam.ShowExam();
                    practiceExam.ChangeMode(ExamMode.Finished);
                    break;

                case '2':
                    finalExam.ChangeMode(ExamMode.Starting);
                    finalExam.ShowExam();
                    SimulateSubmissions(finalExam);
                    finalExam.ShowResults();
                    finalExam.ChangeMode(ExamMode.Finished);
                    break;

                default:
                    WriteLine("Invalid selection. Exiting.");
                    break;
            }

            WriteLine("\nPress any key to exit...");
            ReadKey();
        }

        private static void EnsureSampleQuestions(QuestionList qList)
        {
            if (qList.Count > 0)
                return;

            qList.Add(new TFQuestion("Is the earth round?", "Basic geography", 1, true));
            qList.Add(new ChooseOneQuestion(
                "What is 2+2?", "Simple arithmetic", 1,
                1,
                new List<string> { "3", "4", "5" }
            ));
            qList.Add(new ChooseAllQuestion(
                "Select the prime numbers.", "Number theory", 2,
                new List<int> { 1, 3 },
                new List<string> { "4", "3", "7", "8" }
            ));
        }

        private static void SimulateSubmissions(FinalExam exam)
        {
            var ids = new List<int>();
            foreach (var q in exam.Questions)
                if (q.QuestionId.HasValue)
                    ids.Add(q.QuestionId.Value);

            if (ids.Count < 3)
                return;

            AnswerSet set1 = new AnswerSet(1);
            set1.AddAnswer(new TFAnswer(ids[0], true));
            set1.AddAnswer(new ChooseOneAnswer(ids[1], 2));
            set1.AddAnswer(new ChooseAllAnswer(ids[2], new List<int> { 2, 3 }));
            exam.SubmitAnswers(set1);

            AnswerSet set2 = new AnswerSet(2);
            set2.AddAnswer(new TFAnswer(ids[0], false));
            set2.AddAnswer(new ChooseOneAnswer(ids[1], 2));
            set2.AddAnswer(new ChooseAllAnswer(ids[2], new List<int> { 2, 4 }));
            exam.SubmitAnswers(set2);
        }
    }
}
