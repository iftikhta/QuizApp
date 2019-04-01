using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Cryptography.X509Certificates;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;


// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Questions
{
    public sealed partial class NavigationBarQuizView : UserControl
    {
        public NavigationBarQuizView()
        {
            this.InitializeComponent();
            HasStartButton = HasBackButton = true;
        }

        public event EventHandler Back;
        public event EventHandler Start;

      //  public event EventHandler Back;

        public bool HasBackButton
        {
            get { return BackButton.IsEnabled; }
            set { BackButton.IsEnabled = value; }
        }

        public bool HasStartButton
        {
            get { return StartButton.IsEnabled; }
            set { StartButton.IsEnabled = value; }
        }


        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            if (HasStartButton)
                Start?.Invoke(this, EventArgs.Empty);
        }


        private void Back_Click(object sender, RoutedEventArgs e)
        {
            if (HasBackButton)
                Back?.Invoke(this, EventArgs.Empty); //only calls this method if there is a listener
        }
    }



}
