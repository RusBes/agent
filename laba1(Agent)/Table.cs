using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace laba1_Agent_
{
    class Table : List<Client>
    {
        Point location;
        Panel panel;
        Color color;
        // properties
        public Point Location
        {
            get { return location; }
            set
            {
                if (value.X <= 9 && value.Y <= 5)
                    location = value;
                else
                    throw new IndexOutOfRangeException();
            }
        }
        public Panel Panel
        {
            get
            {
                return panel;
            }

            set
            {
                panel = value;
                panel.Paint += Panel_Paint;
            }
        }

        private void Panel_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            SolidBrush b = new SolidBrush(color);

            g.FillEllipse(b, 0, 0, panel.Width, panel.Height);
        }

        // constructors
        public Table() { }
        public Table(Point location)
        {
            this.location = location;
            color = Color.Brown;
        }
    }
}
