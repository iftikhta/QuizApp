using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Radios;
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

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Questions
{
    public sealed partial class QuizDisplayControl : UserControl
    {
        public QuizDisplayControl()
        {
            this.InitializeComponent();
            Hide();
        }

        public QuestionType QuestionType { get; set; }

        public void ShowQuestion()
        {
            if (QuestionType == QuestionType.Options)
                OptionsQuestion.Visibility = Visibility.Visible;
            if (QuestionType == QuestionType.Text)
                TextQuestion.Visibility = Visibility.Visible;
            if (QuestionType == QuestionType.TrueFalse)
                TrueFalseQuestion.Visibility = Visibility.Visible;
        }
        private void Hide()
        {
            TrueFalseQuestion.Visibility = OptionsQuestion.Visibility = TextQuestion.Visibility = Visibility.Collapsed;
        }

        public string AccessTextInput()
        {
            return TextGivenAnswer.DataContext.ToString();
        }

        public RichEditBox AccessTextBox()
        {
            return TextGivenAnswer;
        }

        public RadioButton GetSelectedRadioButton()
        {
            var SelectedRadio = True.IsChecked == true ? True : False;
            return SelectedRadio;
        }

        public RadioButton GetTrueRadioButton()
        {
            return True;
        }
        public RadioButton GetFalseRadioButton()
        {
            return False;
        }

        public bool GetTrueFalseAnswer() // will default to false if nothing is checked, may want to alter behaviour
        {
            return True.IsChecked ?? false;
        }
        

        public ListBox AccessList()
        {
            return OptionsList;
        }
    }
}
