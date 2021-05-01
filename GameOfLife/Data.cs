using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameOfLife
{
    public static class Data
    {
        public static int CellsX = 80, CellsY = 80;

        public static Point CellSize = new Point(10, 10);

        public static Cell[,] Cells;

        public static float deltaTime;


        public static Texture2D debugTexture;
        public static void DrawCell(SpriteBatch _spriteBatch, Cell cell)
        {
            if (debugTexture == null)
            {
                debugTexture = new Texture2D(_spriteBatch.GraphicsDevice, 1, 1);
                debugTexture.SetData(new Color[] { Color.White });
            }
            _spriteBatch.Draw(debugTexture, new Rectangle(cell.Position * CellSize, CellSize), cell.Color);
        }
    }
}
