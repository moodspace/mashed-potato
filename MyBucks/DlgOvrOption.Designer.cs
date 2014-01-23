namespace MyBucks
{
  partial class DlgOvrOption
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
      this.yes = new System.Windows.Forms.Button();
      this.no = new System.Windows.Forms.Button();
      this.allYes = new System.Windows.Forms.Button();
      this.allNo = new System.Windows.Forms.Button();
      this.abort = new System.Windows.Forms.Button();
      this.label1 = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.SuspendLayout();
      // 
      // yes
      // 
      this.yes.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
      this.yes.Location = new System.Drawing.Point(12, 117);
      this.yes.Name = "yes";
      this.yes.Size = new System.Drawing.Size(75, 23);
      this.yes.TabIndex = 0;
      this.yes.Text = "Yes (Enter)";
      this.yes.UseVisualStyleBackColor = true;
      this.yes.Click += new System.EventHandler(this.yes_Click);
      // 
      // no
      // 
      this.no.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
      this.no.Location = new System.Drawing.Point(93, 117);
      this.no.Name = "no";
      this.no.Size = new System.Drawing.Size(75, 23);
      this.no.TabIndex = 1;
      this.no.Text = "No";
      this.no.UseVisualStyleBackColor = true;
      this.no.Click += new System.EventHandler(this.no_Click);
      // 
      // allYes
      // 
      this.allYes.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
      this.allYes.Location = new System.Drawing.Point(12, 146);
      this.allYes.Name = "allYes";
      this.allYes.Size = new System.Drawing.Size(75, 23);
      this.allYes.TabIndex = 2;
      this.allYes.Text = "All yes";
      this.allYes.UseVisualStyleBackColor = true;
      this.allYes.Click += new System.EventHandler(this.allYes_Click);
      // 
      // allNo
      // 
      this.allNo.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
      this.allNo.Location = new System.Drawing.Point(93, 146);
      this.allNo.Name = "allNo";
      this.allNo.Size = new System.Drawing.Size(75, 23);
      this.allNo.TabIndex = 3;
      this.allNo.Text = "All no";
      this.allNo.UseVisualStyleBackColor = true;
      this.allNo.Click += new System.EventHandler(this.allNo_Click);
      // 
      // abort
      // 
      this.abort.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
      this.abort.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.abort.Location = new System.Drawing.Point(175, 117);
      this.abort.Name = "abort";
      this.abort.Size = new System.Drawing.Size(75, 52);
      this.abort.TabIndex = 4;
      this.abort.Text = "Abort\r\n(Esc)";
      this.abort.UseVisualStyleBackColor = true;
      this.abort.Click += new System.EventHandler(this.abort_Click);
      // 
      // label1
      // 
      this.label1.AutoEllipsis = true;
      this.label1.Location = new System.Drawing.Point(12, 9);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(237, 45);
      this.label1.TabIndex = 5;
      this.label1.Text = "Use file        /        combine folder \'A\'";
      // 
      // label2
      // 
      this.label2.AutoEllipsis = true;
      this.label2.Location = new System.Drawing.Point(10, 54);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(237, 60);
      this.label2.TabIndex = 6;
      this.label2.Text = "to replace  /        with \'B\'?";
      // 
      // DlgOvrOption
      // 
      this.AcceptButton = this.yes;
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
      this.CancelButton = this.abort;
      this.ClientSize = new System.Drawing.Size(262, 177);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.abort);
      this.Controls.Add(this.allNo);
      this.Controls.Add(this.allYes);
      this.Controls.Add(this.no);
      this.Controls.Add(this.yes);
      this.Font = new System.Drawing.Font("Source Sans Pro", 9F);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "DlgOvrOption";
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Overwrite options";
      this.TopMost = true;
      this.Load += new System.EventHandler(this.DlgOvrOption_Load);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Button yes;
    private System.Windows.Forms.Button no;
    private System.Windows.Forms.Button allYes;
    private System.Windows.Forms.Button allNo;
    private System.Windows.Forms.Button abort;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label2;
  }
}