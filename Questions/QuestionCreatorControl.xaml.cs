using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Text;
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
        private ImmutableList<string> _options = ImmutableList<string>.Empty;

        private void Hide()
        {
            NoSelection.Visibility = TrueFalseCreator.Visibility = OptionsCreator.Visibility = TextCreator.Visibility = Visibility.Collapsed;
        }

        private string CurrentText
        {
            get
            {
                string result;
                TextAnswer.Document.GetText(TextGetOptions.None, out result);
                result = result.Trim();
                return string.IsNullOrEmpty(result) ? null : result;
            }
        }

        private Dictionary<string, bool> CurrentOptions
        {
            get
            {
                // At least 2 Options + At least 1 Selected
                if (OptionsList.SelectedItems.Count == 0 || _options.Count < 2) return null;

                var options = new Dictionary<string, bool>();
                _options.ForEach((option) => options[option] = OptionsList.SelectedItems.Contains(option));
                return options;
            }
        }

        private bool? CurrentChecks
        {
            get
            {
                if (True.IsChecked == true || False.IsChecked == true) return True.IsChecked;
                return null;
            }
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

        private void UpdateValue()
        {
            ValueUpdated?.Invoke(this, EventArgs.Empty);
        }

        public QuestionCreatorControl()
        {
            this.InitializeComponent();
            Hide();
        }

        public event EventHandler ValueUpdated;

        public CreationType Type
        {
            get { return _type; }
            set
            {
                _type = value;
                SetVisibility();
                UpdateValue();
            }
        }

        public object Value
        {
            get
            {
                switch (_type)
                {
                    case CreationType.Options:
                        return CurrentOptions;
                    case CreationType.Text:
                        return CurrentText;
                    case CreationType.TrueFalse:
                        return CurrentChecks;
                    default:
                        return null;
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Pseudo-Binding (Ask about efficiency)
            var text = NewOption.Text;
            if (string.IsNullOrEmpty(text) || _options.Count >= 5) return;

            OptionsList.ItemsSource = _options = _options.Add(text);
            NewOption.Text = "";
            UpdateValue();
        }

        // TODO: Trigger custom Update-Event on any changes + Selection-Change
        private void TextAnswer_OnTextChanged(object sender, RoutedEventArgs e)
        {
            UpdateValue();
        }

        private void OnChecked(object sender, RoutedEventArgs e)
        {
            UpdateValue();
        }

        private void OptionsList_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateValue();
        }
    }
}
