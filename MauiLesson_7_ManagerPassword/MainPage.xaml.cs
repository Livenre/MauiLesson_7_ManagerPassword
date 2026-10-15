using System.Collections.ObjectModel;
using System.Text;

namespace MauiLesson_7_ManagerPassword
{
    public partial class MainPage : ContentPage
    {
        public ObservableCollection<PasswordModel> SavedPasswords { get; set; } = new();
        private int _passwordComplexity = 2;

        public MainPage()
        {
            InitializeComponent();
            PasswordsList.ItemsSource = SavedPasswords;
        }

        private void OnToggleMainPasswordClicked(object sender, EventArgs e)
        {
            PasswordEntry.IsPassword = !PasswordEntry.IsPassword;
            MainEyeButton.Text = PasswordEntry.IsPassword ? "◡" : "👁";
        }

        private void OnToggleItemPasswordClicked(object sender, EventArgs e)
        {
            if (sender is Button btn && btn.CommandParameter is PasswordModel model)
            {
                model.IsVisible = !model.IsVisible;
            }
        }

        private void OnComplexitySelected(object sender, EventArgs e)
        {
            if (sender is Button btn && int.TryParse(btn.CommandParameter.ToString(), out int level))
            {
                _passwordComplexity = level;

                BtnSimple.BackgroundColor = Color.FromArgb("#2C2C2C");
                BtnMedium.BackgroundColor = Color.FromArgb("#2C2C2C");
                BtnHard.BackgroundColor = Color.FromArgb("#2C2C2C");

                btn.BackgroundColor = Color.FromArgb("#4CAF50");
            }
        }

        private void OnGeneratePasswordClicked(object sender, EventArgs e)
        {
            string lowercase = "abcdefghijklmnopqrstuvwxyz";
            string uppercase = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string numbers = "0123456789";
            string symbols = "!@#$%^&*()_-+=";

            string pool = lowercase;
            int length = 8;

            if (_passwordComplexity >= 2)
            {
                pool += uppercase + numbers;
                length = 12;
            }
            if (_passwordComplexity == 3)
            {
                pool += symbols;
                length = 16;
            }

            var rnd = new Random();
            var pwd = new StringBuilder();

            for (int i = 0; i < length; i++)
            {
                pwd.Append(pool[rnd.Next(pool.Length)]);
            }

            PasswordEntry.Text = pwd.ToString();

            PasswordEntry.IsPassword = false;
            MainEyeButton.Text = "👁";
        }

        private void OnSaveClicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TitleEntry.Text) ||
                string.IsNullOrWhiteSpace(WebsiteEntry.Text) ||
                string.IsNullOrWhiteSpace(PasswordEntry.Text))
            {
                DisplayAlert("Помилка", "Будь ласка, заповніть всі поля", "OK");
                return;
            }

            string selectedCategory = CategoryPicker.SelectedItem?.ToString() ?? "Інше";

            SavedPasswords.Add(new PasswordModel
            {
                Title = TitleEntry.Text,
                Website = WebsiteEntry.Text,
                Password = PasswordEntry.Text,
                Category = selectedCategory,
                IsVisible = false
            });

            TitleEntry.Text = string.Empty;
            WebsiteEntry.Text = string.Empty;
            PasswordEntry.Text = string.Empty;
            CategoryPicker.SelectedItem = null;

            PasswordEntry.IsPassword = true;
            MainEyeButton.Text = "◡";
        }
    }
}