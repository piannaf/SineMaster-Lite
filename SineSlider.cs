/* File: SineSlider.cs
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
using System.Windows; //RoutedPropertyChangedEventHandler
using System.Windows.Controls;

namespace SineMaster
{
    public class SineSlider:Grid
    {
        #region Instance Variables
        Label label = new Label();
        public Slider slider = new Slider();
        Label curVal = new Label();

        double _defaultVal;
        #endregion

        #region Constructors
        public SineSlider(string slabel, double min, double max)
        {

            #region grid
            ColumnDefinition sliderLabels = new ColumnDefinition();
            sliderLabels.MaxWidth = 70;
            sliderLabels.MinWidth = 60;
            this.ColumnDefinitions.Add(sliderLabels);

            ColumnDefinition sliderSlider = new ColumnDefinition();
            sliderSlider.MinWidth = 100;
            this.ColumnDefinitions.Add(sliderSlider);

            ColumnDefinition sliderValues = new ColumnDefinition();
            sliderValues.MaxWidth = 55;
            sliderLabels.MinWidth = 50;
            this.ColumnDefinitions.Add(sliderValues);

            #endregion

            #region label
            label.Content = slabel;
            label.MouseDoubleClick += new System.Windows.Input.MouseButtonEventHandler(label_MouseDoubleClick);

            Grid.SetRow(label, 0);
            Grid.SetColumn(label, 0);
            this.Children.Add(label);
            #endregion

            #region slider
            slider.Minimum = min;
            slider.Maximum = max;
            slider.LargeChange = 0.1;
            slider.SmallChange = 0.01;
            slider.ValueChanged += new RoutedPropertyChangedEventHandler<double>(slider_ValueChanged);
            
            Grid.SetRow(slider, 0);
            Grid.SetColumn(slider, 1);
            this.Children.Add(slider);
            #endregion

            #region current value
            curVal.Content = 0;
            Grid.SetRow(curVal, 0);
            Grid.SetColumn(curVal, 2);
            this.Children.Add(curVal);
            #endregion
        }
        #endregion

        #region Properties
        public double DefaultVal
        { 
            get { return _defaultVal; }
            set { _defaultVal = value; }
        }
        #endregion

        #region Event Handlers
        void slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            curVal.Content = decimal.Round((decimal)e.NewValue, 2, MidpointRounding.AwayFromZero);
        }

        void label_MouseDoubleClick(object sender, System.Windows.Input.InputEventArgs e)
        {
            this.slider.Value = DefaultVal;
        }
        #endregion

    }

    public class SineSliderRow : RowDefinition
    {
        public SineSliderRow()
        {
            this.Height = new GridLength(30.0);
        }
    }
}