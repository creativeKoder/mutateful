﻿using Mutate4l.Cli;
using Mutate4l.ClipActions;
using Mutate4l.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Mutate4l.Dto
{
    public class OptionParser
    {
        public static T ParseOptions<T>(Dictionary<TokenType, List<Token>> options) where T : new()
        {
            T result = new T();
            System.Reflection.MemberInfo info = typeof(T);
            var props = result.GetType().GetProperties();
            var togglesByGroupId = new Dictionary<int, List<TokenType>>();
            //var valueProperties = new List<>

            foreach (var property in props)
            {
                TokenType option;
                try
                {
                    option = Enum.Parse<TokenType>(property.Name);
                }
                catch (ArgumentException)
                {
                    throw new Exception($"No corresponding entity found for {property.Name}");
                }
                var attributes = property.GetCustomAttributes(false);
                if (attributes.Length > 0)
                {
                    foreach (var attrib in attributes)
                    {
                        OptionInfo attribInfo = (OptionInfo)attrib;
                        if (attribInfo.Type == OptionType.InverseToggle)
                        {
                            List<TokenType> toggles;
                            if (togglesByGroupId.ContainsKey(attribInfo.GroupId))
                            {
                                toggles = togglesByGroupId[attribInfo.GroupId];
                            }
                            else
                            {
                                toggles = new List<TokenType>();
                                togglesByGroupId[attribInfo.GroupId] = toggles;
                            }
                            toggles.Add(option);
                        }
                    }
                }
                else
                {
                    if (options.ContainsKey(option))
                    {
                        var tokens = options[option];
                        TokenType type = tokens.First()?.Type ?? TokenType.Unset;
                        var typedTokens = tokens.Where(x => x.Type == type).ToList();
                        if (typedTokens.Count == 1)
                        {
                            // handle single value
                            if (type == TokenType.MusicalDivision && property.PropertyType == typeof(decimal))
                            {
                                property.SetMethod?.Invoke(result, new object[] { Utility.MusicalDivisionToDecimal(typedTokens[0].Value) });
                            }
                        }
                        else if (typedTokens.Count > 1)
                        {
                            // handle list
                        }
                        Console.WriteLine(property.GetType());
                        /*                        tokens.Where(x => x.Type == type).

                                                if (tokens.Count > 0)
                                                {
                                                    var type = tokens.First()
                                                }
                                                if (token.Type)*/
                    }
                }
            }

            var optionGroups = new List<OptionGroup>();
            foreach (var toggle in togglesByGroupId.Keys)
            {
                var group = new OptionGroup();
                group.Type = OptionGroupType.InverseToggle;
                group.Options = togglesByGroupId[toggle].ToArray();
                optionGroups.Add(group);
            }
            var optionsDefinition = new OptionsDefinition() { OptionGroups = optionGroups.ToArray() };

            foreach (var optionGroup in optionsDefinition.OptionGroups)
            {
                switch (optionGroup.Type)
                {
                    case OptionGroupType.InverseToggle:
                        var specifiedOptions = Utility.GetValidOptions(options, optionGroup.Options);
                        bool noneOrAllSpecified = specifiedOptions.Keys.Count == 0 || specifiedOptions.Keys.Count == optionGroup.Options.Length;
                        foreach (var option in optionGroup.Options)
                        {
                            if (noneOrAllSpecified || specifiedOptions.ContainsKey(option))
                            {
                                result.GetType().GetProperty(option.ToString())?.SetMethod?.Invoke(result, new object[] { true });
                            }
                        }
                        break;
                    case OptionGroupType.Value:
                        break;
                }
            }
            return result;
        }
    }
}
