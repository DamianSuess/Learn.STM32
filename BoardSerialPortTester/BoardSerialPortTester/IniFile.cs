namespace BoardSerialPortTester;

internal sealed class IniFile
{
  public IniFile(string path) => Path = path;

  public string Path { get; }

  public string? Read(string section, string key)
  {
    if (!File.Exists(Path))
      return null;

    string currentSection = string.Empty;
    foreach (var rawLine in File.ReadAllLines(Path))
    {
      var line = rawLine.Trim();
      if (line.Length == 0 || line.StartsWith(";") || line.StartsWith("#"))
        continue;

      if (line.StartsWith("[") && line.EndsWith("]"))
      {
        currentSection = line.Substring(1, line.Length - 2).Trim();
        continue;
      }

      if (!string.Equals(currentSection, section, StringComparison.OrdinalIgnoreCase))
        continue;

      var idx = line.IndexOf('=');
      if (idx <= 0)
        continue;

      var k = line.Substring(0, idx).Trim();
      if (!string.Equals(k, key, StringComparison.OrdinalIgnoreCase))
        continue;

      return line.Substring(idx + 1).Trim();
    }

    return null;
  }

  public void Write(string section, string key, string value)
  {
    var data = new Dictionary<string, Dictionary<string, string>>(StringComparer.OrdinalIgnoreCase);

    if (File.Exists(Path))
    {
      string currentSection = string.Empty;
      foreach (var rawLine in File.ReadAllLines(Path))
      {
        var line = rawLine.Trim();
        if (line.Length == 0 || line.StartsWith(";") || line.StartsWith("#"))
          continue;

        if (line.StartsWith("[") && line.EndsWith("]"))
        {
          currentSection = line.Substring(1, line.Length - 2).Trim();
          if (!data.ContainsKey(currentSection))
            data[currentSection] = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
          continue;
        }

        var idx = line.IndexOf('=');
        if (idx <= 0)
          continue;

        var k = line.Substring(0, idx).Trim();
        var v = line.Substring(idx + 1).Trim();
        if (currentSection.Length == 0)
          continue;

        if (!data.ContainsKey(currentSection))
          data[currentSection] = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        data[currentSection][k] = v;
      }
    }

    if (!data.ContainsKey(section))
      data[section] = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

    data[section][key] = value;

    var dir = System.IO.Path.GetDirectoryName(Path);
    if (!string.IsNullOrWhiteSpace(dir))
      Directory.CreateDirectory(dir);

    using var sw = new StreamWriter(Path, false);
    foreach (var sec in data)
    {
      sw.WriteLine($"[{sec.Key}]");
      foreach (var kvp in sec.Value)
        sw.WriteLine($"{kvp.Key}={kvp.Value}");

      sw.WriteLine();
    }
  }
}
