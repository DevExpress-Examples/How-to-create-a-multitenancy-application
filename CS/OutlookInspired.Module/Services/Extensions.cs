using System.Linq.Expressions;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using OutlookInspired.Module.BusinessObjects;
using OutlookInspired.Module.Common;
using OutlookInspired.Module.Features;
using OutlookInspired.Module.Services.Internal;

namespace OutlookInspired.Module.Services{
    public static class Extensions{
        public static (double min, double max) Map(this Stage stage){
            switch (stage){
                case Stage.High:
                    return (0.6, 1.0);
                case Stage.Medium:
                    return (0.3, 0.6);
                case Stage.Low:
                    return (0.12, 0.3);
                case Stage.Summary:
                    return (0.0, 1.0);
                case Stage.Unlikely:
                    return (0.0, 0.12);
            }

            throw new InvalidOperationException(stage.ToString());
        }

        public static string Color(this Stage stage){
            switch (stage){
                case Stage.High:
                    return "#D11C1C"; // Red
                case Stage.Medium:
                    return "#1177D7"; // Blue
                case Stage.Low:
                    return "#FFB115"; // Yellow
                case Stage.Unlikely:
                    return "#727272"; // Gray
            }

            throw new InvalidOperationException(stage.ToString());
        }
        
        public static IEnumerable<IUserControl> FilterUserControl(this DetailView view, LambdaExpression expression) 
            => view.UserControl().YieldItem().WhereNotDefault()
                .Where(control => control.ObjectType == expression.Parameters.First().Type)
                .Do(control => control.SetCriteria(expression)).ToArray();
        
        public static T UseCurrentEmployee<T>(this ActionBase action,Func<Employee,T> withEmployee){
            var view = action.View();
            var applicationUser = view!=null? view.ObjectSpace.CurrentUser():action.Application.NewObjectSpace().CurrentUser();
            var result = withEmployee(applicationUser.Employee());
            if (view == null){
                ((IObjectSpaceLink)applicationUser).ObjectSpace.Dispose();
            }
            return result;
        }
        public static T UseCurrentUser<T>(this ActionBase action,Func<ApplicationUser, T> withUser){
            var view = action.View();
            var applicationUser = view!=null? view.ObjectSpace.CurrentUser():action.Application.NewObjectSpace().CurrentUser();
            var result = withUser(applicationUser);
            if (view == null){
                ((IObjectSpaceLink)applicationUser).ObjectSpace.Dispose();
            }
            return result;
        }

        public static Employee Employee(this ApplicationUser applicationUser) 
            => ((IObjectSpaceLink)applicationUser).ObjectSpace.GetObjectsQuery<Employee>().FirstOrDefault(employee => employee.User.ID == applicationUser.ID);
        
        public static ApplicationUser CurrentUser(this IObjectSpace objectSpace) 
            => objectSpace.CurrentUser<ApplicationUser>();
    }
}