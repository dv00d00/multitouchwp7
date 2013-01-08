using System;
using System.Linq;
using System.Windows;
using Microsoft.Phone.Controls;
using MultiTouch.Behaviors.Silverlight4;
using SilverlightWP7MultiTouchSample.ViewModels;

namespace SilverlightWP7MultiTouchSample
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();

            DataContext = new MainPageViewModel();

            //At this time only supported Orientation: Portrait
            SupportedOrientations = SupportedPageOrientation.Portrait;

            //Initialize the items
            var multiTouchBehaviors = System.Windows.Interactivity.Interaction.GetBehaviors(image1).OfType
                    <MultiTouchBehavior>();
            if (multiTouchBehaviors.ToList().Count > 0)
               multiTouchBehaviors.First().Move(new Point(100, 250), 45, 100);

            multiTouchBehaviors = System.Windows.Interactivity.Interaction.GetBehaviors(image2).OfType<MultiTouchBehavior>();
            if (multiTouchBehaviors.ToList().Count > 0)
                multiTouchBehaviors.First().Move(new Point(300, 300), -45, 150);
        }

        /// <summary>
        /// Navigate to the Image Test Page
        /// </summary>
        private void GoToImagePageMenuItem_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/Views/ImagePage.xaml", UriKind.Relative));
        }
    }
}
