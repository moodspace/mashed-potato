namespace MyBucks
{
  partial class PreviewPane
  {
    /// <summary> 
    /// 必需的设计器变量。
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary> 
    /// 清理所有正在使用的资源。
    /// </summary>
    /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region 组件设计器生成的代码

    /// <summary> 
    /// 设计器支持所需的方法 - 不要
    /// 使用代码编辑器修改此方法的内容。
    /// </summary>
    private void InitializeComponent()
    {
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PreviewPane));
      this.X = new System.Windows.Forms.Label();
      this.webPane = new System.Windows.Forms.WebBrowser();
      this.picPane = new System.Windows.Forms.PictureBox();
      this.txtPane = new System.Windows.Forms.RichTextBox();
      this.hScrollBar1 = new System.Windows.Forms.HScrollBar();
      this.label1 = new System.Windows.Forms.Label();
      ((System.ComponentModel.ISupportInitialize)(this.picPane)).BeginInit();
      this.SuspendLayout();
      // 
      // X
      // 
      this.X.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
      this.X.Dock = System.Windows.Forms.DockStyle.Top;
      this.X.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.X.ForeColor = System.Drawing.Color.SteelBlue;
      this.X.Location = new System.Drawing.Point(0, 0);
      this.X.Name = "X";
      this.X.Size = new System.Drawing.Size(200, 18);
      this.X.TabIndex = 3;
      this.X.Text = resources.GetString("X.Text");
      this.X.Click += new System.EventHandler(this.X_Click);
      this.X.MouseDown += new System.Windows.Forms.MouseEventHandler(this.X_MouseDown);
      this.X.MouseUp += new System.Windows.Forms.MouseEventHandler(this.X_MouseUp);
      // 
      // webPane
      // 
      this.webPane.AllowWebBrowserDrop = false;
      this.webPane.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.webPane.Location = new System.Drawing.Point(0, 18);
      this.webPane.MinimumSize = new System.Drawing.Size(20, 20);
      this.webPane.Name = "webPane";
      this.webPane.ScriptErrorsSuppressed = true;
      this.webPane.Size = new System.Drawing.Size(200, 400);
      this.webPane.TabIndex = 6;
      // 
      // picPane
      // 
      this.picPane.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.picPane.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
      this.picPane.Location = new System.Drawing.Point(0, 18);
      this.picPane.Name = "picPane";
      this.picPane.Size = new System.Drawing.Size(200, 400);
      this.picPane.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
      this.picPane.TabIndex = 5;
      this.picPane.TabStop = false;
      // 
      // txtPane
      // 
      this.txtPane.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.txtPane.AutoWordSelection = true;
      this.txtPane.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.txtPane.Location = new System.Drawing.Point(0, 18);
      this.txtPane.Name = "txtPane";
      this.txtPane.Size = new System.Drawing.Size(200, 400);
      this.txtPane.TabIndex = 4;
      this.txtPane.Text = "";
      // 
      // hScrollBar1
      // 
      this.hScrollBar1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.hScrollBar1.LargeChange = 1;
      this.hScrollBar1.Location = new System.Drawing.Point(167, -2);
      this.hScrollBar1.Maximum = 8;
      this.hScrollBar1.Minimum = 1;
      this.hScrollBar1.Name = "hScrollBar1";
      this.hScrollBar1.Size = new System.Drawing.Size(33, 21);
      this.hScrollBar1.TabIndex = 7;
      this.hScrollBar1.Value = 5;
      this.hScrollBar1.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollBar1_Scroll);
      this.hScrollBar1.ValueChanged += new System.EventHandler(this.hScrollBar1_ValueChanged);
      // 
      // label1
      // 
      this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.label1.Location = new System.Drawing.Point(0, 18);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(200, 399);
      this.label1.TabIndex = 8;
      this.label1.Text = "No Preview";
      this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // PreviewPane
      // 
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
      this.BackColor = System.Drawing.Color.Transparent;
      this.BackgroundImage = global::MyBucks.Properties.Resources.navbg_dark;
      this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
      this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.Controls.Add(this.txtPane);
      this.Controls.Add(this.webPane);
      this.Controls.Add(this.picPane);
      this.Controls.Add(this.hScrollBar1);
      this.Controls.Add(this.X);
      this.Controls.Add(this.label1);
      this.DoubleBuffered = true;
      this.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.MaximumSize = new System.Drawing.Size(450, 4000);
      this.MinimumSize = new System.Drawing.Size(10, 0);
      this.Name = "PreviewPane";
      this.Size = new System.Drawing.Size(200, 417);
      ((System.ComponentModel.ISupportInitialize)(this.picPane)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Label X;
    private System.Windows.Forms.WebBrowser webPane;
    private System.Windows.Forms.PictureBox picPane;
    private System.Windows.Forms.RichTextBox txtPane;
    private System.Windows.Forms.HScrollBar hScrollBar1;
    private System.Windows.Forms.Label label1;
  }
}
