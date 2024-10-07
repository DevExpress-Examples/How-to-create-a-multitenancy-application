using DevExpress.ExpressApp.Blazor.Components.Models;

namespace OutlookInspired.Win.Services.Blazor{
    public class ContentService{
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