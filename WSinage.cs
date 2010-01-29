/* File: WSinage.cs
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
using System.Windows; //window
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes; //PolyLine
using System.Collections.Generic; //List
using System.Windows.Markup;

namespace SineMaster
{
    public class WSinage : Window
    {
        #region Instance Variables
        DockPanel layout = new DockPanel();
        Grid mainGrid = new Grid();


        #endregion
        List<Polyline> Lines = new List<Polyline>();
        List<WaveControl> Waves = new List<WaveControl>();
        
        Canvas allWaves = new Canvas();

        #region Constructors
        public WSinage()
        {
            this.Content = layout;

            #region Window Properties
            this.Title = "SineMaster Lite";
            this.Width = 325;
            this.Height = 350;

            this.ResizeMode = ResizeMode.NoResize;
            this.MinHeight = 350;
            this.MinWidth = 325;

            this.Background = Brushes.SteelBlue;

            this.SizeChanged += new SizeChangedEventHandler(WSinage_SizeChanged);
            #endregion

            #region Menu
            TopMenu topMenu = new TopMenu();

            DockPanel.SetDock(topMenu, Dock.Top);
            layout.Children.Add(topMenu);

            #endregion

            #region Grid Properties
            mainGrid.Margin = new Thickness(5.0);

            #region Columns
            mainGrid.ColumnDefinitions.Add(new ColumnDefinition());
            #endregion

            #region Rows
            RowDefinition wcRow = new RowDefinition();
            wcRow.MaxHeight = 105;
            mainGrid.RowDefinitions.Add(wcRow);

            RowDefinition allWavesRow = new RowDefinition();
            allWavesRow.MaxHeight = 205;
            mainGrid.RowDefinitions.Add(allWavesRow);
            
            layout.Children.Add(mainGrid);
            #endregion
            #endregion

            Waves.Add(new WaveControl());

            Grid.SetColumn((WaveControl)Waves[0], 0);
            Grid.SetRow(Waves[0], 0);
            mainGrid.Children.Add(Waves[0]);

            
            //temp
            this.Show();
            allWaves.Margin = new Thickness(5);
            allWaves.Background = Brushes.White;

            Grid.SetColumn(allWaves, 0);
            Grid.SetRow(allWaves, 1);
            mainGrid.Children.Add(allWaves);

            Lines.Add(new Polyline());
            Lines[0].Stroke = Waves[0].Stroke;
            Lines[0].StrokeThickness = 1;
            allWaves.Children.Add(Lines[0]);

            RenderAllWaves();

            Waves[0].OnWaveChange += new WaveControl.WaveChangeHandler(WaveChanged);
        }
        #endregion

        #region Event Handlers
        void WaveChanged(object sender, WaveControl.WaveChangeArgs e)
        {
                Lines[0].Points = ((WaveControl)sender).plotWave(allWaves.Width, allWaves.Height, 2);
        }
        #endregion

        void RenderAllWaves()
        {
            double width = this.ActualWidth;
            double height = this.ActualHeight;

            allWaves.Width = width - 30;
            allWaves.Height = height - 170;

            if(Lines.Count > 0)
                Lines[0].Points = Waves[0].plotWave(allWaves.Width, allWaves.Height, 2);
        }

        void WSinage_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            RenderAllWaves();
        }
    }
}