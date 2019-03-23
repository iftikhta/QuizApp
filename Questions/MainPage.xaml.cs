using System;
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
        private readonly QuizController _controller = new QuizController(new StaticLoader());

        public MainPage()
        {
            this.InitializeComponent();

            _controller.Load();
            QuizList.ItemsSource = _controller.Quizes;
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("Exit");
        }

        private void Add_New(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("Added");
        }

        private void Selected(object sender, SelectionChangedEventArgs e)
        {
            Debug.WriteLine(QuizList.SelectedIndex);
        }
    }
}
