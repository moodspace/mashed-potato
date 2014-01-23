namespace MyBucks
{
  partial class DialogSettings
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DialogSettings));
      this.tabControl1 = new System.Windows.Forms.TabControl();
      this.tabDisplay = new System.Windows.Forms.TabPage();
      this.label4 = new System.Windows.Forms.Label();
      this.comboBoxPickIcon = new System.Windows.Forms.ComboBox();
      this.checkBox2 = new System.Windows.Forms.CheckBox();
      this.checkBox1 = new System.Windows.Forms.CheckBox();
      this.label2 = new System.Windows.Forms.Label();
      this.comboBoxPickFont = new System.Windows.Forms.ComboBox();
      this.label1 = new System.Windows.Forms.Label();
      this.comboBoxPickLang = new System.Windows.Forms.ComboBox();
      this.tabPath = new System.Windows.Forms.TabPage();
      this.label3 = new System.Windows.Forms.Label();
      this.checkBox3 = new System.Windows.Forms.CheckBox();
      this.button2 = new System.Windows.Forms.Button();
      this.button1 = new System.Windows.Forms.Button();
      this.tabControl1.SuspendLayout();
      this.tabDisplay.SuspendLayout();
      this.tabPath.SuspendLayout();
      this.SuspendLayout();
      // 
      // tabControl1
      // 
      this.tabControl1.Controls.Add(this.tabDisplay);
      this.tabControl1.Controls.Add(this.tabPath);
      this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tabControl1.Location = new System.Drawing.Point(0, 0);
      this.tabControl1.Name = "tabControl1";
      this.tabControl1.SelectedIndex = 0;
      this.tabControl1.Size = new System.Drawing.Size(504, 249);
      this.tabControl1.TabIndex = 2;
      // 
      // tabDisplay
      // 
      this.tabDisplay.Controls.Add(this.label4);
      this.tabDisplay.Controls.Add(this.comboBoxPickIcon);
      this.tabDisplay.Controls.Add(this.checkBox2);
      this.tabDisplay.Controls.Add(this.checkBox1);
      this.tabDisplay.Controls.Add(this.label2);
      this.tabDisplay.Controls.Add(this.comboBoxPickFont);
      this.tabDisplay.Controls.Add(this.label1);
      this.tabDisplay.Controls.Add(this.comboBoxPickLang);
      this.tabDisplay.Location = new System.Drawing.Point(4, 24);
      this.tabDisplay.Name = "tabDisplay";
      this.tabDisplay.Padding = new System.Windows.Forms.Padding(3);
      this.tabDisplay.Size = new System.Drawing.Size(496, 221);
      this.tabDisplay.TabIndex = 0;
      this.tabDisplay.Text = "Display";
      this.tabDisplay.UseVisualStyleBackColor = true;
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point(17, 75);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(44, 15);
      this.label4.TabIndex = 6;
      this.label4.Text = "Iconset";
      // 
      // comboBoxPickIcon
      // 
      this.comboBoxPickIcon.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.comboBoxPickIcon.FormattingEnabled = true;
      this.comboBoxPickIcon.Location = new System.Drawing.Point(80, 72);
      this.comboBoxPickIcon.Name = "comboBoxPickIcon";
      this.comboBoxPickIcon.Size = new System.Drawing.Size(159, 23);
      this.comboBoxPickIcon.TabIndex = 5;
      // 
      // checkBox2
      // 
      this.checkBox2.AutoEllipsis = true;
      this.checkBox2.Location = new System.Drawing.Point(299, 57);
      this.checkBox2.Name = "checkBox2";
      this.checkBox2.Size = new System.Drawing.Size(180, 38);
      this.checkBox2.TabIndex = 4;
      this.checkBox2.Text = "Show file extention in explorer";
      this.checkBox2.UseVisualStyleBackColor = false;
      this.checkBox2.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
      // 
      // checkBox1
      // 
      this.checkBox1.AutoEllipsis = true;
      this.checkBox1.Location = new System.Drawing.Point(299, 14);
      this.checkBox1.Name = "checkBox1";
      this.checkBox1.Size = new System.Drawing.Size(180, 38);
      this.checkBox1.TabIndex = 3;
      this.checkBox1.Text = "Show full path in titlebar";
      this.checkBox1.UseVisualStyleBackColor = false;
      this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(17, 46);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(31, 15);
      this.label2.TabIndex = 3;
      this.label2.Text = "Font";
      // 
      // comboBoxPickFont
      // 
      this.comboBoxPickFont.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.comboBoxPickFont.FormattingEnabled = true;
      this.comboBoxPickFont.Location = new System.Drawing.Point(80, 43);
      this.comboBoxPickFont.Name = "comboBoxPickFont";
      this.comboBoxPickFont.Size = new System.Drawing.Size(159, 23);
      this.comboBoxPickFont.TabIndex = 2;
      this.comboBoxPickFont.DropDown += new System.EventHandler(this.comboBoxPickFont_DropDown);
      this.comboBoxPickFont.SelectedIndexChanged += new System.EventHandler(this.comboBoxPickFont_SelectedIndexChanged);
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(17, 17);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(57, 15);
      this.label1.TabIndex = 1;
      this.label1.Text = "Language";
      // 
      // comboBoxPickLang
      // 
      this.comboBoxPickLang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.comboBoxPickLang.FormattingEnabled = true;
      this.comboBoxPickLang.Location = new System.Drawing.Point(80, 14);
      this.comboBoxPickLang.Name = "comboBoxPickLang";
      this.comboBoxPickLang.Size = new System.Drawing.Size(159, 23);
      this.comboBoxPickLang.TabIndex = 0;
      // 
      // tabPath
      // 
      this.tabPath.Controls.Add(this.label3);
      this.tabPath.Controls.Add(this.checkBox3);
      this.tabPath.Controls.Add(this.button2);
      this.tabPath.Controls.Add(this.button1);
      this.tabPath.Location = new System.Drawing.Point(4, 24);
      this.tabPath.Name = "tabPath";
      this.tabPath.Padding = new System.Windows.Forms.Padding(3);
      this.tabPath.Size = new System.Drawing.Size(496, 221);
      this.tabPath.TabIndex = 3;
      this.tabPath.Text = "Path";
      this.tabPath.UseVisualStyleBackColor = true;
      // 
      // label3
      // 
      this.label3.Location = new System.Drawing.Point(17, 39);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(471, 110);
      this.label3.TabIndex = 10;
      this.label3.Text = resources.GetString("label3.Text");
      // 
      // checkBox3
      // 
      this.checkBox3.AutoSize = true;
      this.checkBox3.Checked = true;
      this.checkBox3.CheckState = System.Windows.Forms.CheckState.Checked;
      this.checkBox3.Enabled = false;
      this.checkBox3.Location = new System.Drawing.Point(17, 17);
      this.checkBox3.Name = "checkBox3";
      this.checkBox3.Size = new System.Drawing.Size(282, 16);
      this.checkBox3.TabIndex = 9;
      this.checkBox3.Text = "Create a new folder to hold extracted files";
      this.checkBox3.UseVisualStyleBackColor = true;
      // 
      // button2
      // 
      this.button2.Location = new System.Drawing.Point(17, 186);
      this.button2.Name = "button2";
      this.button2.Size = new System.Drawing.Size(344, 23);
      this.button2.TabIndex = 8;
      this.button2.Text = "Open temporary folder (all begin with \'mbstemp\')";
      this.button2.UseVisualStyleBackColor = true;
      this.button2.Click += new System.EventHandler(this.button2_Click);
      // 
      // button1
      // 
      this.button1.Location = new System.Drawing.Point(17, 157);
      this.button1.Name = "button1";
      this.button1.Size = new System.Drawing.Size(344, 23);
      this.button1.TabIndex = 7;
      this.button1.Text = "Clear temporary files";
      this.button1.UseVisualStyleBackColor = true;
      this.button1.Click += new System.EventHandler(this.button1_Click);
      // 
      // DialogSettings
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 15F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(504, 249);
      this.Controls.Add(this.tabControl1);
      this.Font = new System.Drawing.Font("Source Sans Pro", 9F);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "DialogSettings";
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Settings";
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DialogSettings_FormClosing);
      this.Load += new System.EventHandler(this.DialogSettings_Load);
      this.tabControl1.ResumeLayout(false);
      this.tabDisplay.ResumeLayout(false);
      this.tabDisplay.PerformLayout();
      this.tabPath.ResumeLayout(false);
      this.tabPath.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.TabControl tabControl1;
    private System.Windows.Forms.TabPage tabDisplay;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.ComboBox comboBoxPickLang;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.ComboBox comboBoxPickFont;
    private System.Windows.Forms.CheckBox checkBox1;
    private System.Windows.Forms.CheckBox checkBox2;
    private System.Windows.Forms.TabPage tabPath;
    private System.Windows.Forms.Button button2;
    private System.Windows.Forms.Button button1;
    private System.Windows.Forms.CheckBox checkBox3;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.ComboBox comboBoxPickIcon;
  }
}