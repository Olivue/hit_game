using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace hit_game
{
    public partial class Form1 : Form
    {
        Random random = new Random();
        Stats stats = new Stats();
        public Form1()
        {
            InitializeComponent();
            //startButton.Visible = false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            listBox1.Items.Add((Keys)random.Next(65, 90));
            if (listBox1.Items.Count > 7)
            {
                listBox1.Items.Clear();
                listBox1.Items.Add("Игра окончена!");
                timer1.Stop();
                startButton.Visible = true;
                difficultyProgressBar.Value = 0;
                timer1.Interval = 800;
            }
        }


        private void listBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(listBox1.Items[0]))
            {
                listBox1.Items.Remove(e.KeyCode);
                listBox1.Update();
                if (timer1.Interval > 400)
                    timer1.Interval -= 10;
                if (timer1.Interval > 250)
                    timer1.Interval -= 7;
                if (timer1.Interval > 100)
                    timer1.Interval -= 2;
                difficultyProgressBar.Value = 800 - timer1.Interval;
                // При правильном нажатии клавиши обновляем объект Stats,
                // вызывая метод Update() с аргументом true
                stats.Update(true);
            }
            else
            {
                // При неправильном нажатии клавиши обновляем объект Stats,
                // вызывая метод Update() с аргументом false
                stats.Update(false);
            }
            // Обновление меток элемента StatusStrip
            correctLabel.Text = "Correct: " + stats.Correct;
            missedLabel.Text = "Missed: " + stats.Missed;
            totalLabel.Text = "Total: " + stats.Total;
            accuracyLabel.Text = "Accuracy: " + stats.Accuracy + "%";
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            startButton.Visible = false;
            listBox1.Items.Clear();
            stats.Accuracy = 0;
            stats.Correct = 0;
            stats.Missed = 0;
            stats.Total = 0;
            timer1.Start();

        }
    }
}
