using SoloTest.Components;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SoloTest
{
    public partial class Main : Form
    {
        Board board;
        
        public Main()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            board = new Board();
            //board.MouseClick += BoardOnClick;
            this.Controls.Add(board);
            
        }
        
        

        
       
/*
        public void BoardOnClick(object sender, MouseEventArgs e)
        {

            int row = 0;
            int verticalOffset = 0;
            foreach (int h in board.GetRowHeights())
            {

                int column = 0;
                int horizontalOffset = 0;
                foreach (int w in board.GetColumnWidths())
                {
                    Rectangle rectangle = new Rectangle(horizontalOffset, verticalOffset, w, h);
                    if (rectangle.Contains(e.Location))
                    {
                        MessageBox.Show(String.Format("row {0}, column {1} was clicked", row, column));
                        return;
                    }
                    horizontalOffset += w;
                    column++;
                }
                verticalOffset += h;
                row++;
            }

        }*/
    }
}
