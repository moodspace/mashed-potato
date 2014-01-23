namespace MyBucks
{
  partial class DialogZip
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
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.groupBox3 = new System.Windows.Forms.GroupBox();
      this.radioBtnOverwrite = new System.Windows.Forms.RadioButton();
      this.radioBtnMerge = new System.Windows.Forms.RadioButton();
      this.button1 = new System.Windows.Forms.Button();
      this.label3 = new System.Windows.Forms.Label();
      this.textBox2 = new System.Windows.Forms.TextBox();
      this.groupBox2 = new System.Windows.Forms.GroupBox();
      this.label1 = new System.Windows.Forms.Label();
      this.comboBox1 = new System.Windows.Forms.ComboBox();
      this.saveFileDialogPack = new System.Windows.Forms.SaveFileDialog();
      this.buttonPack = new System.Windows.Forms.Button();
      this.groupBox1.SuspendLayout();
      this.groupBox3.SuspendLayout();
      this.groupBox2.SuspendLayout();
      this.SuspendLayout();
      // 
      // groupBox1
      // 
      this.groupBox1.Controls.Add(this.groupBox3);
      this.groupBox1.Controls.Add(this.button1);
      this.groupBox1.Controls.Add(this.label3);
      this.groupBox1.Controls.Add(this.textBox2);
      this.groupBox1.Location = new System.Drawing.Point(12, 12);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(357, 116);
      this.groupBox1.TabIndex = 0;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "File Option";
      // 
      // groupBox3
      // 
      this.groupBox3.Controls.Add(this.radioBtnOverwrite);
      this.groupBox3.Controls.Add(this.radioBtnMerge);
      this.groupBox3.Location = new System.Drawing.Point(15, 55);
      this.groupBox3.Name = "groupBox3";
      this.groupBox3.Size = new System.Drawing.Size(201, 55);
      this.groupBox3.TabIndex = 8;
      this.groupBox3.TabStop = false;
      this.groupBox3.Text = "Overwrite Mode";
      // 
      // radioBtnOverwrite
      // 
      this.radioBtnOverwrite.AutoSize = true;
      this.radioBtnOverwrite.Location = new System.Drawing.Point(114, 22);
      this.radioBtnOverwrite.Name = "radioBtnOverwrite";
      this.radioBtnOverwrite.Size = new System.Drawing.Size(75, 19);
      this.radioBtnOverwrite.TabIndex = 7;
      this.radioBtnOverwrite.Text = "Overwrite";
      this.radioBtnOverwrite.UseVisualStyleBackColor = true;
      // 
      // radioBtnMerge
      // 
      this.radioBtnMerge.AutoSize = true;
      this.radioBtnMerge.Checked = true;
      this.radioBtnMerge.Location = new System.Drawing.Point(6, 22);
      this.radioBtnMerge.Name = "radioBtnMerge";
      this.radioBtnMerge.Size = new System.Drawing.Size(102, 19);
      this.radioBtnMerge.TabIndex = 6;
      this.radioBtnMerge.TabStop = true;
      this.radioBtnMerge.Text = "Merge(Update)";
      this.radioBtnMerge.UseVisualStyleBackColor = true;
      // 
      // button1
      // 
      this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.button1.Location = new System.Drawing.Point(272, 26);
      this.button1.Name = "button1";
      this.button1.Size = new System.Drawing.Size(61, 23);
      this.button1.TabIndex = 5;
      this.button1.Text = "Browse";
      this.button1.UseVisualStyleBackColor = false;
      this.button1.Click += new System.EventHandler(this.button1_Click);
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(12, 30);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(45, 15);
      this.label3.TabIndex = 4;
      this.label3.Text = "Save As";
      // 
      // textBox2
      // 
      this.textBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.textBox2.Location = new System.Drawing.Point(72, 26);
      this.textBox2.Name = "textBox2";
      this.textBox2.Size = new System.Drawing.Size(194, 23);
      this.textBox2.TabIndex = 3;
      this.textBox2.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
      // 
      // groupBox2
      // 
      this.groupBox2.Controls.Add(this.label1);
      this.groupBox2.Controls.Add(this.comboBox1);
      this.groupBox2.Location = new System.Drawing.Point(12, 134);
      this.groupBox2.Name = "groupBox2";
      this.groupBox2.Size = new System.Drawing.Size(357, 69);
      this.groupBox2.TabIndex = 0;
      this.groupBox2.TabStop = false;
      this.groupBox2.Text = "Format Option";
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(12, 29);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(44, 15);
      this.label1.TabIndex = 1;
      this.label1.Text = "Format";
      // 
      // comboBox1
      // 
      this.comboBox1.DropDownWidth = 150;
      this.comboBox1.Enabled = false;
      this.comboBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.comboBox1.FormattingEnabled = true;
      this.comboBox1.Items.AddRange(new object[] {
            "ZIP",
            "mbA",
            "7z"});
      this.comboBox1.Location = new System.Drawing.Point(72, 26);
      this.comboBox1.MaxDropDownItems = 3;
      this.comboBox1.Name = "comboBox1";
      this.comboBox1.Size = new System.Drawing.Size(90, 23);
      this.comboBox1.TabIndex = 0;
      this.comboBox1.Text = "ZIP";
      // 
      // saveFileDialogPack
      // 
      this.saveFileDialogPack.Filter = "Zip Archive(*.zip)|*.zip|MyBucks Archive(*.mbA)|*.mbA|7-zip Archive(*.7z)|*.7z";
      // 
      // buttonPack
      // 
      this.buttonPack.Enabled = false;
      this.buttonPack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.buttonPack.Location = new System.Drawing.Point(258, 209);
      this.buttonPack.Name = "buttonPack";
      this.buttonPack.Size = new System.Drawing.Size(111, 23);
      this.buttonPack.TabIndex = 1;
      this.buttonPack.Text = "Add To Archive";
      this.buttonPack.UseVisualStyleBackColor = true;
      this.buttonPack.Click += new System.EventHandler(this.buttonPack_Click);
      // 
      // DialogZip
      // 
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
      this.ClientSize = new System.Drawing.Size(381, 244);
      this.Controls.Add(this.buttonPack);
      this.Controls.Add(this.groupBox2);
      this.Controls.Add(this.groupBox1);
      this.Font = new System.Drawing.Font("Source Sans Pro", 9F);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "DialogZip";
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Add to archive";
      this.Load += new System.EventHandler(this.DialogZip_Load);
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      this.groupBox3.ResumeLayout(false);
      this.groupBox3.PerformLayout();
      this.groupBox2.ResumeLayout(false);
      this.groupBox2.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.Button button1;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.TextBox textBox2;
    private System.Windows.Forms.GroupBox groupBox2;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.ComboBox comboBox1;
    private System.Windows.Forms.SaveFileDialog saveFileDialogPack;
    private System.Windows.Forms.Button buttonPack;
    private System.Windows.Forms.GroupBox groupBox3;
    private System.Windows.Forms.RadioButton radioBtnOverwrite;
    private System.Windows.Forms.RadioButton radioBtnMerge;

  }
}