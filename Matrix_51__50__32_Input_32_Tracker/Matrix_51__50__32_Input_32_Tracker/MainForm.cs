// <copyright file="MainForm.cs" company="www.PublicDomain.tech">All rights waived.</copyright>

// Programmed by Victor L. Senior (VLS) <support@publicdomain.tech>, 2016
//
// Web: http://publicdomain.tech
//
// Sources: http://github.com/publicdomaintech/
//
// This software and associated documentation files (the "Software") is
// released under the CC0 Public Domain Dedication, version 1.0, as
// published by Creative Commons. To the extent possible under law, the
// author(s) have dedicated all copyright and related and neighboring
// rights to the Software to the public domain worldwide. The Software is
// distributed WITHOUT ANY WARRANTY.
//
// If you did not receive a copy of the CC0 Public Domain Dedication
// along with the Software, see
// <http://creativecommons.org/publicdomain/zero/1.0/>

/// <summary>
/// Main form.
/// </summary>
namespace Matrix_51__50__32_Input_32_Tracker
{
    // Directives
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.Drawing;
    using System.Reflection;
    using System.Windows.Forms;
    using PdBets;

    /// <summary>
    /// Felt European Input Tracker 
    /// </summary>
    [Export(typeof(IPdBets))]
    public partial class MainForm : Form, IPdBets
    {
        /// <summary>
        /// The history list.
        /// </summary>
        private List<int> historyList = new List<int>();

        /// <summary>
        /// The number appearances list.
        /// </summary>
        private List<int> numberAppearancesList = new List<int>();

        /// <summary>
        /// The number color list.
        /// </summary>
        private List<Color> numberColorList = new List<Color>();

        /// <summary>
        /// The default number color list.
        /// </summary>
        private List<Color> defaultNumberColorList = new List<Color>();

        /// <summary>
        /// The last reset spin.
        /// </summary>
        private int lastResetSpin = 0;

        /// <summary>
        /// The current version
        /// </summary>
        private Version version = Assembly.GetExecutingAssembly().GetName().Version;

        /// <summary>
        // TODO Invisible zero button [Fix code to account for no zero instead]
        /// </summary>
        private System.Windows.Forms.Button button0;

        /// <summary>
        /// Initializes a new instance of the <see cref="Felt_32_European_32_Input_32_Tracker.MainForm"/> class.
        /// </summary>
        public MainForm()
        {
            // The InitializeComponent() call is required for Windows Forms designer support.
            this.InitializeComponent();

            // TODO Invisible zero button [Fix code to account for no zero instead]
            this.button0 = new System.Windows.Forms.Button();
            this.button0.Name = "button0";
            this.button0.Visible = false;
            this.Controls.Add(this.button0);

            // Set default number color list
            for (int i = 0; i < 37; i++)
            {
                // Set appearances
                this.numberAppearancesList.Add(0);

                // Set number color
                Color numberColor = this.Controls.Find("button" + i.ToString(), true)[0].BackColor;

                // Add to default number color list
                this.defaultNumberColorList.Add(numberColor);
            }

            // Set number color list
            for (int i = 0; i <= 10; i++)
            {
                // Color by appearances
                switch (i)
                {
                    // First
                    case 1:

                        // Yellow
                        this.numberColorList.Add(Color.Yellow);

                        // Halt flow
                        break;

                        // Second
                    case 2:

                        // Set cyan
                        this.numberColorList.Add(Color.Cyan);

                        // Halt flow
                        break;

                        // Third+
                    default:

                        // Set light pink
                        this.numberColorList.Add(Color.LightPink);

                        // Halt flow
                        break;
                }
            }
        }

        /// <summary>
        /// Occurs when new input is sent.
        /// </summary>
        public event EventHandler<NewInputEventArgs> NewInput;

        /// <summary>
        /// Processes incoming input and bet strings.
        /// </summary>
        /// <param name="inputString">Input string.</param>
        /// <param name="betString">Bet string.</param>
        /// <returns>>The processed bet string.</returns>
        public string Input(string inputString, string betString)
        {
            // Return passed bet string
            return betString;
        }

        /// <summary>
        /// Raises the set colors tool strip menu item drop down item clicked event.
        /// </summary>
        /// <param name="sender">Sender object.</param>
        /// <param name="e">Event arguments.</param>
        private void OnSetColorsToolStripMenuItemDropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            // Hold clicked times
            int times;

            // Try to parse x times integer
            if (!int.TryParse(e.ClickedItem.Text.Replace("&", string.Empty).Replace("x", string.Empty), out times))
            {
                // Halt flow
                return;
            }

            // Set current color dialog color
            this.mainColorDialog.Color = this.numberColorList[times];

            // Open color dialog, then check dialog result
            if (this.mainColorDialog.ShowDialog() == DialogResult.OK)
            {
                // Set current times color
                this.numberColorList[times] = this.mainColorDialog.Color;
            }
        }

        /// <summary>
        /// Raises the reset button click event.
        /// </summary>
        /// <param name="sender">Sender object.</param>
        /// <param name="e">Event arguments.</param>
        private void OnResetButtonClick(object sender, EventArgs e)
        {
            // Reset number appearances list
            this.numberAppearancesList.Clear();

            // Set number appearances list
            for (int i = 0; i < 37; i++)
            {
                // Set appearances
                this.numberAppearancesList.Add(0);
            }

            // Set last reset spins
            this.lastResetSpin = this.historyList.Count;

            // Colorize number buttons
            this.ColorizeNumberButtons();
        }

        /// <summary>
        /// Raises the number button click event.
        /// </summary>
        /// <param name="sender">Sender object.</param>
        /// <param name="e">Event arguments.</param>
        private void OnNumberButtonClick(object sender, EventArgs e)
        {
            // Holds last number
            int lastNumber;

            // Try to parse last number
            if (!int.TryParse(((Button)sender).Name.Replace("button", string.Empty), out lastNumber))
            {
                // Halt flow
                return;
            }

            /* Process SPIN */

            // Validate range
            if (lastNumber >= 0 && lastNumber <= 36)
            {
                // Rise appearances
                this.numberAppearancesList[lastNumber]++;

                // Add to history list
                this.historyList.Add(lastNumber);

                // Colorize buttons
                this.ColorizeNumberButtons();
            }

            // Send last number to pdBets
            this.NewInput(sender, new NewInputEventArgs(lastNumber.ToString()));
        }

        /// <summary>
        /// Colorizes the number buttons.
        /// </summary>
        private void ColorizeNumberButtons()
        {
            // Colorize buttons
            for (int n = 0; n < 37; n++)
            {
                // Set current number button
                Button numberButton = (Button)this.Controls.Find("button" + n.ToString(), true)[0];

                // Update button fore color
                if (this.numberAppearancesList[n] > 0)
                {
                    // Set fore color to black
                    numberButton.ForeColor = Color.Black;

                    try
                    {
                        // Set back color
                        numberButton.BackColor = this.numberColorList[this.numberAppearancesList[n]];   
                    }
                    catch (Exception)
                    {
                        // TODO Use 3+ color [Perhaps add default color for 10+]
                        numberButton.BackColor = Color.LightPink;
                    }
                }

                if (this.numberAppearancesList[n] == 0)
                {
                    // Set fore color to white
                    numberButton.ForeColor = Color.White;

                    // Set default back color
                    numberButton.BackColor = this.defaultNumberColorList[n];
                }
            }

            // Compute reset difference
            int resetDifference = this.historyList.Count - this.lastResetSpin;

            // Update reset button count
            this.resetButton.Text = "Reset" + ((resetDifference < 1) ? string.Empty : " (" + resetDifference.ToString() + ")");
        }

        /// <summary>
        /// Raises the main form load event.
        /// </summary>
        /// <param name="sender">Sender object.</param>
        /// <param name="e">Event arguments.</param>
        private void OnMainFormLoad(object sender, EventArgs e)
        {
            // Code here
        }

        /// <summary>
        /// Raises the new tool strip menu item click event.
        /// </summary>
        /// <param name="sender">Sender object.</param>
        /// <param name="e">Event arguments.</param>
        private void OnNewToolStripMenuItemClick(object sender, EventArgs e)
        {
            // Clear history list
            this.historyList.Clear();

            // Hit reset button
            this.resetButton.PerformClick();

            // Colorize buttons
            this.ColorizeNumberButtons();
        }

        /// <summary>
        /// Raises the about tool strip menu item click event.
        /// </summary>
        /// <param name="sender">Sender object.</param>
        /// <param name="e">Event arguments.</param>
        private void OnAboutToolStripMenuItemClick(object sender, EventArgs e)
        {
            // About message
            MessageBox.Show("Programmed by Victor L. Senior (VLS)" + Environment.NewLine + "www.publicdomain.tech / support@publicdomain.tech" + Environment.NewLine + Environment.NewLine + "Version " + this.version.Major + "." + this.version.Minor + " - September 2016.");
        }

        /// <summary>
        /// Raises the undo tool strip menu item click event.
        /// </summary>
        /// <param name="sender">Sender object.</param>
        /// <param name="e">Event arguments.</param>
        private void OnUndoToolStripMenuItemClick(object sender, EventArgs e)
        {
            // Check there's something to remove
            if (this.historyList.Count > 0)
            {
                // Hold previous number
                int prevNumber = this.historyList[this.historyList.Count - 1];

                // Decrement appearances
                this.numberAppearancesList[prevNumber]--;

                // Remove last from history list
                this.historyList.RemoveAt(this.historyList.Count - 1);

                // Colorize buttons
                this.ColorizeNumberButtons();

                // Send undo message to pdBets
                this.NewInput(sender, new NewInputEventArgs("-U"));
            }
        }

        /// <summary>
        /// Raises the always ontop tool strip menu item click event.
        /// </summary>
        /// <param name="sender">Sender object.</param>
        /// <param name="e">Event arguments.</param>
        private void OnAlwaysOntopToolStripMenuItemClick(object sender, System.EventArgs e)
        {
            // Toggle always on top tool strip menu item
            this.alwaysOntopToolStripMenuItem.Checked = !this.alwaysOntopToolStripMenuItem.Checked;

            // Set TopMost value
            this.TopMost = this.alwaysOntopToolStripMenuItem.Checked;
        }
    }
}
