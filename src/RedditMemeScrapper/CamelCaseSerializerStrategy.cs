using System;
using System.Collections.Generic;
using System.Text;
using SimpleJson;

namespace RedditMemeScraper
{
    public class CamelCaseSerializerStrategy : PocoJsonSerializerStrategy
    {
        protected override string MapClrMemberNameToJsonFieldName(string clrPropertyName)
        {
            return char.ToLower(clrPropertyName[0]) + clrPropertyName.Substring(1);
        }
    }
}
