using Lab_CSharp.Helpers;
using Lab_CSharp.Models;
using System;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Lab_CSharp.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private string _firstName;
        private string _lastName;
        private string _email;
        private DateTime? _birthDate;
        private string _resultText;
        private bool _isProcessing;

        public string FirstName
        {
            get => _firstName;
            set { _firstName = value; OnPropertyChanged(nameof(FirstName)); OnPropertyChanged(nameof(CanProceed)); }
        }

        public string LastName
        {
            get => _lastName;
            set { _lastName = value; OnPropertyChanged(nameof(LastName)); OnPropertyChanged(nameof(CanProceed)); }
        }

        public string Email
        {
            get => _email;
            set { _email = value; OnPropertyChanged(nameof(Email)); OnPropertyChanged(nameof(CanProceed)); }
        }

        public DateTime? BirthDate
        {
            get => _birthDate;
            set { _birthDate = value; OnPropertyChanged(nameof(BirthDate)); OnPropertyChanged(nameof(CanProceed)); }
        }

        public string ResultText
        {
            get => _resultText;
            set { _resultText = value; OnPropertyChanged(nameof(ResultText)); }
        }

        public bool IsProcessing
        {
            get => _isProcessing;
            set { _isProcessing = value; OnPropertyChanged(nameof(IsProcessing)); }
        }

        public bool CanProceed =>
            !string.IsNullOrWhiteSpace(FirstName) &&
            !string.IsNullOrWhiteSpace(LastName) &&
            !string.IsNullOrWhiteSpace(Email) &&
            BirthDate.HasValue;

        public ICommand ProceedCommand { get; }

        public MainViewModel()
        {
            ProceedCommand = new RelayCommand(async obj => await ExecuteProceedAsync(), obj => CanProceed);
        }

        private async Task ExecuteProceedAsync()
        {
            IsProcessing = true;

            // await Task.Delay(1000);
            if (!BirthDate.HasValue) return;

            try
            {
                var person = new Person(FirstName, LastName, Email, BirthDate.Value);
                int age = person.CalculateAge(BirthDate.Value);

                ResultText = $"Name: {person.FirstName}\n" +
                             $"Surname: {person.LastName}\n" +
                             $"Email: {person.Email}\n" +
                             $"Birth date: {person.BirthDate.ToShortDateString()}\n" +
                             $"Age: {age} years old\n" +
                             $"is Adult: {person.IsAdult}\n" +
                             $"Western zodiac: {person.SunSign}\n" +
                             $"Chineese zodiac: {person.ChineseSign}\n" +
                             $"Has birthday today: {person.IsBirthday}";


                if (person.IsBirthday)
                {
                    MessageBox.Show($"Happy birthday, {FirstName}! 🎉", "Congrats!", MessageBoxButton.OK, MessageBoxImage.Information);
                }

            }
            catch (FutureBirthDateException ex)
            {
                MessageBox.Show("You haven't been born yet!", "Error!!!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (TooOldPersonException ex)
            {
                MessageBox.Show("You are dead!", "Error!!!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (InvalidEmailException ex)
            {
                ResultText = "Error: Email is not valid!";
                IsProcessing = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"САЗ умер: {ex.Message}", "504 gateway timeout", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        

        IsProcessing = false;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}


