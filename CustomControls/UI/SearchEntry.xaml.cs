using CustomControls.Extensions;
using System;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using BindExtensions = CustomControls.Extensions.BindableObjectExtensions;

namespace CustomControls.UI
{
    public partial class SearchEntry
    {
        private const double MinWidth = 32;
        private const double HorizontalMargin = 153;

        public static readonly BindableProperty HintTextProperty = BindExtensions.Create<string, SearchEntry>(nameof(HintText));
        public static readonly BindableProperty IsPasswordProperty = BindExtensions.Create<bool, SearchEntry>(nameof(IsPassword));
        public static readonly BindableProperty EntryTextProperty = BindExtensions.Create<string, SearchEntry>(nameof(EntryText), s => s.OnEntryTextChanged);
        public static readonly BindableProperty SearchCommandProperty = BindExtensions.Create<ICommand, SearchEntry>(nameof(SearchCommand));
        public static readonly BindableProperty CancelCommandProperty = BindExtensions.Create<ICommand, SearchEntry>(nameof(CancelCommand));

        public double maxWidth;

        private bool isSearchExpanded;

        public SearchEntry()
        {
            InitializeComponent();

            HintLabel.BindingContext = this;
            SearchTextEntry.BindingContext = this;
            HintLabel.SetBinding(Label.TextProperty, nameof(HintText));
            SearchTextEntry.SetBinding(Entry.TextProperty, nameof(EntryText));
            maxWidth = (DeviceDisplay.MainDisplayInfo.Width / DeviceDisplay.MainDisplayInfo.Density) - HorizontalMargin;
        }

        public ICommand SearchCommand
        {
            get => this.GetProperty<ICommand>();
            set => this.SetProperty(value);
        }

        public ICommand CancelCommand
        {
            get => this.GetProperty<ICommand>();
            set => this.SetProperty(value);
        }

        public bool IsPassword
        {
            get => this.GetProperty<bool>();
            set => this.SetProperty(value);
        }

        public string HintText
        {
            get => this.GetProperty<string>();
            set => this.SetProperty(value);
        }

        public string EntryText
        {
            get => this.GetProperty<string>();
            set => this.SetProperty(value);
        }

        private bool IsEmpty => SearchTextEntry.Text.IsNullOrEmpty();

        private void OnUnfocused(object _, FocusEventArgs __)
        {
            if (IsEmpty)
            {
                СollapseSearch();
            }
        }

        private void OnSearchImageButtonClicked(object _, EventArgs __)
        {
            if (isSearchExpanded)
            {
                if (IsEmpty)
                {
                    СollapseSearch();
                    return;
                }

                SearchCommand.Execute(EntryText);
                return;
            }

            ExpandSearch();
        }

        private void OnCancelImageButtonClicked(object _, EventArgs __)
        {
            EntryText = string.Empty;
            HintLabel.IsVisible = true;
            SearchTextEntry.Text = string.Empty;

            СollapseSearch();
            CancelCommand.Execute(null);
        }

        private void СollapseSearch()
        {
            BorderColor = Color.Transparent;
            CancelImageButton.IsVisible = false;

            HintLabel.AnimateOpacityTo(true);
            SearchTextEntry.AnimateOpacityTo(true);
            this.AnimateWidthTo(Width, MinWidth);

            this.HorizontalOptions = LayoutOptions.EndAndExpand;
            isSearchExpanded = false;
        }

        private void ExpandSearch()
        {
            this.AnimateWidthTo(MinWidth, maxWidth);
            HintLabel.AnimateOpacityTo();
            SearchTextEntry.AnimateOpacityTo();

            BorderColor = Color.Accent;
            BackgroundColor = Color.White;
            isSearchExpanded = true;
        }

        private void OnEntryTextChanged(string oldValue, string newValue)
        {
            EntryText = newValue;
            if (!EntryText.IsNullOrEmpty())
            {
                CancelImageButton.IsVisible = true;
                HintLabel.IsVisible = false;
                return;
            }

            HintLabel.IsVisible = true;
            CancelImageButton.IsVisible = false;
        }
    }
}