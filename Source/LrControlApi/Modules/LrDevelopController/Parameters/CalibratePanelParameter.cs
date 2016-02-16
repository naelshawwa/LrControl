﻿using System;
using LrControlApi.Common;

namespace LrControlApi.Modules.LrDevelopController.Parameters
{
    public class CalibratePanelParameter : Parameter<CalibratePanelParameter>, IDevelopControllerParameter
    {
        public static readonly CalibratePanelParameter Profile         = new CalibratePanelParameter("CameraProfile", "Profile", typeof(ProfileValue));
        public static readonly CalibratePanelParameter ShadowTint      = new CalibratePanelParameter("ShadowTint", "Shadows: Tint");
        public static readonly CalibratePanelParameter RedHue          = new CalibratePanelParameter("RedHue", "Red Primary: Hue");
        public static readonly CalibratePanelParameter RedSaturation   = new CalibratePanelParameter("RedSaturation", "Red Primary: Saturation");
        public static readonly CalibratePanelParameter GreenHue        = new CalibratePanelParameter("GreenHue", "Green Primary: Hue");
        public static readonly CalibratePanelParameter GreenSaturation = new CalibratePanelParameter("GreenSaturation", "Green Primary: Saturation");
        public static readonly CalibratePanelParameter BlueHue         = new CalibratePanelParameter("BlueHue", "Blue Primary: Hue");
        public static readonly CalibratePanelParameter BlueSaturation  = new CalibratePanelParameter("BlueSaturation", "Blue Primary: Saturation");

        private CalibratePanelParameter(string value, string name, Type valueType) : base(name, value, valueType)
        {
        }

        private CalibratePanelParameter(string value, string name) : base(name, value, typeof(int))
        {
        }

        public class ProfileValue : ClassEnum<string,ProfileValue>
        {
            public static readonly ProfileValue AdobeStandard   = new ProfileValue("Adobe Standard");
            public static readonly ProfileValue CameraClear     = new ProfileValue("Adobe Standard");
            public static readonly ProfileValue CameraDeep      = new ProfileValue("Adobe Standard");
            public static readonly ProfileValue CameraLandscape = new ProfileValue("Adobe Standard");
            public static readonly ProfileValue CameraLight     = new ProfileValue("Adobe Standard");
            public static readonly ProfileValue CameraNeutral   = new ProfileValue("Adobe Standard");
            public static readonly ProfileValue CameraPortrait  = new ProfileValue("Adobe Standard");
            public static readonly ProfileValue CameraStandard  = new ProfileValue("Adobe Standard");
            public static readonly ProfileValue CameraVivid     = new ProfileValue("Adobe Standard");

            static ProfileValue()
            {
                AddEnums(AdobeStandard, CameraClear, CameraDeep, CameraLandscape, CameraLight, CameraNeutral,
                    CameraPortrait, CameraStandard, CameraVivid);
            }

            private ProfileValue(string name) : base(name, name)
            {
            }
        }
    }
}