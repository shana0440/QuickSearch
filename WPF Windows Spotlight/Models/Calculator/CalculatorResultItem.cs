﻿using System;
using System.Windows.Controls;
using WPF_Windows_Spotlight.Models.ResultItemsFactory;
using System.Windows;
using System.Windows.Media;

namespace WPF_Windows_Spotlight.Models.Calculator
{
    class CalculatorResultItem : ResultItem
    {
        string _expression;

        public CalculatorResultItem(string answer, string expression)
        {
            GroupName = "計算機";
            Priority = 999;
            Title = answer;
            _expression = String.Format("{0} =", expression);
            _icon = Properties.Resources.calculator_icon;
        }

        public override void GenerateContent(StackPanel contentView)
        {
            contentView.Children.Clear();
            Label expression = new Label();
            expression.Content = _expression;
            expression.FontSize = 24;

            expression.Foreground = Brushes.LightGray;
            expression.HorizontalContentAlignment = HorizontalAlignment.Center;
            Thickness expMargin = new Thickness(0, 100, 0, 10);
            expression.Margin = expMargin;
            contentView.Children.Add(expression);

            Border hr = new Border();
            var hrColor = (Color)ColorConverter.ConvertFromString(Config.SearchbarBorderColor);
            hr.Height = 1;
            hr.BorderBrush = new SolidColorBrush(hrColor);
            hr.BorderThickness = new Thickness(0, 1, 0, 0);
            hr.Width = contentView.Width - 30;
            hr.Margin = new Thickness(15, 10, 15, 10);
            contentView.Children.Add(hr);

            Label answer = new Label();
            answer.Content = Title;
            answer.FontSize = 36;
            answer.HorizontalContentAlignment = HorizontalAlignment.Center;
            answer.Foreground = Brushes.White;
            contentView.Children.Add(answer);
        }

        public override void OpenResource()
        {
        }
    }
}
