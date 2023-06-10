using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Snake
{
    internal class SnakeAI
    {
        public List<SnakeElement> Snake { get; set; }
        public Food Food { get; set; }

        private Direction direction;
        // Максимальное положение x и y, куда змея может переместиться на экране
        private int maxXPos;
        private int maxYPos;

        private Label lblScoreAI;
        private Timer gameTimerAI;

        //Конструктор класса
        public SnakeAI(int maxXPos, int maxYPos, Label lblScoreAI, Timer gameTimerAI)
        {
            // Инициализация элементов змеи, еды и остальных параметров
            this.Snake = new List<SnakeElement>();
            this.Food = new Food();
            this.maxXPos = maxXPos;
            this.maxYPos = maxYPos;
            this.lblScoreAI = lblScoreAI;
            this.gameTimerAI = gameTimerAI;
        }

        // Метод генерации еды
        public void GenerateFood()
        {
            Random random = new Random();
            Food = new Food { X = random.Next(0, maxXPos), Y = random.Next(0, maxYPos) };
        }

        // Метод перемещения змейки ИИ по экрану
        public void MovePlayer()
        {
            SnakeElement newHead = new SnakeElement { X = Snake[0].X, Y = Snake[0].Y }; // Копируем текущее положение головы

            // Расстояние по х и у между едой и головой змеи
            int dx = Food.X - newHead.X;
            int dy = Food.Y - newHead.Y;

            // Определяем новое направление движения змеи, основываясь на положении еды
            // Если разница по горизонтали больше, чем разница по вертикали, то перемещаемся горизонтально
            // В противном случае перемещаемся вертикально
            if (Math.Abs(dx) > Math.Abs(dy))
            {
                // Перемещаемся горизонтально
                if (dx > 0 && direction != Direction.Left)
                    direction = Direction.Right;
                else if (dx < 0 && direction != Direction.Right)
                    direction = Direction.Left;
            }
            else
            {
                // Перемещаемся вертикально
                if (dy > 0 && direction != Direction.Up)
                    direction = Direction.Down;
                else if (dy < 0 && direction != Direction.Down)
                    direction = Direction.Up;
            }

            // Проверяем, врежется ли змея в следующей клетке сама в себя
            // Если это произойдет, не двигайтесь. В противном случае переместитесь в новое положение.
            switch (direction)
            {
                case Direction.Right:
                    if (!BodyAt(newHead.X + 1, newHead.Y))
                        newHead.X++;
                    break;
                case Direction.Left:
                    if (!BodyAt(newHead.X - 1, newHead.Y))
                        newHead.X--;
                    break;
                case Direction.Up:
                    if (!BodyAt(newHead.X, newHead.Y - 1))
                        newHead.Y--;
                    break;
                case Direction.Down:
                    if (!BodyAt(newHead.X, newHead.Y + 1))
                        newHead.Y++;
                    break;
            }

            // Если следующая позиция находится за пределами экрана, то переводим змею на противоположную сторону
            if (newHead.X >= maxXPos)
                newHead.X = 0;
            else if (newHead.Y >= maxYPos)
                newHead.Y = 0;
            else if (newHead.X < 0)
                newHead.X = maxXPos - 1;
            else if (newHead.Y < 0)
                newHead.Y = maxYPos - 1;

            // перемещаем все остальные элементы змеи на место предыдущих, кроме головы
            for (int i = Snake.Count - 1; i >= 1; i--)
            {
                Snake[i].X = Snake[i - 1].X;
                Snake[i].Y = Snake[i - 1].Y;
            }

            // перемещаем голову
            Snake[0] = newHead;

            for (int i = 1; i < Snake.Count; i++)
            {
                if (newHead.X == Snake[i].X && newHead.Y == Snake[i].Y)
                {
                    // Если врезалась в себя то умираем
                    Die();
                    return;
                }
            }
        }

        // Проверяем, занята ли ячейка (x,y) телом змеи
        // Если занята, возвращаем true, иначе false
        private bool BodyAt(int x, int y)
        {
            for (int i = 1; i < Snake.Count; i++)
            {
                if (Snake[i].X == x && Snake[i].Y == y)
                    return true;
            }
            return false;
        }

        public void Die()
        {
            //Snake.Clear();
            //SnakeElement head = new SnakeElement { X = maxXPos / 2, Y = maxYPos / 2 };
            //Snake.Add(head);
            Settings.GameOverAI = true;
            //Settings.GameOver = true;
        }

        public void Eat()
        {
            // по аналогии с функцие для игрока
            SnakeElement body = new SnakeElement
            {
                X = Snake[Snake.Count - 1].X,
                Y = Snake[Snake.Count - 1].Y
            };
            Snake.Add(body);

            Settings.ScoreAI += Settings.PointsAI;
            lblScoreAI.Text = Settings.ScoreAI.ToString();

            // Увеличиваем скорость змейки
            if (gameTimerAI.Interval > 1)
            {
                gameTimerAI.Interval -= 1;
            }

            GenerateFood();
        }

        public void UpdateSnake()
        {
            if (Snake[0].X == Food.X && Snake[0].Y == Food.Y)
            {
                Eat();
            }
        }
    }


}
