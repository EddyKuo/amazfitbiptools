﻿using System;
using System.Collections.Generic;
using NLog;
using WatchFace.Elements.BasicElements;
using WatchFace.Models;

namespace WatchFace.Elements.WeatherElements
{
    public class WeatherIcon
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        public Coordinates Coordinates { get; set; }
        public Coordinates Unknown3 { get; set; }
        public Coordinates Unknown4 { get; set; }

        public static WeatherIcon Parse(List<Parameter> descriptor, string path)
        {
            Logger.Trace("Reading {0}", path);
            if (descriptor == null)
                throw new ArgumentNullException(nameof(descriptor));

            var result = new WeatherIcon();
            foreach (var parameter in descriptor)
            {
                var currentPath = string.Concat(path, '.', parameter.Id.ToString());
                switch (parameter.Id)
                {
                    case 1:
                        result.Coordinates = Coordinates.Parse(parameter.Children, currentPath);
                        break;
                    case 3:
                        result.Unknown3 = Coordinates.Parse(parameter.Children, currentPath);
                        break;
                    case 4:
                        result.Unknown4 = Coordinates.Parse(parameter.Children, currentPath);
                        break;
                    default:
                        throw new InvalidParameterException(parameter, path);
                }
            }
            return result;
        }
    }
}