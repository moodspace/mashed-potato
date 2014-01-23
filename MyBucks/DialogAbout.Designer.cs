namespace MyBucks
{
  partial class DialogAbout
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DialogAbout));
      this.tabControl1 = new System.Windows.Forms.TabControl();
      this.tabWeb = new System.Windows.Forms.TabPage();
      this.webBrowser1 = new System.Windows.Forms.WebBrowser();
      this.tabLegal = new System.Windows.Forms.TabPage();
      this.textBox1 = new System.Windows.Forms.TextBox();
      this.tabControl1.SuspendLayout();
      this.tabWeb.SuspendLayout();
      this.tabLegal.SuspendLayout();
      this.SuspendLayout();
      // 
      // tabControl1
      // 
      this.tabControl1.Controls.Add(this.tabWeb);
      this.tabControl1.Controls.Add(this.tabLegal);
      this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tabControl1.Location = new System.Drawing.Point(0, 0);
      this.tabControl1.Name = "tabControl1";
      this.tabControl1.SelectedIndex = 0;
      this.tabControl1.Size = new System.Drawing.Size(703, 342);
      this.tabControl1.TabIndex = 3;
      // 
      // tabWeb
      // 
      this.tabWeb.Controls.Add(this.webBrowser1);
      this.tabWeb.Location = new System.Drawing.Point(4, 24);
      this.tabWeb.Name = "tabWeb";
      this.tabWeb.Padding = new System.Windows.Forms.Padding(3);
      this.tabWeb.Size = new System.Drawing.Size(695, 314);
      this.tabWeb.TabIndex = 1;
      this.tabWeb.Text = "About MyBucks";
      this.tabWeb.UseVisualStyleBackColor = true;
      // 
      // webBrowser1
      // 
      this.webBrowser1.AllowWebBrowserDrop = false;
      this.webBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.webBrowser1.IsWebBrowserContextMenuEnabled = false;
      this.webBrowser1.Location = new System.Drawing.Point(3, 3);
      this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
      this.webBrowser1.Name = "webBrowser1";
      this.webBrowser1.ScriptErrorsSuppressed = true;
      this.webBrowser1.Size = new System.Drawing.Size(689, 308);
      this.webBrowser1.TabIndex = 0;
      this.webBrowser1.Url = new System.Uri("https://mdsp-mybucks.googlecode.com/", System.UriKind.Absolute);
      this.webBrowser1.WebBrowserShortcutsEnabled = false;
      // 
      // tabLegal
      // 
      this.tabLegal.Controls.Add(this.textBox1);
      this.tabLegal.Location = new System.Drawing.Point(4, 24);
      this.tabLegal.Name = "tabLegal";
      this.tabLegal.Size = new System.Drawing.Size(695, 314);
      this.tabLegal.TabIndex = 2;
      this.tabLegal.Text = "Legal Information";
      this.tabLegal.UseVisualStyleBackColor = true;
      // 
      // textBox1
      // 
      this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.textBox1.Location = new System.Drawing.Point(8, 9);
      this.textBox1.Multiline = true;
      this.textBox1.Name = "textBox1";
      this.textBox1.Size = new System.Drawing.Size(679, 297);
      this.textBox1.TabIndex = 0;
      this.textBox1.Text = resources.GetString("textBox1.Text");
      // 
      // DialogAbout
      // 
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
      this.ClientSize = new System.Drawing.Size(703, 342);
      this.Controls.Add(this.tabControl1);
      this.Font = new System.Drawing.Font("Source Sans Pro", 9F);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "DialogAbout";
      this.Opacity = 0.75D;
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "About";
      this.tabControl1.ResumeLayout(false);
      this.tabWeb.ResumeLayout(false);
      this.tabLegal.ResumeLayout(false);
      this.tabLegal.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.TabControl tabControl1;
    private System.Windows.Forms.TabPage tabWeb;
    private System.Windows.Forms.WebBrowser webBrowser1;
    private System.Windows.Forms.TabPage tabLegal;
    private System.Windows.Forms.TextBox textBox1;
  }
}