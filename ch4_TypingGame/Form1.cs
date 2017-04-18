using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ch4_TypingGame
{
    public partial class Form1 : Form
    {
        Random random = new Random();
        Stats stats = new Stats();
        public int i = 4; //counter variable for timer 2
        public Form1()
        {
            InitializeComponent();
        }
        
        private void timer1_Tick(object sender, EventArgs e)
        {
            // Add a random key to the ListBox
            listBox1.Items.Add((Keys)random.Next(65, 90));
            if (listBox1.Items.Count > 7)
            {
                listBox1.Items.Clear();
                listBox1.Items.Add(" Game over");
                timer1.Stop();
                button3.Visible = true;
            }
        }
        
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            // If the user pressed a key that's in the ListBox, remove it
            // and then make the game a little faster
            if (listBox1.Items.Contains(e.KeyCode))
            {
                listBox1.Items.Remove(e.KeyCode);
                listBox1.Refresh();
                if (timer1.Interval > 400)
                    timer1.Interval -= 10;
                if (timer1.Interval > 250)
                    timer1.Interval -= 7;
                if (timer1.Interval > 100)
                    timer1.Interval -= 2;
                difficultyProgressBar.Value = 800 - timer1.Interval;
                // The user pressed a correct key, so update the Stats object
                // by calling its Update() method with the argument true
                stats.Update(true);
            }
            else
            {
                // The user pressed an incorrect key, so update the Stats object
                // by calling its Update() method with the argument false
                stats.Update(false);
            }
            // Update the labels on the StatusStrip
            correctLabel.Text = "Correct: " + stats.Correct;
            missedLabel.Text = "Missed: " + stats.Missed;
            totalLabel.Text = "Total: " + stats.Total;
            accuracyLabel.Text = "Accuracy: " + stats.Accuracy + "%";
        }

        private void button1_Click(object sender, EventArgs e)  //start button
        {
            button1.Visible = false;
            button2.Visible = false;
            timer2.Start();
        }

        private void button3_Click(object sender, EventArgs e) //play again button
        {
            listBox1.Items.Clear();
            button3.Visible = false;

            //reset the missed, correct, accuracy and progressbar value to zero
            stats.Correct = 0;
            stats.Missed = 0;
            stats.Total = 0;
            stats.Accuracy = 0;
            difficultyProgressBar.Value = 0;

            // Update the labels on the StatusStrip before new alphabet starts poped up
            correctLabel.Text = "Correct: " + stats.Correct;
            missedLabel.Text = "Missed: " + stats.Missed;
            totalLabel.Text = "Total: " + stats.Total;
            accuracyLabel.Text = "Accuracy: " + stats.Accuracy + "%";

            //reset the timer interval value to 800 ms
            timer1.Interval = 800;

            timer2.Start();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("by- Jay Nath\n2017", "About");
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            i--;
            listBox1.Items.Clear();
            listBox1.Items.Add("Starts in: " + i);
            if (i == 0) {
                timer2.Stop();
                i = 4;
                listBox1.Items.Clear();
                timer1.Start();
            }
        }
    }
}
