using System.Reflection;

namespace OutlookInspired.Module.Services.Internal{
    internal static class ReflectionExtensions{
        public static object GetPropertyValue(this object obj, string propertyName) 
            => obj.GetType().GetProperty(propertyName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)!.GetValue(obj);
        public new static bool ReferenceEquals(this object objA, object objB)
            => Object.ReferenceEquals(objA, objB);
        
        public static bool HasPublicParameterlessConstructor(this Type type) 
            => type.GetConstructors(BindingFlags.Public | BindingFlags.Instance)
                .Any(ctor => ctor.GetParameters().Length == 0);
        
        
        public static T CreateInstance<T>(this Type type) => (T)CreateInstance(type);

        public static object CreateInstance(this Type type){
            if (type.IsValueType)
                return Activator.CreateInstance(type);

            if (type.HasPublicParameterlessConstructor())
                return Activator.CreateInstance(type);
            throw new InvalidOperationException($"Type {type.FullName} does not have a parameterless constructor.");
        }
        
        public static bool IsDefaultValue<TSource>(this TSource source) {
            var def = default(TSource);
            return def != null ? def.Equals(source) : typeof(TSource) == typeof(object)
                ? source == null || source.Equals(source.GetType().DefaultValue()) : source == null;
        }
        
        public static object DefaultValue(this Type t) => t.IsValueType ? Activator.CreateInstance(t) : null;
        
        [Obsolete]
        public static IEnumerable<TSource> YieldItem<TSource>(this TSource source){
            yield return source;
        }
        
    }
}