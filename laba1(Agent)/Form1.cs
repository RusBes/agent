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
    enum ClientType
    {
        GrandParent,
        Parent,
        Child,
        Pet
    }
    enum TimeType
    {
        /// <summary>
        /// клієнт думає над замовленням
        /// </summary>
        MakeOrder,
        /// <summary>
        /// клієнт чекає на замовлення
        /// </summary>
        WaitOrder,
        /// <summary>
        /// клієнт їсть
        /// </summary>
        Eat,
        /// <summary>
        /// для тваринок
        /// </summary>
        None

    }
    

    enum AgentType
    {
        /// <summary>
        /// чекає завдання, якщо немає то йде додому
        /// </summary>
        Wait,
        /// <summary>
        /// вертається додому за їжею, не зважає на інших відвідувачів
        /// </summary>
        ToHome,
        /// <summary>
        /// несе замовлення до столику, не зважає на інших відвідувачів
        /// </summary>
        BringOrder,
        /// <summary>
        /// обслуговує столик (1 хід)
        /// </summary>
        Service,
        /// <summary>
        /// чекає поки приготується їжа (1 хід)
        /// </summary>
        WaitFood,
        /// <summary>
        /// йде забирати замовлення, якщо зявляється клієнт ближче то йде до нього
        /// </summary>
        ReceiveOrder,
        /// <summary>
        /// коли не встиг віднести замовлення, то має його повернути додому а потім вільний
        /// </summary>
        ReturnWithOrder

    }

    public partial class Form1 : Form
    {
        // for paint
        Point[] chairLoc;
        Point tableLoc;
        Size chairSize;
        Size tableSize;

        Panel[,] panels;
        Table[,] tables;
        Table tableToOrder;
        Random rnd;
        const int maxClientCount = 6;
        Agent agent;
        Point homeP;
        List<Client> newClients;

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
            newClients = new List<Client>();


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
            Label l = new Label() { Location = new Point(-1, 10), Text = "Барна стійка" };
            panels[P_X - 1, P_Y - 1].Controls.Add(l);
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
            agent = new Agent(panels, homeP, tables);
        }



        Point[] WaveAlgorithm(Point s, Point t)
        {
            if (t == s)
            {
                return new Point[1] { s };
            }
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

            // перешкоди (столи)
            List<Point> obstacles = new List<Point>();
            for (int i = 0; i < tables.GetLength(0); i++)
            {
                for (int j = 0; j < tables.GetLength(1); j++)
                {
                    if (tables[i, j].Location != t && tables[i, j].Location != s)
                        obstacles.Add(tables[i, j].Location);
                }
            }
            // шукаю шлях до столика
            while (!newFront.Contains(t))
            {
                newFront = new List<Point>();
                for (int i = 0; i < oldFront.Count; i++)
                {
                    if (oldFront[i].X - 1 >= 0 && !obstacles.Contains(new Point(oldFront[i].X - 1, oldFront[i].Y)))
                    {
                        if (TT[oldFront[i].X - 1, oldFront[i].Y] == -1)
                        {
                            TT[oldFront[i].X - 1, oldFront[i].Y] = T + 1;
                            newFront.Add(new Point(oldFront[i].X - 1, oldFront[i].Y));
                        }
                    }
                    if (oldFront[i].X + 1 < P_X && !obstacles.Contains(new Point(oldFront[i].X + 1, oldFront[i].Y)))
                    {
                        if (TT[oldFront[i].X + 1, oldFront[i].Y] == -1)
                        {
                            TT[oldFront[i].X + 1, oldFront[i].Y] = T + 1;
                            newFront.Add(new Point(oldFront[i].X + 1, oldFront[i].Y));
                        }
                    }
                    if (oldFront[i].Y - 1 >= 0 && !obstacles.Contains(new Point(oldFront[i].X, oldFront[i].Y - 1)))
                    {
                        if (TT[oldFront[i].X, oldFront[i].Y - 1] == -1)
                        {
                            TT[oldFront[i].X, oldFront[i].Y - 1] = T + 1;
                            newFront.Add(new Point(oldFront[i].X, oldFront[i].Y - 1));
                        }
                    }
                    if (oldFront[i].Y + 1 < P_Y && !obstacles.Contains(new Point(oldFront[i].X, oldFront[i].Y + 1)))
                    {
                        if (TT[oldFront[i].X, oldFront[i].Y + 1] == -1)
                        {
                            TT[oldFront[i].X, oldFront[i].Y + 1] = T + 1;
                            newFront.Add(new Point(oldFront[i].X, oldFront[i].Y + 1));
                        }
                    }
                }

                if (newFront.Count == 0)
                    return null;

                oldFront = new List<Point>(newFront);
                T++;
            }

            List<Point> way = new List<Point>();
            Point loc = t;
            //T = Math.Abs(t.X - s.X) + Math.Abs(t.Y - s.Y);
            while (T != 1)
            {
                if (loc.X - 1 >= 0)
                {
                    if (TT[loc.X - 1, loc.Y] == T - 1)
                    {
                        loc = new Point(loc.X - 1, loc.Y);
                        way.Add(loc);
                        T--;
                        continue;
                    }
                }
                if (loc.X + 1 < P_X)
                {
                    if (TT[loc.X + 1, loc.Y] == T - 1)
                    {
                        loc = new Point(loc.X + 1, loc.Y);
                        way.Add(loc);
                        T--;
                        continue;
                    }
                }
                if (loc.Y - 1 >= 0)
                {
                    if (TT[loc.X, loc.Y - 1] == T - 1)
                    {
                        loc = new Point(loc.X, loc.Y - 1);
                        way.Add(loc);
                        T--;
                        continue;
                    }
                }
                if (loc.Y + 1 < P_Y)
                {
                    if (TT[loc.X, loc.Y + 1] == T - 1)
                    {
                        loc = new Point(loc.X, loc.Y + 1);
                        way.Add(loc);
                        T--;
                        continue;
                    }
                }

            }
            way.Reverse();
            way.Add(t);
            return way.ToArray();

        }
        void ServiceTable(Table t)
        {
            for (int k = 0; k < t.Count; k++)
            {
                if (t[k].TimeType == TimeType.MakeOrder)
                    t[k].TimeType = TimeType.WaitOrder;
            }
        }
        void LookForBetterWay()
        {
            //if (agent.Type == AgentType.Wait)
            //{
            List<Point[]> list = new List<Point[]>();
            for (int i = 0; i < tables.GetLength(0); i++)
            {
                for (int j = 0; j < tables.GetLength(1); j++)
                {
                    for (int k = 0; k < tables[i, j].Count; k++)
                    {
                        // шукаю всіх клієнтів хто чекає офіціанта (крім дітей і тваринок)
                        if (tables[i, j][k].TimeType == TimeType.MakeOrder && !IsAllChildrenOrPets(tables[i, j]))
                        {
                            list.Add(WaveAlgorithm(agent.Location, tables[i, j].Location));
                        }
                    }
                }
            }
            if (agent.Type == AgentType.Wait)
            {
                if (list.Count > 0)
                {
                    agent.Dest = GetMinWay(list);
                    agent.Type = AgentType.ReceiveOrder;
                }
                else
                {
                    agent.Dest = WaveAlgorithm(agent.Location, homeP);
                }
            }
            else if (agent.Type == AgentType.ReceiveOrder)
            {
                //List<Point[]> list = new List<Point[]>();
                //for (int i = 0; i < tables.GetLength(0); i++)
                //{
                //    for (int j = 0; j < tables.GetLength(1); j++)
                //    {
                //        for (int k = 0; k < tables[i, j].Count; k++)
                //        {
                //            if (tables[i, j][k].TimeType == TimeType.MakeOrder)
                //            {
                //                list.Add(WaveAlgorithm(agent.Location, tables[i, j].Location));
                //            }
                //        }
                //    }
                //}
                if (list.Count > 0)
                {
                    Point[] minWay = GetMinWay(list);
                    if (minWay.Length < agent.Dest.Length)
                        agent.Dest = minWay;
                }
                else
                {
                    agent.Type = AgentType.Wait;
                    agent.Dest = WaveAlgorithm(agent.Location, homeP);
                }
            }
            //}
        }
        void AddClient(Point p, ClientType type)
        {
            if (tables[p.X, p.Y].Count < maxClientCount)
            {
                Client c = new Client(type);
                c.Panel = new Panel() { Location = chairLoc[tables[p.X, p.Y].Count], Size = chairSize };
                tables[p.X, p.Y].Add(c);
                panels[tables[p.X, p.Y].Location.X, tables[p.X, p.Y].Location.Y].Controls.Add(c.Panel);

                newClients.Add(c);
            }
            else
            {
                MessageBox.Show("Недостатньо місць, оберіть інший столик!");
            }
        }
        void ChangeAgentState()
        {
            if (agent.Type == AgentType.ReceiveOrder && agent.Dest.Length == 0)     // якщо агент прийшов забирати замовлення
            {
                agent.Type = AgentType.Service;             // то обслуговує його один хід
                return;
            }
            if (agent.Type == AgentType.Service)            // якщо агент щойно забрав замовлення
            {
                agent.Type = AgentType.ToHome;              // то має вернутись додому за їжею
                for (int i = 0; i < tables.GetLength(0); i++)
                {
                    for (int j = 0; j < tables.GetLength(1); j++)
                    {
                        if (tables[i, j].Location == agent.Location)
                        {
                            tableToOrder = tables[i, j];        // запамятовую за який столик потрібно віднести
                        }
                    }
                }
                agent.Dest = WaveAlgorithm(agent.Location, homeP);
                return;
            }
            if (agent.Type == AgentType.ToHome && agent.Dest.Length == 0)            // якщо агент дістався додому за їжею
            {
                agent.Type = AgentType.WaitFood;              // то чекає один хід поки готується їжа
                return;
            }
            if (agent.Type == AgentType.WaitFood)            // якщо агент дочекався поки приготується їжа
            {
                agent.Dest = WaveAlgorithm(agent.Location, tableToOrder.Location);   // то має віднести її до столику
                agent.Type = AgentType.BringOrder;
                return;
            }
            if (agent.Type == AgentType.BringOrder && agent.Dest.Length == 0)   // якщо віддав заказ
            {
                agent.Type = AgentType.Wait;    // то вільний
                LookForBetterWay();
            }
            if (agent.Type == AgentType.ReturnWithOrder && agent.Location == homeP)   // якщо вернув заказ додому
            {
                agent.Type = AgentType.Wait;    // то вільний
                LookForBetterWay();
            }
        }
        void ChangeClientsTiming()
        {
            for (int i = 0; i < T_X; i++)
            {
                for (int j = 0; j < T_Y; j++)
                {
                    for (int k = 0; k < tables[i, j].Count; k++)
                    {
                        Client c = tables[i, j][k];    // якщо дитина і її час очікування вийшов то нічого не роблю, оскільки діти мають чекати бітьків
                        if (c.Type != ClientType.Pet)
                        {
                            if (c.TimeType == TimeType.MakeOrder)
                            {
                                if (!(c.Type == ClientType.Child && c.MakeOrderTime == 0))
                                    c.MakeOrderTime--;
                            }
                            else if (c.TimeType == TimeType.WaitOrder)
                            {
                                if (!(c.Type == ClientType.Child && c.WaitOrderime == 0))
                                    c.WaitOrderime--;
                            }
                            else if (c.TimeType == TimeType.Eat)
                            {
                                if (!(c.Type == ClientType.Child && c.EatTime == 0))
                                    c.EatTime--;
                            }
                        }
                    }
                }
            }
        }
        void ChangeClientsState()
        {
            for (int i = 0; i < T_X; i++)
            {
                for (int j = 0; j < T_Y; j++)
                {
                    for (int k = 0; k < tables[i, j].Count; k++)        // реагую коли клієнт(и) уходить(ять)
                    {
                        Client c = tables[i, j][k];
                        if (!(c.Type == ClientType.Pet || c.Type == ClientType.Child))
                        {
                            // якщо клієнт пішов
                            if ((
                                (c.MakeOrderTime < 0 && c.TimeType == TimeType.MakeOrder) ||
                                (c.WaitOrderime < 0 && c.TimeType == TimeType.WaitOrder) || 
                                (c.EatTime < 0 && c.TimeType == TimeType.Eat)) && 
                                agent.Location != tables[i, j].Location)
                            {
                                panels[tables[i, j].Location.X, tables[i, j].Location.Y].Controls.Remove(c.Panel);
                                tables[i, j].Remove(c);
                                // якщо агент відносить замовлення 
                                if (agent.Type == AgentType.BringOrder)
                                {
                                    // якщо агент відносив замовлення до стола, за яким був клієнт який пішов
                                    if (agent.DestP == tables[i, j].Location)
                                    {
                                        // якщо немає більше нікого хто чекає на замовлення або залишилися тільки діти і тваринки
                                        if (!IsAnyInTable(TimeType.WaitOrder, tables[i, j]) || IsAllChildrenOrPets(tables[i, j]))
                                        {
                                            agent.Type = AgentType.ReturnWithOrder;
                                            agent.Dest = WaveAlgorithm(agent.Location, homeP);
                                        }
                                    }
                                }
                                // якщо агент не відносить замовлення (зайнятий чимось іншим)
                                else
                                {
                                    if (agent.Type != AgentType.Wait)
                                    {
                                        if (agent.DestP == tables[i, j].Location)
                                        {
                                            if (IsAllChildrenOrPets(tables[i, j]))
                                            {
                                                agent.Type = AgentType.Wait;
                                            }
                                        }
                                    }
                                }
                                LookForBetterWay();
                            }
                        }
                    }
                    if (agent.Location == tables[i, j].Location && agent.Type == AgentType.ToHome)      // після обслуговування столику
                    {
                        for (int k = 0; k < tables[i, j].Count; k++)
                        {
                            if (tables[i, j][k].Type != ClientType.Pet)              // тепер всі клієнти крім тваринок чекають на замовлення
                            {
                                if (tables[i, j][k].TimeType == TimeType.MakeOrder)
                                    tables[i, j][k].TimeType = TimeType.WaitOrder;
                                //else if (tables[i, j][k].TimeType == TimeType.WaitOrder)
                                //    tables[i, j][k].TimeType = TimeType.Eat;
                            }
                        }
                    }
                    // коли агент приніс замовлення (коли приніс то став або wait або receive)
                    if (agent.Location == tables[i, j].Location && (agent.Type == AgentType.Wait || agent.Type == AgentType.ReceiveOrder))
                    {
                        for (int k = 0; k < tables[i, j].Count; k++)
                        {
                            if (tables[i, j][k].Type != ClientType.Pet)              // всі клієнти крім тваринок починають їсти
                            {
                                if (tables[i, j][k].TimeType == TimeType.WaitOrder)
                                    tables[i, j][k].TimeType = TimeType.Eat;
                            }
                        }
                    }

                    if (tables[i, j].Count > 0)         // якщо за столом лишаються тільки діти і тваринки то всі вони уходять
                    {
                        if (IsAllChildrenOrPets(tables[i, j]))
                        {
                            for (int k = 0; k < tables[i, j].Count; k++)
                            {
                                panels[tables[i, j].Location.X, tables[i, j].Location.Y].Controls.Remove(tables[i, j][k].Panel);
                                tables[i, j].Remove(tables[i, j][k]);
                            }
                        }
                    }
                }
            }
        }


        #region допоміжні ф-ї

        Point[] GetMinWay(List<Point[]> list)
        {
            int indMin = 0;
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].Length < list[indMin].Length)
                {
                    indMin = i;
                }
            }
            return list[indMin];
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
        Point GetInd(int[,] arr, int value)
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
        bool IsAnyInTable(TimeType type, Table t)
        {
            for (int i = 0; i < t.Count; i++)
            {
                if (t[i].TimeType == type)
                {
                    return true;
                }
            }
            return false;
        }
        bool IsAllChildrenOrPets(Table t)
        {
            for (int k = 0; k < t.Count; k++)
            {
                if (t[k].Type != ClientType.Pet && t[k].Type != ClientType.Child)
                {
                    return false;
                }
            }
            return true;
        }
        Point GetTableByLocation(Point loc)
        {
            for (int i = 0; i < T_X; i++)
            {
                for (int j = 0; j < T_Y; j++)
                {
                    if (tables[i, j].Location == loc)
                        return new Point(i, j);
                }
            }
            return Point.Empty;
        }

        #endregion

        private void butNewClient_Click(object sender, EventArgs e)
        {
            List<Type> tt = new List<Type>();
            for (int i = 0; i < Controls.Count; i++)
            {
                tt.Add(Controls[i].GetType());
            }

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

            LookForBetterWay();
        }
        
        private void butNextStep_Click(object sender, EventArgs e)
        {
            agent.MoveToDest();
            ChangeAgentState();

            ChangeClientsTiming();
            ChangeClientsState();

            // допоміжні штуки
            newClients.Clear();
            lbInfo.Items.Clear();
            lbInfo.Items.Add("Агент: " + agent.ToolTip.GetToolTip(agent.Panel));
            lbInfo.Items.Add("");
            for (int i = 0; i < T_X; i++)
            {
                for (int j = 0; j < T_Y; j++)
                {
                    if (tables[i, j].Count > 0)
                    {
                        lbInfo.Items.Add("Стіл (" + (i + 1) + ", " + (j + 1) + "):");
                        for (int k = 0; k < tables[i, j].Count; k++)
                        {
                            lbInfo.Items.Add("\tклієнт" + (k + 1) + ": " + tables[i, j][k].toolTip.GetToolTip(tables[i, j][k].Panel));
                        }
                    }
                }
            }
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

    
}