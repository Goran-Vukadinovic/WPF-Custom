using System;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Threading;

namespace EntryPoint.Common
{
    [TemplatePart(Name = PartRootPanelName, Type = typeof(Panel))]
    [TemplatePart(Name = PartHeadIconName, Type = typeof(Image))]
    [TemplatePart(Name = PartTextBoxName, Type = typeof(TextBox))]
    [TemplatePart(Name = PartSearchIconName, Type = typeof(Button))]
    [TemplatePart(Name = PartCloseButtonName, Type = typeof(Button))]

    public class SearchText : ContentControl
    {
        // Part Names.
        private const string PartRootPanelName = "PART_RootPanel";
        private const string PartHeadIconName = "PART_HeadIcon";
        private const string PartTextBoxName = "PART_TextBox";
        private const string PartSearchIconName = "PART_SearchIcon";
        private const string PartCloseButtonName = "PART_CloseButton";

        // Commands.
        public static readonly RoutedCommand ActivateSearchCommand;
        public static readonly RoutedCommand CancelSearchCommand;

        // Properties.
        public static readonly DependencyProperty HandleClickOutsidesProperty;
        public static readonly DependencyProperty UpdateDelayMillisProperty;
        public static readonly DependencyProperty HintTextProperty;
        public static readonly DependencyProperty DefaultFocusedElementProperty;


        static SearchText()
        {
            DefaultStyleKeyProperty.OverrideMetadata(
                typeof(SearchText),
                new FrameworkPropertyMetadata(typeof(SearchText)));

            // Register commands.
            ActivateSearchCommand = new RoutedCommand();
            CancelSearchCommand = new RoutedCommand();

            // Using of CommandManager.
            CommandManager.RegisterClassCommandBinding(typeof(SearchText),
                new CommandBinding(ActivateSearchCommand, ActivateSearchCommand_Invoke));

            CommandManager.RegisterClassCommandBinding(typeof(SearchText),
                new CommandBinding(CancelSearchCommand, CancelSearchCommand_Invoke));

            // Register properties.
            HandleClickOutsidesProperty = DependencyProperty.Register(
                nameof(HandleClickOutsides), typeof(bool), typeof(SearchText),
                new UIPropertyMetadata(true));

            UpdateDelayMillisProperty = DependencyProperty.Register(
                nameof(UpdateDelayMillis), typeof(int), typeof(SearchText),
                new UIPropertyMetadata(1000));

            HintTextProperty = DependencyProperty.Register(
            nameof(HintText), typeof(string), typeof(SearchText),
            new UIPropertyMetadata("Set HintText property"));

            DefaultFocusedElementProperty = DependencyProperty.Register(
                nameof(DefaultFocusedElement), typeof(UIElement), typeof(SearchText));

        }

        public static void ActivateSearchCommand_Invoke(object sender, ExecutedRoutedEventArgs e)
        {
            if (sender is SearchText self)
                self.ActivateSearch();
        }

        public static void CancelSearchCommand_Invoke(object sender, ExecutedRoutedEventArgs e)
        {
            if (sender is SearchText self)
            {
                self.textBox.Text = "";
                self.CancelPreviousSearchFilterUpdateTask();
                self.UpdateFilterText();
                self.DeactivateSearch();
            }
        }

        private static UIElement GetFirstSelectedControl(Selector list)
            => list.SelectedItem == null ? null :
                list.ItemContainerGenerator.ContainerFromItem(list.SelectedItem) as UIElement;


        private static UIElement GetDefaultSelectedControl(Selector list)
            => list.ItemContainerGenerator.ContainerFromIndex(0) as UIElement;

        // Events
        public event EventHandler SearchTextFocused;
        public event EventHandler SearchTextUnfocused;
        public event EventHandler<string> SearchRequested;

        // Parts
        private Panel rootPanel;
        private TextBox textBox;
        private Button searchIcon;
        private Button closeButton;

        // Handlers
        // Field for click-outsides handling.
        private readonly MouseButtonEventHandler windowWideMouseButtonEventHandler;

        // Other fields.
        private CancellationTokenSource waitingSearchUpdateTaskCancellationTokenSource;

        public SearchText()
        {
            // Click events in the window will be previewed by
            // function OnWindowWideMouseEvent (defined later)
            // when the handler is on. Now it's off.
            windowWideMouseButtonEventHandler =
                new MouseButtonEventHandler(OnWindowWideMouseEvent);
        }

        // Properties
        public bool HandleClickOutsides
        {
            get => (bool)GetValue(HandleClickOutsidesProperty);
            set => SetValue(HandleClickOutsidesProperty, value);
        }

        public int UpdateDelayMillis
        {
            get => (int)GetValue(UpdateDelayMillisProperty);
            set => SetValue(UpdateDelayMillisProperty, value);
        }

        public string HintText
        {
            get => (string)GetValue(HintTextProperty);
            set => SetValue(HintTextProperty, value);
        }


        public UIElement DefaultFocusedElement
        {
            get => (UIElement)GetValue(DefaultFocusedElementProperty);
            set => SetValue(DefaultFocusedElementProperty, value);
        }

        //Event handler functions
        private void OnWindowWideMouseEvent(object sender, MouseButtonEventArgs e)
        {
            // By clicking outsides the search box deactivate the search box.
            if (!IsMouseOver) DeactivateSearch();
        }


        public void OnTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            SearchTextFocused?.Invoke(this, e);
            if (HandleClickOutsides)
                // Get window.
                Window.GetWindow(this).AddHandler(Window.PreviewMouseDownEvent, windowWideMouseButtonEventHandler);
        }


        public void OnTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            SearchTextUnfocused?.Invoke(this, e);
            if (HandleClickOutsides)
                Window.GetWindow(this).RemoveHandler(
                    Window.PreviewMouseDownEvent, windowWideMouseButtonEventHandler);
        }


        private void OnTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                CancelSearchCommand.Execute(null, this);
            else if (e.Key == Key.Enter)
            {
                CancelPreviousSearchFilterUpdateTask();
                UpdateFilterText();
            }
            else
            {
                CancelPreviousSearchFilterUpdateTask();
                // delay == 0: Update now;
                // delay < 0: Don't update except Enter or Esc;
                // delay > 0: Delay and update.
                var delay = UpdateDelayMillis;
                if (delay == 0) UpdateFilterText();
                else if (delay > 0)
                {
                    // // Delayed task.
                    // // https://stackoverflow.com/questions/15599884/how-to-put-delay-before-doing-an-operation-in-wpf
                    waitingSearchUpdateTaskCancellationTokenSource = new CancellationTokenSource();
                    Task.Delay(delay, waitingSearchUpdateTaskCancellationTokenSource.Token)
                       .ContinueWith(self => {
                           if (!self.IsCanceled) Dispatcher.Invoke(() => UpdateFilterText());
                       });
                }
            }
        }

        // Public interface
        public void ActivateSearch()
        {
            textBox?.Focus();
        }

        public void DeactivateSearch()
        {
            //Use keyboard focus instead.
            if (DefaultFocusedElement != null)
            {
                UIElement focusee = null;
                if (DefaultFocusedElement is Selector list)
                {
                    focusee = GetFirstSelectedControl(list);
                    if (focusee == null)
                        focusee = GetDefaultSelectedControl(list);
                }
                if (focusee == null) focusee = DefaultFocusedElement;
                Keyboard.Focus(focusee);
            }
            else
            {
                rootPanel.Focusable = true;
                Keyboard.Focus(rootPanel);
                rootPanel.Focusable = false;
            }
        }

        // Helper functions
        private void CancelPreviousSearchFilterUpdateTask()
        {
            if (waitingSearchUpdateTaskCancellationTokenSource != null)
            {
                waitingSearchUpdateTaskCancellationTokenSource.Cancel();
                waitingSearchUpdateTaskCancellationTokenSource.Dispose();
                waitingSearchUpdateTaskCancellationTokenSource = null;
            }
        }
        private void UpdateFilterText() => SearchRequested?.Invoke(this, textBox.Text);

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            if (textBox != null)
            {
                textBox.GotKeyboardFocus -= OnTextBox_GotFocus;
                textBox.LostKeyboardFocus -= OnTextBox_LostFocus;
                textBox.KeyUp -= OnTextBox_KeyUp;
            }

            rootPanel = GetTemplateChild(PartRootPanelName) as Panel;
            textBox = GetTemplateChild(PartTextBoxName) as TextBox;
            searchIcon = GetTemplateChild(PartSearchIconName) as Button;
            closeButton = GetTemplateChild(PartCloseButtonName) as Button;

            if (textBox != null)
            {
                textBox.GotKeyboardFocus += OnTextBox_GotFocus;
                textBox.LostKeyboardFocus += OnTextBox_LostFocus;
                textBox.KeyUp += OnTextBox_KeyUp;
            }
        }
    }
}
