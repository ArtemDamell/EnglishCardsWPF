using EnglishCards.ViewModel;
using System;
using System.Runtime.InteropServices.ComTypes;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace EnglishCards
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Dictionary dict;
        CardVM cardVM;

        public MainWindow()
        {
            InitializeComponent();
            dict = new Dictionary();
            cardVM = new CardVM(dict);

            // Bind the data
            cardVM.GetCurrentCardInfo(cardVM.CardId);
            DataContext = cardVM;


            cardVM.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName == nameof(cardVM.CurrentEnglishWord))
                    lbl_English.Content = cardVM.CurrentEnglishWord;

                if (e.PropertyName == nameof(cardVM.CurrentRussianWord))
                    lbl_Russian.Content = cardVM.CurrentRussianWord;
                if (e.PropertyName == nameof(cardVM.Image))
                {
                    var bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.UriSource = new Uri(cardVM.Image, UriKind.RelativeOrAbsolute);
                    bitmap.EndInit();
                    img_front.Source = bitmap;
                }
                if (e.PropertyName == nameof(cardVM.Video))
                    lbl_English.Content = cardVM.Video;
            };
        }

        /// <summary>
        /// Plays audio from the specified source.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event data.</param>
        private void PlayAudio(object sender, RoutedEventArgs e)
        {
            string audioSource = ((Button)sender).Name == "btn_audio_rus" ? cardVM.AudioRus : cardVM.AudioEng;
            if (!string.IsNullOrWhiteSpace(audioSource))
            {
                mediaPlayer.Source = new Uri(audioSource);
                mediaPlayer.Position = TimeSpan.Zero;
                mediaPlayer.Play();
            }
        }

        /// <summary>
        /// Switches the card when the next or previous button is clicked.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void SwitchCard(object sender, RoutedEventArgs e)
        {
            if (((Button)sender).Name == "btn_next")
            {
                cardVM.SwitchCard(++cardVM.CardId);
            }
            else if (((Button)sender).Name == "btn_prew")
            {
                cardVM.SwitchCard(--cardVM.CardId);
            }
        }
    }
}
