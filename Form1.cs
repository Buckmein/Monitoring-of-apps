using System;
using System.IO;
using System.Windows.Forms;
using System.Management;
using System.Data;
using System.Drawing;
using System.Linq;

public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            SProcess();
        }
        string[] nm = new string[100];
        int[] cnt = new int[100];

        void SProcess()
        {

            ManagementClass proc = new ManagementClass("Win32_Process");
            ManagementObjectCollection a = proc.GetInstances();
            foreach (ManagementObject d1 in a)
            {
                dg1.Rows.Add(d1["Name"]);
                label1.Text = Convert.ToString(d1["Handle"]);
            }
        }
        static public int j;
        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < 100; i++)
                {
                    cnt[i] = -33;
                }
                string a;
                StreamReader f = new StreamReader("cfg.txt");
                string[] str;
                j = 0;
                while ((f.ReadLine()) != null)
                {
                    a = f.ReadLine();
                    str = a.Split(new char[] { ' ', '|' }, StringSplitOptions.RemoveEmptyEntries);
                    nm[j] = str[0];
                    cnt[j] = Convert.ToInt32(str[1]);
                    j++;
                }
                f.Close();
            }
            catch
            {
                label1.Text = "Ошибка файла";
            }
        }
        private void Button1_Click_1(object sender, EventArgs e)
        {
            dg1.Rows.Clear();
            SProcess();
        }
        int[] d = { 1, 1, 1 };
        private void Button3_Click(object sender, EventArgs e)
        {
            bool z = true;
            if (dg2.Rows.Count != 3)
            {
                for (int i = 0; i < 100; i++)
                {
                    if (Convert.ToString(dg1.CurrentCell.Value) == nm[i])
                    {
                        dg2.Rows.Add(nm[i], cnt[i]);
                        z = false;
                    }
                }
                if (z)
                {
                    dg2.Rows.Add(dg1.CurrentCell.Value, 1);
                }
                dg1.Rows.Remove(dg1.CurrentRow);
            }
        }
        private void Button2_Click(object sender, EventArgs e)
        {
            if (dg2.Rows.Count != 0)
            {
                dg1.Rows.Add(dg2.CurrentCell.Value);
                dg2.Rows.Remove(dg2.CurrentRow);
            }
        }
        private void Button4_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 3; i++)
            {
                d[i] = 0;
            }
            for (int i = 0; i < dg2.RowCount; i++)
                dg2[1, i].Value = Convert.ToString(d[0]);
        }
        private void DataGridView2_CellContentClick(object sender, EventArgs e)
        {

        }
        private void Button5_Click(object sender, EventArgs e)
        {
            try
            {
                ManagementEventWatcher startWatch = new ManagementEventWatcher(
                   new WqlEventQuery("SELECT * FROM Win32_ProcessStartTrace"));
                startWatch.EventArrived += StartWatch_EventArrived;
                startWatch.Start();
            }
            catch { }
        }
        private void Button4_Click_1(object sender, EventArgs e)
        {
            try
            {
                bool z = true;
                for (int y = 0; y < 3; y++)
                {
                    z = true;
                    for (int i = 0; i < 100; i++)
                    {
                        if ((Convert.ToString(dg2[0, y].Value) == nm[i]) & (z))
                        {
                            nm[i] = Convert.ToString(dg2[0, y].Value);
                            cnt[i] = d[y];
                            z = false;
                        }
                    }
                    if (z)
                    {
                        cnt[j] = d[y];
                        nm[j] = Convert.ToString(dg2[0, y].Value);
                        j++;
                    }
                }
                StreamWriter sr = new StreamWriter("cfg.txt", false);
                sr.WriteLine("Название_Процесса Количество");
                for (int i = 0; i < 100; i++)
                {
                    if (cnt[i] != -33)
                    {
                        sr.WriteLine(nm[i] + " " + cnt[i]);
                    }
                }
                sr.Close();
            }
            catch
            {

            }
        }
        void StartWatch_EventArrived(object sender, EventArrivedEventArgs e)
        {
            for (int i = 0; i < 3; i++)
            {
                if (Convert.ToString(e.NewEvent.Properties["ProcessName"].Value) == Convert.ToString(dg2[0, i].Value))
                {
                    d[i] = Convert.ToInt32(dg2[1, i].Value);
                    d[i]++;
                    dg2[1, i].Value = Convert.ToString(d[i]);
                }
            }
        }
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {

        }
    }
    
