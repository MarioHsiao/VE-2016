﻿using System;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;

namespace WinExplorer
{
    public partial class ConsolePropertyForm : Form
    {
        public ConsolePropertyForm()
        {
            InitializeComponent();
            rb = richTextBox1;
            PreviewText();
        }

        private void button2_Click(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            this.Close();
        }

        private RichTextBox rb { get; set; }

        private Color bg = Color.Black;

        private Font font;

        public ArrayList R { get; set; }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cb = comboBox1;
            if (cb.SelectedIndex < 0)
                return;
            int i = cb.SelectedIndex;
            int s = 14;
            try
            {
                s = Convert.ToInt32(cb.Items[i]);
            }
            catch (Exception ex) { };

            font = new Font("Consolas", s, FontStyle.Bold);

            rb.Font = font;

            if (R != null)
            {
                R.Clear();
                R.Add(font);
            }

            PreviewText();
        }

        private void PreviewText()
        {
            rb.BackColor = bg;

            rb.Enabled = false;

            rb.Clear();

            rb.AppendText("Console text preview\n");
            rb.AppendText("Console text preview\n");
            rb.AppendText("Console text preview\n");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            DialogResult r = cd.ShowDialog();
            if (r != DialogResult.OK)
                return;
            bg = cd.Color;
            PreviewText();
        }
    }
}