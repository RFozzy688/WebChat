using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace WebChatClient
{
    /// <summary>
    /// Модель базового представления, которая запускает события 
    /// изменения свойства по мере необходимости.
    /// </summary>
    public class BaseViewModel : INotifyPropertyChanged
    {
        // Событие, которое вызывается, когда какое-либо дочернее свойство меняет свое значение.
        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };

        /// <summary>
        /// Вызовите это, чтобы вызвать событие <see cref="PropertyChanged"/>.
        /// </summary>
        /// <param name="name"></param>
        public void OnPropertyChanged(string name)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
    }
}
