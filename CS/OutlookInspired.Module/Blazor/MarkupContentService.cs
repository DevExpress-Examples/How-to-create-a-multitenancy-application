using DevExpress.ExpressApp.Blazor.Components.Models;

namespace OutlookInspired.Module.Blazor{
    public class MarkupContentService{
        private IComponentModelRenderable _model;
        public event Action OnChange;

        public IComponentModelRenderable Model{
            get => _model;
            set{
                if (_model != value){
                    _model = value;
                    NotifyStateChanged();
                }
            }
        }

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}