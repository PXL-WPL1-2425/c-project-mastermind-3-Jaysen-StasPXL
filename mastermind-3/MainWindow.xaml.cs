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

     
