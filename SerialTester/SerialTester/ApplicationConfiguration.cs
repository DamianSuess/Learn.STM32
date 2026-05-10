namespace BoardSerialPortTester;

internal static class ApplicationConfiguration
{
  [STAThread]
  internal static void Initialize()
  {
    Application.SetHighDpiMode(HighDpiMode.SystemAware);
    Application.EnableVisualStyles();
    Application.SetCompatibleTextRenderingDefault(false);
  }
}
