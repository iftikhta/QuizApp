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
using Questions.Application.QuizRunner;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Questions
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ResultsPage : Page
    {
        QuizRunner q = new QuizRunner();
        //private object q;
        public ResultsPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter is QuizRunner)
            {
                q = (QuizRunner) e.Parameter;
                CalculateResult(q);

            }

        }

        private void CalculateResult(QuizRunner quizrunner)
        {
            float pointsEarned = 0;
            int possiblePoints = quizrunner.CurrentQuiz.Points;
            int i = 0;
            foreach (var answer in quizrunner.Answer)
            {
                pointsEarned += quizrunner.CurrentQuiz[i].ToQuestion().CheckAnswer(answer.GivenAnswer); //strings work,can typecast to string and still works. List doesnt
                i++;
            }

            FinalScore.Text = $"{pointsEarned}";
            PercentScore.Text = $"{Math.Round(pointsEarned / possiblePoints *100)} %";
            TotalPossible.Text = $"{possiblePoints}";
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }
    }
}
