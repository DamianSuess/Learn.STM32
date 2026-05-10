using System.IO.Ports;
using System.Text;

namespace BoardSerialPortTester;

public partial class MainForm : Form
{
  private const int DefaultBaudRate = 115200;
  private const string IniKeyLastBaudRate = "LastBaud";
  private const string IniKeyLastPort = "LastPort";
  private const string IniSection = "Settings";

  private readonly IniFile _ini;

  private SerialPort? _serial;

  public MainForm()
  {
    InitializeComponent();

    var iniPath = System.IO.Path.Combine(AppContext.BaseDirectory, "settings.ini");
    _ini = new IniFile(iniPath);

    PopulateBaudRates();
    UpdateUi(isConnected: false);
  }

  private enum ReceiveOutputMode
  {
    AsciiHexSideBySide,
    AsciiOnly,
    HexOnly
  }

  private ReceiveOutputMode CurrentReceiveMode =>
    RbRecvAsciiOnly.Checked
    ? ReceiveOutputMode.AsciiOnly
    : RbRecvHexOnly.Checked
      ? ReceiveOutputMode.HexOnly
      : ReceiveOutputMode.AsciiHexSideBySide;

  private int SelectedBaudRate =>
    CmbBaud.SelectedItem is int b ? b : DefaultBaudRate;

  protected override void OnFormClosing(FormClosingEventArgs e)
  {
    if (CmbPorts.SelectedItem is string port && !string.IsNullOrWhiteSpace(port))
      _ini.Write(IniSection, IniKeyLastPort, port);

    SafeCloseSerial();
    base.OnFormClosing(e);
  }

  protected override void OnLoad(EventArgs e)
  {
    base.OnLoad(e);

    var lastPort = _ini.Read(IniSection, IniKeyLastPort);
    RefreshPorts(selectPortIfPresent: lastPort);

    // If we didn't find the last port, auto-select first.
    if (CmbPorts.Items.Count > 0 && CmbPorts.SelectedIndex < 0)
      CmbPorts.SelectedIndex = 0;

    var lastBaud = _ini.Read(IniSection, IniKeyLastBaudRate);
    if (int.TryParse(lastBaud, out int baud) && CmbBaud.Items.Contains(baud))
    {
      CmbBaud.SelectedItem = baud;
    }
  }

  private void BtnClear_Click(object sender, EventArgs e)
  {
    TxtReceive.Clear();
  }

  private void BtnConnect_Click(object sender, EventArgs e)
  {
    if (_serial is { IsOpen: true })
    {
      MessageBox.Show(this, "Already connected.", "Serial", MessageBoxButtons.OK, MessageBoxIcon.Information);
      return;
    }

    if (CmbPorts.SelectedItem is not string portName || string.IsNullOrWhiteSpace(portName))
    {
      MessageBox.Show(this, "No COM port selected.", "Serial", MessageBoxButtons.OK, MessageBoxIcon.Warning);
      return;
    }

    try
    {
      _serial = new SerialPort(portName)
      {
        BaudRate = SelectedBaudRate,
        DataBits = 8,
        Parity = Parity.None,
        StopBits = StopBits.One,
        Handshake = Handshake.None,
        ReadTimeout = 2000,
        WriteTimeout = 2000,
        DtrEnable = true,
        RtsEnable = false,
        Encoding = Encoding.ASCII
      };

      _serial.DataReceived += Serial_DataReceived;
      _serial.Open();

      _ini.Write(IniSection, IniKeyLastPort, portName);

      UpdateUi(isConnected: true);
    }
    catch (Exception ex)
    {
      SafeCloseSerial();
      MessageBox.Show(this, $"Failed to connect: {ex.Message}", "Serial", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
  }

  private void BtnDisconnect_Click(object sender, EventArgs e)
  {
    SafeCloseSerial();
    UpdateUi(isConnected: false);
  }

  private void BtnRefreshPorts_Click(object sender, EventArgs e)
  {
    var lastPort = _ini.Read(IniSection, IniKeyLastPort);
    RefreshPorts(selectPortIfPresent: lastPort);
  }

  private void BtnSendText_Click(object sender, EventArgs e)
  {
    if (_serial is null || !_serial.IsOpen)
    {
      MessageBox.Show(this, "Not connected.", "Serial", MessageBoxButtons.OK, MessageBoxIcon.Warning);
      return;
    }

    var text = TxtSend.Text ?? string.Empty;

    try
    {
      if (RbString.Checked)
      {
        _serial.Write(text);
      }
      else
      {
        var bytes = _serial.Encoding.GetBytes(text);
        _serial.Write(bytes, 0, bytes.Length);
      }
    }
    catch (Exception ex)
    {
      MessageBox.Show(this, $"Send failed: {ex.Message}", "Serial", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
  }

  private void CmbPorts_SelectedIndexChanged(object sender, EventArgs e)
  {
    if (CmbPorts.SelectedItem is string port && !string.IsNullOrWhiteSpace(port))
    {
      _ini.Write(IniSection, IniKeyLastPort, port);
    }
  }

  private void PopulateBaudRates()
  {
    var bauds = new[] { 1200, 2400, 4800, 9600, 19200, 38400, 57600, 115200, 230400 };
    CmbBaud.BeginUpdate();
    try
    {
      CmbBaud.Items.Clear();
      foreach (var b in bauds)
        CmbBaud.Items.Add(b);
      CmbBaud.SelectedItem = 115200;
      if (CmbBaud.SelectedIndex < 0)
        CmbBaud.SelectedItem = 9600;
    }
    finally
    {
      CmbBaud.EndUpdate();
    }
  }

  private void RefreshPorts(string? selectPortIfPresent)
  {
    var ports = SerialPort.GetPortNames()
      .OrderBy(p => p, StringComparer.OrdinalIgnoreCase)
      .ToArray();

    var previous = selectPortIfPresent ?? (CmbPorts.SelectedItem as string);

    CmbPorts.BeginUpdate();
    try
    {
      CmbPorts.Items.Clear();
      CmbPorts.Items.AddRange(ports);

      if (!string.IsNullOrWhiteSpace(previous))
      {
        var idx = Array.FindIndex(ports, p => string.Equals(p, previous, StringComparison.OrdinalIgnoreCase));
        if (idx >= 0)
          CmbPorts.SelectedIndex = idx;
        else if (ports.Length > 0)
          CmbPorts.SelectedIndex = 0;
      }
      else if (ports.Length > 0)
      {
        CmbPorts.SelectedIndex = 0;
      }
    }
    finally
    {
      CmbPorts.EndUpdate();
    }
  }

  private void SafeCloseSerial()
  {
    try
    {
      if (_serial is not null)
      {
        _serial.DataReceived -= Serial_DataReceived;

        if (_serial.IsOpen)
          _serial.Close();

        _serial.Dispose();
      }
    }
    catch
    {
      // swallow shutdown exceptions
    }
    finally
    {
      _serial = null;
    }
  }

  private void Serial_DataReceived(object? sender, SerialDataReceivedEventArgs e)
  {
    try
    {
      if (_serial is null)
        return;

      int count = _serial.BytesToRead;
      if (count <= 0)
        return;

      ////// Clear the buffer to avoid stale data (optional, depending on use case
      ////_serial.ReadExisting();
      byte[] buffer = new byte[count];
      int read = _serial.Read(buffer, 0, buffer.Length);
      if (read <= 0)
        return;

      // Capture mode on the UI thread (safe), but format string now or inside invoke—either is fine.
      BeginInvoke(new Action(() =>
      {
        string output;

        switch (CurrentReceiveMode)
        {
          case ReceiveOutputMode.AsciiOnly:
            // Use the port encoding to decode bytes
            var text = _serial.Encoding.GetString(buffer, 0, read);
            output = ByteHelper.ToAsciiOnlyWithTimestamp(text);
            break;

          case ReceiveOutputMode.HexOnly:
            ////output = ByteHelper.ToHex(buffer, read);
            output = ByteHelper.ToHexGroupedWithTimestamp(buffer, read, bytesPerLine: 16);
            break;

          default: // ASCII + Hex side-by-side
            output = ByteHelper.ToHexAndAsciiSideBySide(buffer, read, bytesPerLine: 16);
            break;
        }

        TxtReceive.AppendText(output);
      }));
    }
    catch
    {
      // Ignore receive errors during disconnect/close
    }
  }

  private void UpdateUi(bool isConnected)
  {
    BtnConnect.Enabled = !isConnected;
    BtnDisconnect.Enabled = isConnected;
    BtnSendText.Enabled = isConnected;

    CmbPorts.Enabled = !isConnected;
    CmbBaud.Enabled = !isConnected;
    BtnRefreshPorts.Enabled = !isConnected;

    LblStatus.Text = isConnected ? "Connected" : "Disconnected";
  }

  private void TxtSend_KeyPress(object sender, KeyPressEventArgs e)
  {
    if (e.KeyChar == (char)Keys.Return || e.KeyChar == (char)Keys.Enter)
    {
      e.Handled = true;
      BtnSendText_Click(sender, e);
    }
  }
}
