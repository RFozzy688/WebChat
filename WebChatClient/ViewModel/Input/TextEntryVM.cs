using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WebChatClient
{
    /// <summary>
    /// Модель представления текстовой записи для редактирования строкового значения
    /// <summary>
    public class TextEntryVM : BaseViewModel
    {
        // Метка, определяющая, для чего предназначено это значение
        public string Label { get; set; }

        // Текущее сохраненное значение
        public string OriginalText { get; set; }

        // Текущий отредактированный текст без фиксации
        public string EditedText { get; set; }

        // Указывает, находится ли текущий текст в режиме редактирования
        public bool Editing { get; set; }

        // Переводит элемент управления в режим редактирования
        public ICommand EditCommand { get; set; }

        // Выход из режима редактирования
        public ICommand CancelCommand { get; set; }

        // Фиксирует изменения и сохраняет значение, а также возвращается в режим без редактирования
        public ICommand SaveCommand { get; set; }

        public TextEntryVM()
        {
            // Создание команд
            EditCommand = new Command((o) => Edit());
            CancelCommand = new Command((o) => Cancel());
            SaveCommand = new Command((o) => Save());
        }

        // Переводит элемент управления в режим редактирования
        public void Edit()
        {
            // Установить отредактированному тексту текущее значение
            EditedText = OriginalText;

            // Перейти в режим редактирования
            Editing = true;
        }

        // Отменяет выход из режима редактирования
        public void Cancel()
        {
            Editing = false;
        }

        // Фиксирует содержимое и выходит из режима редактирования
        public void Save()
        {
            // Сохранить контент
            OriginalText = EditedText;

            Editing = false;
        }
    }
}
