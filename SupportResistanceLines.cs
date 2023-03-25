#region Using declarations
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Xml.Serialization;
using NinjaTrader.Cbi;
using NinjaTrader.Gui;
using NinjaTrader.Gui.Chart;
using NinjaTrader.Gui.SuperDom;
using NinjaTrader.Gui.Tools;
using NinjaTrader.Data;
using NinjaTrader.NinjaScript;
using NinjaTrader.Core.FloatingPoint;
using NinjaTrader.NinjaScript.DrawingTools;
using NinjaTrader.Core;
#endregion

namespace NinjaTrader.NinjaScript.Indicators
{
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
				DrawHorizontalGridLines	= true;
				DrawVerticalGridLines = true;
				PaintPriceMarkers = true;
				ScaleJustification= NinjaTrader.Gui.Chart.ScaleJustification.Right;
				IsSuspendedWhileInactive = true;
				DisplayLabels = true;
				LabelOffset = -10;
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
						Draw.HorizontalLine(this, "Line-" + labelPart, (double)price, isResistance ? Brushes.Red : Brushes.LimeGreen, isStrong ? DashStyleHelper.Solid : DashStyleHelper.Dash, 2);
						if (DisplayLabels)
						{
							Draw.Text(this, "Label" + labelPart, true, String.Concat(labelPart, isStrong ? "+" : ""), (int)LabelOffset, (double)price, 0, Brushes.White, new SimpleFont("Arial", 14), TextAlignment.Left, null, null, 0);
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
		[Display(Name = "Label Offset", Description = "", Order = 3, GroupName = "Parameters")]
		public int LabelOffset
		{
			get;
			set;
		}
  	}
}
