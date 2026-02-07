using System;
using System.Collections.Generic;
using System.Text;

namespace CoreBanking.Application.Extensions
{
    public static class TypeExtensions
    {
        public static string GetGenericTypeName(this object @object)
        {
            var type = @object.GetType();

            if (!type.IsGenericType)
                return type.Name;

            var genericTypes = string.Join(",", type.GetGenericArguments().Select(t => t.Name));
            return $"{type.Name[..type.Name.IndexOf('`')]}<{genericTypes}>";
        }
    }
}
