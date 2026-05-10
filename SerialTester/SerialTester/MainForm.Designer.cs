namespace BoardSerialPortTester;

partial class MainForm
{
    private System.ComponentModel.IContainer? components = null;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

  #region Windows Form Designer generated code

  private void InitializeComponent()
  {
    CmbPorts = new ComboBox();
    BtnRefreshPorts = new Button();
    CmbBaud = new ComboBox();
    BtnConnect = new Button();
    BtnDisconnect = new Button();
    TxtSend = new TextBox();
    BtnSendText = new Button();
    GrpSendMode = new GroupBox();
    RbBytes = new RadioButton();
    RbString = new RadioButton();
    LblPorts = new Label();
    LblBaud = new Label();
    LblSend = new Label();
    LblReceive = new Label();
    TxtReceive = new TextBox();
    LblStatusCaption = new Label();
    LblStatus = new Label();
    GrpReceiveOutput = new GroupBox();
    RbRecvHexOnly = new RadioButton();
    RbRecvAsciiOnly = new RadioButton();
    RbRecvAsciiHex = new RadioButton();
    BtnClear = new Button();
    GrpSendMode.SuspendLayout();
    GrpReceiveOutput.SuspendLayout();
    SuspendLayout();
    // 
    // CmbPorts
    // 
    CmbPorts.DropDownStyle = ComboBoxStyle.DropDownList;
    CmbPorts.FormattingEnabled = true;
    CmbPorts.Location = new Point(92, 51);
    CmbPorts.Name = "CmbPorts";
    CmbPorts.Size = new Size(162, 23);
    CmbPorts.TabIndex = 0;
    CmbPorts.SelectedIndexChanged += CmbPorts_SelectedIndexChanged;
    // 
    // BtnRefreshPorts
    // 
    BtnRefreshPorts.Location = new Point(409, 12);
    BtnRefreshPorts.Name = "BtnRefreshPorts";
    BtnRefreshPorts.Size = new Size(110, 25);
    BtnRefreshPorts.TabIndex = 1;
    BtnRefreshPorts.Text = "Refresh Ports";
    BtnRefreshPorts.UseVisualStyleBackColor = true;
    BtnRefreshPorts.Click += BtnRefreshPorts_Click;
    // 
    // CmbBaud
    // 
    CmbBaud.DropDownStyle = ComboBoxStyle.DropDownList;
    CmbBaud.FormattingEnabled = true;
    CmbBaud.Location = new Point(346, 51);
    CmbBaud.Name = "CmbBaud";
    CmbBaud.Size = new Size(162, 23);
    CmbBaud.TabIndex = 2;
    // 
    // BtnConnect
    // 
    BtnConnect.Location = new Point(177, 12);
    BtnConnect.Name = "BtnConnect";
    BtnConnect.Size = new Size(110, 25);
    BtnConnect.TabIndex = 3;
    BtnConnect.Text = "Connect";
    BtnConnect.UseVisualStyleBackColor = true;
    BtnConnect.Click += BtnConnect_Click;
    // 
    // BtnDisconnect
    // 
    BtnDisconnect.Location = new Point(293, 12);
    BtnDisconnect.Name = "BtnDisconnect";
    BtnDisconnect.Size = new Size(110, 25);
    BtnDisconnect.TabIndex = 4;
    BtnDisconnect.Text = "Disconnect";
    BtnDisconnect.UseVisualStyleBackColor = true;
    BtnDisconnect.Click += BtnDisconnect_Click;
    // 
    // TxtSend
    // 
    TxtSend.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    TxtSend.Location = new Point(92, 91);
    TxtSend.Name = "TxtSend";
    TxtSend.Size = new Size(516, 23);
    TxtSend.TabIndex = 6;
    // 
    // BtnSendText
    // 
    BtnSendText.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    BtnSendText.Location = new Point(614, 88);
    BtnSendText.Name = "BtnSendText";
    BtnSendText.Size = new Size(79, 27);
    BtnSendText.TabIndex = 8;
    BtnSendText.Text = "Send";
    BtnSendText.UseVisualStyleBackColor = true;
    BtnSendText.Click += BtnSendText_Click;
    // 
    // GrpSendMode
    // 
    GrpSendMode.Controls.Add(RbBytes);
    GrpSendMode.Controls.Add(RbString);
    GrpSendMode.Location = new Point(92, 120);
    GrpSendMode.Name = "GrpSendMode";
    GrpSendMode.Size = new Size(162, 54);
    GrpSendMode.TabIndex = 7;
    GrpSendMode.TabStop = false;
    GrpSendMode.Text = "Send Mode";
    // 
    // RbBytes
    // 
    RbBytes.AutoSize = true;
    RbBytes.Location = new Point(80, 22);
    RbBytes.Name = "RbBytes";
    RbBytes.Size = new Size(56, 19);
    RbBytes.TabIndex = 1;
    RbBytes.Text = "Byte[]";
    RbBytes.UseVisualStyleBackColor = true;
    // 
    // RbString
    // 
    RbString.AutoSize = true;
    RbString.Checked = true;
    RbString.Location = new Point(18, 22);
    RbString.Name = "RbString";
    RbString.Size = new Size(56, 19);
    RbString.TabIndex = 0;
    RbString.TabStop = true;
    RbString.Text = "String";
    RbString.UseVisualStyleBackColor = true;
    // 
    // LblPorts
    // 
    LblPorts.AutoSize = true;
    LblPorts.Location = new Point(16, 54);
    LblPorts.Name = "LblPorts";
    LblPorts.Size = new Size(63, 15);
    LblPorts.TabIndex = 9;
    LblPorts.Text = "COM Port:";
    // 
    // LblBaud
    // 
    LblBaud.AutoSize = true;
    LblBaud.Location = new Point(270, 54);
    LblBaud.Name = "LblBaud";
    LblBaud.Size = new Size(63, 15);
    LblBaud.TabIndex = 10;
    LblBaud.Text = "Baud Rate:";
    // 
    // LblSend
    // 
    LblSend.AutoSize = true;
    LblSend.Location = new Point(16, 94);
    LblSend.Name = "LblSend";
    LblSend.Size = new Size(60, 15);
    LblSend.TabIndex = 11;
    LblSend.Text = "Send Text:";
    // 
    // LblReceive
    // 
    LblReceive.AutoSize = true;
    LblReceive.Location = new Point(16, 189);
    LblReceive.Name = "LblReceive";
    LblReceive.Size = new Size(50, 15);
    LblReceive.TabIndex = 12;
    LblReceive.Text = "Receive:";
    // 
    // TxtReceive
    // 
    TxtReceive.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    TxtReceive.Font = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
    TxtReceive.Location = new Point(92, 186);
    TxtReceive.Multiline = true;
    TxtReceive.Name = "TxtReceive";
    TxtReceive.ReadOnly = true;
    TxtReceive.ScrollBars = ScrollBars.Vertical;
    TxtReceive.Size = new Size(601, 231);
    TxtReceive.TabIndex = 13;
    // 
    // LblStatusCaption
    // 
    LblStatusCaption.AutoSize = true;
    LblStatusCaption.Location = new Point(16, 15);
    LblStatusCaption.Name = "LblStatusCaption";
    LblStatusCaption.Size = new Size(42, 15);
    LblStatusCaption.TabIndex = 14;
    LblStatusCaption.Text = "Status:";
    // 
    // LblStatus
    // 
    LblStatus.AutoSize = true;
    LblStatus.Location = new Point(92, 15);
    LblStatus.Name = "LblStatus";
    LblStatus.Size = new Size(79, 15);
    LblStatus.TabIndex = 15;
    LblStatus.Text = "Disconnected";
    // 
    // GrpReceiveOutput
    // 
    GrpReceiveOutput.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    GrpReceiveOutput.Controls.Add(RbRecvHexOnly);
    GrpReceiveOutput.Controls.Add(RbRecvAsciiOnly);
    GrpReceiveOutput.Controls.Add(RbRecvAsciiHex);
    GrpReceiveOutput.Location = new Point(270, 120);
    GrpReceiveOutput.Name = "GrpReceiveOutput";
    GrpReceiveOutput.Size = new Size(423, 54);
    GrpReceiveOutput.TabIndex = 16;
    GrpReceiveOutput.TabStop = false;
    GrpReceiveOutput.Text = "Receive Output";
    // 
    // RbRecvHexOnly
    // 
    RbRecvHexOnly.AutoSize = true;
    RbRecvHexOnly.Location = new Point(179, 25);
    RbRecvHexOnly.Name = "RbRecvHexOnly";
    RbRecvHexOnly.Size = new Size(73, 19);
    RbRecvHexOnly.TabIndex = 2;
    RbRecvHexOnly.Text = "Hex Only";
    RbRecvHexOnly.UseVisualStyleBackColor = true;
    // 
    // RbRecvAsciiOnly
    // 
    RbRecvAsciiOnly.AutoSize = true;
    RbRecvAsciiOnly.Location = new Point(95, 25);
    RbRecvAsciiOnly.Name = "RbRecvAsciiOnly";
    RbRecvAsciiOnly.Size = new Size(78, 19);
    RbRecvAsciiOnly.TabIndex = 1;
    RbRecvAsciiOnly.Text = "Ascii Only";
    RbRecvAsciiOnly.UseVisualStyleBackColor = true;
    // 
    // RbRecvAsciiHex
    // 
    RbRecvAsciiHex.AutoSize = true;
    RbRecvAsciiHex.Checked = true;
    RbRecvAsciiHex.Location = new Point(16, 25);
    RbRecvAsciiHex.Name = "RbRecvAsciiHex";
    RbRecvAsciiHex.Size = new Size(73, 19);
    RbRecvAsciiHex.TabIndex = 0;
    RbRecvAsciiHex.TabStop = true;
    RbRecvAsciiHex.Text = "Ascii Hex";
    RbRecvAsciiHex.UseVisualStyleBackColor = true;
    // 
    // BtnClear
    // 
    BtnClear.Location = new Point(16, 207);
    BtnClear.Name = "BtnClear";
    BtnClear.Size = new Size(63, 23);
    BtnClear.TabIndex = 17;
    BtnClear.Text = "Clear";
    BtnClear.UseVisualStyleBackColor = true;
    BtnClear.Click += BtnClear_Click;
    // 
    // MainForm
    // 
    AutoScaleDimensions = new SizeF(7F, 15F);
    AutoScaleMode = AutoScaleMode.Font;
    ClientSize = new Size(713, 441);
    Controls.Add(BtnClear);
    Controls.Add(GrpReceiveOutput);
    Controls.Add(LblStatus);
    Controls.Add(LblStatusCaption);
    Controls.Add(TxtReceive);
    Controls.Add(LblReceive);
    Controls.Add(LblSend);
    Controls.Add(LblBaud);
    Controls.Add(LblPorts);
    Controls.Add(GrpSendMode);
    Controls.Add(BtnSendText);
    Controls.Add(TxtSend);
    Controls.Add(BtnDisconnect);
    Controls.Add(BtnConnect);
    Controls.Add(CmbBaud);
    Controls.Add(BtnRefreshPorts);
    Controls.Add(CmbPorts);
    MaximizeBox = false;
    Name = "MainForm";
    StartPosition = FormStartPosition.CenterScreen;
    Text = "Board Serial Port Tester";
    GrpSendMode.ResumeLayout(false);
    GrpSendMode.PerformLayout();
    GrpReceiveOutput.ResumeLayout(false);
    GrpReceiveOutput.PerformLayout();
    ResumeLayout(false);
    PerformLayout();

  }

  #endregion

  private System.Windows.Forms.ComboBox CmbPorts;
    private System.Windows.Forms.Button BtnRefreshPorts;
    private System.Windows.Forms.ComboBox CmbBaud;
    private System.Windows.Forms.Button BtnConnect;
    private System.Windows.Forms.Button BtnDisconnect;
    private System.Windows.Forms.TextBox TxtSend;
    private System.Windows.Forms.Button BtnSendText;
    private System.Windows.Forms.GroupBox GrpSendMode;
    private System.Windows.Forms.RadioButton RbBytes;
    private System.Windows.Forms.RadioButton RbString;
    private System.Windows.Forms.Label LblPorts;
    private System.Windows.Forms.Label LblBaud;
    private System.Windows.Forms.Label LblSend;
    private System.Windows.Forms.Label LblReceive;
    private System.Windows.Forms.TextBox TxtReceive;
    private System.Windows.Forms.Label LblStatusCaption;
    private System.Windows.Forms.Label LblStatus;
  private GroupBox GrpReceiveOutput;
  private RadioButton RbRecvAsciiOnly;
  private RadioButton RbRecvAsciiHex;
  private RadioButton RbRecvHexOnly;
  private Button BtnClear;
}
