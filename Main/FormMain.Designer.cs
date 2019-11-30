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
            this.labelName = new System.Windows.Forms.Label();
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
            this.flowLayoutPanelDay = new System.Windows.Forms.FlowLayoutPanel();
            this.listViewWordList = new System.Windows.Forms.ListView();
            this.buttonWordBefore = new System.Windows.Forms.Button();
            this.buttonWordAfter = new System.Windows.Forms.Button();
            this.richTextBoxWord = new System.Windows.Forms.RichTextBox();
            this.checkBoxRepeat = new System.Windows.Forms.CheckBox();
            this.checkBoxRandom = new System.Windows.Forms.CheckBox();
            this.checkBoxBlink = new System.Windows.Forms.CheckBox();
            this.checkBoxAuto = new System.Windows.Forms.CheckBox();
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
            this.flowLayoutPanelLogIn.Location = new System.Drawing.Point(455, 24);
            this.flowLayoutPanelLogIn.Margin = new System.Windows.Forms.Padding(2);
            this.flowLayoutPanelLogIn.Name = "flowLayoutPanelLogIn";
            this.flowLayoutPanelLogIn.Size = new System.Drawing.Size(361, 24);
            this.flowLayoutPanelLogIn.TabIndex = 18;
            // 
            // buttonAdmin
            // 
            this.buttonAdmin.Location = new System.Drawing.Point(290, 2);
            this.buttonAdmin.Margin = new System.Windows.Forms.Padding(2);
            this.buttonAdmin.Name = "buttonAdmin";
            this.buttonAdmin.Size = new System.Drawing.Size(69, 19);
            this.buttonAdmin.TabIndex = 17;
            this.buttonAdmin.Text = "회원관리";
            this.buttonAdmin.UseVisualStyleBackColor = true;
            this.buttonAdmin.Visible = false;
            this.buttonAdmin.Click += new System.EventHandler(this.buttonAdmin_Click);
            // 
            // buttonLogOut
            // 
            this.buttonLogOut.Location = new System.Drawing.Point(217, 2);
            this.buttonLogOut.Margin = new System.Windows.Forms.Padding(2);
            this.buttonLogOut.Name = "buttonLogOut";
            this.buttonLogOut.Size = new System.Drawing.Size(69, 19);
            this.buttonLogOut.TabIndex = 18;
            this.buttonLogOut.Text = "로그아웃";
            this.buttonLogOut.UseVisualStyleBackColor = true;
            this.buttonLogOut.Visible = false;
            this.buttonLogOut.Click += new System.EventHandler(this.buttonLogOut_Click);
            // 
            // buttonLogin
            // 
            this.buttonLogin.Location = new System.Drawing.Point(144, 2);
            this.buttonLogin.Margin = new System.Windows.Forms.Padding(2);
            this.buttonLogin.Name = "buttonLogin";
            this.buttonLogin.Size = new System.Drawing.Size(69, 19);
            this.buttonLogin.TabIndex = 19;
            this.buttonLogin.Text = "로그인";
            this.buttonLogin.UseVisualStyleBackColor = true;
            this.buttonLogin.Click += new System.EventHandler(this.buttonLogin_Click);
            // 
            // buttonSignUp
            // 
            this.buttonSignUp.Location = new System.Drawing.Point(71, 2);
            this.buttonSignUp.Margin = new System.Windows.Forms.Padding(2);
            this.buttonSignUp.Name = "buttonSignUp";
            this.buttonSignUp.Size = new System.Drawing.Size(69, 19);
            this.buttonSignUp.TabIndex = 20;
            this.buttonSignUp.Text = "회원가입";
            this.buttonSignUp.UseVisualStyleBackColor = true;
            this.buttonSignUp.Click += new System.EventHandler(this.buttonSignUp_Click);
            // 
            // labelName
            // 
            this.labelName.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelName.AutoSize = true;
            this.labelName.Font = new System.Drawing.Font("나눔스퀘어 Bold", 8.999999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.labelName.Location = new System.Drawing.Point(67, 5);
            this.labelName.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(0, 13);
            this.labelName.TabIndex = 21;
            this.labelName.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // menuStripMain
            // 
            this.menuStripMain.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.단어입력ToolStripMenuItem});
            this.menuStripMain.Location = new System.Drawing.Point(0, 0);
            this.menuStripMain.Name = "menuStripMain";
            this.menuStripMain.Padding = new System.Windows.Forms.Padding(4, 1, 0, 1);
            this.menuStripMain.Size = new System.Drawing.Size(560, 24);
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
            this.단어입력ToolStripMenuItem.Size = new System.Drawing.Size(43, 22);
            this.단어입력ToolStripMenuItem.Text = "파일";
            // 
            // 불러오기ToolStripMenuItem
            // 
            this.불러오기ToolStripMenuItem.Name = "불러오기ToolStripMenuItem";
            this.불러오기ToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.I)));
            this.불러오기ToolStripMenuItem.Size = new System.Drawing.Size(291, 22);
            this.불러오기ToolStripMenuItem.Text = "엑셀로 단어 업로드";
            this.불러오기ToolStripMenuItem.Click += new System.EventHandler(this.불러오기ToolStripMenuItem_Click);
            // 
            // 내보내기ToolStripMenuItem
            // 
            this.내보내기ToolStripMenuItem.Name = "내보내기ToolStripMenuItem";
            this.내보내기ToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.O)));
            this.내보내기ToolStripMenuItem.Size = new System.Drawing.Size(291, 22);
            this.내보내기ToolStripMenuItem.Text = "오늘의 단어 내려받기";
            this.내보내기ToolStripMenuItem.Click += new System.EventHandler(this.내보내기ToolStripMenuItem_Click);
            // 
            // 입력양식내려받기ToolStripMenuItem
            // 
            this.입력양식내려받기ToolStripMenuItem.Name = "입력양식내려받기ToolStripMenuItem";
            this.입력양식내려받기ToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.F)));
            this.입력양식내려받기ToolStripMenuItem.Size = new System.Drawing.Size(291, 22);
            this.입력양식내려받기ToolStripMenuItem.Text = "단어 업로드 양식 내려받기";
            this.입력양식내려받기ToolStripMenuItem.Click += new System.EventHandler(this.입력양식내려받기ToolStripMenuItem_Click);
            // 
            // statusStripMain
            // 
            this.statusStripMain.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.statusStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabelMain,
            this.toolStripProgressBar});
            this.statusStripMain.Location = new System.Drawing.Point(0, 474);
            this.statusStripMain.Name = "statusStripMain";
            this.statusStripMain.Padding = new System.Windows.Forms.Padding(1, 0, 10, 0);
            this.statusStripMain.Size = new System.Drawing.Size(825, 22);
            this.statusStripMain.TabIndex = 20;
            this.statusStripMain.Text = "statusStrip1";
            // 
            // toolStripStatusLabelMain
            // 
            this.toolStripStatusLabelMain.Name = "toolStripStatusLabelMain";
            this.toolStripStatusLabelMain.Size = new System.Drawing.Size(0, 17);
            // 
            // toolStripProgressBar
            // 
            this.toolStripProgressBar.Name = "toolStripProgressBar";
            this.toolStripProgressBar.Size = new System.Drawing.Size(70, 16);
            this.toolStripProgressBar.Visible = false;
            // 
            // flowLayoutPanelCombo
            // 
            this.flowLayoutPanelCombo.Controls.Add(this.comboBoxLanguage);
            this.flowLayoutPanelCombo.Controls.Add(this.comboBoxTestGroup);
            this.flowLayoutPanelCombo.Controls.Add(this.comboBoxTest);
            this.flowLayoutPanelCombo.Controls.Add(this.comboBoxTestLevel);
            this.flowLayoutPanelCombo.Location = new System.Drawing.Point(8, 24);
            this.flowLayoutPanelCombo.Margin = new System.Windows.Forms.Padding(2);
            this.flowLayoutPanelCombo.Name = "flowLayoutPanelCombo";
            this.flowLayoutPanelCombo.Size = new System.Drawing.Size(506, 24);
            this.flowLayoutPanelCombo.TabIndex = 21;
            this.flowLayoutPanelCombo.Visible = false;
            // 
            // comboBoxLanguage
            // 
            this.comboBoxLanguage.FormattingEnabled = true;
            this.comboBoxLanguage.Location = new System.Drawing.Point(2, 2);
            this.comboBoxLanguage.Margin = new System.Windows.Forms.Padding(2);
            this.comboBoxLanguage.Name = "comboBoxLanguage";
            this.comboBoxLanguage.Size = new System.Drawing.Size(86, 20);
            this.comboBoxLanguage.TabIndex = 0;
            this.comboBoxLanguage.Visible = false;
            this.comboBoxLanguage.SelectedIndexChanged += new System.EventHandler(this.comboBoxLanguage_SelectedIndexChanged);
            // 
            // comboBoxTestGroup
            // 
            this.comboBoxTestGroup.FormattingEnabled = true;
            this.comboBoxTestGroup.Location = new System.Drawing.Point(92, 2);
            this.comboBoxTestGroup.Margin = new System.Windows.Forms.Padding(2);
            this.comboBoxTestGroup.Name = "comboBoxTestGroup";
            this.comboBoxTestGroup.Size = new System.Drawing.Size(86, 20);
            this.comboBoxTestGroup.TabIndex = 1;
            this.comboBoxTestGroup.Visible = false;
            this.comboBoxTestGroup.SelectedIndexChanged += new System.EventHandler(this.comboBoxTestGroup_SelectedIndexChanged);
            // 
            // comboBoxTest
            // 
            this.comboBoxTest.FormattingEnabled = true;
            this.comboBoxTest.Location = new System.Drawing.Point(182, 2);
            this.comboBoxTest.Margin = new System.Windows.Forms.Padding(2);
            this.comboBoxTest.Name = "comboBoxTest";
            this.comboBoxTest.Size = new System.Drawing.Size(86, 20);
            this.comboBoxTest.TabIndex = 2;
            this.comboBoxTest.Visible = false;
            this.comboBoxTest.SelectedIndexChanged += new System.EventHandler(this.comboBoxTest_SelectedIndexChanged);
            // 
            // comboBoxTestLevel
            // 
            this.comboBoxTestLevel.FormattingEnabled = true;
            this.comboBoxTestLevel.Location = new System.Drawing.Point(272, 2);
            this.comboBoxTestLevel.Margin = new System.Windows.Forms.Padding(2);
            this.comboBoxTestLevel.Name = "comboBoxTestLevel";
            this.comboBoxTestLevel.Size = new System.Drawing.Size(86, 20);
            this.comboBoxTestLevel.TabIndex = 3;
            this.comboBoxTestLevel.Visible = false;
            this.comboBoxTestLevel.SelectedIndexChanged += new System.EventHandler(this.comboBoxTestLevel_SelectedIndexChanged);
            // 
            // flowLayoutPanelDay
            // 
            this.flowLayoutPanelDay.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.flowLayoutPanelDay.AutoScroll = true;
            this.flowLayoutPanelDay.AutoSize = true;
            this.flowLayoutPanelDay.BackColor = System.Drawing.SystemColors.Control;
            this.flowLayoutPanelDay.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanelDay.Location = new System.Drawing.Point(11, 57);
            this.flowLayoutPanelDay.Name = "flowLayoutPanelDay";
            this.flowLayoutPanelDay.Size = new System.Drawing.Size(93, 414);
            this.flowLayoutPanelDay.TabIndex = 22;
            // 
            // listViewWordList
            // 
            this.listViewWordList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewWordList.Font = new System.Drawing.Font("나눔고딕", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.listViewWordList.HideSelection = false;
            this.listViewWordList.Location = new System.Drawing.Point(688, 57);
            this.listViewWordList.MultiSelect = false;
            this.listViewWordList.Name = "listViewWordList";
            this.listViewWordList.Size = new System.Drawing.Size(121, 414);
            this.listViewWordList.TabIndex = 26;
            this.listViewWordList.UseCompatibleStateImageBehavior = false;
            this.listViewWordList.View = System.Windows.Forms.View.List;
            this.listViewWordList.Visible = false;
            this.listViewWordList.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.listViewWordList_ItemSelectionChanged);
            // 
            // buttonWordBefore
            // 
            this.buttonWordBefore.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonWordBefore.Location = new System.Drawing.Point(334, 437);
            this.buttonWordBefore.Name = "buttonWordBefore";
            this.buttonWordBefore.Size = new System.Drawing.Size(75, 23);
            this.buttonWordBefore.TabIndex = 27;
            this.buttonWordBefore.Text = "이전";
            this.buttonWordBefore.UseVisualStyleBackColor = true;
            this.buttonWordBefore.Click += new System.EventHandler(this.buttonWordBefore_Click);
            // 
            // buttonWordAfter
            // 
            this.buttonWordAfter.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonWordAfter.Location = new System.Drawing.Point(415, 437);
            this.buttonWordAfter.Name = "buttonWordAfter";
            this.buttonWordAfter.Size = new System.Drawing.Size(75, 23);
            this.buttonWordAfter.TabIndex = 28;
            this.buttonWordAfter.Text = "다음";
            this.buttonWordAfter.UseVisualStyleBackColor = true;
            this.buttonWordAfter.Click += new System.EventHandler(this.buttonWordAfter_Click);
            // 
            // richTextBoxWord
            // 
            this.richTextBoxWord.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBoxWord.BackColor = System.Drawing.SystemColors.Control;
            this.richTextBoxWord.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBoxWord.Font = new System.Drawing.Font("굴림", 72F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.richTextBoxWord.Location = new System.Drawing.Point(127, 57);
            this.richTextBoxWord.Name = "richTextBoxWord";
            this.richTextBoxWord.ReadOnly = true;
            this.richTextBoxWord.Size = new System.Drawing.Size(541, 374);
            this.richTextBoxWord.TabIndex = 32;
            this.richTextBoxWord.Text = "";
            // 
            // checkBoxRepeat
            // 
            this.checkBoxRepeat.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.checkBoxRepeat.AutoSize = true;
            this.checkBoxRepeat.Location = new System.Drawing.Point(160, 444);
            this.checkBoxRepeat.Name = "checkBoxRepeat";
            this.checkBoxRepeat.Size = new System.Drawing.Size(48, 16);
            this.checkBoxRepeat.TabIndex = 33;
            this.checkBoxRepeat.Text = "반복";
            this.checkBoxRepeat.UseVisualStyleBackColor = true;
            this.checkBoxRepeat.Visible = false;
            this.checkBoxRepeat.CheckedChanged += new System.EventHandler(this.checkBoxRepeat_CheckedChanged);
            // 
            // checkBoxRandom
            // 
            this.checkBoxRandom.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.checkBoxRandom.AutoSize = true;
            this.checkBoxRandom.Location = new System.Drawing.Point(214, 444);
            this.checkBoxRandom.Name = "checkBoxRandom";
            this.checkBoxRandom.Size = new System.Drawing.Size(48, 16);
            this.checkBoxRandom.TabIndex = 34;
            this.checkBoxRandom.Text = "랜덤";
            this.checkBoxRandom.UseVisualStyleBackColor = true;
            this.checkBoxRandom.Visible = false;
            // 
            // checkBoxBlink
            // 
            this.checkBoxBlink.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.checkBoxBlink.AutoSize = true;
            this.checkBoxBlink.Location = new System.Drawing.Point(268, 444);
            this.checkBoxBlink.Name = "checkBoxBlink";
            this.checkBoxBlink.Size = new System.Drawing.Size(60, 16);
            this.checkBoxBlink.TabIndex = 35;
            this.checkBoxBlink.Text = "깜빡이";
            this.checkBoxBlink.UseVisualStyleBackColor = true;
            this.checkBoxBlink.Visible = false;
            // 
            // checkBoxAuto
            // 
            this.checkBoxAuto.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.checkBoxAuto.AutoSize = true;
            this.checkBoxAuto.Location = new System.Drawing.Point(106, 444);
            this.checkBoxAuto.Name = "checkBoxAuto";
            this.checkBoxAuto.Size = new System.Drawing.Size(48, 16);
            this.checkBoxAuto.TabIndex = 36;
            this.checkBoxAuto.Text = "자동";
            this.checkBoxAuto.UseVisualStyleBackColor = true;
            this.checkBoxAuto.Visible = false;
            this.checkBoxAuto.CheckedChanged += new System.EventHandler(this.checkBoxAuto_CheckedChanged);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(825, 496);
            this.Controls.Add(this.checkBoxAuto);
            this.Controls.Add(this.checkBoxBlink);
            this.Controls.Add(this.checkBoxRandom);
            this.Controls.Add(this.checkBoxRepeat);
            this.Controls.Add(this.richTextBoxWord);
            this.Controls.Add(this.buttonWordAfter);
            this.Controls.Add(this.buttonWordBefore);
            this.Controls.Add(this.listViewWordList);
            this.Controls.Add(this.flowLayoutPanelDay);
            this.Controls.Add(this.flowLayoutPanelCombo);
            this.Controls.Add(this.statusStripMain);
            this.Controls.Add(this.flowLayoutPanelLogIn);
            this.Controls.Add(this.menuStripMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStripMain;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "단어 암기 프로그램";
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
        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelDay;
        private System.Windows.Forms.ListView listViewWordList;
        private System.Windows.Forms.Button buttonWordBefore;
        private System.Windows.Forms.Button buttonWordAfter;
        private System.Windows.Forms.RichTextBox richTextBoxWord;
        private System.Windows.Forms.CheckBox checkBoxRepeat;
        private System.Windows.Forms.CheckBox checkBoxRandom;
        private System.Windows.Forms.CheckBox checkBoxBlink;
        private System.Windows.Forms.CheckBox checkBoxAuto;
    }
}

