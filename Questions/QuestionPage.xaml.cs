using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.ServiceModel.Channels;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Notifications;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Questions.Application.Questions;
using Questions.Application.Quizes;
using Questions.Application.QuizRunner;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Questions
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class QuestionPage : Page
    {
        QuizRunner q = new QuizRunner();
        public QuestionPage()
        {
            this.InitializeComponent();
        }

        private void NavigationBarQuizView_OnBack(object sender, EventArgs e)
        {
           // if quiz._questions //check the current question index, if its first, disable back, if its last, disable forward
           if (!q.IsFirstQuestion())
               this.Frame.GoBack();
           else
           {
               Navbar.HasBack = false; //other bar uses HasBackButton
               //TAHA: Ask greg how I can create a qucik notification/dialog/message warning that youc ant go further back
           }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {

            if (e.Parameter is QuizRunner)
            {
                q = (QuizRunner)e.Parameter;
                if (q.CurrentQuiz[q.CurrentQuestionNum].Type == QuestionType.Text)
                {
                    //enable text question structure
                    //populate with question information
                    QuestionDescription.Text = q.CurrentQuiz[q.CurrentQuestionNum].Text;
                    QuestionAnswer.Text = $"{q.CurrentQuiz[q.CurrentQuestionNum].Type}";  //use this to determine what type of strucuture the page uses
                    QuizTotalPoints.Text = $"{q.CurrentQuiz[q.CurrentQuestionNum].Answer}";
                }
                else if (q.CurrentQuiz[q.CurrentQuestionNum].Type == QuestionType.Options)
                {
                    //enable Options question structure
                }
                else if (q.CurrentQuiz[q.CurrentQuestionNum].Type == QuestionType.TrueFalse)
                {
                    //enable True/False question structure
                }
              
            }
        }

        private void Navbar_OnCancel(object sender, EventArgs e)
        {
            //should force a dialog confirming decision to quit quiz
            this.Frame.Navigate(typeof(MainPage));
        }

        private void Navbar_OnNext(object sender, EventArgs e)
        {
            if (!q.IsLastQuestion())
            {
                q.CurrentQuestionNum += 1;
                this.Frame.Navigate(typeof(QuestionPage), q);
            }
            else
            {
                Navbar.HasNext = false;
                Navbar.HasFinish = true;
            }
        }
    }
}
