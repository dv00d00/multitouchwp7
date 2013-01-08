using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace CatMania
{
    public interface IPictureHolder
    {
        void AddPicture(PictureItem picture);

        void DeletePicture(Guid pictureId);

        void Save();
    }

    public interface IPictureSelector
    {
        PictureItem SelectPicture();
    }

    public class PictureItem : ViewModelBase
    {
        private bool isSelected;

        public Guid Id { get; set; }

        public Uri PictureUri { get; set; }

        public ICommand DeleteCommand { get; set; }

        public ICommand SelectCommand { get; set; }

        public bool IsSelected
        {
            get { return isSelected; }
            set
            {
                if (value.Equals(isSelected)) return;
                isSelected = value;
                OnPropertyChanged(() => IsSelected);
            }
        }
    }

    public interface IAvailablePictures
    {
        void SelectPicture(PictureItem pictureItem);
    }

    public class MainPageViewModel : ViewModelBase, IAvailablePictures
    {
        private readonly IPictureHolder pictureHolder;
        private readonly IPictureSelector pictureSelector;

        public MainPageViewModel(IPictureHolder pictureHolder, IPictureSelector pictureSelector)
        {
            this.pictureHolder = pictureHolder;
            this.pictureSelector = pictureSelector;

            PictureItems = new ObservableCollection<PictureItem>();

            DeletePictureCommand = new DelegateCommand<Guid>(OnDeletePicture);

            AddPictureCommand = new DelegateCommand(OnAddPicture);
            pictureSelectedCommand = new DelegateCommand<PictureItem>(this.SelectPicture);
        }

        private void OnDeletePicture(Guid guid)
        {
            this.pictureHolder.DeletePicture(guid);
        }

        private void OnAddPicture()
        {
            var picture = this.pictureSelector.SelectPicture();

            if (picture != null)
            {
                picture.SelectCommand = pictureSelectedCommand;
                picture.DeleteCommand = new DelegateCommand<PictureItem>(item =>
                {
                    this.PictureItems.Remove(item);
                    this.pictureHolder.DeletePicture(item.Id);
                });

                this.PictureItems.Add(picture);
                this.pictureHolder.AddPicture(picture);
            }
        }

        private ObservableCollection<PictureItem> pictureItems;
        private DelegateCommand<PictureItem> pictureSelectedCommand;

        public ObservableCollection<PictureItem> PictureItems
        {
            get { return pictureItems; }
            set
            {
                if (Equals(value, pictureItems)) return;
                pictureItems = value;
                OnPropertyChanged(() => PictureItems);
            }
        }

        public ICommand DeletePictureCommand { get; set; }

        public ICommand AddPictureCommand { get; set; }

        public void SelectPicture(PictureItem pictureItem)
        {
            this.DeselectAll();
            pictureItem.IsSelected = true;
        }

        public void DeselectAll()
        {
            foreach (var item in PictureItems)
            {
                item.IsSelected = false;
            }
        }
    }
}