using System;
using System.Collections.Generic;
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
