using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameOfLife
{
    public enum CellState { Alive, Dead }

    public class Cell
    {
        public Point Position;

        public CellState State = CellState.Dead;

        public Color Color = Color.White;

        public bool ComeAlive = false;

        public int LivingNeighbours = 0, DeadNeighbours = 0;

        public Cell(Point position)
        {
            Position = position;
        }

        public void Update()
        {
            if (State == CellState.Dead && LivingNeighbours == 3)
            {
                State = CellState.Alive;
                return;
            }

            if(LivingNeighbours < 2 || LivingNeighbours > 3)
                State = CellState.Dead;
        }

        public void Draw(SpriteBatch _spriteBatch)
        {
            if (State == CellState.Alive)
                Color = Color.Black;
            else
                Color = Color.White;

            Data.DrawCell(_spriteBatch, this);
        }
    }
}
