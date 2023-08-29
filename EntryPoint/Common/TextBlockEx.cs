using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows;
using static System.Net.Mime.MediaTypeNames;

namespace EntryPoint.Common
{
	public class TextBlockEx : UserControl
	{
		private const string _pattern = @"\[/?[a-zA-Z0-9#]+\]";
		private readonly Regex _regex;
		private readonly BrushConverter _brushConverter;
		private readonly TextBlock _textBlock;
		private readonly Label _label;

		public string FormattedText
		{
			get { return (string)GetValue(FormattedTextProperty); }
			set { SetValue(FormattedTextProperty, value); }
		}

		public SolidColorBrush SelectedBrush
		{
			get { return (SolidColorBrush)GetValue(SelectedBrushProperty); }
			set { SetValue(SelectedBrushProperty, value); }
		}

		public string Text
		{
			get { return (string)GetValue(TextProperty); }
			set { SetValue(TextProperty, value); }
		}

		public SolidColorBrush TextHighlightBrush
		{
			get { return (SolidColorBrush)GetValue(TextHighlightBrushProperty); }
			set { SetValue(TextHighlightBrushProperty, value); }
		}

		public SolidColorBrush UnderlineBrush
		{
			get { return (SolidColorBrush)GetValue(UnderlineBrushProperty); }
			set { SetValue(UnderlineBrushProperty, value); }
		}

		public string Label
		{
			get { return (string)GetValue(LabelProperty); }
			set { SetValue(LabelProperty, value); }
		}

		public static readonly DependencyProperty LabelProperty = DependencyProperty.Register("Label", typeof(string), typeof(TextBlockEx), new PropertyMetadata("", OnLabelChanged));
		public static readonly DependencyProperty UnderlineBrushProperty = DependencyProperty.Register("UnderlineBrush", typeof(SolidColorBrush), typeof(TextBlockEx), new PropertyMetadata(Brushes.Gray));
		public static readonly DependencyProperty TextHighlightBrushProperty = DependencyProperty.Register("TextHighlightBrush", typeof(SolidColorBrush), typeof(TextBlockEx), new PropertyMetadata(null, OnPropertyChanged));
		public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(TextBlockEx), new PropertyMetadata("", OnPropertyChanged));
		public static readonly DependencyProperty SelectedBrushProperty = DependencyProperty.Register("SelectedBrush", typeof(SolidColorBrush), typeof(TextBlockEx), new PropertyMetadata(Brushes.White, OnPropertyChanged));
		public static readonly DependencyProperty FormattedTextProperty = DependencyProperty.Register("FormattedText", typeof(string), typeof(TextBlockEx), new PropertyMetadata(null, OnPropertyChanged));

		private static void OnPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			((TextBlockEx)d).UpdateText();
		}

		protected override void OnTextInput(TextCompositionEventArgs e)
		{
			base.OnTextInput(e);
		}

		private static void OnLabelChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			((TextBlockEx)d).UpdateLabel();
		}

		private void UpdateLabel()
		{
			_label.Content = Label;
			if (string.IsNullOrEmpty(Label))
				_label.Visibility = Visibility.Collapsed;
			else
				_label.Visibility = Visibility.Visible;
		}

		private void UpdateText()
		{
			try
			{
				var fontWeight = FontWeights.Regular;
				var fontStyle = FontStyles.Normal;
				Brush highlightBrush = null, underlineBrush = null, deleteBrush = null, textBrush = Brushes.Black;

				_textBlock.Inlines.Clear();

				if (!String.IsNullOrEmpty(FormattedText))
				{
					var i = 0;
					var parts = _regex.Split(FormattedText);
					var codes = _regex.Matches(FormattedText);
					foreach (var part in parts)
					{
						if (!string.IsNullOrEmpty(part))
						{
							if (underlineBrush != null)
							{
								var tdc = new TextDecorationCollection();
								var td = new TextDecoration()
								{
									Pen = new Pen(underlineBrush, 1.0),
								};
								tdc.Add(td);
								_textBlock.Inlines.Add(new Run(part) { FontStyle = fontStyle, Background = highlightBrush, FontWeight = fontWeight, TextDecorations = tdc });
							}
							else if (deleteBrush != null)
							{
								var tdc = new TextDecorationCollection();
								var td = new TextDecoration()
								{
									Location=TextDecorationLocation.Strikethrough,
									Pen = new Pen(deleteBrush, 1.0)
								};
								tdc.Add(td);
								_textBlock.Inlines.Add(new Run(part) { FontStyle = fontStyle, Background = highlightBrush, FontWeight = fontWeight, Foreground=textBrush, TextDecorations = tdc });
							}
							else
								_textBlock.Inlines.Add(new Run(part) { FontStyle = fontStyle, Background = highlightBrush, FontWeight = fontWeight, Foreground=textBrush });
						}

						if (i < codes.Count)
						{
							var code = codes[i++];
							switch (code.Value)
							{
								case var text when text.StartsWith("[c"):
									highlightBrush = _brushConverter.ConvertFromString(text.Substring(2, 7)) as SolidColorBrush;
									break;
								case "[/c]":
									highlightBrush = null;
									break;
								case "[i]":
									fontStyle = FontStyles.Italic;
									break;
								case "[/i]":
									fontStyle = FontStyles.Normal;
									break;
								case "[d]":
									textBrush = deleteBrush = _brushConverter.ConvertFromString("#be1619") as SolidColorBrush;
									break;
								case "[/d]":
									textBrush = Brushes.Black;
									deleteBrush = null;
									break;
								case var text when text.StartsWith("[t"):
									textBrush = _brushConverter.ConvertFromString(text.Substring(2, 7)) as SolidColorBrush;
									break;
								case "[/t]":
									textBrush = Brushes.Black;
									break;
								case string text when text.StartsWith("[u"):
									if (text.Equals("[u]"))
										underlineBrush = UnderlineBrush;
									else
										underlineBrush = _brushConverter.ConvertFromString(text.Substring(2, 7)) as SolidColorBrush;
									break;
								case "[/u]":
									underlineBrush = null;
									break;
								case "[b]":
									fontWeight = FontWeights.Bold;
									break;
								case "[/b]":
									fontWeight = FontWeights.Regular;
									break;
							}
						}
					}
				}
				else if (!string.IsNullOrEmpty(Text))
				{
					if (TextHighlightBrush is null)
						_textBlock.Inlines.Add(new Run(Text));
					else
						_textBlock.Inlines.Add(new Run(Text) { Background = TextHighlightBrush });
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		public TextBlockEx()
		{
			_regex = new Regex(_pattern, RegexOptions.IgnoreCase);
			_brushConverter = new BrushConverter();

			_label = new Label()
			{
				FontSize = 11,
				Padding = new Thickness(0),
				FontWeight = FontWeights.Regular,
				Margin = new Thickness(0, 0, 0, 5),
				Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#616163")),
				Visibility = Visibility.Collapsed
			};

			_textBlock = new TextBlock()
			{
				TextWrapping = TextWrapping.Wrap,
				HorizontalAlignment = HorizontalAlignment.Left,
				VerticalAlignment = VerticalAlignment.Top,
				SnapsToDevicePixels = true
			};

			var panel = new StackPanel();
			panel.Children.Add(_label);
			panel.Children.Add(_textBlock);
			this.AddChild(panel);
		}
	}
}
