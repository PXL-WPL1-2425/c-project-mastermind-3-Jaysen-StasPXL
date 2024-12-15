using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Mastermind
{
    public partial class MainWindow : Window
    {
        // -------------------------------
        // 12. MASTERMINDSPEL: Extra spelers
        // -------------------------------
        private List<string> _players = new List<string>();
        private int _currentPlayerIndex = 0;
        private int _score = 100; // Begin score
        private const int _colorHintPenalty = 15;
        private const int _positionHintPenalty = 25;
        private string[] _generatedCode;

        public object ComboBox5 { get; }

        public MainWindow()
        {
            InitializeComponent();

            // Voeg kleuren toe aan ComboBoxes
            List<string> kleuren = new List<string> { "Red", "Blue", "Green", "Yellow", "Purple", "Orange" };
            ComboBox1.ItemsSource = kleuren;
            ComboBox2.ItemsSource = kleuren;
            ComboBox3.ItemsSource = kleuren;
            ComboBox4.ItemsSource = kleuren;

            // Voeg extra kleuren toe voor variaties
            ComboBox5.ItemsSource = kleuren;
            ComboBox6.ItemsSource = kleuren;
        }

        // -------------------------------
        // 13. MASTERMINDSPEL: Speleinde en volgende speler
        // -------------------------------
        private void EndGame()
        {
            string currentPlayer = _players[_currentPlayerIndex];
            string nextPlayer = _players[(_currentPlayerIndex + 1) % _players.Count];

            MessageBox.Show($"Speler {currentPlayer} heeft het spel beëindigd. Volgende speler: {nextPlayer}");
            _currentPlayerIndex = (_currentPlayerIndex + 1) % _players.Count; // Volgende speler
            UpdatePlayerDisplay();
        }

        // -------------------------------
        // 14. MASTERMINDSPEL: Huidige speler
        // -------------------------------
        private void UpdatePlayerDisplay()
        {
            // Toon de naam van de huidige speler in de titel van het venster
            Title = $"Mastermind - Huidige Speler: {_players[_currentPlayerIndex]}";
            CurrentPlayerLabel.Content = $"Huidige Speler: {_players[_currentPlayerIndex]} - Score: {_score}";
        }

        // -------------------------------
        // 15. MASTERMINDSPEL: Hint kopen
        // -------------------------------
        private void BuyHint()
        {
            MessageBoxResult result = MessageBox.Show("Wil je een hint kopen? (15 strafpunten voor kleur, 25 voor kleur op juiste plaats)", "Koop Hint", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                // Vraag de speler welk type hint hij wil kopen
                string hintChoice = Microsoft.VisualBasic.Interaction.InputBox("Welke hint wil je kopen? Kies 'kleur' of 'positie'.", "Hint Keuze");

                if (hintChoice.ToLower() == "kleur" && _score >= _colorHintPenalty)
                {
                    _score -= _colorHintPenalty;
                    MessageBox.Show("Je hebt 15 strafpunten betaald voor een kleur hint.");
                }
                else if (hintChoice.ToLower() == "positie" && _score >= _positionHintPenalty)
                {
                    _score -= _positionHintPenalty;
                    MessageBox.Show("Je hebt 25 strafpunten betaald voor een positie hint.");
                }
                else
                {
                    MessageBox.Show("Je hebt niet genoeg punten voor deze hint.");
                }
                UpdatePlayerDisplay();  // Score updaten na hint aankoop
            }
        }

        // -------------------------------
        // 16. MASTERMINDSPEL: Tooltip voor feedback
        // -------------------------------
        private void CheckCode(object sender, RoutedEventArgs e)
        {
            var guesses = new[] {
                ComboBox1.SelectedItem as string,
                ComboBox2.SelectedItem as string,
                ComboBox3.SelectedItem as string,
                ComboBox4.SelectedItem as string
            };

            var labels = new[] { Label1, Label2, Label3, Label4 };

            // Loop door elke ComboBox en vergelijk de keuzes met de gegenereerde code
            for (int i = 0; i < 4; i++)
            {
                if (guesses[i] == null) continue;  // Sla over als geen kleur geselecteerd is

                // Controleer of de gok correct is en handel de randkleur af
                if (guesses[i] == _generatedCode[i])
                {
                    labels[i].BorderBrush = Brushes.DarkRed;  // Correcte kleur en positie
                    labels[i].BorderThickness = new Thickness(2);  // Stel randdikte in
                    labels[i].ToolTip = "Juiste kleur, juiste positie";  // Tooltip voor juiste positie
                }
                else if (_generatedCode.Contains(guesses[i]))
                {
                    labels[i].BorderBrush = Brushes.Wheat;  // Correcte kleur, verkeerde positie
                    labels[i].BorderThickness = new Thickness(2);  // Stel randdikte in
                    labels[i].ToolTip = "Juiste kleur, foute positie";  // Tooltip voor verkeerde positie
                }
                else
                {
                    labels[i].BorderBrush = Brushes.Black;  // Onjuiste kleur
                    labels[i].BorderThickness = new Thickness(1);  // Stel randdikte in
                    labels[i].ToolTip = "Foute kleur";  // Tooltip voor foute kleur
                }
            }
        }

        // -------------------------------
        // Extra-01: Meer kleuren
        // -------------------------------
        private void SetUpGameColors(int numColors)
        {
            List<string> availableColors = new List<string> { "Red", "Blue", "Green", "Yellow", "Purple", "Orange" };
            _generatedCode = new string[numColors];

            // Vul de gegenereerde code met willekeurige kleuren
            Random rand = new Random();
            for (int i = 0; i < numColors; i++)
            {
                _generatedCode[i] = availableColors[rand.Next(availableColors.Count)];
            }

            // Pas de ComboBoxes aan op basis van het aantal kleuren
            if (numColors == 4)
            {
                ComboBox5.Visibility = Visibility.Hidden;
                ComboBox6.Visibility = Visibility.Hidden;
            }
            else if (numColors == 5)
            {
                ComboBox5.Visibility = Visibility.Visible;
                ComboBox6.Visibility = Visibility.Hidden;
            }
            else
            {
                ComboBox5.Visibility = Visibility.Visible;
                ComboBox6.Visibility = Visibility.Visible;
            }
        }

        // -------------------------------
        // Start Game en spelers toevoegen
        // -------------------------------
        private void StartGame(object sender, RoutedEventArgs e)
        {
            // Voeg nieuwe spelers toe
            do
            {
                string playerName = Microsoft.VisualBasic.Interaction.InputBox("Voer de naam van de speler in:", "Speler Toevoegen", "");
                if (!string.IsNullOrEmpty(playerName))
                {
                    _players.Add(playerName);
                }
            } while (MessageBox.Show("Wil je een andere speler toevoegen?", "Nieuwe Speler?", MessageBoxButton.YesNo) == MessageBoxResult.Yes);

            // Stel de eerste speler in
            _currentPlayerIndex = 0;
            UpdatePlayerDisplay();

            // Vraag om het aantal kleuren
            string colorChoice = Microsoft.VisualBasic.Interaction.InputBox("Kies het aantal kleuren (4, 5, of 6):", "Aantal Kleuren");
            int numColors;
            if (int.TryParse(colorChoice, out numColors) && (numColors == 4 || numColors == 5 || numColors == 6))
            {
                SetUpGameColors(numColors);
            }
            else
            {
                MessageBox.Show("Ongeldige keuze. Het spel wordt gestart met 4 kleuren.");
                SetUpGameColors(4); // Standaard naar 4 kleuren als de keuze niet geldig is
            }
        }
    }
}
