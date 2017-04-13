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
    class Agent
    {
        private ToolTip toolTip;
        Panel panel;
        Panel[,] space;
        Panel locPanel;
        Point locInPanel;
        Point locInGeneral;
        Point[] dest;
        Size size;
        Color color;
        Timer timer;
        AgentType type;
        Table[,] tables;

        public Point Location
        {
            get { return locInGeneral; }
            set
            {
                if (value.X <= 9 && value.Y <= 5)
                    locInGeneral = value;
                else
                    throw new IndexOutOfRangeException();
            }
        }
        public AgentType Type
        {
            get
            {
                return type;
            }
            set
            {
                type = value;

                if (type == AgentType.Wait)
                {
                    toolTip.SetToolTip(Panel, "Очікую замовлень");
                }
                else if (type == AgentType.ReceiveOrder)
                {
                    toolTip.SetToolTip(Panel, "Йду по замовлення до столику (" + (GetTableByLocation(Dest[Dest.Length - 1]).X + 1) + "; " + (GetTableByLocation(Dest[Dest.Length - 1]).Y + 1) + ")");
                }
                else if (type == AgentType.Service)
                {
                    toolTip.SetToolTip(Panel, "Обслуговую столик (" + (GetTableByLocation(Location).X + 1) + "; " + (GetTableByLocation(Location).Y + 1) + ")");
                }
                else if (type == AgentType.ToHome)
                {
                    toolTip.SetToolTip(Panel, "Йду додому по замовлення");
                }
                else if (type == AgentType.WaitFood)
                {
                    toolTip.SetToolTip(Panel, "Чекаю поки приготується їжа");
                }
                else if (type == AgentType.BringOrder)
                {
                    toolTip.SetToolTip(Panel, "Відношу замовлення до столику (" + (GetTableByLocation(Dest[Dest.Length - 1]).X + 1) + "; " + (GetTableByLocation(Dest[Dest.Length - 1]).Y + 1) + ")");
                }
                else if (type == AgentType.ReturnWithOrder)
                {
                    toolTip.SetToolTip(Panel, "Вертаю замовлення додому");
                }
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
            }
        }
        public Point[] Dest
        {
            get
            {
                return dest;
            }

            set
            {
                dest = value;
            }
        }
        public ToolTip ToolTip
        {
            get
            {
                return toolTip;
            }
            set
            {
                toolTip = value;
            }
        }
        public Point DestP
        {
            get
            {
                if (dest.Length > 0)
                    return dest[dest.Length - 1];
                else
                    return Location;
            }
        }
        Point GetTableByLocation(Point loc)
        {
            for (int i = 0; i < tables.GetLength(0); i++)
            {
                for (int j = 0; j < tables.GetLength(1); j++)
                {
                    if (tables[i, j].Location == loc)
                        return new Point(i, j);
                }
            }
            return Point.Empty;
        }


        public Agent(Panel[,] space, Point loc, Table[,] tables_)
        {
            this.space = space;
            locPanel = space[loc.X, loc.Y];
            locInGeneral = loc;
            size = new Size(10, 10);
            locInPanel = new Point(locPanel.Width - size.Width, locPanel.Height - size.Height);
            color = Color.Blue;
            type = AgentType.Wait;

            panel = new Panel();
            panel.BackColor = color;
            panel.Size = size;
            panel.Location = locInPanel;

            locPanel.Controls.Add(panel);

            timer = new Timer() { Interval = 800 };
            timer.Tick += Timer_Tick;

            toolTip = new ToolTip() { IsBalloon = true };
            toolTip.SetToolTip(panel, "Очікую замовлень");

            tables = tables_;
        }


        private void Timer_Tick(object sender, EventArgs e)
        {
            timer.Stop();
        }
        Point[] RemoveAt(Point[] source, int index)
        {
            Point[] dest = new Point[source.Length - 1];
            if (index > 0)
                Array.Copy(source, 0, dest, 0, index);

            if (index < source.Length - 1)
                Array.Copy(source, index + 1, dest, index, source.Length - index - 1);

            return dest;
        }
        public void MoveToDest()
        {
            if (dest != null)
            {
                if (dest.Length > 0)
                {
                    MoveIntoPanel(dest[0]);
                    dest = RemoveAt(dest, 0);
                }
            }

        }
        public void MoveIntoPanel(Point p)
        {
            locInGeneral = p;
            locPanel.Controls.Remove(panel);
            locPanel = space[p.X, p.Y];
            locPanel.Controls.Add(panel);

        }

    }
}
