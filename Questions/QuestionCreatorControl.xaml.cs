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
    public enum CreationType { TrueFalse, Options, Text, NoSelection }

    public sealed partial class QuestionCreatorControl : UserControl
    {
        private CreationType _type;
        private readonly List<string> _options = new List<string>();

        private void Hide()
        {
            NoSelection.Visibility = TrueFalseCreator.Visibility = OptionsCreator.Visibility = TextCreator.Visibility = Visibility.Collapsed;
        }

        private void SetVisibility()
        {
            Hide();

            switch (_type)
            {
                case CreationType.Options:
                    OptionsCreator.Visibility = Visibility.Visible;
                    break;
                case CreationType.Text:
                    TextCreator.Visibility = Visibility.Visible;
                    break;
                case CreationType.TrueFalse:
                    TrueFalseCreator.Visibility = Visibility.Visible;
                    break;
                default:
                    NoSelection.Visibility = Visibility.Visible;
                    break;
            }
        }

        public QuestionCreatorControl()
        {
            this.InitializeComponent();
            Hide();
            OptionsList.ItemsSource = _options;
        }

        public CreationType Type
        {
            get { return _type; }
            set
            {
                _type = value;
                SetVisibility();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _options.Add(NewOption.Text);
        }
    }
}
