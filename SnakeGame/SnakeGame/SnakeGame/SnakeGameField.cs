using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace SnakeGame
{
    internal class SnakeGameField
    {
        #region Fields
        private Canvas _field;
        private const int _mapSquareSize = 20;

        public Canvas Field { get { return _field; } }
        public static int MapSquareSize => _mapSquareSize;

        #endregion

        #region Constructors
        public SnakeGameField(Canvas canvas)
        {
            _field=canvas;
            BackGroundInit();
            
        }
        #endregion

        #region Methods
        private void BackGroundInit()
        {
            for (int i = 0; i < Field.Height / _mapSquareSize; i++)
            {
                for (int j = 0; j < Field.Width / _mapSquareSize; j++)
                {
                    Rectangle rect = new Rectangle
                    {
                        Width = MapSquareSize,
                        Height = MapSquareSize,
                        Fill = Convert.ToBoolean((i+j)%2)? Brushes.SeaGreen : Brushes.YellowGreen
                    };
                    Field.Children.Add(rect);
                    Canvas.SetTop(rect, i * MapSquareSize);
                    Canvas.SetLeft(rect, j*MapSquareSize);
                }
            }
        }
        #endregion
    }
}
