namespace Main
{
    partial class FormSignUp
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSignUp));
            this.labelPassword = new System.Windows.Forms.Label();
            this.labelID = new System.Windows.Forms.Label();
            this.textBoxPassword = new System.Windows.Forms.TextBox();
            this.textBoxID = new System.Windows.Forms.TextBox();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonLogin = new System.Windows.Forms.Button();
            this.labelPasswordCheck = new System.Windows.Forms.Label();
            this.textBoxPasswordCheck = new System.Windows.Forms.TextBox();
            this.buttonSignUp = new System.Windows.Forms.Button();
            this.labelName = new System.Windows.Forms.Label();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // labelPassword
            // 
            this.labelPassword.AutoSize = true;
            this.labelPassword.Location = new System.Drawing.Point(83, 105);
            this.labelPassword.Name = "labelPassword";
            this.labelPassword.Size = new System.Drawing.Size(80, 18);
            this.labelPassword.TabIndex = 10;
            this.labelPassword.Text = "패스워드";
            // 
            // labelID
            // 
            this.labelID.AutoSize = true;
            this.labelID.Location = new System.Drawing.Point(83, 37);
            this.labelID.Name = "labelID";
            this.labelID.Size = new System.Drawing.Size(62, 18);
            this.labelID.TabIndex = 8;
            this.labelID.Text = "이메일";
            // 
            // textBoxPassword
            // 
            this.textBoxPassword.Location = new System.Drawing.Point(210, 102);
            this.textBoxPassword.Name = "textBoxPassword";
            this.textBoxPassword.PasswordChar = '*';
            this.textBoxPassword.Size = new System.Drawing.Size(186, 28);
            this.textBoxPassword.TabIndex = 3;
            // 
            // textBoxID
            // 
            this.textBoxID.Location = new System.Drawing.Point(210, 34);
            this.textBoxID.Name = "textBoxID";
            this.textBoxID.Size = new System.Drawing.Size(186, 28);
            this.textBoxID.TabIndex = 1;
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(321, 183);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 28);
            this.buttonCancel.TabIndex = 6;
            this.buttonCancel.Text = "취소";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // buttonLogin
            // 
            this.buttonLogin.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.buttonLogin.Location = new System.Drawing.Point(86, 183);
            this.buttonLogin.Name = "buttonLogin";
            this.buttonLogin.Size = new System.Drawing.Size(75, 28);
            this.buttonLogin.TabIndex = 7;
            this.buttonLogin.Text = "로그인";
            this.buttonLogin.UseVisualStyleBackColor = true;
            // 
            // labelPasswordCheck
            // 
            this.labelPasswordCheck.AutoSize = true;
            this.labelPasswordCheck.Location = new System.Drawing.Point(83, 139);
            this.labelPasswordCheck.Name = "labelPasswordCheck";
            this.labelPasswordCheck.Size = new System.Drawing.Size(122, 18);
            this.labelPasswordCheck.TabIndex = 11;
            this.labelPasswordCheck.Text = "패스워드 확인";
            // 
            // textBoxPasswordCheck
            // 
            this.textBoxPasswordCheck.Location = new System.Drawing.Point(210, 136);
            this.textBoxPasswordCheck.Name = "textBoxPasswordCheck";
            this.textBoxPasswordCheck.PasswordChar = '*';
            this.textBoxPasswordCheck.Size = new System.Drawing.Size(186, 28);
            this.textBoxPasswordCheck.TabIndex = 4;
            this.textBoxPasswordCheck.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxPasswordCheck_KeyPress);
            // 
            // buttonSignUp
            // 
            this.buttonSignUp.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonSignUp.Location = new System.Drawing.Point(192, 183);
            this.buttonSignUp.Name = "buttonSignUp";
            this.buttonSignUp.Size = new System.Drawing.Size(98, 28);
            this.buttonSignUp.TabIndex = 5;
            this.buttonSignUp.Text = "회원가입";
            this.buttonSignUp.UseVisualStyleBackColor = true;
            // 
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.Location = new System.Drawing.Point(83, 71);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(44, 18);
            this.labelName.TabIndex = 9;
            this.labelName.Text = "이름";
            // 
            // textBoxName
            // 
            this.textBoxName.Location = new System.Drawing.Point(210, 68);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(186, 28);
            this.textBoxName.TabIndex = 2;
            // 
            // FormSignUp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(478, 244);
            this.Controls.Add(this.labelName);
            this.Controls.Add(this.textBoxName);
            this.Controls.Add(this.buttonSignUp);
            this.Controls.Add(this.labelPasswordCheck);
            this.Controls.Add(this.textBoxPasswordCheck);
            this.Controls.Add(this.labelPassword);
            this.Controls.Add(this.labelID);
            this.Controls.Add(this.textBoxPassword);
            this.Controls.Add(this.textBoxID);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonLogin);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormSignUp";
            this.Text = "회원가입";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelPassword;
        private System.Windows.Forms.Label labelID;
        public System.Windows.Forms.TextBox textBoxPassword;
        public System.Windows.Forms.TextBox textBoxID;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonLogin;
        private System.Windows.Forms.Label labelPasswordCheck;
        public System.Windows.Forms.TextBox textBoxPasswordCheck;
        private System.Windows.Forms.Button buttonSignUp;
        private System.Windows.Forms.Label labelName;
        public System.Windows.Forms.TextBox textBoxName;
    }
}