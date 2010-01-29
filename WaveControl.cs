/* File: WaveControl.cs
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
using System.Windows; //GridLength
using System.Windows.Controls; //Grid, Canvas
using System.Windows.Media; //Brushes
using System.Windows.Shapes; //PolyLine

namespace SineMaster
{
    public class WaveControl : Grid
    {
        public struct Wave
        {
            public double Amplitude;
            public double Frequency;
            public double Phase;
        }

        #region Instance Variables
        Canvas waveCanvas = new Canvas();
        Polyline sine = new Polyline();

        SineSlider phase = new SineSlider("Phase", -180.0, 180.0);
        SineSlider amplitude = new SineSlider("Amplitude", 0.0, 1.0);
        SineSlider frequency = new SineSlider("Frequency", 0.0, 10.0);

        Wave thisWave;
        #endregion

        #region Event Handling
        public class WaveChangeArgs : System.EventArgs
        {
            private Wave _newWave;

            public Wave NewWave
            {
                get { return _newWave; }
                set { _newWave = value; }
            }

            public WaveChangeArgs(Wave newWave)
            {
                NewWave = newWave;
            }
        }

        public delegate void WaveChangeHandler(object sender, WaveChangeArgs e);

        public event WaveChangeHandler OnWaveChange;
        #endregion

        #region Constructors
        public WaveControl()
        {
            thisWave.Amplitude = 0.0;
            thisWave.Frequency = 1.0;
            thisWave.Phase = 0.0;

            #region grid
            #region Columns
            ColumnDefinition sinSlider = new ColumnDefinition();
            sinSlider.MinWidth = 205;
            this.ColumnDefinitions.Add(sinSlider);

            ColumnDefinition canvasHolder = new ColumnDefinition();
            canvasHolder.Width = new GridLength(100.0);
            this.ColumnDefinitions.Add(canvasHolder);
            #endregion

            #region Rows
            this.RowDefinitions.Add(new SineSliderRow());
            this.RowDefinitions.Add(new SineSliderRow());
            this.RowDefinitions.Add(new SineSliderRow());
            #endregion
            #endregion
            
            #region Sliders
            #region Amplitude Slider
            amplitude.DefaultVal = thisWave.Amplitude;
            amplitude.slider.Name = "Amplitude";
            amplitude.slider.Value = amplitude.DefaultVal;
            amplitude.slider.ValueChanged += new RoutedPropertyChangedEventHandler<double>(slider_ValueChanged);

            Grid.SetRow(amplitude, 0);
            Grid.SetColumn(amplitude, 0);
            this.Children.Add(amplitude);
            #endregion

            #region Frequency Slider
            frequency.slider.Name = "Frequency";
            frequency.DefaultVal = thisWave.Frequency;
            frequency.slider.Value = frequency.DefaultVal;
            frequency.slider.ValueChanged += new RoutedPropertyChangedEventHandler<double>(slider_ValueChanged);

            Grid.SetRow(frequency, 1);
            Grid.SetColumn(frequency, 0);
            this.Children.Add(frequency);
            #endregion

            #region Phase Slider
            phase.DefaultVal = thisWave.Phase;
            phase.slider.Name = "Phase";
            phase.slider.Value = phase.DefaultVal;
            phase.slider.ValueChanged += new RoutedPropertyChangedEventHandler<double>(slider_ValueChanged);

            Grid.SetRow(phase, 2);
            Grid.SetColumn(phase, 0);
            this.Children.Add(phase);
            #endregion
            #endregion

            #region WaveCanvas
            waveCanvas.Background = Brushes.White;
            waveCanvas.Width = 100;
            waveCanvas.Height = 100;

            Grid.SetRow(waveCanvas, 0);
            Grid.SetColumn(waveCanvas, 3);
            Grid.SetRowSpan(waveCanvas, 3);
            this.Children.Add(waveCanvas);

            sine.Stroke = Brushes.Black;
            sine.StrokeThickness = 1;
            waveCanvas.Children.Add(sine);

            renderWave(100.0, 100.0, 1);
            #endregion
        }
        #endregion

        #region Properties
        public Brush Stroke 
        {
            get { return sine.Stroke; }
            set { sine.Stroke = value; } 
        }
        #endregion

        #region Event Handlers
        void slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            updateValues((Slider)sender, e.NewValue);
            renderWave(100.0, 100.0, 1);
        }

        #endregion

        #region Methods

        void updateValues(Slider slider, double NewValue)
        {
            switch (slider.Name)
            {
                case "Amplitude":
                    thisWave.Amplitude = NewValue;
                    break;
                case "Frequency":
                    thisWave.Frequency = NewValue;
                    break;
                case "Phase":
                    thisWave.Phase = NewValue;
                    break;
            }
            OnWaveChange(this, new WaveChangeArgs(thisWave));
        }

        protected void renderWave(double Width, double Height, Int32 Resolution)
        {
            sine.Points.Clear();
            sine.Points = plotWave(Width, Height, Resolution);
        }

        public PointCollection plotWave(double Width, double Height, Int32 Resolution)
        {
            PointCollection points = new PointCollection();

            for (Int32 x = 0; x < Width; x += Resolution)
            {
                points.Add(new Point(x, SinFunction(x, Width, Height)));
            }

            return points;
        }

        double SinFunction(double x, double Width, double Height)
        {
            return Height / 2 - 
                thisWave.Amplitude * Height / 2 *
                Math.Sin(thisWave.Frequency * x * Math.PI / Width + thisWave.Phase / 180 * Math.PI);
        }
        #endregion
    }
}