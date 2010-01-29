/* File: TopMenu.cs
 * 
 * SineMaster Lite
 * 
 * Copyright (c) 2010 Justin Mancinelli <jmancine@gmail.com>
 * 
 * Permission is hereby granted, free of charge, to any person
 * obtaining a copy of this software and associated documentation
 * files (the "Software"), to deal in the Software without
 * restriction, including without limitation the rights to use,
 * copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the
 * Software is furnished to do so, subject to the following
 * conditions:
 * 
 * The above copyright notice and this permission notice shall be
 * included in all copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
 * EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
 * OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
 * NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
 * HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
 * WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
 * FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
 * OTHER DEALINGS IN THE SOFTWARE.
 */

using System;
using System.Windows;
using System.Windows.Controls; //Menu, Grid, Canvas, TextBlock
using System.Windows.Media; //Brushes
using System.Windows.Shapes; //PolyLine

namespace SineMaster
{
    public class TopMenu : Menu
    {
        MenuItem file = new MenuItem();
        MenuItem help = new MenuItem();
        public TopMenu()
        {
            this.Height = 20;

            FileMenu();
            HelpMenu();

        }

        
        void FileMenu()
        {
            file.Header = "File";
            this.Items.Add(file);

            MenuItem exit = new MenuItem();
            exit.Click += new RoutedEventHandler(OnMenuExit);
            exit.Header = "Exit";
            file.Items.Add(exit);
        }

        void HelpMenu()
        {
            help.Header = "Help";
            this.Items.Add(help);

            MenuItem about = new MenuItem();
            about.Click += new RoutedEventHandler(OnMenuAbout);
            about.Header = "About";
            help.Items.Add(about);
        }

        #region Event Handlers
        void OnMenuExit(object sender, RoutedEventArgs e)
        {
            ((Window)((DockPanel)this.Parent).Parent).Close();
        }

        void OnMenuAbout(object sender, RoutedEventArgs e)
        {
            Window parent = (Window)((DockPanel)this.Parent).Parent;

            AboutBox aboutBox = new AboutBox(parent);
            aboutBox.ShowDialog();

            //MessageBox.Show(parent, message, parent.Title, MessageBoxButton.OK, MessageBoxImage.Information);

        }
        #endregion
    }

    class AboutBox : Window
    {
        #region Instance Variables
        Grid aboutGrid = new Grid();
        Canvas aboutCanvas = new Canvas();
        TextBlock aboutMessage = new TextBlock();
        #endregion

        #region Constructors
        public AboutBox()
        {
            ConfigWindow();
        }

        public AboutBox(Window parent)
        {
            this.Owner = parent;
            ConfigWindow();
        }

        #endregion

        private void ConfigWindow()
        {
            this.ShowInTaskbar = false;
            this.SizeToContent = SizeToContent.WidthAndHeight;
            this.ResizeMode = ResizeMode.NoResize;
            this.WindowStyle = WindowStyle.ThreeDBorderWindow;
            this.Title = "About " + this.Owner.Title;
            this.Background = Brushes.BurlyWood;

            this.Content = aboutGrid;

            ConfigGrid();
            ConfigCanvas();
            ConfigureMessage();
            ConfigureButtons();
        }

        private void ConfigGrid()
        {
            aboutGrid.RowDefinitions.Add(new RowDefinition());
            aboutGrid.RowDefinitions.Add(new RowDefinition());
            aboutGrid.RowDefinitions.Add(new RowDefinition());
            aboutGrid.ColumnDefinitions.Add(new ColumnDefinition());
            aboutGrid.ColumnDefinitions.Add(new ColumnDefinition());

            Grid.SetRow(aboutCanvas, 0);
            Grid.SetColumn(aboutCanvas, 0);
            Grid.SetColumnSpan(aboutCanvas, 2);
            aboutGrid.Children.Add(aboutCanvas);

            Grid.SetRow(aboutMessage, 1);
            Grid.SetColumn(aboutMessage, 0);
            Grid.SetColumnSpan(aboutMessage, 2);
            aboutGrid.Children.Add(aboutMessage);
        }

        private void ConfigCanvas()
        {
            aboutCanvas.Height = 100;
            aboutCanvas.Width = 300;
            aboutCanvas.Background = Brushes.White;

            #region sine1
            Polyline sine1 = new Polyline();
            sine1.Stroke = Brushes.Green;
            sine1.StrokeThickness = 2;
            PointCollection sine1Points = new PointCollection();

            for (Int32 x = 0; x < 300; x += 1)
            {
                sine1Points.Add(new Point(x, 100 / 2 - 
                    1 * 100 / 2 *
                    Math.Sin(2 * x * Math.PI / 300 + 0 / 180 * Math.PI)));
            }

            sine1.Points = sine1Points;
            aboutCanvas.Children.Add(sine1);
            #endregion

            #region sine2
            Polyline sine2 = new Polyline();
            sine2.Stroke = Brushes.SteelBlue;
            sine2.StrokeThickness = 2;
            PointCollection sine2Points = new PointCollection();

            for (Int32 x = 0; x < 300; x += 1)
            {
                sine2Points.Add(new Point(x, 100 / 2 -
                    0.5 * 100 / 2 *
                    Math.Sin((3 * x * Math.PI / 300) + (90.0 / 180 * Math.PI))));
            }

            sine2.Points = sine2Points;
            aboutCanvas.Children.Add(sine2);
            #endregion
            
            #region sine3
            Polyline sine3 = new Polyline();
            sine3.Stroke = Brushes.RosyBrown;
            sine3.StrokeThickness = 2;
            PointCollection sine3Points = new PointCollection();

            for (Int32 x = 0; x < 300; x += 1)
            {
                sine3Points.Add(new Point(x, 100 / 2 -
                    0.85 * 100 / 2 *
                    Math.Sin((1 * x * Math.PI / 300) + (-110.0 / 180 * Math.PI))));
            }

            sine3.Points = sine3Points;
            aboutCanvas.Children.Add(sine3);
            #endregion
        }

        private void ConfigureMessage()
        {
            string message1 = this.Owner.Title + " - By Justin Mancinelli\n\n";
            message1 += this.Owner.Title + " is a simple program allowing students to visualize aspects of the sine wave.";
            aboutMessage.Inlines.Add(message1);

            Separator separator = new Separator();
            separator.Height = 30;
            separator.Width = 300;
            aboutMessage.Inlines.Add(separator);

            string message2 = "\nInstructions:\n\nUse the sliders to manipulate the wave.";
            message2 += "\n\nDouble-click the slider's name to reset the slider to its default value.";
            aboutMessage.Inlines.Add(message2);

            aboutMessage.Height = 200;
            aboutMessage.Width = 300;
            aboutMessage.TextWrapping = TextWrapping.Wrap;
            aboutMessage.FontSize = 12;
            aboutMessage.Padding = new Thickness(5, 10, 5, 5);
        }

        private void ConfigureButtons()
        {
            #region Close
            Button close = new Button();
            close.Height = 25;
            close.Width = 75;
            close.Content = "Close";
            close.Click += new RoutedEventHandler((object sender, RoutedEventArgs e) => { this.Close(); });
            close.Margin = new Thickness(0, 10, 0, 20);

            Grid.SetRow(close, 2);
            Grid.SetColumn(close, 0);
            aboutGrid.Children.Add(close);
            #endregion

            #region Donate
            Button donate = new Button();
            donate.Height = 25;
            donate.Width = 75;
            donate.Content = "Donate";
            donate.Click += new RoutedEventHandler((object sender, RoutedEventArgs e) =>
                { System.Diagnostics.Process.Start("http://jmancine.wordpress.com/donate/"); });
            donate.Margin = new Thickness(0, 10, 0, 20);

            Grid.SetRow(donate, 2);
            Grid.SetColumn(donate, 1);
            aboutGrid.Children.Add(donate);
            #endregion
        }
    }
}