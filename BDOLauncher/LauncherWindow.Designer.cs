namespace BDOLauncher
{
    partial class LauncherWindow
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
            this.components = new System.ComponentModel.Container();
            this.emailTextBox = new System.Windows.Forms.TextBox();
            this.passwordTextBox = new System.Windows.Forms.TextBox();
            this.emailLabel = new System.Windows.Forms.Label();
            this.passwordLabel = new System.Windows.Forms.Label();
            this.steamOverlayRadio = new System.Windows.Forms.RadioButton();
            this.passwordCheckbox = new System.Windows.Forms.CheckBox();
            this.startButton = new System.Windows.Forms.Button();
            this.autoStartCheckbox = new System.Windows.Forms.CheckBox();
            this.overlayHelpButton = new System.Windows.Forms.Button();
            this.autoStartTimer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // emailTextBox
            // 
            this.emailTextBox.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.emailTextBox.Location = new System.Drawing.Point(472, 125);
            this.emailTextBox.Name = "emailTextBox";
            this.emailTextBox.Size = new System.Drawing.Size(196, 25);
            this.emailTextBox.TabIndex = 0;
            // 
            // passwordTextBox
            // 
            this.passwordTextBox.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.passwordTextBox.Location = new System.Drawing.Point(472, 192);
            this.passwordTextBox.Name = "passwordTextBox";
            this.passwordTextBox.PasswordChar = '•';
            this.passwordTextBox.Size = new System.Drawing.Size(196, 25);
            this.passwordTextBox.TabIndex = 1;
            // 
            // emailLabel
            // 
            this.emailLabel.AutoSize = true;
            this.emailLabel.BackColor = System.Drawing.Color.Transparent;
            this.emailLabel.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.emailLabel.ForeColor = System.Drawing.Color.White;
            this.emailLabel.Location = new System.Drawing.Point(468, 103);
            this.emailLabel.Name = "emailLabel";
            this.emailLabel.Size = new System.Drawing.Size(41, 19);
            this.emailLabel.TabIndex = 1;
            this.emailLabel.Text = "Email";
            // 
            // passwordLabel
            // 
            this.passwordLabel.AutoSize = true;
            this.passwordLabel.BackColor = System.Drawing.Color.Transparent;
            this.passwordLabel.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.passwordLabel.ForeColor = System.Drawing.Color.White;
            this.passwordLabel.Location = new System.Drawing.Point(468, 170);
            this.passwordLabel.Name = "passwordLabel";
            this.passwordLabel.Size = new System.Drawing.Size(67, 19);
            this.passwordLabel.TabIndex = 1;
            this.passwordLabel.Text = "Password";
            // 
            // steamOverlayCheckbox
            // 
            this.steamOverlayRadio.AutoSize = true;
            this.steamOverlayRadio.BackColor = System.Drawing.Color.Transparent;
            this.steamOverlayRadio.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.steamOverlayRadio.ForeColor = System.Drawing.Color.White;
            this.steamOverlayRadio.Location = new System.Drawing.Point(472, 260);
            this.steamOverlayRadio.Name = "steamOverlayRadio";
            this.steamOverlayRadio.Size = new System.Drawing.Size(169, 23);
            this.steamOverlayRadio.TabIndex = 3;
            this.steamOverlayRadio.Text = "Steam Overlay enabled";
            this.steamOverlayRadio.UseVisualStyleBackColor = false;
            this.steamOverlayRadio.CheckedChanged += new System.EventHandler(this.steamOverlayCheckbox_CheckedChanged);
            // 
            // passwordCheckbox
            // 
            this.passwordCheckbox.AutoSize = true;
            this.passwordCheckbox.BackColor = System.Drawing.Color.Transparent;
            this.passwordCheckbox.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.passwordCheckbox.ForeColor = System.Drawing.Color.White;
            this.passwordCheckbox.Location = new System.Drawing.Point(472, 223);
            this.passwordCheckbox.Name = "passwordCheckbox";
            this.passwordCheckbox.Size = new System.Drawing.Size(156, 23);
            this.passwordCheckbox.TabIndex = 2;
            this.passwordCheckbox.Text = "Remember Password";
            this.passwordCheckbox.UseVisualStyleBackColor = false;
            // 
            // startButton
            // 
            this.startButton.BackColor = System.Drawing.Color.White;
            this.startButton.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.startButton.Location = new System.Drawing.Point(472, 305);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(100, 37);
            this.startButton.TabIndex = 4;
            this.startButton.Text = "Start";
            this.startButton.UseVisualStyleBackColor = false;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // autoStartCheckbox
            // 
            this.autoStartCheckbox.AutoSize = true;
            this.autoStartCheckbox.BackColor = System.Drawing.Color.Transparent;
            this.autoStartCheckbox.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.autoStartCheckbox.ForeColor = System.Drawing.Color.White;
            this.autoStartCheckbox.Location = new System.Drawing.Point(578, 313);
            this.autoStartCheckbox.Name = "autoStartCheckbox";
            this.autoStartCheckbox.Size = new System.Drawing.Size(86, 23);
            this.autoStartCheckbox.TabIndex = 5;
            this.autoStartCheckbox.Text = "Autostart";
            this.autoStartCheckbox.UseVisualStyleBackColor = false;
            this.autoStartCheckbox.CheckedChanged += new System.EventHandler(this.autoStartCheckbox_CheckedChanged);
            // 
            // overlayHelpButton
            // 
            this.overlayHelpButton.BackColor = System.Drawing.Color.White;
            this.overlayHelpButton.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.overlayHelpButton.Location = new System.Drawing.Point(642, 257);
            this.overlayHelpButton.Name = "overlayHelpButton";
            this.overlayHelpButton.Size = new System.Drawing.Size(26, 26);
            this.overlayHelpButton.TabIndex = 6;
            this.overlayHelpButton.Text = "?";
            this.overlayHelpButton.UseVisualStyleBackColor = false;
            this.overlayHelpButton.Click += new System.EventHandler(this.overlayHelpButton_Click);
            // 
            // autoStartTimer
            // 
            this.autoStartTimer.Interval = 1000;
            this.autoStartTimer.Tick += new System.EventHandler(this.autoStartTimer_Tick);
            // 
            // LauncherWindow
            // 
            this.AcceptButton = this.startButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.BackgroundImage = global::BDOLauncher.Properties.Resources.bg;
            this.ClientSize = new System.Drawing.Size(717, 394);
            this.Controls.Add(this.overlayHelpButton);
            this.Controls.Add(this.autoStartCheckbox);
            this.Controls.Add(this.startButton);
            this.Controls.Add(this.passwordCheckbox);
            this.Controls.Add(this.steamOverlayRadio);
            this.Controls.Add(this.passwordLabel);
            this.Controls.Add(this.emailLabel);
            this.Controls.Add(this.passwordTextBox);
            this.Controls.Add(this.emailTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "LauncherWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Unofficial BDO Launcher";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox emailTextBox;
        private System.Windows.Forms.TextBox passwordTextBox;
        private System.Windows.Forms.Label emailLabel;
        private System.Windows.Forms.Label passwordLabel;
        private System.Windows.Forms.RadioButton steamOverlayRadio;
        private System.Windows.Forms.CheckBox passwordCheckbox;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.CheckBox autoStartCheckbox;
        private System.Windows.Forms.Button overlayHelpButton;
        private System.Windows.Forms.Timer autoStartTimer;
    }
}

