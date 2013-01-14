using System;
using System.Windows.Input;

namespace CatMania
{
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
}