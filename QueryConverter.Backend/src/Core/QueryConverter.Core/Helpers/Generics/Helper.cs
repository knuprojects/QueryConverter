using System.Diagnostics;
using System.Reflection;

namespace QueryConverter.Core.Helpers.Generics
{
    public class Helper<T> : IHelper<T>
    {
        public T? GetAttribute(MemberInfo member, Type attributeType)
        {
            object[] attributes = member.GetCustomAttributes(attributeType, false);
            if (attributes.Length == 1)
                return (T)attributes[0];

            Debug.Assert(attributes.Length == 0);
            return default(T);
        }
    }
}
