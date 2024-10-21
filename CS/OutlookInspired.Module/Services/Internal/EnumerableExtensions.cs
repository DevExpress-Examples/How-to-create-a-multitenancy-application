using System.Collections;

namespace OutlookInspired.Module.Services.Internal{
    internal static class EnumerableExtensions{
        public static TValue DynamicSum<TValue>(this IEnumerable<TValue> values) 
            => values.Aggregate<TValue, dynamic>(0, (current, value) => current + (dynamic)value);
        [Obsolete]
        public static IEnumerable<T> Do<T>(this IEnumerable<T> source,Action<T,int> action) 
            => source.Select((arg1, i) => {
                action(arg1, i);
                return arg1;
            });

        public static IEnumerable<T> To<T>(this IEnumerable<object> source) 
            => source.Select(o =>o is T arg? arg: default);
        
        public static IEnumerable<T> SwitchIfEmpty<T>(this IEnumerable<T> source, T defaultValue){
            return _();
            IEnumerable<T> _(){
                var isEmpty = true;
                foreach (var item in source){
                    isEmpty = false;
                    yield return item;
                }
                if (isEmpty) yield return defaultValue;
            }
        }

        public static IEnumerable<T> Finally<T>(this IEnumerable<T> source, Action action){
            return _();
            IEnumerable<T> _(){
                foreach (var element in source) yield return element;
                action();
            }
        }
        
        public static IEnumerable<T> SelectManyRecursive<T>(this IEnumerable<T> source, Func<T, IEnumerable<T>> childrenSelector){
            foreach (var i in source){
                yield return i;
                var children = childrenSelector(i);
                if (children == null) continue;
                foreach (var child in SelectManyRecursive(children, childrenSelector))
                    yield return child;
            }
        }
        [Obsolete]
        public static void Enumerate<T>(this IEnumerable<T> source) {
            using var e = source.GetEnumerator();
            while (e.MoveNext()) { }
        }

        [Obsolete]
        public static IEnumerable<TSource> Do<TSource>(
            this IEnumerable<TSource> source, Action<TSource> action)
            => source.Select(item => {
                action(item);
                return item;
            });
        
        public static System.ComponentModel.BindingList<T> ToBindingList<T>(this IEnumerable<T> source) 
            => new((IList<T>) (source as IList ?? source.ToList()));


        [Obsolete]
        public static IEnumerable<T> Concat<T>(this IEnumerable<T> source,params T[] values) => source.Concat(values.AsEnumerable());
        
        public static IEnumerable<T> WhereNotDefault<T,T2>(this IEnumerable<T> source, Func<T,T2> predicate) 
            => source.Where(arg => !predicate(arg).IsDefaultValue());

        public static IEnumerable<TSource> WhereNotDefault<TSource>(this IEnumerable<TSource> source) {
            var type = typeof(TSource);
            if (type.IsClass || type.IsInterface){
                return source.Where(source1 => source1!=null);   
            }
            var instance = type.CreateInstance();
            return source.Where(source1 => !source1.Equals(instance));
        }
        
        public static IEnumerable<string> WhereNotNullOrEmpty(this IEnumerable<string> source) 
            => source.Where(s => s.IsNotNullOrEmpty());
        
        public static bool IsNotNullOrEmpty(this string strString)
            => !string.IsNullOrEmpty(strString);
    }
}