using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRG182_Project
{
    //Programming 182 Group Assignment -------------- Credit ---------------> Anele Gumede(601941), Daluvuyo Magagane(601821), Denzel Muranganwa(602217), Regoratile Mokoene(602012)

    //Applicant class
    class Applicant
    {
        public string name;
        public string surname;
        public int age;
        public int highscore_rank;
        public DateTime starting_date;
        public int number_of_pizzas;
        public int bowling_highscore;
        public bool employed;
        public string slush_flavour;
        public int number_of_slush;
    }

    //Program class
    class Program
    {
        //Variables
        static Applicant[] applicant_list;
        static Applicant[] token_applicant_list;
        static List<Applicant> list_to_array = new List<Applicant>();
        static List<Applicant> token_list_to_array = new List<Applicant>();
        static bool data_loaded = false;

        //Enumerators
        //Menu Enumerator
        public enum Menu
        {
            Register_User = 1,
            Token_Qualification,
            AveragePizzas,
            Show_Stats,
            Exit
        }


        //method that gets filepath of the current pc
        static string GetFilePath()
        {
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            return Path.Combine(baseDirectory, "data_file.txt");
        }

        // method checks existence of storage file, if not then it creates a file
        static void EnsureFileExists()
        {
            string textfilepath = GetFilePath();
            if (!File.Exists(textfilepath))
            {
                File.Create(textfilepath).Close();
            }
        }
        //Functions
        static void save_data()
        {
            string textfilepath = GetFilePath();
            EnsureFileExists(); // Ensure the file exists before writing to it
            List<string> applicant_string_list = new List<string>();
            string[] applicant_string_array = { };

            foreach (Applicant applicant in applicant_list)
            {
                string array_text = applicant.name + "," + applicant.surname + "," + applicant.age + "," + applicant.highscore_rank + "," + applicant.starting_date + "," + applicant.number_of_pizzas + "," + applicant.bowling_highscore + "," + applicant.employed + "," + applicant.slush_flavour + "," + applicant.number_of_slush;
                applicant_string_list.Add(array_text);
            }

            applicant_string_array = applicant_string_list.ToArray();

            //Write into file
            File.WriteAllLines(textfilepath, applicant_string_array);
        }

        static void load_data()
        {
            string textfilepath = GetFilePath();
            EnsureFileExists(); // Ensure the file exists before writing to it
            if (!data_loaded)
            {
                foreach (string person in File.ReadAllLines(textfilepath))
                {
                    string[] applicant_data = person.Split(',');
                    //Recreate applicant object
                    Applicant applicant = new Applicant();
                    applicant.name = applicant_data[0];
                    applicant.surname = applicant_data[1];
                    applicant.age = int.Parse(applicant_data[2]);
                    applicant.highscore_rank = int.Parse(applicant_data[3]);
                    applicant.starting_date = DateTime.Parse(applicant_data[4]);
                    applicant.number_of_pizzas = int.Parse(applicant_data[5]);
                    applicant.bowling_highscore = int.Parse(applicant_data[6]);
                    if (applicant_data[7] == "True") applicant.employed = true;
                    else applicant.employed = false;
                    applicant.slush_flavour = applicant_data[8];
                    applicant.number_of_slush = int.Parse(applicant_data[9]);

                    list_to_array.Add(applicant);
                }

                applicant_list = list_to_array.ToArray();
                Console.WriteLine("Data Loaded....\n");
                data_loaded = true;
            }
        }


        static void register_user()
        {
            //Section Title
            Console.Clear();
            Console.WriteLine("-------- Applicant Registration ---------");

            //Create an applicant object
            Applicant applicant = new Applicant();

            //Enter applicants information
            //Get applicants name 
            Console.WriteLine("What is the applicant name: ");
            applicant.name = Console.ReadLine().ToLower();

            //Get applicants surname
            Console.WriteLine("What is the applicant surname: ");
            applicant.surname = Console.ReadLine().ToLower();

            //Get applicants age
            Console.WriteLine("How old is the applicant: ");
            applicant.age = int.Parse(Console.ReadLine());

            //Get applicants highscore rank
            Console.WriteLine("Applicants highscore rank: ");
            applicant.highscore_rank = int.Parse(Console.ReadLine());

            //Get applicants starting date
            Console.WriteLine("Applicants starting date in format yyyy-mm-dd: ");
            applicant.starting_date = DateTime.Parse(Console.ReadLine());

            //Get applicants pizza count
            Console.WriteLine("Number of pizzas eaten: ");
            applicant.number_of_pizzas = int.Parse(Console.ReadLine());

            //Get applicants bowling score
            Console.WriteLine("Bowling Highscore: ");
            applicant.bowling_highscore = int.Parse(Console.ReadLine());

            //Get applicants employment status
            Console.WriteLine("Employmed? yes/no: ");
            var line = Console.ReadLine().ToLower();
            if (line == "yes") applicant.employed = true;
            else applicant.employed = false;

            //Get applicant slushy flavour
            Console.WriteLine("Favourite slushy flavour number: 1.Gooey Gulp Galore 2.Strawberry Bliss 3.Orange Mango Tango: ");
            int flavour = int.Parse(Console.ReadLine());
            switch (flavour)
            {
                case 1:
                    applicant.slush_flavour = "Gooey Gulp Galore".ToLower();
                    break;
                case 2:
                    applicant.slush_flavour = "Strawberry Bliss".ToLower();
                    break;
                case 3:
                    applicant.slush_flavour = "Orange Mango Tango".ToLower();
                    break;
            }

            //Get applicant number of slush
            Console.WriteLine("Number of slushies consumed: ");
            applicant.number_of_slush = int.Parse(Console.ReadLine());

            //Add applicant to applicant list
            list_to_array.Add(applicant);
            applicant_list = list_to_array.ToArray();
            Console.WriteLine(" ");
            //Output Message
            Console.WriteLine($"Applicant {applicant.name + " " + applicant.surname} has been entered.");
            Console.WriteLine( " ");
        }


        static void token_qualification()
        {
            //Variables
            int token_applicants = 0;

            foreach (var applicant in applicant_list)
            {
                //Variables
                bool qualifies = true;

                //Clear arrays and lists > prevents duplicate information
                token_list_to_array.Clear();
                token_applicant_list = null;

                if (applicant.age < 18 && applicant.employed == false)
                {
                    qualifies = false;
                }
                else if ((DateTime.Now.Year - applicant.starting_date.Year) < 2)
                {
                    qualifies = false;
                }
                else if (applicant.highscore_rank <= 2000 || applicant.bowling_highscore <= 1500 || ((applicant.highscore_rank + applicant.bowling_highscore) / 2) <= 1500)
                {
                    qualifies = false;
                }
                else if (applicant.number_of_pizzas / ((DateTime.Now - applicant.starting_date).Days / 30) < 3)
                {
                    qualifies = false;
                }
                else if (applicant.number_of_slush / ((DateTime.Now - applicant.starting_date).Days / 30) <= 4 || applicant.slush_flavour == "gooey gulp galore")
                {
                    qualifies = false;
                }

                if (qualifies)
                {
                    token_list_to_array.Add(applicant);
                    token_applicants += 1;
                    token_applicant_list = token_list_to_array.ToArray();
                }
                Console.WriteLine(" ");
            }

            //Output message
            Console.Clear();
            Console.WriteLine("-------- Token Verification ---------");
            Console.WriteLine($"{token_applicants} applicants qualify...");

        }
        //method that retrieves the lowest and highest age of all applicants in the collection
        static void show_stats()
        {
            Console.Clear();
            Console.WriteLine("-------- Statistics ---------");
            if (token_list_to_array != null)
            {
                Console.WriteLine($"Number of qualifiers: {token_list_to_array.Count}");
                Console.WriteLine($"Number of non-qualifiers: {list_to_array.Count - token_list_to_array.Count}");
            }
            else
            {
                Console.WriteLine("No data...");
            }

            //Get Advanced Stats
            int low_age = 250, high_age = 0;
            float employed = 0;
            int highest_score = 0, highest_score_bowling = 0;
            int total_slushies = 0, total_pizza = 0;

            //Get Stats
            string textfilepath = GetFilePath();
            EnsureFileExists(); // Ensure the file exists before writing to it
            foreach (string person in File.ReadAllLines(textfilepath))
            {
                string[] applicant_data = person.Split(',');
                if (applicant_data[7] == "True")
                {
                    employed++;
                }
            }

            foreach (Applicant applicant in applicant_list)
            {
                if (applicant.age > high_age)
                {
                    high_age = applicant.age;
                }
                if (applicant.age < low_age)
                {
                    low_age = applicant.age;
                }
                if (applicant.highscore_rank > highest_score)
                {
                    highest_score = applicant.highscore_rank;
                }
                if (applicant.bowling_highscore > highest_score_bowling)
                {
                    highest_score_bowling = applicant.bowling_highscore;
                }

                total_slushies += applicant.number_of_slush;
                total_pizza += applicant.number_of_pizzas;
            }

            //Ouput Stats
            Console.WriteLine($"Highest Age: {high_age}");
            Console.WriteLine($"Lowest Age: {low_age}");
            Console.WriteLine($"Highest Score: {highest_score}");
            Console.WriteLine($"Highest Bowling Score: {highest_score_bowling}");
            Console.WriteLine($"Percent employed: {((employed / (float)list_to_array.Count) * 100)}%");
            Console.WriteLine($"Total pizzas eaten: {total_pizza}");
            Console.WriteLine($"Total slushies consumed: {total_slushies}");
            Console.WriteLine("  ");

        }
        //f animation/loading effects
        static void Animation()
        {
            for (int i = 0; i < 3; i++)
            {
                Console.Write("*=*Vuyo*=*Anele*=*Rego*=*Denzel");
                System.Threading.Thread.Sleep(500); // Wait for half a second
            }
            Console.WriteLine();
        }

        //Calculates the total  average pizzas 
        static double CalculateAveragePizzas()
        {
            if (applicant_list == null || applicant_list.Length == 0)
                return 0;

            double totalPizzas = 0;
            int count = applicant_list.Length;

            foreach (var applicant in applicant_list)
            {
                totalPizzas += applicant.number_of_pizzas;
            }

            return totalPizzas / count;
        }

        static void Main(string[] args)
        {
            //Menu
            bool exit = false;

            while (exit != true)
            {
                //Output menu
                Console.Clear();
                Console.WriteLine("-------- Retro Slice Token System ---------");
                Console.WriteLine($"Current Date: {DateTime.Now.Day}/{DateTime.Now.Month}/{DateTime.Now.Year}");
                Console.WriteLine($"Current Time: {DateTime.Now.TimeOfDay}");
                Console.WriteLine("\nProgrammers:\nAnele Gumede(601941)\nDaluvuyo Magagane(601821)\nDenzel Muranganwa(602217)\nRegoratile Mokoene(602012)\n");
                Console.WriteLine("-------- Menu Options ---------");

                //Load Data
                load_data();

                //Output menu options
                foreach (Menu menu in Enum.GetValues(typeof(Menu)))
                {
                    Console.WriteLine($"To {menu}, press {(int)menu}");
                }

                //Capture choice
                int choice = int.Parse(Console.ReadLine());

                //Switch cases for menu
                switch (choice)
                {
                    //Register user
                    case 1:
                        register_user();
                        Animation();// the animation threading
                        Console.WriteLine(" ");
                        Console.WriteLine("Press any key to continue...");         
                        break;

                    //Token qualification
                    case 2:
                        token_qualification();
                        Animation();// the animation threading
                        Console.WriteLine(" ");
                        Console.WriteLine("Press any key to continue...");  
                        break;

                    case 3:
                        CalculateAveragePizzas();
                        Animation();// the animation threading
                        Console.WriteLine(" ");
                        Console.WriteLine("Press any key to continue...");  
                        break;
                    //Show stats
                    case 4:
                        show_stats();
                        Animation();// the animation threading
                        Console.WriteLine(" ");
                        Console.WriteLine("Press any key to continue...");  
                        break;

                    //Exit
                    case 5:
                        Console.Clear();
                        save_data();
                        Console.WriteLine("All applicant data saved, program is closing...");
                        Console.WriteLine("Press any key to continue...");
                        exit = true;
                        break;

                    //Test (Hidden Option)
                    case 7:
                        load_data();
                        break;
                }

                //Prevent application from closing
                Console.ReadLine();

            }
        }
    }
}
