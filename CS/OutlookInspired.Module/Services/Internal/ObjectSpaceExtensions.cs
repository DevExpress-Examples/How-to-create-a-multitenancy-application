using System.Linq.Expressions;
using DevExpress.Data.Linq;
using DevExpress.Data.Linq.Helpers;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.EFCore;
using DevExpress.ExpressApp.EFCore.Internal;
using DevExpress.ExpressApp.MultiTenancy;
using DevExpress.ExpressApp.Security;
using DevExpress.Persistent.BaseImpl.EF;
using DevExpress.Persistent.BaseImpl.EF.MultiTenancy;
using Microsoft.Extensions.DependencyInjection;
using Type = System.Type;

namespace OutlookInspired.Module.Services.Internal{
    internal static class ObjectSpaceExtensions{
        public static TUser CurrentUser<TUser>(this IObjectSpace objectSpace) where TUser:ISecurityUser 
            => objectSpace.GetObjectByKey<TUser>(objectSpace.ServiceProvider.GetRequiredService<ISecurityStrategyBase>().UserId);

        public static string TenantName(this IObjectSpace objectSpace) 
            => objectSpace.ServiceProvider.GetRequiredService<ITenantProvider>().TenantName;
        
        public static TTenant CurrentTenant<TTenant>(this IObjectSpace objectSpace) where TTenant:Tenant 
            => objectSpace.GetObjectByKey<TTenant>(objectSpace.ServiceProvider.GetRequiredService<ITenantProvider>().TenantId);
        
        
        public static T EnsureObject<T>(this IObjectSpace objectSpace,
            Expression<Func<T, bool>> criteriaExpression = null, Action<T> initialize = null, Action<T> update = null,
            bool inTransaction = false) where T : class{
            var o = objectSpace.FirstOrDefault(criteriaExpression??(arg =>true) ,inTransaction);
            if (o != null) {
                update?.Invoke(o);
                return o;
            }
            var ensureObject = objectSpace.CreateObject<T>();
            initialize?.Invoke(ensureObject);
            update?.Invoke(ensureObject);
            return ensureObject;
        }
        
        public static RichTextMailMergeData NewMailMergeData(this IObjectSpace objectSpace, string name ,Type dataType,byte[] bytes ){
            var richTextMailMergeData = objectSpace.CreateObject<RichTextMailMergeData>();
            richTextMailMergeData.Name = name;
            richTextMailMergeData.Template = bytes;
            richTextMailMergeData.DataType = dataType;
            return richTextMailMergeData;
        }
        
        

        [Obsolete]
        public static IQueryable Query(this EFCoreObjectSpace objectSpace,Type objectType, string criteria){
            var queryable = objectSpace.GetQueryable(objectType.FullName);
            return criteria != null ? queryable.AppendWhere(
                    new CriteriaToEFCoreExpressionConverter(queryable.Provider.GetType(), objectSpace.GetQueryable),
                    objectSpace.ParseCriteria(criteria)) : queryable;
        }
        
        public static IEnumerable<IObjectSpace> YieldAll(this IObjectSpace objectSpace)
            => objectSpace is not CompositeObjectSpace compositeObjectSpace ? objectSpace.YieldItem()
                : objectSpace.YieldItem().Concat(compositeObjectSpace.AdditionalObjectSpaces);
        
        public static bool Any<T>(this IObjectSpace objectSpace,Expression<Func<T, bool>> expression=null) 
            => objectSpace.GetObjectsQuery<T>().Any(expression??(arg =>true));
        
        public static T FindObject<T>(this IObjectSpace objectSpace, Expression<Func<T,bool>> expression,bool inTransaction=false) 
            => objectSpace.GetObjectsQuery<T>(inTransaction).FirstOrDefault(expression);

        public static RichTextMailMergeData MailMergeData(this IObjectSpace space,string name) 
            => space.FindObject<RichTextMailMergeData>(data => data.Name==name);
    }
}