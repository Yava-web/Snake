using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Snake
{
    internal class Input
    {
        // Этот статический словарь хранит состояние всех ключей.
        // Если клавиша нажата, то ее значение в словаре равно true. Если не нажата, то ложь.
        private static Dictionary<Keys, bool> KeyTable = new Dictionary<Keys, bool>();

        // Изменение состояние ключа.
        public static void ChangeState(Keys key, bool state)
        {
            KeyTable[key] = state;
        }

        // Этот метод проверяет, нажата ли клавиша (ее состояние равно true)
        public static bool KeyPressed(Keys key)
        {
            if (KeyTable.ContainsKey(key))
                return KeyTable[key];
            else
                return false;
        }
    }
}
