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
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;
using Questions.Application.Quizes;
using Questions.ViewModels;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Questions
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class QuizCreationPage : Page
    {
        private QuizBuilder _builder;

        private void UpdateButton()
        {
            NextButton.IsEnabled = TitleInput.Text.Length > 0;
        }

        public QuizCreationPage()
        {
            this.InitializeComponent();
        }

        private void OnCancel(object sender, RoutedEventArgs e)
        {
            if (Frame.CanGoBack) Frame.GoBack();
        }

        private void OnNext(object sender, RoutedEventArgs e)
        {
            _builder.SetName(TitleInput.Text);
            var state = new CreationState() {Builder = _builder, Position = 1};


            Frame.Navigate(typeof(QuestionCreationPage), state, new SlideNavigationTransitionInfo(){ Effect = SlideNavigationTransitionEffect.FromRight });
        }

        private void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateButton();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (e.Parameter is CreationState state)
                _builder = state.Builder;
            else
                _builder = QuizBuilder.Create();

            TitleInput.Text = _builder.Name;
            UpdateButton();
        }
    }
}
