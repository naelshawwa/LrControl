﻿using System;
using LrControlApi.Common;

namespace LrControlApi.LrDevelopController.Parameters
{
    public class AdjustPanelParameter : Parameter<AdjustPanelParameter>, IDevelopControllerParameter
    {
        public static readonly AdjustPanelParameter WhiteBalance = new AdjustPanelParameter("WhiteBalance", "Whitebalance", typeof(WhiteBalanceValue));
        public static readonly AdjustPanelParameter Temperature  = new AdjustPanelParameter("Temperature", "Whitebalance: Temperature");
        public static readonly AdjustPanelParameter Tint         = new AdjustPanelParameter("Tint", "Whitebalance: Tint");
        public static readonly AdjustPanelParameter Exposure     = new AdjustPanelParameter("Exposure", "Tone: Exposure");
        public static readonly AdjustPanelParameter Contrast     = new AdjustPanelParameter("Contrast", "Tone: Contrast");
        public static readonly AdjustPanelParameter Highlights   = new AdjustPanelParameter("Highlights", "Tone: Highlights");
        public static readonly AdjustPanelParameter Shadows      = new AdjustPanelParameter("Shadows", "Tone: Shadows");
        public static readonly AdjustPanelParameter Whites       = new AdjustPanelParameter("Whites", "Tone: Whites");
        public static readonly AdjustPanelParameter Blacks       = new AdjustPanelParameter("Blacks", "Tone: Blacks");
        public static readonly AdjustPanelParameter Clarity      = new AdjustPanelParameter("Clarity", "Presence: Clarity");
        public static readonly AdjustPanelParameter Vibrance     = new AdjustPanelParameter("Vibrance", "Presence: Vibrance");
        public static readonly AdjustPanelParameter Saturation   = new AdjustPanelParameter("Saturation", "Presence: Saturation");
        
        private AdjustPanelParameter(string name, string displayName, Type valueType) : base(name, displayName, valueType)
        {
        }

        private AdjustPanelParameter(string name, string displayName) : base(name, displayName, typeof(int))
        {
        }

        public class WhiteBalanceValue : ClassEnum<string,WhiteBalanceValue>
        {
            public static readonly WhiteBalanceValue AsShot      = new WhiteBalanceValue("As Shot");
            public static readonly WhiteBalanceValue Auto        = new WhiteBalanceValue("Auto");
            public static readonly WhiteBalanceValue Cloudy      = new WhiteBalanceValue("Cloudy");
            public static readonly WhiteBalanceValue Daylight    = new WhiteBalanceValue("Daylight");
            public static readonly WhiteBalanceValue Flash       = new WhiteBalanceValue("Flash");
            public static readonly WhiteBalanceValue Flourescent = new WhiteBalanceValue("Flourescent");
            public static readonly WhiteBalanceValue Shade       = new WhiteBalanceValue("Shade");
            public static readonly WhiteBalanceValue Tungsten    = new WhiteBalanceValue("Tungsten");

            private WhiteBalanceValue(string name) : base(name, name)
            {
            }
        }
    }
}