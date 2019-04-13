using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.ServiceModel.Channels;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Notifications;
using Windows.UI.Popups;
using Windows.UI.Text;
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
            AppendAnswer();

           // if quiz._questions //check the current question index, if its first, disable back, if its last, disable forward
           if (!q.IsFirstQuestion)
           {
               q.PreviousQuestion();
               this.Frame.Navigate(typeof(QuestionPage), q);
               //this.Frame.GoBack();
           }
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
                var CurrQuestion = q.CurrentQuiz[q.CurrentQuestionNum]; //cache and easily read the current queston
                int CurrAnswer = -1;
                if (q.CurrentQuestionNum < q.Answer.Count)
                    CurrAnswer = q.CurrentQuestionNum;

                QuestionDescription.Text = CurrQuestion.Text;
                if (CurrQuestion.Type == QuestionType.Text)
                {
                    DisplayedQuestion.QuestionType = QuestionType.Text;
                    DisplayedQuestion.ShowQuestion();
                    if (CurrAnswer > -1)
                    {
                        DisplayedQuestion.AccessTextBox().Document.SetText(TextSetOptions.None, q.Answer[CurrAnswer].GivenAnswer.ToString()); //make sure this works
                    }

                    // QuizTotalPoints.Text = $"{q.CurrentQuiz[q.CurrentQuestionNum].Answer}";
                }
                else if (CurrQuestion.Type == QuestionType.Options)
                {
                    //enable Options question structure
                    DisplayedQuestion.QuestionType = QuestionType.Options;
               
                    //populate the list 
                    var OptionList = (CurrQuestion.ToQuestion() as OptionsQuestion).Options;

                    var Selections = CurrAnswer == -1?new List<string>():q.Answer[CurrAnswer].GivenAnswer as List<string>;
                    var i = 0;
                    foreach (var item in OptionList)
                    {
                        
                        DisplayedQuestion.AccessList().Items.Add(item);
                        if (Selections.Contains(item)) //given answer here should be a list of selected items
                        {
                           DisplayedQuestion.AccessList().SelectedItems.Insert(i++, item);
                        }
                    }

                    //foreach (var selection in (q.Answer[CurrAnswer].GivenAnswer as OptionsQuestion)
                    //{

                    //}



                    DisplayedQuestion.ShowQuestion();

                    if (CurrAnswer > -1)
                    {
                        //DisplayedQuestion.AccessList().SelectedValue = q.Answer[q.CurrentQuestionNum].GivenAnswer; //make sure this works
                        DisplayedQuestion.AccessList().SelectedValue = 1; //make sure this works

                    }
                }
                else if (CurrQuestion.Type == QuestionType.TrueFalse)
                {
                    //enable True/False question structure
                    DisplayedQuestion.QuestionType = QuestionType.TrueFalse;
                    DisplayedQuestion.ShowQuestion();
                   // var TrueFalseAnswer = (CurrQuestion.ToQuestion() as OptionsQuestion).CheckAnswer();

                }

                //because of the below code i can remove these checks anwhere else
                if (q.IsFirstQuestion)
                    Navbar.HasBack = false;
                if (q.IsLastQuestion())
                {
                    Navbar.HasNext = false;
                    Navbar.HasFinish = true;
                }

            }
        }

        private void Navbar_OnCancel(object sender, EventArgs e)
        {
            //need to make dialog box confirm that you are canceling the entire quiz
            MessageDialog md = new MessageDialog("Are you sure you want to leave?");
            md.ShowAsync();
            this.Frame.Navigate(typeof(MainPage));
        }

        private void Navbar_OnNext(object sender, EventArgs e)
        {
            AppendAnswer();
            if (!q.IsLastQuestion())
            {
                q.NextQuestion(); //q.CurrentQuestionNum += 1;
                this.Frame.Navigate(typeof(QuestionPage), q);
            }
            else
            {
                Navbar.HasNext = false;
                Navbar.HasFinish = true;
            }
        }

        private void AppendAnswer() //remember to call on finish
        {
            Answer newAnswer = new Answer();
            newAnswer.QuestionType = q.CurrentQuiz[q.CurrentQuestionNum].Type;
            if (newAnswer.QuestionType == QuestionType.Text)
            {
                string result;
                DisplayedQuestion.AccessTextBox().Document.GetText(TextGetOptions.None, out result);
                newAnswer.GivenAnswer = result ; // is this correct way to save answer?
            }
            else if (newAnswer.QuestionType == QuestionType.Options)
            {
                //var temptest = DisplayedQuestion.AccessList().SelectedItems.ToList();
                //Debug.WriteLine(temptest);
                newAnswer.GivenAnswer = DisplayedQuestion.AccessList().SelectedItems.ToList();
            }
            else if (newAnswer.QuestionType == QuestionType.TrueFalse)
            {
                newAnswer.GivenAnswer = DisplayedQuestion.GetTrueFalseAnswer(); //asumes false by default
            }

            if (q.CurrentQuestionNum >= q.Answer.Count)
            {
                q.Answer.Add(newAnswer);
            }
            else
            {
                q.Answer[q.CurrentQuestionNum] = newAnswer;
            }
        }
    }
}
