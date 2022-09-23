using System.Reflection;

namespace QueryConverter.Core.Helpers.Generics
{
    public interface IHelper<T>
    {
        T GetAttribute(MemberInfo member, Type attributeType);
    }
}
