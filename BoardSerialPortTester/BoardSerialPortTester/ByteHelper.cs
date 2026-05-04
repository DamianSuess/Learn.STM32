using System.Text;

namespace BoardSerialPortTester;

public static class ByteHelper
{
  /// <summary>Formats non-printable characters as '.'.</summary>
  /// <param name="b">Byte.</param>
  /// <returns>String format.</returns>
  public static string FormatAsciiPrintable(byte b)
  {
    // Printable ASCII range 0x20..0x7E
    return (b >= 0x20 && b <= 0x7E)
      ? ((char)b).ToString()
      : ".";
  }

  /// <summary>Now timestamp (hh=12hr, HH=24hr).</summary>
  /// <returns>Current timestamp.</returns>
  public static string NowStamp() => DateTime.Now.ToString("HH:mm:ss.fff");

  public static string ToAsciiOnlyWithTimestamp(string text)
  {
    // You can decide whether to add newline; here we do.
    return $"{NowStamp()} {text}{Environment.NewLine}";
  }

  public static string ToHex(byte[] data, int length)
  {
    var sb = new StringBuilder(length * 3);
    for (int i = 0; i < length; i++)
    {
      sb.Append(data[i].ToString("X2"));
      sb.Append(' ');
    }

    return sb.ToString();
  }

  public static string ToHexAndAsciiSideBySide(byte[] data, int length, int bytesPerLine = 16)
  {
    var sb = new StringBuilder();

    for (int offset = 0; offset < length; offset += bytesPerLine)
    {
      int lineLen = Math.Min(bytesPerLine, length - offset);
      string ts = NowStamp();

      sb.Append(ts).Append(' ').Append("| ");

      // Hex column
      for (int i = 0; i < bytesPerLine; i++)
      {
        if (i < lineLen)
          sb.Append(data[offset + i].ToString("X2")).Append(' ');
        else
          sb.Append("   ");
      }

      sb.Append("| ");

      // ASCII column
      for (int i = 0; i < lineLen; i++)
        sb.Append(FormatAsciiPrintable(data[offset + i]));

      sb.AppendLine();
    }

    return sb.ToString();
  }

  public static string ToHexGroup(byte[] data, int length)
  {
    var sb = new StringBuilder();

    for (int i = 0; i < length; i++)
    {
      sb.Append(data[i].ToString("X2")).Append(' ');

      if ((i + 1) % 16 == 0)
        sb.AppendLine();
    }

    sb.AppendLine();
    return sb.ToString();
  }

  public static string ToHexGroupedWithTimestamp(byte[] data, int length, int bytesPerLine = 16)
  {
    var sb = new StringBuilder();

    for (int offset = 0; offset < length; offset += bytesPerLine)
    {
      int lineLen = Math.Min(bytesPerLine, length - offset);
      string ts = NowStamp();

      sb.Append(ts).Append(' ').Append("| ");

      // Hex bytes (grouped)
      for (int i = 0; i < bytesPerLine; i++)
      {
        if (i < lineLen)
          sb.Append(data[offset + i].ToString("X2")).Append(' ');
        else
          sb.Append("   "); // pad for alignment
      }

      sb.Append("|").AppendLine();
    }

    return sb.ToString();
  }
}
