using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Threading.Tasks;
using System.Windows;
using TextChanger.Services;

namespace TextChanger.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel()
        {
            ApiService.InitializeClient();
        }

        private string _changeImage;
        public string ChangeImage
        {
            get => _changeImage;
            set => Set(ref _changeImage, value);
        }

        private bool _isPreviousImageLoaded = true;
        public bool IsPreviousImageLoaded
        { 
            get => _isPreviousImageLoaded;
            set => Set(ref _isPreviousImageLoaded, value);
        }

        private bool _isNextImageLoaded;
        public bool IsNextImageLoaded
        {
            get => _isNextImageLoaded;
            set => Set(ref _isNextImageLoaded, value);
        }

        private int maxNumber = 0;
        private int currentNumber = 0;

        public RelayCommand ShowCommand => new(GetNewImage);
        public RelayCommand PreviousCommand => new(GetPreviousImage);
        public RelayCommand NextCommand => new(GetNextImage);

        private async void GetNewImage()
        {
            try
            {
                await LoadImage();
            }
            catch
            {
                MessageBox.Show("Please enter correct uri");
            }
        }

        private async Task LoadImage(int imageNumber = 0)
        {
            var comic = await ComicProcessor.LoadComic(imageNumber);

            if (imageNumber == 0)
            {
                maxNumber = comic.Num;
            }
            currentNumber = comic.Num;

            var uriSource = new Uri(comic.Img!, UriKind.Absolute);
            ChangeImage = uriSource.AbsoluteUri;
        }

        private async void GetPreviousImage()
        {
            if (currentNumber > 1)
            {
                currentNumber -= 1;
                IsNextImageLoaded = true;
                await LoadImage(currentNumber);

                if (currentNumber == 1) 
                { 
                    IsPreviousImageLoaded = false;
                }
            }
        }
        
        private async void GetNextImage()
        {
            if (currentNumber < maxNumber)
            {
                currentNumber += 1;
                IsPreviousImageLoaded = true;
                await LoadImage(currentNumber);

                if (currentNumber == maxNumber) 
                {
                    IsNextImageLoaded = false;
                }
            }
        }
    }
}
