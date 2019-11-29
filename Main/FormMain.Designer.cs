namespace Main
{
    partial class FormMain
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.flowLayoutPanelLogIn = new System.Windows.Forms.FlowLayoutPanel();
            this.buttonAdmin = new System.Windows.Forms.Button();
            this.buttonLogOut = new System.Windows.Forms.Button();
            this.buttonLogin = new System.Windows.Forms.Button();
            this.buttonSignUp = new System.Windows.Forms.Button();
            this.menuStripMain = new System.Windows.Forms.MenuStrip();
            this.단어입력ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.불러오기ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.내보내기ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.입력양식내려받기ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStripMain = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabelMain = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.flowLayoutPanelCombo = new System.Windows.Forms.FlowLayoutPanel();
            this.comboBoxLanguage = new System.Windows.Forms.ComboBox();
            this.comboBoxTestGroup = new System.Windows.Forms.ComboBox();
            this.comboBoxTest = new System.Windows.Forms.ComboBox();
            this.comboBoxTestLevel = new System.Windows.Forms.ComboBox();
            this.comboBoxDay = new System.Windows.Forms.ComboBox();
            this.labelName = new System.Windows.Forms.Label();
            this.flowLayoutPanelLogIn.SuspendLayout();
            this.menuStripMain.SuspendLayout();
            this.statusStripMain.SuspendLayout();
            this.flowLayoutPanelCombo.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowLayoutPanelLogIn
            // 
            this.flowLayoutPanelLogIn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanelLogIn.Controls.Add(this.buttonAdmin);
            this.flowLayoutPanelLogIn.Controls.Add(this.buttonLogOut);
            this.flowLayoutPanelLogIn.Controls.Add(this.buttonLogin);
            this.flowLayoutPanelLogIn.Controls.Add(this.buttonSignUp);
            this.flowLayoutPanelLogIn.Controls.Add(this.labelName);
            this.flowLayoutPanelLogIn.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanelLogIn.Location = new System.Drawing.Point(650, 36);
            this.flowLayoutPanelLogIn.Name = "flowLayoutPanelLogIn";
            this.flowLayoutPanelLogIn.Size = new System.Drawing.Size(516, 36);
            this.flowLayoutPanelLogIn.TabIndex = 18;
            // 
            // buttonAdmin
            // 
            this.buttonAdmin.Location = new System.Drawing.Point(415, 3);
            this.buttonAdmin.Name = "buttonAdmin";
            this.buttonAdmin.Size = new System.Drawing.Size(98, 28);
            this.buttonAdmin.TabIndex = 17;
            this.buttonAdmin.Text = "회원관리";
            this.buttonAdmin.UseVisualStyleBackColor = true;
            this.buttonAdmin.Visible = false;
            this.buttonAdmin.Click += new System.EventHandler(this.buttonAdmin_Click);
            // 
            // buttonLogOut
            // 
            this.buttonLogOut.Location = new System.Drawing.Point(311, 3);
            this.buttonLogOut.Name = "buttonLogOut";
            this.buttonLogOut.Size = new System.Drawing.Size(98, 28);
            this.buttonLogOut.TabIndex = 18;
            this.buttonLogOut.Text = "로그아웃";
            this.buttonLogOut.UseVisualStyleBackColor = true;
            this.buttonLogOut.Visible = false;
            this.buttonLogOut.Click += new System.EventHandler(this.buttonLogOut_Click);
            // 
            // buttonLogin
            // 
            this.buttonLogin.Location = new System.Drawing.Point(207, 3);
            this.buttonLogin.Name = "buttonLogin";
            this.buttonLogin.Size = new System.Drawing.Size(98, 28);
            this.buttonLogin.TabIndex = 19;
            this.buttonLogin.Text = "로그인";
            this.buttonLogin.UseVisualStyleBackColor = true;
            this.buttonLogin.Click += new System.EventHandler(this.buttonLogin_Click);
            // 
            // buttonSignUp
            // 
            this.buttonSignUp.Location = new System.Drawing.Point(103, 3);
            this.buttonSignUp.Name = "buttonSignUp";
            this.buttonSignUp.Size = new System.Drawing.Size(98, 28);
            this.buttonSignUp.TabIndex = 20;
            this.buttonSignUp.Text = "회원가입";
            this.buttonSignUp.UseVisualStyleBackColor = true;
            this.buttonSignUp.Click += new System.EventHandler(this.buttonSignUp_Click);
            // 
            // menuStripMain
            // 
            this.menuStripMain.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.menuStripMain.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.단어입력ToolStripMenuItem});
            this.menuStripMain.Location = new System.Drawing.Point(0, 0);
            this.menuStripMain.Name = "menuStripMain";
            this.menuStripMain.Size = new System.Drawing.Size(800, 36);
            this.menuStripMain.TabIndex = 19;
            this.menuStripMain.Text = "menuStrip1";
            this.menuStripMain.Visible = false;
            // 
            // 단어입력ToolStripMenuItem
            // 
            this.단어입력ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.불러오기ToolStripMenuItem,
            this.내보내기ToolStripMenuItem,
            this.입력양식내려받기ToolStripMenuItem});
            this.단어입력ToolStripMenuItem.Name = "단어입력ToolStripMenuItem";
            this.단어입력ToolStripMenuItem.Size = new System.Drawing.Size(64, 32);
            this.단어입력ToolStripMenuItem.Text = "파일";
            // 
            // 불러오기ToolStripMenuItem
            // 
            this.불러오기ToolStripMenuItem.Name = "불러오기ToolStripMenuItem";
            this.불러오기ToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.I)));
            this.불러오기ToolStripMenuItem.Size = new System.Drawing.Size(440, 34);
            this.불러오기ToolStripMenuItem.Text = "엑셀로 단어 업로드";
            this.불러오기ToolStripMenuItem.Click += new System.EventHandler(this.불러오기ToolStripMenuItem_Click);
            // 
            // 내보내기ToolStripMenuItem
            // 
            this.내보내기ToolStripMenuItem.Name = "내보내기ToolStripMenuItem";
            this.내보내기ToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.O)));
            this.내보내기ToolStripMenuItem.Size = new System.Drawing.Size(440, 34);
            this.내보내기ToolStripMenuItem.Text = "오늘의 단어 내려받기";
            this.내보내기ToolStripMenuItem.Click += new System.EventHandler(this.내보내기ToolStripMenuItem_Click);
            // 
            // 입력양식내려받기ToolStripMenuItem
            // 
            this.입력양식내려받기ToolStripMenuItem.Name = "입력양식내려받기ToolStripMenuItem";
            this.입력양식내려받기ToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.F)));
            this.입력양식내려받기ToolStripMenuItem.Size = new System.Drawing.Size(440, 34);
            this.입력양식내려받기ToolStripMenuItem.Text = "단어 업로드 양식 내려받기";
            this.입력양식내려받기ToolStripMenuItem.Click += new System.EventHandler(this.입력양식내려받기ToolStripMenuItem_Click);
            // 
            // statusStripMain
            // 
            this.statusStripMain.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.statusStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabelMain,
            this.toolStripProgressBar});
            this.statusStripMain.Location = new System.Drawing.Point(0, 722);
            this.statusStripMain.Name = "statusStripMain";
            this.statusStripMain.Size = new System.Drawing.Size(1178, 22);
            this.statusStripMain.TabIndex = 20;
            this.statusStripMain.Text = "statusStrip1";
            // 
            // toolStripStatusLabelMain
            // 
            this.toolStripStatusLabelMain.Name = "toolStripStatusLabelMain";
            this.toolStripStatusLabelMain.Size = new System.Drawing.Size(0, 15);
            // 
            // toolStripProgressBar
            // 
            this.toolStripProgressBar.Name = "toolStripProgressBar";
            this.toolStripProgressBar.Size = new System.Drawing.Size(100, 15);
            this.toolStripProgressBar.Visible = false;
            // 
            // flowLayoutPanelCombo
            // 
            this.flowLayoutPanelCombo.Controls.Add(this.comboBoxLanguage);
            this.flowLayoutPanelCombo.Controls.Add(this.comboBoxTestGroup);
            this.flowLayoutPanelCombo.Controls.Add(this.comboBoxTest);
            this.flowLayoutPanelCombo.Controls.Add(this.comboBoxTestLevel);
            this.flowLayoutPanelCombo.Controls.Add(this.comboBoxDay);
            this.flowLayoutPanelCombo.Location = new System.Drawing.Point(12, 36);
            this.flowLayoutPanelCombo.Name = "flowLayoutPanelCombo";
            this.flowLayoutPanelCombo.Size = new System.Drawing.Size(723, 36);
            this.flowLayoutPanelCombo.TabIndex = 21;
            this.flowLayoutPanelCombo.Visible = false;
            // 
            // comboBoxLanguage
            // 
            this.comboBoxLanguage.FormattingEnabled = true;
            this.comboBoxLanguage.Location = new System.Drawing.Point(3, 3);
            this.comboBoxLanguage.Name = "comboBoxLanguage";
            this.comboBoxLanguage.Size = new System.Drawing.Size(121, 26);
            this.comboBoxLanguage.TabIndex = 0;
            // 
            // comboBoxTestGroup
            // 
            this.comboBoxTestGroup.FormattingEnabled = true;
            this.comboBoxTestGroup.Location = new System.Drawing.Point(130, 3);
            this.comboBoxTestGroup.Name = "comboBoxTestGroup";
            this.comboBoxTestGroup.Size = new System.Drawing.Size(121, 26);
            this.comboBoxTestGroup.TabIndex = 1;
            // 
            // comboBoxTest
            // 
            this.comboBoxTest.FormattingEnabled = true;
            this.comboBoxTest.Location = new System.Drawing.Point(257, 3);
            this.comboBoxTest.Name = "comboBoxTest";
            this.comboBoxTest.Size = new System.Drawing.Size(121, 26);
            this.comboBoxTest.TabIndex = 2;
            // 
            // comboBoxTestLevel
            // 
            this.comboBoxTestLevel.FormattingEnabled = true;
            this.comboBoxTestLevel.Location = new System.Drawing.Point(384, 3);
            this.comboBoxTestLevel.Name = "comboBoxTestLevel";
            this.comboBoxTestLevel.Size = new System.Drawing.Size(121, 26);
            this.comboBoxTestLevel.TabIndex = 3;
            // 
            // comboBoxDay
            // 
            this.comboBoxDay.FormattingEnabled = true;
            this.comboBoxDay.Location = new System.Drawing.Point(511, 3);
            this.comboBoxDay.Name = "comboBoxDay";
            this.comboBoxDay.Size = new System.Drawing.Size(121, 26);
            this.comboBoxDay.TabIndex = 4;
            // 
            // labelName
            // 
            this.labelName.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelName.AutoSize = true;
            this.labelName.Font = new System.Drawing.Font("나눔스퀘어 Bold", 8.999999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.labelName.Location = new System.Drawing.Point(97, 7);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(0, 20);
            this.labelName.TabIndex = 21;
            this.labelName.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1178, 744);
            this.Controls.Add(this.flowLayoutPanelCombo);
            this.Controls.Add(this.statusStripMain);
            this.Controls.Add(this.flowLayoutPanelLogIn);
            this.Controls.Add(this.menuStripMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStripMain;
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "단어 암기 프로그램";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.Shown += new System.EventHandler(this.FormMain_Shown);
            this.flowLayoutPanelLogIn.ResumeLayout(false);
            this.flowLayoutPanelLogIn.PerformLayout();
            this.menuStripMain.ResumeLayout(false);
            this.menuStripMain.PerformLayout();
            this.statusStripMain.ResumeLayout(false);
            this.statusStripMain.PerformLayout();
            this.flowLayoutPanelCombo.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelLogIn;
        public System.Windows.Forms.Button buttonAdmin;
        private System.Windows.Forms.Button buttonLogOut;
        private System.Windows.Forms.MenuStrip menuStripMain;
        private System.Windows.Forms.StatusStrip statusStripMain;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelMain;
        private System.Windows.Forms.ToolStripMenuItem 단어입력ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 불러오기ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 내보내기ToolStripMenuItem;
        private System.Windows.Forms.Button buttonLogin;
        private System.Windows.Forms.Button buttonSignUp;
        private System.Windows.Forms.ToolStripMenuItem 입력양식내려받기ToolStripMenuItem;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelCombo;
        private System.Windows.Forms.ComboBox comboBoxLanguage;
        private System.Windows.Forms.ComboBox comboBoxTestGroup;
        private System.Windows.Forms.ComboBox comboBoxTest;
        private System.Windows.Forms.ComboBox comboBoxTestLevel;
        private System.Windows.Forms.ComboBox comboBoxDay;
        private System.Windows.Forms.Label labelName;
    }
}

