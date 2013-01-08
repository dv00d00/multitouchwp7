using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CatMania
{
    public partial class AddedImage : UserControl
    {
        public AddedImage()
        {
            InitializeComponent();
        }

        public Guid Id
        {
            get
            {
                return ((PictureItem) DataContext).Id;
            }
        }

        private void Clicked(object sender, MouseButtonEventArgs e)
        {
            ((PictureItem)DataContext).SelectCommand.Execute(this.DataContext);
        }
    }
}
