using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Snake
{
    //Этот класс является основной формой, в которой реализована логика.
    public partial class Form1 : Form
    {
        //Список для хранения элементов из которых состоит змейка.
        private List<SnakeElement> Snake = new List<SnakeElement>();
        //Элемент еды
        private Food food = new Food();
        // Таймер, который управляет скоростью игры
        private System.Windows.Forms.Timer gameTimer = new Timer();
        private System.Windows.Forms.Timer gameTimerAI = new Timer();

        private SnakeAI aiSnake;
        private Stopwatch timer;

        //Конструктор класса Form1. Вызывается при создании нового объекта Form1
        public Form1()
        {
            //InitializeComponent инициализирует компоненты формы (PictureBox и др)
            InitializeComponent();

            //Привязываем к событию Paint PictureBox метод PictureBox_Paint
            PictureBox.Paint += new PaintEventHandler(PictureBox_Paint);
            PictureBoxAI.Paint += new PaintEventHandler(PictureBoxAI_Paint);

            // Сбрасываем настройки игры до значений по умолчанию
            new Settings();

            // Максимальные координаты которые может достигнуть змейка
            int cellsWidth = PictureBoxAI.Width / Settings.Width;
            int cellsHeight = PictureBoxAI.Height / Settings.Height;
            aiSnake = new SnakeAI(cellsWidth, cellsHeight, lblScoreAI, gameTimerAI);

            // Устанавливаем интервал действия таймера в соответствии с настройками скорости игры
            // Интервал таймера определяет, как часто срабатывает событие Tick таймера
            gameTimer.Interval = 1000 / Settings.Speed;
            gameTimerAI.Interval = 1000 / Settings.SpeedAI;

            // Прикрепляем метод UpdateScreen к событию Tick таймера. Этот метод будет вызывается каждый раз, когда запускается событие Tick
            gameTimer.Tick += UpdateScreen;
            gameTimerAI.Tick += UpdateScreenAI;

            // Запускаем таймер
            gameTimer.Start();
            gameTimerAI.Start();

            // Запуск новой игры
            StartGame();

            // Определяем методы для событий KeyDown и KeyUp. Эти методы вызываются при нажатии и отпускании клавиши соответственно
            this.KeyDown += (sender, e) => { Input.ChangeState(e.KeyCode, true); };
            this.KeyUp += (sender, e) => { Input.ChangeState(e.KeyCode, false); };
        }

        //Метод, который запускает новую игру
        private void StartGame()
        {
            // Делаем надпись lblGameOver невидимой
            lblGameOver.Visible = false;
            lblGameOverAI.Visible = false;

            // Сбросываем настройки игры до значений по умолчанию
            new Settings();

            // Ставим начальную скорость игры
            gameTimer.Interval = 1000 / Settings.Speed;
            gameTimerAI.Interval = 1000 / Settings.SpeedAI;

            aiSnake.Snake.Clear();
            SnakeElement aiHead = new SnakeElement { X = 10, Y = 5 };
            aiSnake.Snake.Add(aiHead);

            // Очищаем все существующие элементы змеи в List
            Snake.Clear();

            timer = new Stopwatch();
            timer.Start();

            // Создаем новый элемент головы для змеи и добавляем его в List
            SnakeElement head = new SnakeElement { X = 10, Y = 5 };
            Snake.Add(head);

            // Записываем в lblScore очки из настроек
            lblScore.Text = Settings.Score.ToString();

            lblScoreAI.Text = Settings.Score.ToString();

            // Вызываем функцию генерации еды
            GenerateFood();
            aiSnake.GenerateFood();
        }

        private void GenerateFood()
        {
            // Определяем максимально возможные координаты x и y для еды
            int maxXPos = PictureBox.Size.Width / Settings.Width;
            int maxYPos = PictureBox.Size.Height / Settings.Height;

            // Генерируем еду в случайном месте
            Random random = new Random();
            food = new Food { X = random.Next(0, maxXPos), Y = random.Next(0, maxYPos) };
        }

        // Метод, который обновляет состояние игры
        private void UpdateScreen(object sender, EventArgs e)
        {
            
            // Проверка закончена ли игра
            if (Settings.GameOver)
            {
                // Если нажать ентер, игра запустится
                if (Input.KeyPressed(Keys.Enter))
                {
                    StartGame();
                }
            }
            else
            {
                // Если игра не закончена, обрабатываем нажатия клавиш 
                // Далем проверку на движение змеи в протиоположном направлении
                if (Input.KeyPressed(Keys.Right) && Settings.direction != Direction.Left)
                    Settings.direction = Direction.Right;
                else if (Input.KeyPressed(Keys.Left) && Settings.direction != Direction.Right)
                    Settings.direction = Direction.Left;
                else if (Input.KeyPressed(Keys.Up) && Settings.direction != Direction.Down)
                    Settings.direction = Direction.Up;
                else if (Input.KeyPressed(Keys.Down) && Settings.direction != Direction.Up)
                    Settings.direction = Direction.Down;

                //Вызываем функцию перемещения змейки
                MovePlayer();
            }
            // Обновляем PictureBox. Здесь срабатывает событие Paint в PictureBox и перерисовывается состояние игры
            PictureBox.Invalidate();
        }

        private void UpdateScreenAI(object sender, EventArgs e)
        {
            if (Settings.GameOverAI)
            {
                lblGameOverAI.Visible = true;

                if(timer.IsRunning)
                {
                    timer.Stop();
                    string elapsedTimeInSeconds = timer.Elapsed.TotalSeconds.ToString("F1");
                    // Логируем результат
                    LogResults(elapsedTimeInSeconds, Settings.ScoreAI);
                }

                if (Input.KeyPressed(Keys.Enter))
                {
                    StartGame();
                }
            }
            else
            {
                lblTime.Text = "Время игры: " + timer.Elapsed.TotalSeconds.ToString("F1") + " с.";

                aiSnake.MovePlayer();
                aiSnake.UpdateSnake();

                // Обновляем PictureBoxAI. Здесь срабатывает событие Paint в PictureBox и перерисовывается состояние игры
                PictureBoxAI.Invalidate();
            }
        }

        // Метод, который перемещает змею
        private void MovePlayer()
        {
            // Перемещаем каждый элемент змеи
            for (int i = Snake.Count - 1; i >= 0; i--)
            {
                // Перемещаем голову в соответствии с текущим направлением
                if (i == 0)
                {
                    switch (Settings.direction)
                    {
                        case Direction.Right:
                            Snake[i].X++;
                            break;
                        case Direction.Left:
                            Snake[i].X--;
                            break;
                        case Direction.Up:
                            Snake[i].Y--;
                            break;
                        case Direction.Down:
                            Snake[i].Y++;
                            break;
                    }

                    // Получаем максимальные координаты x и y
                    int maxXPos = PictureBox.Size.Width / Settings.Width;
                    int maxYPos = PictureBox.Size.Height / Settings.Height;

                    // Если змея достигает границы поля, то появляется с другой стороны
                    if (Snake[i].X < 0)
                    {
                        Snake[i].X = maxXPos;
                    }
                    else if (Snake[i].Y < 0)
                    {
                        Snake[i].Y = maxYPos;
                    }
                    else if (Snake[i].X >= maxXPos)
                    {
                        Snake[i].X = 0;
                    }
                    else if (Snake[i].Y >= maxYPos)
                    {
                        Snake[i].Y = 0;
                    }

                    //Если нужено чтобы игра заканчивалась когда змея достигает границы
                    //if (Snake[i].X < 0 || Snake[i].Y < 0 || Snake[i].X >= maxXPos || Snake[i].Y >= maxYPos)
                    //{
                    //    Die();
                    //}

                    // Проверяем, если координаты головы совпадают с координатами любого другого элемента змеи, то игра заканчивается
                    for (int j = 1; j < Snake.Count; j++)
                    {
                        if (Snake[i].X == Snake[j].X && Snake[i].Y == Snake[j].Y)
                        {
                            Die();
                        }
                    }

                    // Если координаты головы змеи совпадают с координатами еды, то кушаем, запуская метод Eat
                    if (Snake[0].X == food.X && Snake[0].Y == food.Y)
                    {
                        Eat();
                    }
                }
                else
                {
                    // Перемещаем элементы тела змеи на место предыдущего элемента
                    Snake[i].X = Snake[i - 1].X;
                    Snake[i].Y = Snake[i - 1].Y;
                }
            }
        }

        // Метод, который завершает игру
        private void Die()
        {
            // Установливаем флаг GameOver в значение true
            Settings.GameOver = true;
            //Settings.GameOverAI = true;
        }

        // Метод, который срабатывает когда змея съедает пищу
        private void Eat()
        {
            // Добавляем в конец змейки новый элемент
            SnakeElement circle = new SnakeElement
            {
                X = Snake[Snake.Count - 1].X,
                Y = Snake[Snake.Count - 1].Y
            };
            Snake.Add(circle);

            // Увеличиваем очки и выводим их в lblScore
            Settings.Score += Settings.Points;
            lblScore.Text = Settings.Score.ToString();

            // Увеличиваем скорость змейки
            if (gameTimer.Interval > 1)
            {
                gameTimer.Interval -= 1;
            }

            // Генерируем новую еду
            GenerateFood();
        }

        // Метод, который отображает текущее состояние игры. Вызывается каждый раз, когда запускается событие Paint в PictureBox.
        private void PictureBox_Paint(object sender, PaintEventArgs e)
        {
            Graphics canvas = e.Graphics;

            // Проверяем, закончена ли игра
            if (!Settings.GameOver)
            {
                // Если игра не закончена, рисуем змею и еду

                // Отрисовываем все элементы змеи в цикле
                for (int i = 0; i < Snake.Count; i++)
                {
                    //Цвет границы элементов змеи
                    Brush snakeColour;
                    Pen snakeBorder = Pens.Black;

                    if (i == 0)
                        snakeColour = Brushes.White;     //Цвет головы змеи
                    else
                        snakeColour = Brushes.Green;    //Цвет тела змеи

                    //Отрисовываем элементы змеи 
                    canvas.FillRectangle(snakeColour,
                     new Rectangle(Snake[i].X * Settings.Width,
                         Snake[i].Y * Settings.Height,
                         Settings.Width, Settings.Height));

                    //Отрисовываем границы элементов змеи 
                    canvas.DrawRectangle(snakeBorder,
                        new Rectangle(Snake[i].X * Settings.Width,
                            Snake[i].Y * Settings.Height,
                            Settings.Width, Settings.Height));

                }
                // Рисуем еду
                canvas.FillEllipse(Brushes.Red,
                    new Rectangle(food.X * Settings.Width,
                         food.Y * Settings.Height, Settings.Width, Settings.Height));
            }
            else
            {
                // Если игра окончена, выводим сообщение в gameOver
                string gameOver = "Игра окончена \nНабрано очков: " + Settings.Score + "\nНажмите Enter для новой игры";
                lblGameOver.Text = gameOver;
                lblGameOver.Visible = true;
            }
        }

        private void PictureBoxAI_Paint(object sender, PaintEventArgs e)
        {
            Graphics canvas = e.Graphics;

            if (!Settings.GameOverAI)
            {
                for (int i = 0; i < aiSnake.Snake.Count; i++)
                {
                    Brush snakeColour;
                    Pen snakeBorder = Pens.Black;
                    if (i == 0)
                        snakeColour = Brushes.White;
                    else
                        snakeColour = Brushes.Green;

                    canvas.FillRectangle(snakeColour,
                        new Rectangle(aiSnake.Snake[i].X * Settings.Width,
                                      aiSnake.Snake[i].Y * Settings.Height,
                                      Settings.Width, Settings.Height));

                    canvas.DrawRectangle(snakeBorder,
                            new Rectangle(aiSnake.Snake[i].X * Settings.Width,
                                        aiSnake.Snake[i].Y * Settings.Height,
                                        Settings.Width, Settings.Height));

                }
                canvas.FillEllipse(Brushes.Red,
                        new Rectangle(aiSnake.Food.X * Settings.Width,
                                      aiSnake.Food.Y * Settings.Height, Settings.Width, Settings.Height));
            }
            else
            {
                // Если игра окончена, выводим сообщение в gameOver
                string gameOver = "Игра ИИ окончена \nНабрано очков: " + Settings.ScoreAI;
                lblGameOverAI.Text = gameOver;
                lblGameOverAI.Visible = true;
            }
        }
        // Функция для записывания логов в файл
        private void LogResults(string gameDuration, int pointsScored)
        {
            // Ключевое слово using в C# используется для указания области, в конце которой будет удален объект
            // Другими словами, объект StreamWriter будет закрыт, а его ресурсы освобождены, когда блок using закончится
            using (StreamWriter writer = new StreamWriter("../../logs/results.log", true))
            {
                writer.WriteLine("-------------- AI Log --------------");
                writer.WriteLine("Длительность игры: " + gameDuration);
                writer.WriteLine("Набрано очков: " + pointsScored);
                writer.WriteLine("------------------------------------");
                writer.WriteLine();
            }
        }
    }
}
