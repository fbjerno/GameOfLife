using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameOfLife
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        InputManager input = new InputManager();

        float lifeCycleTimer = 0;

        bool isPaused = true;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            IsFixedTimeStep = false;
        }

        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = 800;
            _graphics.PreferredBackBufferHeight = 800;
            Window.IsBorderless = false;
            _graphics.ApplyChanges();

            int cursorWidth = 5, cursorHeight = 5;
            Texture2D cursor = new Texture2D(GraphicsDevice, cursorWidth, cursorHeight);
            Color[] cursorData = new Color[cursorWidth * cursorHeight];
            for (int i = 0; i < cursorData.Length; i++)
                cursorData[i] = Color.Red;
            cursor.SetData(cursorData);

            Mouse.SetCursor(MouseCursor.FromTexture2D(cursor, 1, 1));

            Data.Cells = new Cell[Data.CellsX, Data.CellsY];

            Clear();

            base.Initialize();
        }

        void Clear()
        {
            for (int y = 0; y < Data.CellsY; y++)
            {
                for (int x = 0; x < Data.CellsX; x++)
                {
                    if (Data.Cells[x, y] == null)
                        Data.Cells[x, y] = new Cell(new Point(x, y)) { State = CellState.Dead };
                    else
                        Data.Cells[x, y].State = CellState.Dead;
                }
            }
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            input.Update();

            Data.deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (input.MouseX >= 0 && input.MouseX <= Data.CellsX * Data.CellSize.X && input.MouseY >= 0 && input.MouseY <= Data.CellsY * Data.CellSize.Y)
            {
                if (input.LeftMousePressed)
                    Data.Cells[input.MouseX / Data.CellSize.X, input.MouseY / Data.CellSize.Y].State = CellState.Alive;

                if (input.RightMousePressed)
                    Data.Cells[input.MouseX / Data.CellSize.X, input.MouseY / Data.CellSize.Y].State = CellState.Dead;
            }

            if(input.KeyClicked(Keys.Space))
            {
                isPaused = !isPaused;
                lifeCycleTimer = 0f;
            }

            if (input.KeyClicked(Keys.Back))
                Clear();

            if (lifeCycleTimer > 1f)
            {
                int maxX = Data.Cells.GetLength(0), maxY = Data.Cells.GetLength(1);

                for (int y = 0; y < maxY; y++)
                {
                    for (int x = 0; x < maxX; x++)
                    {
                        int livingNeighbours = 0;
                        for (int y2 = -1; y2 <= 1; y2++)
                        {
                            for (int x2 = -1; x2 <= 1; x2++)
                            {
                                int currentX = x + x2, currentY = y + y2;

                                if ((x2 == 0 && y2 == 0) || currentX < 0 || currentY < 0 || currentX >= maxX || currentY >= maxY)
                                    continue;

                                if (Data.Cells[x + x2, y + y2].State == CellState.Alive)
                                    livingNeighbours++;
                            }
                        }

                        Data.Cells[x, y].LivingNeighbours = livingNeighbours;
                        Data.Cells[x, y].DeadNeighbours = 8 - livingNeighbours;
                    }
                }

                foreach (Cell cell in Data.Cells)
                    cell.Update();

                lifeCycleTimer = 0;
            }
            if(!isPaused)
                lifeCycleTimer += Data.deltaTime;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            _spriteBatch.Begin();

            foreach (Cell cell in Data.Cells)
                cell.Draw(_spriteBatch);

            if (isPaused)
                _spriteBatch.Draw(Data.debugTexture, new Rectangle(0, 0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight), Color.Gray * 0.5f);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
