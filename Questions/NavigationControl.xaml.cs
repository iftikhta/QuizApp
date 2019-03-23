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

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Questions
{
    public sealed partial class NavigationControl : UserControl
    {
        // Events
        public event EventHandler Cancel;
        public event EventHandler Back;
        public event EventHandler Next;
        public event EventHandler Finish;

        public NavigationControl()
        {
            this.InitializeComponent();
        }

        // Properties

        public bool HasCancel { get; set; }
        public bool HasBack { get; set; }
        public bool HasNext { get; set; }
        public bool HasFinish { get; set; }

        // Event-Handlers + Mappers

        private void OnCancel(object sender, RoutedEventArgs e)
        {
            if(HasCancel)
            Cancel?.Invoke(this, EventArgs.Empty);
        }

        private void OnBack(object sender, RoutedEventArgs e)
        {
            if(HasBack)
            Back?.Invoke(this, EventArgs.Empty);
        }

        private void OnNext(object sender, RoutedEventArgs e)
        {
            if(HasNext)
            Next?.Invoke(this, EventArgs.Empty);
        }

        private void OnFinish(object sender, RoutedEventArgs e)
        {
            if(HasFinish)
            Finish?.Invoke(this, EventArgs.Empty);
        }
    }
}
