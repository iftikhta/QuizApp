using System;
using System.Collections.Generic;
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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Questions
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class QuizCreationPage : Page
    {
        private void UpdateButton()
        {
            NextButton.IsEnabled = TitleInput.Text.Length > 0;
        }

        public QuizCreationPage()
        {
            this.InitializeComponent();
            UpdateButton();
        }

        private void OnCancel(object sender, RoutedEventArgs e)
        {
            if (Frame.CanGoBack) Frame.GoBack();
        }

        private void OnNext(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(QuestionCreationPage), QuizBuilder.Create(TitleInput.Text));
        }

        private void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateButton();
        }
    }
}
