using BIKE_Actual.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BIKE_Actual
{
    public partial class Form1 : Form
    {
        private List<CustomLabel> symbolsArray;
        private string text = "BIKE";

        public Form1() => InitializeComponent();

        private void UpdateForm()
        {
            int pos_x = 0;
            symbolsArray = text.ToUpper().ToCharArray().Select(x => new CustomLabel() { Text = x.ToString() }).ToList();
            panel1.Controls.Clear();

            foreach (var label in symbolsArray)
            {
                label.Font = new Font("Arial", 24, FontStyle.Bold);
                label.AutoSize = true;
                label.Location = new Point(pos_x, 10);
                label.MouseEnter += label_MouseEnter;
                label.MouseLeave += label_MouseLeave;
                label.Click += label_Click;
                label.ForeColor = Color.White;
                label.isActive = false;

                panel1.Controls.Add(label);

                pos_x += label.Width;
            }

            if (checkBox1.Checked)
            {
                CustomLabel mulligan = new CustomLabel()
                {
                    Text = "M",
                    Font = new Font("Arial", 14, FontStyle.Bold),
                    AutoSize = true,
                    Location = new Point(4, 50),
                    ForeColor = Color.White,
                    isActive = false
                };

                mulligan.Click += Consumables_Click;
                mulligan.MouseEnter += label_MouseEnter;
                mulligan.MouseLeave += label_MouseLeave;

                panel1.Controls.Add(mulligan);
            }

            if (checkBox2.Checked)
            {
                CustomLabel rdu = new CustomLabel()
                {
                    Text = "RDU",
                    Font = new Font("Arial", 14, FontStyle.Bold),
                    AutoSize = true,
                    Location = new Point(34, 50),
                    ForeColor = Color.White,
                    isActive = false
                };

                rdu.Click += Consumables_Click;
                rdu.MouseEnter += label_MouseEnter;
                rdu.MouseLeave += label_MouseLeave;

                panel1.Controls.Add(rdu);
            }

            panel1.Controls.Add(label1);
            panel1.Controls.Add(label2);
            
            if (checkBox1.Checked || checkBox2.Checked)
                this.Size = new Size(symbolsArray[symbolsArray.Count() - 1].Location.X + 100, this.Size.Height);
            else
                this.Size = new Size(symbolsArray[symbolsArray.Count() - 1].Location.X + 100, this.Size.Height - 30);

            panel1.Size = this.Size;

            label1.Location = new Point(this.Size.Width - 25, 0);
            label2.Location = new Point(this.Size.Width - 50, 0);
        }

        private void Form1_Load(object sender, EventArgs e) => UpdateForm();

        private void label_MouseEnter(object sender, EventArgs e) => ((CustomLabel)sender).ForeColor = Color.Red;

        private void label_MouseLeave(object sender, EventArgs e)
        {
            if (!((CustomLabel)sender).isActive)
                ((CustomLabel)sender).ForeColor = Color.White;
        }

        private void label_Click(object sender, EventArgs e) => ChangeSymbol(symbolsArray, symbolsArray.IndexOf((CustomLabel)sender));

        private void ChangeSymbol(List<CustomLabel> list, int position)
        {
            try
            {
                if (list[position].isActive)
                {
                    for (int i = position + 1; i < list.Count(); i++)
                        list[i].isActive = false;
                }
                else if (position > 0 && !list[position - 1].isActive)
                {
                    MessageBox.Show("The previous letter is not crossed out");
                    return;
                }
                list[position].isActive = !list[position].isActive;
                ChangeColor();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ChangeColor()
        {
            foreach (var label in symbolsArray)
                label.ForeColor = label.isActive ? Color.Red : Color.White;
        }

        private void Close_MouseEnter(object sender, EventArgs e) => ((Label)sender).Image = new Bitmap(Resources.close_icon_active2);

        private void Close_Click(object sender, EventArgs e) => this.Close();

        private void Close_MouseLeave(object sender, EventArgs e) => ((Label)sender).Image = new Bitmap(Resources.close_icon_active);

        private void Settings_MouseEnter(object sender, EventArgs e) => ((Label)sender).Image = new Bitmap(Resources.settings_icon_active);

        private void Settings_MouseLeave(object sender, EventArgs e) => ((Label)sender).Image = new Bitmap(Resources.settings_icon);

        private void Settings_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            panel2.Visible = true;
            this.Size = panel2.Size;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length == 0)
            {
                MessageBox.Show("Please enter at least one letter");
                return;
            }
            text = textBox1.Text;
            UpdateForm();
            panel2.Visible = false;
            panel1.Visible = true;
        }

        private void label3_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
            panel1.Visible = true;
            this.Size = panel1.Size;
        }

        private void Consumables_Click(object sender, EventArgs e) => ((CustomLabel)sender).isActive = !((CustomLabel)sender).isActive;
    }
}
