using Questions.Application.Quizes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Security.Cryptography.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Questions.Application.QuizRunner;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Questions
{

    public sealed partial class QuizView : Page
    {
        Quiz q = new Quiz(null, null); //need this to be able to navigate forward with q
        public QuizView()
        {
            this.InitializeComponent();
            //Quiz q = new Quiz(null,null);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
           // base.OnNavigatedTo(e);  Taha: I do not recall what this does or if we need it. Please go over this with me, copied from class code

            if (e.Parameter is Quiz)
            {
                q = (Quiz)e.Parameter;
                
                QuizNameText.Text = q.Name;
                QuizTotalQuestions.Text = $"{q.Count}";
                QuizTotalPoints.Text = $"{q.Points}";
            }
        }

        private void NavigationBarQuizView_OnBack(object sender, EventArgs e)
        {
            this.Frame.GoBack();
        }

        private void NavigationBarQuizView_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void NavigationBarQuizView_OnStart(object sender, EventArgs e)
        {
            QuizRunner QuizRunner = new QuizRunner { CurrentQuiz = q };

            this.Frame.Navigate(typeof(QuestionPage), QuizRunner); //change to quiz start page
        }
    }

    
    
    

}
