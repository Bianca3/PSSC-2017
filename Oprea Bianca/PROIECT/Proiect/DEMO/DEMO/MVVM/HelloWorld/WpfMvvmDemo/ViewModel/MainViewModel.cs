using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using WpfMvvmDemo.Messages;

namespace WpfMvvmDemo.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            ////if (IsInDesignMode)
            ////{
            ////    // Code runs in Blend --> create design time data.
            ////}
            ////else
            ////{
            ////    // Code runs "for real"
            ////}
        }

        /// <summary>
        /// The <see cref="MyText" /> property's name.
        /// </summary>
        public const string MyTextPropertyName = "MyText";

        private string _myText = "";

        /// <summary>
        /// Sets and gets the MyText property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string MyText
        {
            get
            {
                return _myText;
            }

            set
            {
                if (_myText == value)
                {
                    return;
                }

                RaisePropertyChanging(MyTextPropertyName);
                _myText = value;
                RaisePropertyChanged(MyTextPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="Message" /> property's name.
        /// </summary>
        public const string MessagePropertyName = "Message";

        private string _message = "";

        /// <summary>
        /// Sets and gets the Message property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string Message
        {
            get
            {
                return _message;
            }

            set
            {
                if (_message == value)
                {
                    return;
                }

                RaisePropertyChanging(MessagePropertyName);
                _message = value;
                RaisePropertyChanged(MessagePropertyName);
            }
        }

        private RelayCommand _sayHello;

        /// <summary>
        /// Gets the SayHello.
        /// </summary>
        public RelayCommand SayHello
        {
            get
            {
                return _sayHello
                    ?? (_sayHello = new RelayCommand(
                                          () =>
                                          {
                                              Message = string.Format("Hello {0}", MyText);
                                              MessengerInstance.Send(new HelloMessage() { Text = Message});
                                          }));
            }
        }
    }
}