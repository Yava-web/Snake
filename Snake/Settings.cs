using System.Drawing;

namespace Snake
{
    // Класс enumeration
    internal enum Direction
    {
        Up,
        Down,
        Left,
        Right
        //enum - это специальный класс, который представляет группу констант.
        //Здесь Direction - это перечисление, имеющее четыре константы: Вверх, вниз, влево, вправо.
        //enum обычно используется для создания коллекции связанных констант для удобства использования и читаемости всего вашего кода.
    };

    // Класс Settings
    internal class Settings
    {

        // Ширина части змейки
        public static int Width { get; set; }

        // Высота части змейки
        public static int Height { get; set; }

        // Скорость змейки
        public static int Speed { get; set; }
        public static int SpeedAI { get; set; }

        // Текущий счет в игре
        public static int Score { get; set; }
        public static int ScoreAI { get; set; }

        // На сколько увеличивается счет после пищи
        public static int Points { get; set; }
        public static int PointsAI { get; set; }

        // Состояние игры - true означает, что она закончена, false означает, что она все еще запущена
        public static bool GameOver { get; set; }
        public static bool GameOverAI { get; set; }

        public static Brush HeadColor { get; set; }
        public static Brush BodyColor { get; set; }
        public static Pen BodyBorderColor { get; set; }
        public static Brush FoodColor { get; set; }

        // Направление, в котором в данный момент движется змейка
        public static Direction direction { get; set; }

        // Конструктор класса
        public Settings()
        {
            // Значения по умолчанию
            Width = 16;
            Height = 16;
            Speed = 10;
            SpeedAI = 10;
            Score = 0;
            Points = 100;
            GameOver = false;
            GameOverAI = false;
            direction = Direction.Down;

            ScoreAI = 0;
            PointsAI = 100;

            HeadColor = Brushes.White;
            BodyColor = Brushes.Green;
            BodyBorderColor = Pens.Black;

            FoodColor = Brushes.Red;
        }
    }
}

// В этом классе хранятся все настройки и текущее состояние игры
// Конструктор этого класса инициализирует переменные некоторыми значениями по умолчанию
