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
/// Matrix32 Input Tracker
/// </summary>
namespace Matrix_51__50__32_Input_32_Tracker
{
    // Directives
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.Drawing;
    using System.Windows.Forms;
    using PdBets;

    /// <summary>
    /// Matrix32 Input Tracker
    /// </summary>
    [Export(typeof(IPdBets))]
    public partial class MainForm : Form, IPdBets
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Matrix_51__50__32_Input_32_Tracker.MainForm"/> class.
        /// </summary>
        public MainForm()
        {
            // Windows Forms designer support.
            this.InitializeComponent();
        }

        /// <summary>
        /// Occurs when new input is sent.
        /// </summary>
        public event EventHandler<NewInputEventArgs> NewInput;

        /// <summary>
        /// Processes incoming input and bet string.
        /// </summary>
        /// <param name="inputString">Input string.</param>
        /// <param name="betString">Bet string.</param>
        /// <returns>>The processed bet string.</returns>
        public string Input(string inputString, string betString)
        {
            // Return passed bet string
            return betString;
        }
    }
}
