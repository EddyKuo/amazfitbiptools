﻿using System;
using System.Collections.Generic;
using System.Reflection;
using NLog;
using WatchFace.Parser.Models;

namespace WatchFace.Parser.Utils
{
    public class ParametersConverter
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public static List<Parameter> Build<T>(T serializable, string path = "")
        {
            var result = new List<Parameter>();
            var currentType = typeof(T);

            if (!string.IsNullOrEmpty(path))
                Logger.Trace("{0} '{1}'", path, currentType.Name);
            foreach (var kv in ElementsHelper.SortedProperties<T>())
            {
                var id = kv.Key;
                var currentPath = string.IsNullOrEmpty(path)
                    ? id.ToString()
                    : string.Concat(path, '.', id.ToString());

                var propertyInfo = kv.Value;
                var propertyType = propertyInfo.PropertyType;
                dynamic propertyValue = propertyInfo.GetValue(serializable, null);

                if (propertyValue == null)
                    continue;

                if (propertyType == typeof(long))
                {
                    Logger.Trace("{0} '{1}': {2}", currentPath, propertyInfo.Name, propertyValue);
                    result.Add(new Parameter(id, propertyValue));
                }
                else if (propertyType.IsGenericType && propertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                {
                    Logger.Trace("{0} '{1}': {2}", currentPath, propertyInfo.Name, propertyValue);
                    result.Add(new Parameter(id, propertyValue));
                }
                else if (propertyType.IsGenericType && propertyType.GetGenericTypeDefinition() == typeof(List<>))
                {
                    foreach (var item in propertyValue)
                        result.Add(new Parameter(id, Build(item, currentPath)));
                }
                else
                {
                    result.Add(new Parameter(id, Build(propertyValue, currentPath)));
                }
            }
            return result;
        }

        public static T Parse<T>(List<Parameter> descriptor, string path = "") where T : new()
        {
            var properties = ElementsHelper.SortedProperties<T>();
            var result = new T();
            var currentType = typeof(T);

            var thisMethod = typeof(ParametersConverter).GetMethod(nameof(Parse));

            if (!string.IsNullOrEmpty(path))
                Logger.Trace("{0} '{1}'", path, currentType.Name);
            foreach (var parameter in descriptor)
            {
                var currentPath = string.IsNullOrEmpty(path)
                    ? parameter.Id.ToString()
                    : string.Concat(path, '.', parameter.Id.ToString());
                if (!properties.ContainsKey(parameter.Id))
                    throw new ArgumentException($"Parameter {parameter.Id} isn't supported for {currentType.Name}");

                var propertyInfo = properties[parameter.Id];
                var propertyType = propertyInfo.PropertyType;

                if (propertyType == typeof(long) || propertyType.IsGenericType &&
                    propertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                {
                    Logger.Trace("{0} '{1}': {2}", currentPath, propertyInfo.Name, parameter.Value);
                    dynamic propertyValue = propertyInfo.GetValue(result, null);

                    if (propertyType.IsGenericType && propertyValue != null)
                        throw new ArgumentException($"Parameter {parameter.Id} is already set for {currentType.Name}");

                    if (!propertyType.IsGenericType && propertyValue != 0)
                        throw new ArgumentException($"Parameter {parameter.Id} is already set for {currentType.Name}");

                    propertyInfo.SetValue(result, parameter.Value, null);
                }
                else if (propertyType.IsGenericType && propertyType.GetGenericTypeDefinition() == typeof(List<>))
                {
                    dynamic propertyValue = propertyInfo.GetValue(result, null);
                    if (propertyValue == null)
                    {
                        propertyValue = Activator.CreateInstance(propertyType);
                        propertyInfo.SetValue(result, propertyValue, null);
                    }

                    try
                    {
                        var generic = thisMethod.MakeGenericMethod(propertyType.GetGenericArguments()[0]);
                        dynamic parsedValue = generic.Invoke(null,
                            new dynamic[] {parameter.Children, currentPath});
                        propertyValue.Add(parsedValue);
                    }
                    catch (TargetInvocationException e)
                    {
                        throw e.InnerException;
                    }
                }
                else
                {
                    dynamic propertyValue = propertyInfo.GetValue(result, null);
                    if (propertyValue != null)
                        throw new ArgumentException($"Parameter {parameter.Id} is already set for {currentType.Name}");

                    try
                    {
                        var generic = thisMethod.MakeGenericMethod(propertyType);
                        dynamic parsedValue = generic.Invoke(null, new object[] {parameter.Children, currentPath});
                        propertyInfo.SetValue(result, parsedValue, null);
                    }
                    catch (TargetInvocationException e)
                    {
                        throw e.InnerException;
                    }
                }
            }
            return result;
        }
    }
}