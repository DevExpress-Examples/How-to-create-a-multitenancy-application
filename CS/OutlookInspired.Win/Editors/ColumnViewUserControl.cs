using System.Collections;
using System.Linq.Expressions;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.EFCore;
using DevExpress.ExpressApp.Security;
using DevExpress.Utils.Serializing.Helpers;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using OutlookInspired.Module.Common;
using OutlookInspired.Module.Features;
using OutlookInspired.Module.Services.Internal;
using static DevExpress.ExpressApp.Security.SecurityOperations;


namespace OutlookInspired.Win.Editors{
    [Obsolete("remove inheritance")]
    public partial class ColumnViewUserControl : UserControl, IUserControl{
        private EFCoreObjectSpace _objectSpace;
        protected ColumnView ColumnView;
        protected IList DataSource;
        private string _criteria;
        private XafApplication _application;
        public ColumnViewUserControl() => Load += (_, _) => Refresh();
        public event EventHandler CurrentObjectChanged;
        public event EventHandler SelectionChanged;
        public event EventHandler SelectionTypeChanged;
        public event EventHandler<ObjectEventArgs> ProcessObject;
        
        public void SetCriteria<T>(Expression<Func<T, bool>> lambda) 
            => SetCriteria((LambdaExpression)lambda);
        
        public void SetCriteria(LambdaExpression lambda) 
            => SetCriteria(lambda.ToCriteria(ObjectType).ToString());

        public void SetCriteria(string criteria){
            _criteria = criteria;
            Refresh();
        }
        
        public virtual void Refresh(object currentObject) => Refresh();
        
        public virtual void Setup(IObjectSpace objectSpace, XafApplication application){
            _objectSpace = (EFCoreObjectSpace)objectSpace;
            _application=application;
            ColumnView = (ColumnView)Controls.OfType<GridControl>().First().MainView;
            ColumnView.FocusedRowObjectChanged += (_, _) => {
                SelectionChanged?.Invoke(this, EventArgs.Empty);
                CurrentObjectChanged?.Invoke(this, EventArgs.Empty);
            };
            ColumnView.DoubleClick += (_, _) => {
                if (!IsNotGroupedRow()) return;
                ProcessObject?.Invoke(this, new ObjectEventArgs(SelectedObjects.Cast<object>().First()));
            };
            ColumnView.ColumnFilterChanged += (_, _) => OnDataSourceOfFilterChanged();
            ColumnView.DataSourceChanged += (_, _) => OnDataSourceOfFilterChanged();
            ColumnView.DataError+=(_, e) => throw new AggregateException(e.DataException.Message,e.DataException);
            ProtectDetailViews();
        }
        
        bool IsNotGroupedRow( ) 
            => ColumnView is not GridView view|| !view.IsGroupRow(ColumnView.FocusedRowHandle);
        void ProtectDetailViews() 
            => ColumnView.GridControl.LevelTree.Nodes.ToArray()
                .Where(node => {
                    var listElementType = _application.TypesInfo.FindTypeInfo(ObjectType)
                        .FindMember(node.RelationName).ListElementType;
                    return !_application.Security.IsGranted(new PermissionRequest(listElementType, Read));
                })
                .Do(node => ColumnView.GridControl.LevelTree.Nodes.Remove(node))
                .Enumerate();

        
        public override void Refresh() {
            if (ColumnView == null) return;
            ColumnView.GridControl.DataSource =
                (object)DataSource ?? _objectSpace.GetObjects(ObjectType,_objectSpace.ParseCriteria(_criteria));
        }
        
        public virtual Type ObjectType{ get; set; }

        public object CurrentObject => FocusedRowObject();

        public IList SelectedObjects{
            get{
                var rows = ColumnView.GetSelectedRows();
                return rows.Any() ? rows.Select(i => ColumnView.GetRow(i)).ToArray()
                    : ColumnView.FocusedRowHandle.YieldItem()
                        .Select(i => ColumnView.GetRow(i)).ToArray();
            }
        }
        object FocusedRowObject() 
            => ColumnView.FocusedRowObject == null || !ColumnView.IsServerMode ? ColumnView.FocusedRowObject
                : !IsNotGroupedRow() || !IsNotInvalidRow() ? null
                : _objectSpace.GetObjectByKey(ObjectType, ColumnView.FocusedRowObject);

        
        bool IsNotInvalidRow( ) 
            => ColumnView.FocusedRowHandle!=GridControl.InvalidRowHandle;
        public SelectionType SelectionType => SelectionType.Full;
        public bool IsRoot => false;

        protected virtual void OnDataSourceOfFilterChanged(){
        }

        protected virtual void OnSelectionTypeChanged() => SelectionTypeChanged?.Invoke(this, EventArgs.Empty);
    }
}
