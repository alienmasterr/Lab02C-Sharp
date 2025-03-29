using System;

namespace Lab_CSharp.Models
{
    public class Person
    {
        public string FirstName { get; }
        public string LastName { get; }
        public string Email { get; }
        public DateTime BirthDate { get; }

        public bool IsAdult => (DateTime.Now - BirthDate).TotalDays / 365.25 >= 18;
        public bool IsBirthday => BirthDate.Day == DateTime.Now.Day && BirthDate.Month == DateTime.Now.Month;
        public string SunSign => GetSunSign();
        public string ChineseSign => GetChineseSign();

        public Person(string firstName, string lastName, string email, DateTime birthDate)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            BirthDate = birthDate;
        }

        public Person(string firstName, string lastName, string email)
            : this(firstName, lastName, email, DateTime.MinValue) { }

        public Person(string firstName, string lastName, DateTime birthDate)
            : this(firstName, lastName, null, birthDate) { }

        private string GetSunSign()
        {
            int day = BirthDate.Day;
            int month = BirthDate.Month;
            return month switch
            {
                1 => day <= 19 ? "Capricorn" : "Aquarius",
                2 => day <= 18 ? "Aquarius" : "Pisces",
                3 => day <= 20 ? "Pisces" : "Aries",
                4 => day <= 19 ? "Aries" : "Taurus",
                5 => day <= 20 ? "Taurus" : "Gemini",
                6 => day <= 20 ? "Gemini" : "Cancer",
                7 => day <= 22 ? "Cancer" : "Leo",
                8 => day <= 22 ? "Leo" : "Virgo",
                9 => day <= 22 ? "Virgo" : "Libra",
                10 => day <= 22 ? "Libra" : "Scorpius",
                11 => day <= 21 ? "Scorpius" : "Sagittarius",
                12 => day <= 21 ? "Sagittarius" : "Capricorn",
                _ => "Unkown"
            };
        }

        private string GetChineseSign()
        {
            string[] signs = { "Monkey", "Rooster", "Dog", "Pig", "Rat", "Ox", "Tiger", "Rabbit", "Dragon", "Snake", "Horse", "Goat" };

            return signs[BirthDate.Year % 12];
        }


        public int CalculateAge(DateTime birthDate)
        {
            DateTime today = DateTime.Today;
            int age = today.Year - birthDate.Year;
            if (birthDate > today.AddYears(-age)) age--; // Якщо день народження ще не настав цього року
            return age;
        }
    }
}


