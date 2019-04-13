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
               q.PreviousQuestion();
               this.Frame.Navigate(typeof(QuestionPage), q);
               //this.Frame.GoBack();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter is QuizRunner)
            {
                //setup beheind the scenes
                q = (QuizRunner)e.Parameter;
                Heading.Text = $"Question #{q.CurrentQuestionNum + 1}";
                var CurrQuestion = q.CurrentQuiz[q.CurrentQuestionNum]; //cache and easily read the current queston
                int CurrAnswer = -1;
                if (q.CurrentQuestionNum < q.Answer.Count)
                    CurrAnswer = q.CurrentQuestionNum;

                QuestionDescription.Text = CurrQuestion.Text; //ask the question

                //determine type and display accordingly. Fill in with pre-existing answer if user has already given one
                if (CurrQuestion.Type == QuestionType.Text)
                {
                    DisplayedQuestion.QuestionType = QuestionType.Text;
                    if (CurrAnswer > -1)
                    {
                        DisplayedQuestion.AccessTextBox().Document.SetText(TextSetOptions.None, q.Answer[CurrAnswer].GivenAnswer.ToString()); //make sure this works
                    }
                }
                else if (CurrQuestion.Type == QuestionType.Options)
                {
                    //enable Options question structure
                    DisplayedQuestion.QuestionType = QuestionType.Options;
                    //CurrQuestion.ToQuestion().CheckAnswer()  //inside checkanswer i put the GivenAnswer

                    var OptionList = (CurrQuestion.ToQuestion() as OptionsQuestion).Options;

                    var Selections = CurrAnswer == -1?new List<string>(): (List<string>) q.Answer[CurrAnswer].GivenAnswer;
                    var i = 0;

                    DisplayedQuestion.AccessList().ItemsSource = OptionList;

                    foreach (var item in OptionList)
                    {                                    
                        if (Selections.Contains(item)) //given answer here should be a list of selected items
                        {
                            DisplayedQuestion.AccessList().SelectedItems.Insert(i++, item);
                        }
                    }
                }
                else if (CurrQuestion.Type == QuestionType.TrueFalse)
                {
                    //enable True/False question structure
                    DisplayedQuestion.QuestionType = QuestionType.TrueFalse;
                    if (CurrAnswer > -1)
                    {
                        if ((bool) q.Answer[CurrAnswer].GivenAnswer)
                        {
                            DisplayedQuestion.GetTrueRadioButton().IsChecked = true;
                        }
                        else if ((bool) q.Answer[CurrAnswer].GivenAnswer == false)
                        {
                            DisplayedQuestion.GetFalseRadioButton().IsChecked = true;
                        }
                    }
                }
                DisplayedQuestion.ShowQuestion();
                //Handle button states
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
            md.Commands.Add(new UICommand(
                "Exit",
                new UICommandInvokedHandler(this.ExitFunc)));
            md.Commands.Add(new UICommand(
                "Close",
                new UICommandInvokedHandler(this.DoNothing))); //close the dialog
            md.ShowAsync();
        }

        public void DoNothing(IUICommand command)
        {
            //Do nothing and just close the MessageDialog
        }

        public void ExitFunc(IUICommand command)
        {
            this.Frame.Navigate(typeof(MainPage));
        }

        private void Navbar_OnNext(object sender, EventArgs e)
        {
            AppendAnswer();
            q.NextQuestion(); //q.CurrentQuestionNum += 1;
            this.Frame.Navigate(typeof(QuestionPage), q);
        }

        //record all given answers for tracking back and forth as well as calculating 
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
                newAnswer.GivenAnswer = DisplayedQuestion.AccessList().SelectedItems.Select(s => s.ToString()).ToList();
            }
            else if (newAnswer.QuestionType == QuestionType.TrueFalse)
            {
                newAnswer.GivenAnswer = DisplayedQuestion.GetTrueFalseAnswer(); //asumes false by default
            }

            //this ensures i dont break the list and only add to it when there is a new question
            if (q.CurrentQuestionNum >= q.Answer.Count)
            {
                q.Answer.Add(newAnswer);
            }
            else
            {
                q.Answer[q.CurrentQuestionNum] = newAnswer;
            }
        }

        private void Navbar_OnFinish(object sender, EventArgs e)
        {
            AppendAnswer();
            this.Frame.Navigate(typeof(ResultsPage), q);
        }
    }
}
