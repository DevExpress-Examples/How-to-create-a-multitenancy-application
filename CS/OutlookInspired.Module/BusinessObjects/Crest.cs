using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;


namespace OutlookInspired.Module.BusinessObjects{
    [XafDefaultProperty(nameof(CityName))]
    [VisibleInReports(false)]
    public class Crest:OutlookInspiredBaseObject {
        [MaxLength(100)]
        public virtual string CityName { get; set; }
        [ImageEditor(ListViewImageEditorMode = ImageEditorMode.PictureEdit,
            DetailViewImageEditorMode = ImageEditorMode.PictureEdit)]
        public virtual byte[] SmallImage { get; set; }
        [ImageEditor(ListViewImageEditorMode = ImageEditorMode.PictureEdit,
            DetailViewImageEditorMode = ImageEditorMode.PictureEdit)]
        [XafDisplayName("Large")][HideInUI(HideInUI.ListView)][VisibleInLookupListView(false)]
        public virtual byte[] LargeImage { get; set; }

        public virtual ObservableCollection<CustomerStore> CustomerStores{ get; set; } = new();

    }
}