using System;
using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Media;
using Avalonia.Threading;

namespace _3a;

public partial class MainWindow : Window
{
    // ── State ──────────────────────────────────────────────────────────────
    private enum Phase { Work, Break }

    private Phase   _currentPhase   = Phase.Work;
    private bool    _isRunning      = false;
    private int     _workMinutes    = 25;
    private int     _breakMinutes   = 5;
    private int     _totalSeconds;          // total seconds in current phase
    private int     _remainingSeconds;      // seconds still left
    private int     _completedCycles = 0;
    private readonly List<string> _history = new();

    private readonly DispatcherTimer _timer;

    // Colours
    private static readonly IBrush WorkAccent  = SolidColorBrush.Parse("#89B4FA");
    private static readonly IBrush BreakAccent = SolidColorBrush.Parse("#A6E3A1");

    // ── Constructor ────────────────────────────────────────────────────────
    public MainWindow()
    {
        InitializeComponent();

        _timer = new DispatcherTimer
        {
            Interval = System.TimeSpan.FromSeconds(1)
        };
        _timer.Tick += OnTimerTick;

        ResetToPhase(Phase.Work, restart: false);
    }

    // ── Timer tick ─────────────────────────────────────────────────────────
    private void OnTimerTick(object? sender, System.EventArgs e)
    {
        if (_remainingSeconds <= 0)
        {
            // Phase just finished
            _timer.Stop();
            _isRunning = false;

            if (_currentPhase == Phase.Work)
            {
                _completedCycles++;
                CycleDisplay.Text = _completedCycles.ToString();
                _history.Add($"{DateTime.Now:HH:mm} — cykl {_completedCycles}");
                HistoryList.ItemsSource = _history.ToArray();
                // Automatically start the break
                ResetToPhase(Phase.Break, restart: true);
            }
            else
            {
                // Break finished — go back to work, but wait for user to start
                ResetToPhase(Phase.Work, restart: false);
            }
            return;
        }

        _remainingSeconds--;
        UpdateDisplay();
    }

    // ── Button handlers ────────────────────────────────────────────────────
    private void OnStartPauseClicked(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        if (_isRunning)
        {
            _timer.Stop();
            _isRunning = false;
            StartPauseButton.Content = "Start";
        }
        else
        {
            _timer.Start();
            _isRunning = true;
            StartPauseButton.Content = "Pauza";
        }
    }

    private void OnResetClicked(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        _timer.Stop();
        _isRunning = false;
        _completedCycles = 0;
        CycleDisplay.Text = "0";
        ResetToPhase(Phase.Work, restart: false);
    }

    // ── Settings handlers ──────────────────────────────────────────────────
    private void OnWorkMinutesChanged(object? sender, NumericUpDownValueChangedEventArgs e)
    {
        int value = (int)(e.NewValue ?? 25);
        if (value < 1) value = 1;
        _workMinutes = value;

        // If currently showing a work phase that hasn't started, update display
        if (_currentPhase == Phase.Work && !_isRunning)
            ResetToPhase(Phase.Work, restart: false);
    }

    private void OnBreakMinutesChanged(object? sender, NumericUpDownValueChangedEventArgs e)
    {
        int value = (int)(e.NewValue ?? 5);
        if (value < 1) value = 1;
        _breakMinutes = value;

        // If currently showing a break phase that hasn't started, update display
        if (_currentPhase == Phase.Break && !_isRunning)
            ResetToPhase(Phase.Break, restart: false);
    }

    // ── Internal helpers ───────────────────────────────────────────────────
    /// <summary>
    /// Sets up the UI for the given phase.
    /// When <paramref name="restart"/> is true the timer starts immediately.
    /// </summary>
    private void ResetToPhase(Phase phase, bool restart)
    {
        _currentPhase = phase;

        if (phase == Phase.Work)
        {
            _totalSeconds     = _workMinutes * 60;
            PhaseLabel.Text   = "Praca";
            PhaseLabel.Foreground = WorkAccent;
            TimerDisplay.Foreground = WorkAccent;
            ProgressBar.Foreground  = WorkAccent;
        }
        else
        {
            _totalSeconds     = _breakMinutes * 60;
            PhaseLabel.Text   = "Przerwa";
            PhaseLabel.Foreground = BreakAccent;
            TimerDisplay.Foreground = BreakAccent;
            ProgressBar.Foreground  = BreakAccent;
        }

        _remainingSeconds = _totalSeconds;
        UpdateDisplay();

        if (restart)
        {
            _isRunning = true;
            StartPauseButton.Content = "Pauza";
            _timer.Start();
        }
        else
        {
            _isRunning = false;
            StartPauseButton.Content = "Start";
        }
    }

    /// <summary>Updates TimerDisplay and ProgressBar without changing any state.</summary>
    private void UpdateDisplay()
    {
        int minutes = _remainingSeconds / 60;
        int seconds = _remainingSeconds % 60;
        TimerDisplay.Text = $"{minutes:D2}:{seconds:D2}";

        double progress = _totalSeconds > 0
            ? (double)_remainingSeconds / _totalSeconds * 100.0
            : 0;
        ProgressBar.Value = progress;
    }
}