using StudentDB.model.Tables.dbo.Students;

namespace StudentDB
{
    public class Program
    {
        public static StudentCRUD studentDB;
        public static List<Student> students;
        public static void Main()
        {
            studentDB = new StudentCRUD();
            students = studentDB.students.ToList();
            string strInput;
            int numInput = -1;

            Console.WriteLine("Welcome!");

            while (true)
            {
                PrintAllStudents();
                strInput = GetUserInput("Please enter a student ID or a student's name to select a student \n" +
                    "or \"all\" to find all student info. You can also add a student by entering \n" +
                    "\"add\" or remove one by typing \"remove.\"");

                if (students.Exists(stu => stu.StudentId.ToString() == strInput))
                {
                    numInput = students.FindIndex(stu => stu.StudentId.ToString() == strInput);
                    break;
                }
                else if (students.Exists(stu => stu.Name.ToLower() == strInput))
                {
                    numInput = students.FindIndex(student => student.Name.ToLower() == strInput);
                    break;
                }
                else if (strInput == "all") 
                {
                    Console.WriteLine("Here's all student info.");
                    for (int i = 0; i < students.Count; i++)
                    {
                        Console.WriteLine(students[i].Name + " has student ID: " + students[i].StudentId + " is from "
                            + students[i].Hometown + " and their favorite food is " + students[i].FavFood + ".");
                    }

                    OneMo("Do you want to run the program again? y/n");
                    return;
                }
                else if (strInput == "add")
                {
                    string name = GetUserInput("Please enter the student's name."); 
                    string homeTown = GetUserInput("Please enter their hometown.");
                    string favFood = GetUserInput("Please enter their favorite food"); 
                    studentDB.Add(name, homeTown, favFood);

                    studentDB = new StudentCRUD();
                    students = studentDB.students.ToList();

                    OneMo("Do you want to run the program again? y/n");
                    return; 
                }
                else if (strInput == "remove")
                {
                    while (true)
                    {
                        PrintAllStudents();

                        strInput = GetUserInput("Please enter the index of the student you'd like to remove.");

                        if (students.Exists(stu => stu.StudentId.ToString() == strInput))
                        {
                            numInput = students.FindIndex(student => student.StudentId.ToString() == strInput);
                            studentDB.Remove(numInput);

                            break;
                        }

                        Console.WriteLine("That was not a correct value. Let's try again.");
                    }

                    OneMo("Do you want to run the program again? y/n");
                    return;
                }

                Console.WriteLine("Please enter a correct response.");
            }

            do 
            {
                strInput = GetUserInput("Enter \"hometown\" to see their hometown or \"favorite food\" for their favorite food.");

                if (strInput.Contains("food") || strInput.Contains("fav"))
                {
                    Console.WriteLine(students[numInput].FavFood + " is " + students[numInput].Name + "'s favorite food.");
                    break;
                }
                else if (strInput.Contains("home") || strInput.Contains("town")) 
                {
                    Console.WriteLine(students[numInput].Hometown + " is " + students[numInput].Name + "'s hometown.");
                    break;
                }
                else
                {
                    Console.WriteLine("That was not a valid input. Let's try again.");
                }
            } while (true);


            OneMo("Do you want to run the program again? y/n");
        }

        public static void OneMo(string prompt)
        {
            Console.WriteLine(prompt);
            string input = Console.ReadLine().ToLower().Trim();

            if (input == "y" || input == "yes")
            {
                Main();
            }
            else if (input == "n" || input == "no")
            {
                Console.WriteLine("Goodbye!");
                return;
            }
            else
            {
                Console.WriteLine("Sorry I didn't catch taht. Let's try again.");
                OneMo(prompt);
            }
        }

        public static string GetUserInput(string prompt)
        {
            Console.WriteLine(prompt);

            string response = Console.ReadLine().ToLower().Trim();

            if (response == "")
            {
                Console.WriteLine("Please select an option.");
                return GetUserInput(prompt);
            }

            return response;
        }

        public static void PrintAllStudents()
        {
            Console.WriteLine("Here is the list of the students with their student ID.");
            for (int i = 0; i < students.Count; i++)
                Console.WriteLine(students[i].Name + " is student id number " + students[i].StudentId + ".");
        }
    }
}