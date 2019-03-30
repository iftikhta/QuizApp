using Questions.Application.Quizes;
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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Questions
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class QuizView : Page
    {
        public QuizView()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
           // base.OnNavigatedTo(e);  Taha: I do not recall what this does or if we need it. Please go over this with me, copied from class code

            if (e.Parameter is Quiz)
            {
                Quiz q = (Quiz)e.Parameter;

                QuizNameText.Text = q.Name;
                QuizTotalQuestions.Text = $"{q.Count}";
                QuizTotalPoints.Text = $"{q.Points}";
            }
        }

        private void NavigationBarQuizView_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }

    
    
    

}
