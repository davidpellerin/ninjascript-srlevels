#region Using declarations
using NinjaTrader.Gui;
using NinjaTrader.Gui.Tools;
using NinjaTrader.NinjaScript.DrawingTools;
using System;
using System.ComponentModel.DataAnnotations;
using System.Windows;
using System.Windows.Media;
#endregion

//This namespace holds Indicators in this folder and is required. Do not change it. 
namespace NinjaTrader.NinjaScript.Indicators
{
    [Gui.CategoryOrder("Parameters", 1)]
    [Gui.CategoryOrder("Visual", 2)]
    public class SupportResisanceLines : Indicator
    {
        protected override void OnStateChange()
        {
            if (State == State.SetDefaults)
            {
                Description = @"Adds Support & Resistance Lines Based on Key Value Pairs";
                Name = "SupportResisanceLines";
                Calculate = Calculate.OnBarClose;
                IsOverlay = true;
                DisplayInDataBox = true;
                DrawOnPricePanel = true;
                DrawHorizontalGridLines = true;
                DrawVerticalGridLines = true;
                PaintPriceMarkers = true;
                ScaleJustification = NinjaTrader.Gui.Chart.ScaleJustification.Right;
                //Disable this property if your indicator requires custom values that cumulate with each new market data event. 
                //See Help Guide for additional information.
                IsSuspendedWhileInactive = true;
                DisplayLabels = true;
                LabelOffset = -10;
                LineThickness = 1;
                ResistanceColor = Brushes.Red;
                SupportColor = Brushes.Lime;
                LabelColor = Brushes.White;
                LabelAlignment = TextAlignment.Left;
                NormalLineStyle = DashStyleHelper.Dash;
                StrongLineStyle = DashStyleHelper.Solid;
                LabelFont = new SimpleFont("Arial", 14);
            }
        }

        protected override void OnBarUpdate()
        {
            var levelStrings = InputLevels.Split(',');

            foreach (var levelString in levelStrings)
            {
                var parts = levelString.Split('=');
                if (parts.Length == 2)
                {
                    string labelPart = parts[0];
                    string pricePart = parts[1];
                    bool isResistance = labelPart.Contains("R");
                    bool isStrong = pricePart.Contains("+");
                    decimal price;
                    if (decimal.TryParse(pricePart.Trim('$', '+'), out price))
                    {
                        Draw.HorizontalLine(this, "Line-" + labelPart, (double)price, isResistance ? ResistanceColor : SupportColor, isStrong ? StrongLineStyle : NormalLineStyle, LineThickness);
                        if (DisplayLabels)
                        {
                            Draw.Text(this, "Label" + labelPart, true, String.Concat(labelPart, isStrong ? "+" : ""), (int)LabelOffset, (double)price, 0, LabelColor, LabelFont, LabelAlignment, null, null, 0);
                        }
                    }
                }
            }
        }

        [NinjaScriptProperty]
        [Display(Name = "Input Levels", Description = "", Order = 1, GroupName = "Parameters")]
        public string InputLevels
        {
            get;
            set;
        }

        [NinjaScriptProperty]
        [Display(Name = "Display Labels?", Description = "", Order = 2, GroupName = "Parameters")]
        public bool DisplayLabels
        {
            get;
            set;
        }

        [NinjaScriptProperty]
        [Display(Name = "Label Offset", Description = "", Order = 1, GroupName = "Visual")]
        public int LabelOffset
        {
            get;
            set;
        }

        [NinjaScriptProperty]
        [Display(Name = "Line Thickness", Description = "", Order = 2, GroupName = "Visual")]
        public int LineThickness
        {
            get;
            set;
        }

        [NinjaScriptProperty]
        [Display(Name = "Resistance Color", Description = "Color for the Resistance Lines", Order = 3, GroupName = "Visual")]
        public Brush ResistanceColor
        {
            get;
            set;
        }

        [NinjaScriptProperty]
        [Display(Name = "Support Color", Description = "Color for the Support Lines", Order = 4, GroupName = "Visual")]
        public Brush SupportColor
        {
            get;
            set;
        }

        [NinjaScriptProperty]
        [Display(Name = "Label Font", Description = "Label Font", Order = 5, GroupName = "Visual")]
        public SimpleFont LabelFont
        {
            get;
            set;
        }

        [NinjaScriptProperty]
        [Display(Name = "Label Color", Description = "Label Color", Order = 6, GroupName = "Visual")]
        public Brush LabelColor
        {
            get;
            set;
        }

        [NinjaScriptProperty]
        [Display(Name = "Label Alignment", Description = "Label Alignment", Order = 7, GroupName = "Visual")]
        public TextAlignment LabelAlignment
        {
            get;
            set;
        }

        [NinjaScriptProperty]
        [Display(Name = "Normal Lines", Description = "Normal Line Syle", Order = 8, GroupName = "Visual")]
        public DashStyleHelper NormalLineStyle
        {
            get;
            set;
        }

        [NinjaScriptProperty]
        [Display(Name = "Strong Lines", Description = "Strong Line Syle", Order = 9, GroupName = "Visual")]
        public DashStyleHelper StrongLineStyle
        {
            get;
            set;
        }
    }
}
