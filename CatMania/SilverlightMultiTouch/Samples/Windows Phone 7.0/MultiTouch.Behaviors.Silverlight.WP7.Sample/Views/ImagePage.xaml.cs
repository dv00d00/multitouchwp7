using System;
using System.Linq;
using System.Windows;
using System.Windows.Media.Imaging;
using Microsoft.Phone.Controls;
using MultiTouch.Behaviors.Silverlight4;
using SilverlightWP7MultiTouchSample.ViewModels;

namespace SilverlightWP7MultiTouchSample.Views
{
    public partial class ImagePage : PhoneApplicationPage
    {
        //uncomment following lines get the behavior and be able to detach
        //MultiTouchBehavior behavior;
        public ImagePage()
        {
            InitializeComponent();

            DataContext = new ImageViewModel();
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            //Initialize the Behavior and the Image
            var multiTouchBehaviors = System.Windows.Interactivity.Interaction.GetBehaviors(_image).OfType
                    <MultiTouchBehavior>();
            var behavior = multiTouchBehaviors.FirstOrDefault(); 
            if (behavior!=null)
            {
                behavior.Move(new Point(230, 250), 0, 200);
            }

            if (DataContext is ImageViewModel)
                (DataContext as ImageViewModel).SelectedPicture =new BitmapImage(new Uri("../Images/Koala.jpg", UriKind.RelativeOrAbsolute));
            
            base.OnNavigatedTo(e);
        }

        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            base.OnBackKeyPress(e);
        }

        private void DoubleTapBehavior_DoubleTap(object sender, EventArgs e)
        {
            //Handle the DoubleTap event here
            //MessageBox.Show("DoubleTap recognized.");
        }
    }
}