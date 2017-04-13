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
    class Client
    {
        TimeType timeType;
        Panel panel;
        ClientType type;
        Point place;
        int makeOrderTime;
        int waitOrderTime;
        int eatTime;
        public static string name;
        Color color;
        public ToolTip toolTip;

        // properties
        public ClientType Type { get { return type; } }
        public Point Place
        {
            get { return place; }
            set { place = value; }
        }
        public int MakeOrderTime
        {
            get { return makeOrderTime; }
            set
            {
                makeOrderTime = value;
                toolTip.SetToolTip(panel, "Думаю над замовленням(" + makeOrderTime + ")");
            }
        }
        public int WaitOrderime
        {
            get { return waitOrderTime; }
            set
            {
                waitOrderTime = value;
                toolTip.SetToolTip(panel, "Чекаю на замовлення (" + waitOrderTime + ")");
            }
        }
        public int EatTime
        {
            get
            {
                return eatTime;
            }
            set
            {
                eatTime = value;
                toolTip.SetToolTip(panel, "Їм (" + eatTime + ")");
            }
        }
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public Color Color
        {
            get { return color; }
            set { color = value; }
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
                panel.Paint += PaintCircle;
                if (type != ClientType.Pet)
                {
                    toolTip.SetToolTip(panel, "Думаю над замовленням(" + makeOrderTime + ")");
                }
                else
                {
                    toolTip.SetToolTip(panel, "...");
                }
            }
        }
        internal TimeType TimeType
        {
            get
            {
                return timeType;
            }
            set
            {
                timeType = value;
                if (timeType == TimeType.Eat)
                {
                    toolTip.SetToolTip(panel, "Їм (" + eatTime + ")");
                }
                else if (timeType == TimeType.WaitOrder)
                {
                    toolTip.SetToolTip(panel, "Чекаю на замовлення (" + waitOrderTime + ")");
                }
            }
        }

        // constructors
        public Client(ClientType type)
        {
            this.type = type;
            makeOrderTime = GetTimeToOrder() + 8;
            waitOrderTime = GetWaitOrderTime() + 8;
            eatTime = GetEatTime() + 3;
            //waitTime = GetWaitTime();
            name = GetName();
            color = GetColor();
            if(type != ClientType.Pet)
                timeType = TimeType.MakeOrder;
            else
                timeType = TimeType.None;
            toolTip = new ToolTip() { IsBalloon = true };
        }
        public Client(ClientType type, Point place) : this(type)
        {
            this.place = place;
        }

        // private methods
        int GetWaitOrderTime()
        {
            switch (type)
            {
                case ClientType.Child:
                    return 4;
                case ClientType.Parent:
                    return 6;
                case ClientType.GrandParent:
                    return 5;
                case ClientType.Pet:
                    return 0;
                default: return 0;
            }
        }
        int GetTimeToOrder()
        {
            switch (type)
            {
                case ClientType.Child:
                    return 5;
                case ClientType.Parent:
                    return 4;
                case ClientType.GrandParent:
                    return 7;
                case ClientType.Pet:
                    return 0;
                default: return 0;
            }
        }
        int GetEatTime()
        {
            switch (type)
            {
                case ClientType.Child:
                    return 4;
                case ClientType.Parent:
                    return 3;
                case ClientType.GrandParent:
                    return 6;
                case ClientType.Pet:
                    return 0;
                default: return 0;
            }
        }
        int GetWaitTime()
        {
            switch (type)
            {
                case ClientType.Child:
                    return 3;
                case ClientType.Parent:
                    return 5;
                case ClientType.GrandParent:
                    return 5;
                case ClientType.Pet:
                    return 0;
                default: return 0;
            }
        }
        string GetName()
        {
            switch (type)
            {
                case ClientType.Child:
                    return "Дитина";
                case ClientType.Parent:
                    return "Дорослий";
                case ClientType.GrandParent:
                    return "Пожилий";
                case ClientType.Pet:
                    return "Тваринка";
                default: return "";
            }
        }
        Color GetColor()
        {
            switch (type)
            {
                case ClientType.Child:
                    return Color.Red;
                case ClientType.Parent:
                    return Color.Green;
                case ClientType.GrandParent:
                    return Color.Gray;
                case ClientType.Pet:
                    return Color.Yellow;
                default: return Color.Black;
            }
        }
        private void PaintCircle(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            SolidBrush b = new SolidBrush(color);

            g.FillEllipse(b, 0, 0, panel.Width, panel.Height);
        }
    }
}
