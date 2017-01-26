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
    public partial class Form1 : Form
    {
        // for paint
        Point[] chairLoc;
        Point tableLoc;
        Size chairSize;
        Size tableSize;

        Panel[,] panels;
        Table[,] tables;
        Random rnd;
        const int maxClientCount = 6;
        Agent agent;
        Point homeP;

        public int T_X
        {
            get { return tables.GetLength(0); }
        }
        public int T_Y
        {
            get { return tables.GetLength(1); }
        }
        public int P_X
        {
            get { return panels.GetLength(0); }
        }
        public int P_Y
        {
            get { return panels.GetLength(1); }
        }
        public int MaxClientCount
        {
            get { return maxClientCount; }
        }



        public Form1()
        {
            InitializeComponent();
            rnd = new Random();

            //Action a1 = () => listBox1.Items.Add(1);
            //Action a2 = () => listBox1.Items.Add(2);
            //Action a3 = () => listBox1.Items.Add(3);
            //((a1 + a2 + a3) - (a1 + a2))();
            //((a1 + a2 + a3) - (a1 + a3))();


            // свторюю поле з panels і столи tables
            panels = new Panel[9, 5];
            tables = new Table[4, 2];
            int width = panel.Width / panels.GetLength(0);
            int height = panel.Height / panels.GetLength(1);
            for (int i = 0; i < panels.GetLength(0); i++)
            {
                for (int j = 0; j < panels.GetLength(1); j++)
                {
                    panels[i, j] = new Panel() { Location = new Point(i * width, j * height), Size = new Size(width, height), BorderStyle = BorderStyle.FixedSingle };
                    panel.Controls.Add(panels[i, j]);
                }
            }
            for (int i = 0; i < tables.GetLength(0); i++)
            {
                for (int j = 0; j < tables.GetLength(1); j++)
                {
                    tables[i, j] = new Table(new Point(i * 2 + 1, j * 2 + 1));
                }
            }

            // додаю типи клієнтів у комбобокс
            foreach (ClientType item in Enum.GetValues(typeof(ClientType)))
            {
                cbCustomerType.Items.Add(new Client(item).Name);
            }
            cbCustomerType.SelectedIndex = 0;

            // задаю початковий стан форми
            nudX.Minimum = 1;
            nudX.Maximum = T_X;
            nudY.Minimum = 1;
            nudY.Maximum = T_Y;
            chbRandLoc.Checked = true;
            chbRandType.Checked = true;

            // for paint
            chairSize = new Size(8, 8);
            tableSize = new Size(22, 22);
            chairLoc = new Point[maxClientCount];
            double R = (double)panels[0, 0].Width / 2 - (double)panels[0, 0].Width / 8;
            double r = (double)chairSize.Width / 2;
            double center = (double)panels[0, 0].Width / 2;
            tableLoc = new Point((int)(center - (double)tableSize.Width / 2), (int)(center - (double)tableSize.Height / 2));
            for (int i = 0; i < chairLoc.Length; i++)
            {
                chairLoc[i] = new Point((int)(R * Math.Cos(i * 2 * Math.PI / maxClientCount) + center - r), (int)(R * Math.Sin(i * 2 * Math.PI / maxClientCount) + center - r));
            }

            // додаю столи на panels
            for (int i = 0; i < T_X; i++)
            {
                for (int j = 0; j < T_Y; j++)
                {
                    tables[i, j].Panel = new Panel() { Location = tableLoc, Size = tableSize };
                    panels[tables[i, j].Location.X, tables[i, j].Location.Y].Controls.Add(tables[i, j].Panel);
                }
            }

            // задаю початкову точку і створюю агента
            homeP = new Point(panels.GetLength(0) - 1, panels.GetLength(1) - 1);
            agent = new Agent(panels, homeP);

            //ToolTip t = new ToolTip();
            //t.SetToolTip(tables[0, 0].Panel, "bla");
            //t.SetToolTip(tables[0, 0].Panel, "bla1");
            //t.IsBalloon = true;
        }



        void AddClient(Point p, ClientType type)
        {
            if (tables[p.X, p.Y].Count < maxClientCount)
            {
                Client c = new Client(type);
                c.Panel = new Panel() { Location = chairLoc[tables[p.X, p.Y].Count], Size = chairSize };
                tables[p.X, p.Y].Add(c);
                panels[tables[p.X, p.Y].Location.X, tables[p.X, p.Y].Location.Y].Controls.Add(c.Panel);
            }
            else
            {
                MessageBox.Show("Недостатньо місць, оберіть інший столик!");
            }
        }
        void ChooseRightDestination()
        {
            List<int>[,] dist = new List<int>[T_X, T_Y];

            for (int i = 0; i < T_X; i++)
            {
                for (int j = 0; j < T_Y; j++)
                {
                    dist[i, j] = new List<int>();
                    for (int clCount = 0; clCount < tables[i, j].Count; clCount++)
                    {

                    }
                }
            }
        }

        //void Backtracking(int n, int m, int[][] Maze)
        //{
        //    int Begin, End, Current;
        //    Begin = (n - 1) * m;
        //    End = m - 1;
        //    int[] Way, OptimalWay;
        //    int LengthWay, LengthOptimalWay;
        //    Way = new int[n * m];
        //    OptimalWay = new int[n * m];
        //    LengthWay = 0;
        //    LengthOptimalWay = m * n;
        //    for (int i = 0; i < n * m; i++)
        //        Way[i] = OptimalWay[i] = -1;
        //    int[] Dist;
        //    Dist = new int[n * m];
        //    for (int i = 0; i < n; i++)
        //        for (int j = 0; j < m; j++)
        //            Dist[i * m + j] = (Maze[i][j] == 0 ? 0 : -1);
        //    Way[LengthWay++] = Current = Begin;
        //    while (LengthWay > 0)
        //    {
        //        if (Current == End)
        //        {
        //            if (LengthWay < LengthOptimalWay)
        //            {
        //                for (int i = 0; i < LengthWay; i++)
        //                    OptimalWay[i] = Way[i];
        //                LengthOptimalWay = LengthWay;
        //            }
        //            if (LengthWay > 0) Way[--LengthWay] = -1;
        //            Current = Way[LengthWay - 1];
        //        }
        //        else {
        //            int Neighbor = -1;
        //            if ((Current / m - 1) >= 0 && !Insert(Way, Current - m) &&
        //              (Dist[Current - m] == 0 || Dist[Current - m] > LengthWay)
        //              && Dist[Current] < LengthOptimalWay)
        //                Neighbor = Current - m;
        //            else
        //              if ((Current % m - 1) >= 0 && !Insert(Way, Current - 1) &&
        //                (Dist[Current - 1] == 0 || Dist[Current - 1] > LengthWay)
        //                && Dist[Current] < LengthOptimalWay)
        //                Neighbor = Current - 1;
        //            else
        //                if ((Current % m + 1) < m && !Insert(Way, Current + 1) &&
        //                 (Dist[Current + 1] == 0 || Dist[Current + 1] > LengthWay)
        //                && Dist[Current] < LengthOptimalWay)
        //                Neighbor = Current + 1;
        //            else
        //                 if ((Current / m + 1) < n && !Insert(Way, Current + m) &&
        //                  (Dist[Current + m] == 0 || Dist[Current + m] > LengthWay)
        //                 && Dist[Current] < LengthOptimalWay)
        //                Neighbor = Current + m;
        //            if (Neighbor != -1)
        //            {
        //                Way[LengthWay++] = Neighbor;
        //                Dist[Neighbor] = Dist[Current] + 1;
        //                Current = Neighbor;
        //            }
        //            else {
        //                if (LengthWay > 0) Way[--LengthWay] = -1;
        //                Current = Way[LengthWay - 1];
        //            }
        //        }
        //    }
        //    if (LengthOptimalWay < n * m)
        //        cout << endl << "Yes. Length way=" << LengthOptimalWay << endl;
        //    else cout << endl << "No" << endl;
        //}

        void WaveAlgorithm(Point s, Point t)
        {
            List<Point> oldFront = new List<Point>();
            oldFront.Add(s);
            List<Point> newFront = new List<Point>();
            int T = 0;
            int[,] TT = new int[P_X, P_Y];
            for (int i = 0; i < P_X; i++)
            {
                for (int j = 0; j < P_Y; j++)
                {
                    TT[i, j] = -1;
                }
            }

            while (!newFront.Contains(t))
            {
                newFront = new List<Point>();
                for (int i = 0; i < oldFront.Count; i++)
                {
                    if (oldFront[i].X - 1 >= 0)
                    {
                        if (TT[oldFront[i].X - 1, oldFront[i].Y] == -1)
                        {
                            TT[oldFront[i].X - 1, oldFront[i].Y] = T + 1;
                            newFront.Add(new Point(oldFront[i].X - 1, oldFront[i].Y));
                        }
                    }
                    if (oldFront[i].X + 1 < P_X)
                    {
                        if (TT[oldFront[i].X + 1, oldFront[i].Y] == -1)
                        {
                            TT[oldFront[i].X + 1, oldFront[i].Y] = T + 1;
                            newFront.Add(new Point(oldFront[i].X + 1, oldFront[i].Y));
                        }
                    }
                    if (oldFront[i].Y - 1 >= 0)
                    {
                        if (TT[oldFront[i].X, oldFront[i].Y - 1] == -1)
                        {
                            TT[oldFront[i].X, oldFront[i].Y - 1] = T + 1;
                            newFront.Add(new Point(oldFront[i].X, oldFront[i].Y - 1));
                        }
                    }
                    if (oldFront[i].Y + 1 < T_Y)
                    {
                        if (TT[oldFront[i].X, oldFront[i].Y + 1] == -1)
                        {
                            TT[oldFront[i].X, oldFront[i].Y + 1] = T + 1;
                            newFront.Add(new Point(oldFront[i].X, oldFront[i].Y + 1));
                        }
                    }
                }

                if (newFront.Count == 0)
                    return;

                oldFront = new List<Point>(newFront);
                T++;
            }


        }
        Point GetInd(object[,] arr, object value)
        {
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                for (int j = 0; j < arr.GetLength(1); j++)
                {
                    if (arr[i, j] == value)
                        return new Point(i, j);
                }
            }
            return Point.Empty;
        }
        private void butNextStep_Click(object sender, EventArgs e)
        {
            WaveAlgorithm(homeP, new Point(0, 0));

            if (agent.Type == AgentType.BringOrder)
            {

            }


            // зменшую таймери для кожного відвідувача
            for (int i = 0; i < T_X; i++)
            {
                for (int j = 0; j < T_Y; j++)
                {
                    for (int clCount = 0; clCount < tables[i, j].Count; clCount++)
                    {
                        Client c = tables[i, j][clCount];
                        if (c.TimeType == TimeType.Order)
                        {
                            c.OrderTime--;
                        }
                        else if (c.TimeType == TimeType.Wait)
                        {
                            c.WaitTime--;
                        }
                        else if (c.TimeType == TimeType.Eat)
                        {
                            c.EatTime--;
                        }
                    }
                }
            }

        }
        private void butNewClient_Click(object sender, EventArgs e)
        {
            ClientType type;
            Point loc;
            if (chbRandLoc.Checked)
                loc = new Point(rnd.Next(0, T_X), rnd.Next(0, T_Y));
            else
                loc = new Point((int)nudX.Value - 1, (int)nudY.Value - 1);
            if (chbRandType.Checked)
                type = (ClientType)rnd.Next(0, Enum.GetValues(typeof(ClientType)).Length);
            else
                type = (ClientType)cbCustomerType.SelectedIndex;

            AddClient(loc, type);
        }
        private void chbRandType_CheckedChanged(object sender, EventArgs e)
        {
            if (chbRandType.Checked)
                cbCustomerType.Enabled = false;
            else
                cbCustomerType.Enabled = true;
        }
        private void chbRandLoc_CheckedChanged(object sender, EventArgs e)
        {
            if (chbRandLoc.Checked)
            {
                nudX.Enabled = false;
                nudY.Enabled = false;
            }
            else
            {
                nudX.Enabled = true;
                nudY.Enabled = true;
            }
        }
    }



    enum ClientType
    {
        GrandParent,
        Parent,
        Child,
        Pet
    }
    enum TimeType
    {
        Order,
        Wait,
        Eat
    }
    enum AgentType
    {
        Wait,
        ToHome,
        BringOrder,
        Service,

    }
    class Client
    {
        TimeType timeType;
        Panel panel;
        ClientType type;
        Point place;
        int orderTime;
        int waitTime;
        int eatTime;
        string name;
        Color color;

        // properties
        public ClientType Type { get { return type; } }
        public Point Place
        {
            get { return place; }
            set { place = value; }
        }
        public int OrderTime
        {
            get { return orderTime; }
            set { orderTime = value; }
        }
        public int WaitTime
        {
            get { return waitTime; }
            set { waitTime = value; }
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
            }
        }

        // constructors
        public Client(ClientType type)
        {
            this.type = type;
            orderTime = GetTimeToOrder();
            waitTime = GetMaxWaitTime();
            eatTime = GetEatTime();
            name = GetName();
            color = GetColor();
            timeType = TimeType.Order;            
        }
        public Client(ClientType type, Point place) : this(type)
        {
            this.place = place;
        }

        // private methods
        int GetMaxWaitTime()
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

    class Agent
    {
        Panel panel;
        Panel[,] space;
        Panel locPanel;
        Point locInPanel;
        Point locInGeneral;
        Size size;
        Color color;
        Timer timer;
        AgentType type;

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
        public Point LocInPanel
        {
            get
            {
                return locInPanel;
            }
            set
            {
                locInPanel = value;
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

        public Agent(Panel[,] space, Point loc)
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
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            timer.Stop();
        }

        public void MoveToDest(Point dest)
        {

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