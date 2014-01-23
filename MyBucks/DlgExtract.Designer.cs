namespace MyBucks
{
  partial class DlgExtract
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
      this.progressBar1 = new System.Windows.Forms.ProgressBar();
      this.timerDetectFinish = new System.Windows.Forms.Timer(this.components);
      this.buttonAbort = new System.Windows.Forms.Button();
      this.SuspendLayout();
      // 
      // progressBar1
      // 
      this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.progressBar1.Location = new System.Drawing.Point(12, 12);
      this.progressBar1.Name = "progressBar1";
      this.progressBar1.Size = new System.Drawing.Size(389, 23);
      this.progressBar1.TabIndex = 1;
      // 
      // timerDetectFinish
      // 
      this.timerDetectFinish.Tick += new System.EventHandler(this.timerDetectFinish_Tick);
      // 
      // buttonAbort
      // 
      this.buttonAbort.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.buttonAbort.Location = new System.Drawing.Point(326, 41);
      this.buttonAbort.Name = "buttonAbort";
      this.buttonAbort.Size = new System.Drawing.Size(75, 23);
      this.buttonAbort.TabIndex = 2;
      this.buttonAbort.Text = "Abort";
      this.buttonAbort.UseVisualStyleBackColor = true;
      this.buttonAbort.Click += new System.EventHandler(this.buttonAbort_Click);
      // 
      // DlgExtract
      // 
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
      this.CancelButton = this.buttonAbort;
      this.ClientSize = new System.Drawing.Size(413, 76);
      this.Controls.Add(this.buttonAbort);
      this.Controls.Add(this.progressBar1);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "DlgExtract";
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Extraction in progress";
      this.Load += new System.EventHandler(this.DlgExtract_Load);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.ProgressBar progressBar1;
    private System.Windows.Forms.Timer timerDetectFinish;
    private System.Windows.Forms.Button buttonAbort;
  }
}