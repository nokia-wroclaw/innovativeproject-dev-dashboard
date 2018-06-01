﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SimpleJson;

namespace TravisApi
{
    /// <summary>
    /// Credit to https://github.com/restsharp/RestSharp/wiki/Deserialization#overriding-jsonserializationstrategy
    /// </summary>
    public class SnakeJsonSerializerStrategy : PocoJsonSerializerStrategy
    {
        protected override string MapClrMemberNameToJsonFieldName(string clrPropertyName)
        {
            //PascalCase to snake_case
            return string.Concat(clrPropertyName.Select((x, i) => i > 0 && char.IsUpper(x) ? "_" + char.ToLower(x).ToString() : char.ToLower(x).ToString()));
        }
    }
}
