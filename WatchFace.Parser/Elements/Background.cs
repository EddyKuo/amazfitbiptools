﻿using WatchFace.Parser.Attributes;
using WatchFace.Parser.Elements.BasicElements;

namespace WatchFace.Parser.Elements
{
    public class Background
    {
        [ParameterId(1)]
        public Image Image { get; set; }
    }
}