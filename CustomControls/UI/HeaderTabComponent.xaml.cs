using Xamarin.Forms;
using System.Windows.Input;
using BindExtensions = CustomControls.Extensions.BindableObjectExtensions;
using CustomControls.Extensions;

namespace CustomControls.UI
{
    public partial class HeaderTabComponent
    {
        private const double MaxHeaderHeightRequest = 238;
        private const double MinHeaderHeightRequest = 86;

        public static readonly BindableProperty HeaderImageSourceProperty = BindExtensions.Create<string, HeaderTabComponent>(nameof(HeaderImageSource));
        public static readonly BindableProperty LeftTabTextProperty = BindExtensions.Create<string, HeaderTabComponent>(nameof(LeftTabText));
        public static readonly BindableProperty RightTabTextProperty = BindExtensions.Create<string, HeaderTabComponent>(nameof(RightTabText));
        public static readonly BindableProperty SearchHintTextProperty = BindExtensions.Create<string, HeaderTabComponent>(nameof(SearchHintText));

        public static readonly BindableProperty RightTabCommandProperty = BindExtensions.Create<ICommand, HeaderTabComponent>(nameof(RightTabCommand));
        public static readonly BindableProperty LeftTabCommandProperty = BindExtensions.Create<ICommand, HeaderTabComponent>(nameof(LeftTabCommand));
        public static readonly BindableProperty FilterCommandProperty = BindExtensions.Create<ICommand, HeaderTabComponent>(nameof(FilterCommand));
        public static readonly BindableProperty SearchCommandProperty = BindExtensions.Create<ICommand, HeaderTabComponent>(nameof(SearchCommand));
        public static readonly BindableProperty CancelSearchCommandProperty = BindExtensions.Create<ICommand, HeaderTabComponent>(nameof(CancelSearchCommand));

        public static readonly BindableProperty IsSearchVisibleProperty = BindExtensions.Create<bool, HeaderTabComponent>(nameof(IsSearchVisible));
        public static readonly BindableProperty IsBalanceVisibleProperty = BindExtensions.Create<bool, HeaderTabComponent>(nameof(IsBalanceVisible));
        public static readonly BindableProperty IsFilterVisibleProperty = BindExtensions.Create<bool, HeaderTabComponent>(nameof(IsFilterVisible));
        public static readonly BindableProperty AreTabsVisibleProperty = BindExtensions.Create<bool, HeaderTabComponent>(nameof(AreTabsVisible));
        public static readonly BindableProperty IsHeaderExpandedProperty = BindExtensions.Create<bool, HeaderTabComponent>(nameof(IsHeaderExpanded), header => header.OnHeaderStateChanged);

        public static readonly BindableProperty LeftTabTextColorProperty = BindExtensions.Create<Color, HeaderTabComponent>(nameof(LeftTabTextColor));
        public static readonly BindableProperty RightTabTextColorProperty = BindExtensions.Create<Color, HeaderTabComponent>(nameof(RightTabTextColor));
        public static readonly BindableProperty RightTabBackgroundColorProperty = BindExtensions.Create<Color, HeaderTabComponent>(nameof(RightTabBackgroundColor));
        public static readonly BindableProperty LeftTabBackgroundColorProperty = BindExtensions.Create<Color, HeaderTabComponent>(nameof(LeftTabBackgroundColor));

        public HeaderTabComponent()
        {
            InitializeComponent();

            InitializeBindings();

            FilterFrame.GestureRecognizers.Add(new TapGestureRecognizer() { Command = new Command(() => TapOnFilterFrame(FilterFrame)) });
        }

        public Color LeftTabBackgroundColor
        {
            get => this.GetProperty<Color>();
            set => this.SetProperty(value);
        }

        public Color RightTabBackgroundColor
        {
            get => this.GetProperty<Color>();
            set => this.SetProperty(value);
        }

        public Color RightTabTextColor
        {
            get => this.GetProperty<Color>();
            set => this.SetProperty(value);
        }

        public Color LeftTabTextColor
        {
            get => this.GetProperty<Color>();
            set => this.SetProperty(value);
        }

        public ICommand CancelSearchCommand
        {
            get => this.GetProperty<ICommand>();
            set => this.SetProperty(value);
        }

        public ICommand SearchCommand
        {
            get => this.GetProperty<ICommand>();
            set => this.SetProperty(value);
        }

        public ICommand FilterCommand
        {
            get => this.GetProperty<ICommand>();
            set => this.SetProperty(value);
        }

        public ICommand LeftTabCommand
        {
            get => this.GetProperty<ICommand>();
            set => this.SetProperty(value);
        }

        public ICommand RightTabCommand
        {
            get => this.GetProperty<ICommand>();
            set => this.SetProperty(value);
        }

        public string RightTabText
        {
            get => this.GetProperty<string>();
            set => this.SetProperty(value);
        }

        public string LeftTabText
        {
            get => this.GetProperty<string>();
            set => this.SetProperty(value);
        }

        public string HeaderImageSource
        {
            get => this.GetProperty<string>();
            set => this.SetProperty(value);
        }

        public string SearchHintText
        {
            get => this.GetProperty<string>();
            set => this.SetProperty(value);
        }

        public bool AreTabsVisible
        {
            get => this.GetProperty<bool>();
            set => this.SetProperty(value);
        }

        public bool IsFilterVisible
        {
            get => this.GetProperty<bool>();
            set => this.SetProperty(value);
        }

        public bool IsBalanceVisible
        {
            get => this.GetProperty<bool>();
            set => this.SetProperty(value);
        }

        public bool IsSearchVisible
        {
            get => this.GetProperty<bool>();
            set => this.SetProperty(value);
        }

        public bool IsHeaderExpanded
        {
            get => this.GetProperty<bool>();
            set => this.SetProperty(value);
        }

        public void OnHeaderStateChanged(bool _, bool newValue)
        {
            if (newValue)
            {
                ExpandHeader();
                return;
            }

            СollapseHeader();
        }

        private void СollapseHeader()
        {
            if (HeightRequest == MinHeaderHeightRequest)
            {
                return;
            }

            ForegroundHeaderImage.AnimateOpacityTo(true);
            TabsGrid.AnimateOpacityTo(true);
            this.AnimateHeightTo(MaxHeaderHeightRequest, MinHeaderHeightRequest);
        }

        private void ExpandHeader()
        {
            if (HeightRequest == MaxHeaderHeightRequest)
            {
                return;
            }

            ForegroundHeaderImage.AnimateOpacityTo();
            TabsGrid.AnimateOpacityTo();
            this.AnimateHeightTo(MinHeaderHeightRequest, MaxHeaderHeightRequest);
        }

        private void InitializeBindings()
        {
            ForegroundHeaderImage.SetBinding(Image.SourceProperty, new Binding { Source = this, Path = nameof(HeaderImageSource) });
            LeftTabButton.SetBinding(Button.TextProperty, new Binding { Source = this, Path = nameof(LeftTabText) });
            RightTabButton.SetBinding(Button.TextProperty, new Binding { Source = this, Path = nameof(RightTabText) });
            SearchEntry.SetBinding(SearchEntry.HintTextProperty, new Binding { Source = this, Path = nameof(SearchHintText) });

            RightTabButton.SetBinding(Button.CommandProperty, new Binding { Source = this, Path = nameof(RightTabCommand) });
            LeftTabButton.SetBinding(Button.CommandProperty, new Binding { Source = this, Path = nameof(LeftTabCommand) });

            SearchEntry.SetBinding(SearchEntry.SearchCommandProperty, new Binding { Source = this, Path = nameof(SearchCommand) });
            SearchEntry.SetBinding(SearchEntry.CancelCommandProperty, new Binding { Source = this, Path = nameof(CancelSearchCommand) });

            SearchEntry.SetBinding(SearchEntry.IsVisibleProperty, new Binding { Source = this, Path = nameof(IsSearchVisible) });
            BalanceFrame.SetBinding(Frame.IsVisibleProperty, new Binding { Source = this, Path = nameof(IsBalanceVisible) });
            FilterFrame.SetBinding(Frame.IsVisibleProperty, new Binding { Source = this, Path = nameof(IsFilterVisible) });
            TabsGrid.SetBinding(Grid.IsVisibleProperty, new Binding { Source = this, Path = nameof(AreTabsVisible) });

            LeftTabButton.SetBinding(Button.TextColorProperty, new Binding { Source = this, Path = nameof(LeftTabTextColor) });
            RightTabButton.SetBinding(Button.TextColorProperty, new Binding { Source = this, Path = nameof(RightTabTextColor) });
            RightTabButton.SetBinding(Button.BackgroundColorProperty, new Binding { Source = this, Path = nameof(RightTabBackgroundColor) });
            LeftTabButton.SetBinding(Button.BackgroundColorProperty, new Binding { Source = this, Path = nameof(LeftTabBackgroundColor) });
        }

        private void TapOnFilterFrame(View _)
        {
            FilterCommand?.Execute(null);
        }
    }
}