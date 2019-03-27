using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Text;
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
    public sealed partial class QuestionCreationPage : Page
    {
        private readonly QuizBuilder _builder = QuizBuilder.Create("Quiz new"); // TODO: Replace with Naigation

        private static readonly List<CreationType> SelectionOptions = new List<CreationType>()
        {
            CreationType.TrueFalse, CreationType.Options, CreationType.Text
        };

        private CreationType CurrentCreationType => SelectionOptions[TypeSelection.SelectedIndex];

        private string CurrentText
        {
            get
            {
                string result;
                QuestionText.Document.GetText(TextGetOptions.None, out result);
                return result.Trim();
            }
        }

        private int CurrentPoints
        {
            get
            {
                int result;
                if (int.TryParse(QuestionPoints.Text, out result)) return result;
                return -1;
            }
        }

        // Add Points
        private bool IsReady => QuestionCreator.Value != null && !string.IsNullOrEmpty(CurrentText) && CurrentPoints > 0;

        private void UpdateNext()
        {
            if (Navigation != null) Navigation.HasFinish = Navigation.HasNext = IsReady;
        }

        public QuestionCreationPage()
        {
            this.InitializeComponent();
            UpdateNext();
        }

        private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(QuestionCreator != null) QuestionCreator.Type = CurrentCreationType;
        }

        private void NavigationControl_OnNext(object sender, EventArgs e)
        {
            // TODO: Replace with Boolean-Getter
            if (!IsReady) return;

            _builder.AddQuestion(CurrentText, CurrentPoints, QuestionCreator.Value);
            // Position + 1 AND Creation / Deletion / Update
        }

        private void NavigationControl_OnCancel(object sender, EventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }

        private void NavigationControl_OnFinish(object sender, EventArgs e)
        {
            if(IsReady) _builder.AddQuestion(CurrentText, CurrentPoints, QuestionCreator.Value);

            var quiz = _builder.Build();
            ((App)Windows.UI.Xaml.Application.Current).Controller.AddQuiz(quiz);
            Frame.Navigate(typeof(MainPage));
        }

        private void NavigationControl_OnBack(object sender, EventArgs e)
        {
            // Position - 1
        }

        private void QuestionCreator_OnValueUpdated(object sender, EventArgs e)
        {
            UpdateNext();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            QuestionNumber.Text = (_builder.Count + 1) + "";
            Navigation.HasFinish = _builder.Count > 0;
        }

        private void QuestionPoints_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateNext();
        }

        private void QuestionText_TextChanged(object sender, RoutedEventArgs e)
        {
            UpdateNext();
        }
    }
}
