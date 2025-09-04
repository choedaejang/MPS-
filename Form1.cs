using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using ActUtlType64Lib;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;


namespace CDJ_PLC
{
    public partial class Form1 : Form
    {
        ActUtlType64 PLC0 = new ActUtlType64();
        public int count = 0;
        public int num = 1;
        
        public Form1()
        {
            InitializeComponent();
        }

        private void btn_Con_Click(object sender, EventArgs e)//STOP
        {
            tmrUpdate.Stop();
            num = 0;
            //time.Text = "0";
            PLC0.SetDevice("L0", 1);
            Thread.Sleep(200);
            PLC0.SetDevice("L0", 0);
        }

        private void button3_Click(object sender, EventArgs e)//con
        {
            int conErr = 0;
            conErr = PLC0.Open();
            if (conErr == 0)
            {
                label2.Text = "Connected";
                tmrUpdate.Start();
            }
            else
            {
                label2.Text = "Connected Error" + conErr;
            }
        }

        private void button2_Click(object sender, EventArgs e)//discon
        {
            PLC0.Close();
            label2.Text = "DisConnected";
        }

        private void RESET_Click(object sender, EventArgs e)
        {
            PLC0.SetDevice("L1", 1);
            Thread.Sleep(200);
            PLC0.SetDevice("L1", 0);
        }

        private void START_Click(object sender, EventArgs e)
        {
            tmrUpdate.Start();
            PLC0.GetDevice("X0F", out int Check);
            count = 0;
            if (Check==0)
            {
                MessageBox.Show("재고가 없습니다", "재고확인", MessageBoxButtons.OK);
            }
                
            PLC0.SetDevice("L2", 1);
            Thread.Sleep(200);
            PLC0.SetDevice("L2", 0);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            PLC0.GetDevice("D0", out int D0);
            if (D0 == 0)
            {
                STOP.BackColor = Color.Red;
            }
            else
            {
                STOP.BackColor = Color.LightGray;
            }

            PLC0.GetDevice("D11", out int D11);
            PLC0.GetDevice("D10", out int D10);
            PLC0.GetDevice("D2000", out int D2000);

            Metal.Text = D11.ToString();
            nonMetal.Text = D10.ToString();
            SB_move.Text = D2000.ToString();
            PLC0.GetDevice("X0F", out int Check);
            PLC0.GetDevice("X01", out int X01);

            if (Check == 0&& label2.Text == "Connected"&&count==0&&X01==1)
            {
                count++;
                DialogResult dr = MessageBox.Show("재고가 없습니다", "재고확인", MessageBoxButtons.YesNo);
                if (dr == DialogResult.Yes)
                {
                    count = 0;
                }
            }
        }

        private void tmrUpdate_Tick(object sender, EventArgs e)
        {
            num++;
            time.Text = num.ToString();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            
        }

        private void button3_Click_1(object sender, EventArgs e)//공급실린더전진
        {
            PLC0.GetDevice("X03", out int X03);

            if(X03==1)
            {
                PLC0.SetDevice("M3017", 1);
                Thread.Sleep(200);
                PLC0.SetDevice("M3017", 0);
            }
            else
            {
                MessageBox.Show("분배 실린더를 후진시켜주세요", "작동오류", MessageBoxButtons.OK);
            }
        }

        private void button4_Click(object sender, EventArgs e)//공급실린더후진
        {
            PLC0.SetDevice("M3018", 1);
            Thread.Sleep(200);
            PLC0.SetDevice("M3018", 0);

        }

        private void CONVAYOR_Click(object sender, EventArgs e)
        {
            PLC0.SetDevice("M3016", 1);
        }

        private void CONVAYOROFF_Click(object sender, EventArgs e)
        {
            PLC0.SetDevice("M3016", 0);
        }

        private void button5_Click(object sender, EventArgs e)//분배실린더 전진
        {
            PLC0.GetDevice("X01", out int X01);
            if (X01==1)
            {
                PLC0.SetDevice("M3019", 1);
                Thread.Sleep(200);
                PLC0.SetDevice("M3019", 0);
            }
            else
            {
                MessageBox.Show("공급 실린더를 후진시켜주세요", "작동오류", MessageBoxButtons.OK);
            }
        }

        private void button6_Click(object sender, EventArgs e)//분배실린더 후진
        {
            PLC0.SetDevice("M3020", 1);
            Thread.Sleep(200);
            PLC0.SetDevice("M3020", 0);
        }

        private void button7_Click(object sender, EventArgs e)//가공실린더
        {
            PLC0.GetDevice("X04", out int X04);
            if(X04==0)
            {
                PLC0.SetDevice("M3021", 1);
                Thread.Sleep(200);
                PLC0.SetDevice("M3021", 0);
            }
            if(X04 == 1)
            {
                PLC0.SetDevice("M2900", 1);
                Thread.Sleep(200);
                PLC0.SetDevice("M2900", 0);
            }
        }

        private void button1_Click(object sender, EventArgs e)//가공실린더 회전
        {
            PLC0.SetDevice("M3015", 1);

        }


        private void button9_Click(object sender, EventArgs e)//스토퍼하강
        {
            PLC0.SetDevice("M3023", 1);
            Thread.Sleep(200);
            PLC0.SetDevice("M3023", 0);
        }

        private void button2_Click_1(object sender, EventArgs e)//가공실린더회전 정지
        {
            PLC0.SetDevice("M3015", 0);
        }

        private void button10_Click(object sender, EventArgs e)//스토퍼상승
        {
            PLC0.SetDevice("M3024", 1);
            Thread.Sleep(200);
            PLC0.SetDevice("M3024", 0);
        }

        private void button11_Click(object sender, EventArgs e)//흡착실린더 전진
        {
            PLC0.SetDevice("M3025", 1);
            Thread.Sleep(200);
            PLC0.SetDevice("M3025", 0);
        }

        private void button12_Click(object sender, EventArgs e)//흡착실린더 후진
        {
            PLC0.SetDevice("M3026", 1);
            Thread.Sleep(200);
            PLC0.SetDevice("M3026", 0);
        }

        private void button13_Click(object sender, EventArgs e)//저장실린더 전진
        {
            PLC0.SetDevice("M3027", 1);
            Thread.Sleep(200);
            PLC0.SetDevice("M3027", 0);
        }

        private void button14_Click(object sender, EventArgs e)//저장실린더 후진
        {
            PLC0.SetDevice("M3028", 1);
            Thread.Sleep(200);
            PLC0.SetDevice("M3028", 0);
        }

        private void button8_Click(object sender, EventArgs e)//배출실린더 전진
        {
            PLC0.SetDevice("M3022", 1);
            Thread.Sleep(200);
            PLC0.SetDevice("M3022", 0);
        }

        private void button15_Click(object sender, EventArgs e)//배출실린더 후진
        {
            PLC0.SetDevice("M2901", 1);
            Thread.Sleep(200);
            PLC0.SetDevice("M2901", 0);
        }

        private void button16_Click(object sender, EventArgs e)//흡착on
        {
            PLC0.SetDevice("M3029", 1);
            Thread.Sleep(200);
            PLC0.SetDevice("M3029", 0);
        }

        private void button17_Click(object sender, EventArgs e)//흡착off
        {
            PLC0.SetDevice("M3030", 1);
            Thread.Sleep(200);
            PLC0.SetDevice("M3030", 0);
        }
    }
}
