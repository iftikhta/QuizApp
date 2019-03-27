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
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;
using Questions.Application.Questions;
using Questions.Application.Quizes;
using Questions.ViewModels;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Questions
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class QuestionCreationPage : Page
    {
        private QuizBuilder _builder;
        private int _position;

        private bool IsUpdating => _position <= _builder.Count; // Editing existing element

        private QuestionType CurrentCreationType => QuestionTypeConverter.ToType(TypeSelection.SelectedIndex);

        private string CurrentText
        {
            get
            {
                string result;
                QuestionText.Document.GetText(TextGetOptions.None, out result);
                return result.Trim();
            }
            set
            {
                QuestionText.Document.SetText(TextSetOptions.None, value);
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
            set
            {
                QuestionPoints.Text = value + "";
            }
        }

        private bool IsReady => QuestionCreator.Value != null && !string.IsNullOrEmpty(CurrentText) && CurrentPoints > 0 && QuestionCreator.Type != null;

        private void Initialize()
        {
            QuestionNumber.Text = _position + "";
            Navigation.HasFinish = _builder.Count > 0;
            if (IsUpdating)
            {
                var question = _builder.GetQuestion(_position - 1);
                QuestionCreator.Type = question.Type;
                QuestionCreator.Value = question.Answer;
                CurrentText = question.Text;
                CurrentPoints = question.Points;
                TypeSelection.SelectedIndex = QuestionTypeConverter.ToValue(question.Type);
            }
            else
            {
                DeleteButton.Visibility = Visibility.Collapsed; // Unable to delete a new question
            }
            UpdateNext();
        }

        private void UpdateNext()
        {
            if (Navigation != null)
            {
                Navigation.HasNext = IsReady;
                Navigation.HasFinish = IsReady || _builder.Count > 0;
            }
        }

        private void AddQuestion()
        {
            if (IsReady)
                _builder.AddQuestion(CurrentText, CurrentPoints, QuestionCreator.Value, _position - 1);
        }

        public QuestionCreationPage()
        {
            this.InitializeComponent();
        }

        private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(QuestionCreator != null) QuestionCreator.Type = CurrentCreationType;
        }

        private void NavigationControl_OnNext(object sender, EventArgs e)
        {
            AddQuestion();

            var state = new CreationState() {Builder = _builder, Position = _position + 1};
            Frame.Navigate(typeof(QuestionCreationPage), state, new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromRight });
        }

        private void NavigationControl_OnCancel(object sender, EventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }

        private void NavigationControl_OnFinish(object sender, EventArgs e)
        {
            AddQuestion();

            var quiz = _builder.Build();
            ((App)Windows.UI.Xaml.Application.Current).Controller.AddQuiz(quiz);
            Frame.Navigate(typeof(MainPage), null, new ContinuumNavigationTransitionInfo());
        }

        private void NavigationControl_OnBack(object sender, EventArgs e)
        {
            AddQuestion();

            var state = new CreationState() {Builder = _builder, Position = _position - 1};
            var to = _position == 1 ? typeof(QuizCreationPage) : typeof(QuestionCreationPage);
            Frame.Navigate(to, state, new SlideNavigationTransitionInfo() {Effect = SlideNavigationTransitionEffect.FromLeft});
        }

        private void QuestionCreator_OnValueUpdated(object sender, EventArgs e)
        {
            UpdateNext();
        }

        private void QuestionPoints_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateNext();
        }

        private void QuestionText_TextChanged(object sender, RoutedEventArgs e)
        {
            UpdateNext();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var state = new CreationState() { Builder = _builder.RemoveQuestion(_position - 1), Position = _position };
            // Different animation on deletion
            Frame.Navigate(typeof(QuestionCreationPage), state, new DrillInNavigationTransitionInfo()); 
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (e.Parameter is CreationState state)
            {
                _builder = state.Builder;
                _position = state.Position;
                Initialize();
            } else throw new Exception("Invalid navigation");
        }
    }
}
