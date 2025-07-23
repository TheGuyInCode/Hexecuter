using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hexecuter
{
    public partial class AboutForm : Form
    {
        public AboutForm()
        {
            InitializeComponent();
            InitializeAboutContent();
        }
        private void InitializeAboutContent()
        {
            this.Text = "Hakkında";
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterParent;
            this.Width = 360;
            this.Height = 200;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            var label = new Label
            {
                Text = "Hexecuter v1.0 for EMT Electronics  \r\nThis software was developed by Onur TOSUN.  \r\n© 2025 All rights reserved.",
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleLeft,
                Font = new Font("Segoe UI", 9),
                Size = new Size(320, 80),
                Location = new Point(20, 20)
            };

            var closeBtn = new Button
            {
                Text = "Kapat",
                DialogResult = DialogResult.OK,
                Width = 80,
                Location = new Point(this.ClientSize.Width - 100, this.ClientSize.Height - 45),
                Anchor = AnchorStyles.Bottom | AnchorStyles.Right
            };

            this.Controls.Add(label);
            this.Controls.Add(closeBtn);
        }
        private void AboutForm_Load(object sender, EventArgs e)
        {

        }
    }
}
