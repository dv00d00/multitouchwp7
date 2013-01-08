using System.Linq;
using System.Windows;
using System.Windows.Interactivity;
using Microsoft.Phone.Controls;
using MultiTouch.Behaviors.Silverlight4;
using SilverlightWP7MultiTouchSample.ViewModels;

namespace SilverlightWP7MultiTouchSample.Views
{
    public partial class ImagePage : PhoneApplicationPage
    {
        private MultiTouchBehavior _multiTouchBehavior;  
 
        public ImagePage()
        {
            InitializeComponent();

            DataContext = new ImageViewModel();

            this.Loaded += ImagePage_Loaded;
        }

        void ImagePage_Loaded(object sender, RoutedEventArgs e)
        {
                var behaviors = Interaction.GetBehaviors(_image).OfType<MultiTouchBehavior>();
                if (behaviors.ToList().Count > 0)
                {
                    _multiTouchBehavior = behaviors.First();
                }

                _multiTouchBehavior.Move(new Point(230, 250), 0, 200);
        }

        private void DoubleTapBehavior_DoubleTap(object sender, System.EventArgs e)
        {
            //Eventually handle your DoubleTap event here
            //MessageBox.Show("DoupleTap recognized.");
        }
    }
}