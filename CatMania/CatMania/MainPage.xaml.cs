using System;
using System.IO;
using System.Windows;
using System.Windows.Interactivity;
using System.Windows.Media.Imaging;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Tasks;
using Microsoft.Xna.Framework.Media;
using MultiTouch.Behaviors.Silverlight4;
using System.Linq;

namespace CatMania
{
    public partial class MainPage : PhoneApplicationPage, IPictureHolder, IPictureSelector
    {
        private readonly MainPageViewModel mainPageViewModel;
        // Constructor
        public MainPage()
        {
            InitializeComponent();

            this.mainPageViewModel = new MainPageViewModel(this, this);
            this.DataContext = mainPageViewModel;
        }

        public void Save()
        {
            this.mainPageViewModel.DeselectAll();

            Dispatcher.BeginInvoke(() =>
            {
                using (var stream = new MemoryStream())
                {
                    var bitmap = new WriteableBitmap(ContentPanel, null);
                    bitmap.SaveJpeg(stream, bitmap.PixelWidth, bitmap.PixelHeight, 0, 100);
                    stream.Seek(0, SeekOrigin.Begin);

                    using (var mediaLibrary = new MediaLibrary())
                    {
                        mediaLibrary.SavePicture(
                            "Picture" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".jpg",
                            stream);
                    }
                }

                MessageBox.Show("Picture Saved...");
            });
        }

        private void SavePictureClick(object sender, EventArgs e)
        {
            Save();
        }

        private void PickPhoto(object sender, EventArgs e)
        {
            var photoChooserTask = new PhotoChooserTask();
            photoChooserTask.Completed += (o, args) =>
            {
                if (args.TaskResult == TaskResult.OK)
                {
                    var img = new BitmapImage();
                    img.SetSource(args.ChosenPhoto);
                    this.background.Source = img;
                }
            };
            photoChooserTask.Show();
        }

        public void AddPicture(PictureItem picture)
        {
            var image = new AddedImage {DataContext = picture};

            var multiTouchBehavior = new MultiTouchBehavior
            {
                IsConstrainedToParentBounds = true,
                IsScaleEnabled = true,
                // IsRotateEnabled = true,
                IsTranslateEnabled = true,
                IsInertiaEnabled = false,
                IsPivotEnabled = true,
                MaximumScale = 200,
                MinimumScale = 50,
                Scale = 100,
                Rotation = 0
            };

            Interaction.GetBehaviors(image).Add(multiTouchBehavior);
            
            this.Canvas.Children.Add(image);
        }

        public void DeletePicture(Guid pictureId)
        {
            var pictureToDelete = this.Canvas.Children.OfType<AddedImage>().FirstOrDefault(it => it.Id == pictureId);
            if (pictureToDelete != null)
            {
                this.Canvas.Children.Remove(pictureToDelete);
            }
        }

        public PictureItem SelectPicture()
        {
            return new PictureItem
            {
                Id = Guid.NewGuid(),
                PictureUri = new Uri(@"Images/cats/cat1.png", UriKind.Relative)
            };
        }

        private void OnAddClick(object sender, EventArgs e)
        {
            mainPageViewModel.AddPictureCommand.Execute(null);
        }

        private void UndoClick(object sender, EventArgs e)
        {
            if (CommandStorage.Commands.Count > 0)
            {
                var command = CommandStorage.Commands.Pop();
                command.Undo();
            }
        }
    }
}