namespace Hexecuter
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;




        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView dgvDevices;
        private System.Windows.Forms.TextBox txtFilePath;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.DataGridView dgvLogs;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            splitContainer1 = new SplitContainer();
            tabControl1 = new TabControl();
            tabPage2 = new TabPage();
            btnLoad = new Button();
            btnBrowse = new Button();
            txtFilePath = new TextBox();
            dgvDevices = new DataGridView();
            tabPage1 = new TabPage();
            choosenLbl = new Label();
            label1 = new Label();
            lblProgress = new Label();
            extractToSDBtn = new Button();
            progressBar1 = new ProgressBar();
            LoadSdCardBtn = new Button();
            pathFileSdCardTxt = new TextBox();
            browseSdCardBtn = new Button();
            dgvSDCard = new DataGridView();
            dgvLogs = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            tabControl1.SuspendLayout();
            tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvDevices).BeginInit();
            tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvSDCard).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvLogs).BeginInit();
            SuspendLayout();
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(0, 0);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(tabControl1);
            splitContainer1.Panel1.Paint += splitContainer1_Panel1_Paint;
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(dgvLogs);
            splitContainer1.Size = new Size(1291, 435);
            splitContainer1.SplitterDistance = 588;
            splitContainer1.TabIndex = 0;
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Dock = DockStyle.Fill;
            tabControl1.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            tabControl1.ImeMode = ImeMode.On;
            tabControl1.Location = new Point(0, 0);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(588, 435);
            tabControl1.TabIndex = 1;
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(btnLoad);
            tabPage2.Controls.Add(btnBrowse);
            tabPage2.Controls.Add(txtFilePath);
            tabPage2.Controls.Add(dgvDevices);
            tabPage2.Font = new Font("Segoe UI", 9F);
            tabPage2.Location = new Point(4, 24);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(580, 407);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "PICkit";
            tabPage2.UseVisualStyleBackColor = true;
            tabPage2.Click += tabPage2_Click;
            // 
            // btnLoad
            // 
            btnLoad.Anchor = AnchorStyles.Top;
            btnLoad.Enabled = false;
            btnLoad.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnLoad.Location = new Point(221, 357);
            btnLoad.Name = "btnLoad";
            btnLoad.Size = new Size(112, 39);
            btnLoad.TabIndex = 6;
            btnLoad.Text = "Load";
            btnLoad.UseVisualStyleBackColor = true;
            btnLoad.Click += btnLoad_Click_1;
            // 
            // btnBrowse
            // 
            btnBrowse.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnBrowse.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnBrowse.Location = new Point(458, 327);
            btnBrowse.Name = "btnBrowse";
            btnBrowse.Size = new Size(94, 25);
            btnBrowse.TabIndex = 5;
            btnBrowse.Text = "Browse...";
            btnBrowse.UseVisualStyleBackColor = true;
            btnBrowse.Click += btnBrowse_Click_1;
            // 
            // txtFilePath
            // 
            txtFilePath.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtFilePath.Location = new Point(6, 328);
            txtFilePath.Name = "txtFilePath";
            txtFilePath.ReadOnly = true;
            txtFilePath.Size = new Size(470, 23);
            txtFilePath.TabIndex = 4;
            // 
            // dgvDevices
            // 
            dgvDevices.AllowUserToAddRows = false;
            dgvDevices.AllowUserToDeleteRows = false;
            dgvDevices.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvDevices.Dock = DockStyle.Top;
            dgvDevices.Location = new Point(3, 3);
            dgvDevices.MultiSelect = false;
            dgvDevices.Name = "dgvDevices";
            dgvDevices.ReadOnly = true;
            dgvDevices.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDevices.Size = new Size(574, 319);
            dgvDevices.TabIndex = 1;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(choosenLbl);
            tabPage1.Controls.Add(label1);
            tabPage1.Controls.Add(lblProgress);
            tabPage1.Controls.Add(extractToSDBtn);
            tabPage1.Controls.Add(progressBar1);
            tabPage1.Controls.Add(LoadSdCardBtn);
            tabPage1.Controls.Add(pathFileSdCardTxt);
            tabPage1.Controls.Add(browseSdCardBtn);
            tabPage1.Controls.Add(dgvSDCard);
            tabPage1.Location = new Point(4, 24);
            tabPage1.Name = "tabPage1";
            tabPage1.Size = new Size(571, 407);
            tabPage1.TabIndex = 2;
            tabPage1.Text = "SD Card";
            tabPage1.UseVisualStyleBackColor = true;
            tabPage1.Click += tabPage1_Click;
            // 
            // choosenLbl
            // 
            choosenLbl.ForeColor = Color.IndianRed;
            choosenLbl.Location = new Point(302, 355);
            choosenLbl.Name = "choosenLbl";
            choosenLbl.Size = new Size(266, 16);
            choosenLbl.TabIndex = 8;
            choosenLbl.Click += choosenLbl_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(194, 356);
            label1.Name = "label1";
            label1.Size = new Size(102, 15);
            label1.TabIndex = 7;
            label1.Text = "Choosen Device :";
            // 
            // lblProgress
            // 
            lblProgress.Location = new Point(5, 355);
            lblProgress.Name = "lblProgress";
            lblProgress.Size = new Size(76, 15);
            lblProgress.TabIndex = 6;
            lblProgress.Click += label1_Click;
            // 
            // extractToSDBtn
            // 
            extractToSDBtn.Location = new Point(302, 374);
            extractToSDBtn.Name = "extractToSDBtn";
            extractToSDBtn.Size = new Size(112, 27);
            extractToSDBtn.TabIndex = 5;
            extractToSDBtn.Text = "Extract";
            extractToSDBtn.UseVisualStyleBackColor = true;
            extractToSDBtn.Click += extractToSDBtn_Click;
            // 
            // progressBar1
            // 
            progressBar1.Location = new Point(6, 374);
            progressBar1.Name = "progressBar1";
            progressBar1.Size = new Size(291, 23);
            progressBar1.Style = ProgressBarStyle.Continuous;
            progressBar1.TabIndex = 4;
            progressBar1.Visible = false;
            progressBar1.Click += progressBar1_Click;
            // 
            // LoadSdCardBtn
            // 
            LoadSdCardBtn.Location = new Point(420, 374);
            LoadSdCardBtn.Name = "LoadSdCardBtn";
            LoadSdCardBtn.Size = new Size(148, 27);
            LoadSdCardBtn.TabIndex = 3;
            LoadSdCardBtn.Text = "Write Raw";
            LoadSdCardBtn.UseVisualStyleBackColor = true;
            LoadSdCardBtn.Click += LoadSdCardBtn_Click;
            // 
            // pathFileSdCardTxt
            // 
            pathFileSdCardTxt.Location = new Point(5, 329);
            pathFileSdCardTxt.Name = "pathFileSdCardTxt";
            pathFileSdCardTxt.Size = new Size(409, 23);
            pathFileSdCardTxt.TabIndex = 2;
            pathFileSdCardTxt.TextChanged += pathFileSdCardTxt_TextChanged;
            // 
            // browseSdCardBtn
            // 
            browseSdCardBtn.Location = new Point(420, 329);
            browseSdCardBtn.Name = "browseSdCardBtn";
            browseSdCardBtn.Size = new Size(148, 25);
            browseSdCardBtn.TabIndex = 1;
            browseSdCardBtn.Text = "Browse";
            browseSdCardBtn.UseVisualStyleBackColor = true;
            browseSdCardBtn.Click += browseSdCardBtn_Click;
            // 
            // dgvSDCard
            // 
            dgvSDCard.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvSDCard.Location = new Point(0, 0);
            dgvSDCard.Name = "dgvSDCard";
            dgvSDCard.ScrollBars = ScrollBars.None;
            dgvSDCard.Size = new Size(568, 323);
            dgvSDCard.TabIndex = 0;
            dgvSDCard.CellContentClick += dgvSDCard_CellContentClick_1;
            dgvSDCard.SelectionChanged += dgvSDCard_SelectionChanged;
            // 
            // dgvLogs
            // 
            dgvLogs.AllowUserToAddRows = false;
            dgvLogs.AllowUserToDeleteRows = false;
            dgvLogs.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvLogs.Dock = DockStyle.Fill;
            dgvLogs.Location = new Point(0, 0);
            dgvLogs.MultiSelect = false;
            dgvLogs.Name = "dgvLogs";
            dgvLogs.ReadOnly = true;
            dgvLogs.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvLogs.Size = new Size(699, 435);
            dgvLogs.TabIndex = 2;
            dgvLogs.CellContentClick += dgvLogs_CellContentClick_1;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            ClientSize = new Size(1291, 435);
            Controls.Add(splitContainer1);
            Name = "Form1";
            Text = "HexecuterApp";
            Load += Form1_Load;
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            tabControl1.ResumeLayout(false);
            tabPage2.ResumeLayout(false);
            tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvDevices).EndInit();
            tabPage1.ResumeLayout(false);
            tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvSDCard).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvLogs).EndInit();
            ResumeLayout(false);

        }
        #endregion


        private TabControl tabControl1;
        private TabPage tabPage2;
        private TabPage tabPage1;
        private Button browseSdCardBtn;
        private DataGridView dgvSDCard;
        private Button LoadSdCardBtn;
        private TextBox pathFileSdCardTxt;
        private ProgressBar progressBar1;
        private Button extractToSDBtn;
        private Label lblProgress;
        private Label label1;
        private Label choosenLbl;
    }
}
