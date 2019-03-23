using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
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
    public sealed partial class QuizCreationPage : Page
    {
        public QuizCreationPage()
        {
            this.InitializeComponent();
        }

        private void OnCancel(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("Cancel");
        }

        private void OnNext(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("Next");
        }

        private void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            NextButton.IsEnabled = TitleInput.Text.Length > 0;
        }
    }
}
