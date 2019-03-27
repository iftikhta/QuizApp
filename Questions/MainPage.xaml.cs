﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Questions.Application.Quizes;
using Questions.Data;

namespace Questions
{
    public sealed partial class MainPage : Page
    {
        private List<Quiz> Quizes;

        public MainPage()
        {
            this.InitializeComponent();

            Quizes = ((App) Windows.UI.Xaml.Application.Current).Controller.Quizes;
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            // TODO: Change namespace
            Windows.UI.Xaml.Application.Current.Exit();
        }

        private void Add_New(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(QuizCreationPage));
        }

        private void Selected(object sender, SelectionChangedEventArgs e)
        {
            var quiz = Quizes[QuizList.SelectedIndex];
            quiz.Info(); // TODO: Remove
        }
    }
}
