# Examination System

This repository contains a C# console application designed to manage and execute examinations with various question types, answer handling, and exam behaviors. The system is structured into a hierarchy of classes representing questions, answers, exams, subjects, and students.

## 📦 Project Structure

- **Classes/**: Contains all domain models and supporting classes, such as questions, answers, exams, and related utilities.
- **Exams/**: Stores text files with question definitions (e.g., `math_questions.txt`).
- **Students/**: Intended for storing student-related data (currently empty).
- **Answers/**: May hold answer-related files or data.
- **Program.cs**: Entry point for running the application.
- **`ExaminationSystem.csproj`**: Project file for building the solution.

## 🧱 Key Design Components

### Question Hierarchy

- **Base `Question` class**: Contains common properties (`Header`, `Body`, `Marks`) and is `ICloneable`, `IComparable`.
- **Derived types**:
  - `TrueFalseQuestion`
  - `ChooseOneQuestion`
  - `ChooseAllQuestion`

Each derived class implements its specific representation and overrides methods like `ToString`, `Equals`, `GetHashCode`, and implements constructor chaining.

### Question List

- Inherits from `List<Question>` and overrides `Add` to keep default behavior while logging every added question to a file specific to that list. Uses `TextWriter`/`TextReader` for file operations.

### Answer Handling

- **`Answer` class**: Represents a single answer option.
- **`AnswerList` class**: Inherits from `List<Answer>` and likely overrides `Add` for logging or validation.
- Each `Question` has an associated `AnswerList`.

### Exam Hierarchy

- **Base `Exam` class**: Common attributes include `Time`, `NumberOfQuestions`, a `QuestionAnswer` dictionary for correction, and `Subject` association. Implements exam modes (`Starting`, `Queued`, `Finished`) and event/delegate infrastructure to notify students when an exam starts.
- **Derived exams**:
  - `PracticeExam`: Displays correct answers after completion.
  - `FinalExam`: Only shows questions and answers.

Exams implement interfaces (`ICloneable`, `IComparable`) and override essential methods. Constructors use chaining.

### Subject & Student

- **`Subject` class**: Represents a subject with desired properties (e.g., name, code).
- **`Student` class**: Represents a student; used for notifications via events when exams start.

## ⚒ Features and Implementation Notes

- **Generic constraints**: The design considers appropriate constraints for generic classes where applicable (e.g., `QuestionList<T> where T : Question`).
- **Event-based notifications**: When an exam enters `Starting` mode, associated students are notified using events/delegates.
- **Mode management**: Exams transition through modes and trigger related behaviors.

## 🧠 Usage

In `Program.cs`, the `Main` method prompts the user to select between a practice exam and a final exam. It then creates two objects—one `PracticeExam` and one `FinalExam`—and displays the exam based on the user's choice.

## ✔️ Conformance to Requirements

- Implemented `ICloneable`, `IComparable`, overridden `ToString`, `Equals`, and `GetHashCode` across classes.
- Constructors use chaining.
- Custom question and answer lists with file logging.
- Exam modes with notifications to students.
- Association between exams, subjects, and questions/answers.

## 📁 Files of Interest

- `Classes/Question.cs` (and derived types)
- `Classes/QuestionList.cs`
- `Classes/Answer.cs` and `Classes/AnswerList.cs`
- `Classes/Exam.cs`, `Classes/PracticeExam.cs`, `Classes/FinalExam.cs`
- `Classes/Subject.cs`, `Classes/Student.cs`
- `Classes/FilesOperations.cs` (helps with logging)

## 📝 Additional Notes

Feel free to extend the types and add new question or exam behaviors. Logging behavior and file paths for question lists can be adjusted per `QuestionList` instance. The system is built with extensibility and maintainability in mind.

---

_Last updated: March 5, 2026_
