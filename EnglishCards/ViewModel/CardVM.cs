using System;
using System.ComponentModel;
using System.Windows;

namespace EnglishCards.ViewModel
{
    public class CardVM : INotifyPropertyChanged
    {
        private readonly Dictionary _dictionary;

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private string _currentEnglishWord;
        public string CurrentEnglishWord
        {
            get { return _currentEnglishWord; }
            set
            {
                _currentEnglishWord = value;
                OnPropertyChanged(nameof(CurrentEnglishWord));
            }
        }

        private string _currentRussianWord;
        public string CurrentRussianWord
        {
            get { return _currentRussianWord; }
            set
            {
                _currentRussianWord = value;
                OnPropertyChanged(nameof(CurrentRussianWord));
            }
        }
        public string AudioEng { get; set; }
        public string AudioRus { get; set; }
        private string _image;
        public string Image
        {
            get { return _image; }
            set
            {
                _image = value;
                OnPropertyChanged(nameof(Image));
            }
        }
        private string _video;
        public string Video
        {
            get { return _video; }
            set
            {
                _video = value;
                OnPropertyChanged(nameof(Video));
            }
        }

        public int CardId = 0;

        public CardVM(Dictionary dictionary)
        {
            _dictionary = dictionary;
        }

        /// <summary>
        /// This method gets the current card information based on the card ID.
        /// </summary>
        public void GetCurrentCardInfo(int cardId) => GetCardInfo(cardId);

        /// <summary>
        /// Switches the card to the specified cardId.
        /// </summary>
        /// <param name="cardId">The card identifier.</param>
        public void SwitchCard(int cardId)
        {
            if (_dictionary == null)
                return;

            CardId = Math.Max(Math.Min(cardId, _dictionary.Total - 1), 0);
            GetCardInfo(CardId);
        }

        /// <summary>
        /// Retrieves card information from the dictionary based on the card ID.
        /// </summary>
        /// <param name="cardId">The ID of the card to retrieve information for.</param>
        private void GetCardInfo(int cardId)
        {
            CurrentEnglishWord = _dictionary.English(cardId);
            CurrentRussianWord = _dictionary.Russian(cardId);
            AudioEng = _dictionary.Audio_eng(cardId);
            AudioRus = _dictionary.Audio_rus(cardId);
            Image = _dictionary.Image(cardId);
            Video = _dictionary.Video(cardId);

            Application.Current.Dispatcher.Invoke(() =>
            {
                OnPropertyChanged(nameof(CurrentEnglishWord));
            });
        }
    }
}
