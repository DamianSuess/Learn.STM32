# BoardSerialPortTester

WinForms (.NET 8) serial-port tester that:

- Lists COM ports in a ComboBox and auto-selects the first one.
- **Refresh Ports** button to re-scan COM ports.
- **Baud rate** selection.
- Connect/Disconnect buttons.
- Send Text as **String** or **Byte[]**.
- Receive window (append incoming data via `SerialPort.DataReceived`).
- Auto-saves the last selected COM port to `settings.ini` and loads it on startup.

## Build & Run

Open `BoardSerialPortTester.sln` in Visual Studio 2022+ and run.

Or via CLI (Windows):

```powershell
cd BoardSerialPortTester\BoardSerialPortTester
dotnet build

# run

dotnet run
```

### INI file
- Created in the app folder: `settings.ini`
- Key used: `[Settings] LastPort=COMx`
