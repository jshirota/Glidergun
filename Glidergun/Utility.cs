using System.ComponentModel;
using System.Security.Cryptography;
using System.Text.Json;

namespace Glidergun;

internal static class Utility
{
    public static void RegisterWithDotNetInteractive(this SpatialAnalyst _, Action<Type, Action<object, TextWriter>, string>? registerDotNetInteractiveFormatter)
    {
        if (registerDotNetInteractiveFormatter is not null)
        {
            static void Write(TextWriter writer, object obj)
            {
                var rasters = obj switch
                {
                    Raster r => new[] { r },
                    IEnumerable<Raster> r => r.ToArray(),
                    (Raster r1, Raster r2) => new[] { r1, r2 },
                    (Raster r1, Raster r2, Raster r3) => new[] { r1, r2, r3 },
                    (Raster r1, Raster r2, Raster r3, Raster r4) => new[] { r1, r2, r3, r4 },
                    (Raster r1, Raster r2, Raster r3, Raster r4, Raster r5) => new[] { r1, r2, r3, r4, r5 },
                    (Raster r1, Raster r2, Raster r3, Raster r4, Raster r5, Raster r6) => new[] { r1, r2, r3, r4, r5, r6 },
                    (Raster r1, Raster r2, Raster r3, Raster r4, Raster r5, Raster r6, Raster r7) => new[] { r1, r2, r3, r4, r5, r6, r7 },
                    (Raster r1, Raster r2, Raster r3, Raster r4, Raster r5, Raster r6, Raster r7, Raster r8) => new[] { r1, r2, r3, r4, r5, r6, r7, r8 },
                    _ => Array.Empty<Raster>()
                };

                writer.Write($"<table><tr>{string.Join("", rasters.Select(r => $"<td align=left><p>{r}</p>{r.Thumbnail()}</td>"))}</tr></table>");
            }

            registerDotNetInteractiveFormatter(typeof(Raster), (o, w) => Write(w, o), "text/html");
            registerDotNetInteractiveFormatter(typeof(IEnumerable<Raster>), (o, w) => Write(w, o), "text/html");
            registerDotNetInteractiveFormatter(typeof((Raster r1, Raster r2)), (o, w) => Write(w, o), "text/html");
            registerDotNetInteractiveFormatter(typeof((Raster r1, Raster r2, Raster r3)), (o, w) => Write(w, o), "text/html");
            registerDotNetInteractiveFormatter(typeof((Raster r1, Raster r2, Raster r3, Raster r4)), (o, w) => Write(w, o), "text/html");
            registerDotNetInteractiveFormatter(typeof((Raster r1, Raster r2, Raster r3, Raster r4, Raster r5)), (o, w) => Write(w, o), "text/html");
            registerDotNetInteractiveFormatter(typeof((Raster r1, Raster r2, Raster r3, Raster r4, Raster r5, Raster r6)), (o, w) => Write(w, o), "text/html");
            registerDotNetInteractiveFormatter(typeof((Raster r1, Raster r2, Raster r3, Raster r4, Raster r5, Raster r6, Raster r7)), (o, w) => Write(w, o), "text/html");
            registerDotNetInteractiveFormatter(typeof((Raster r1, Raster r2, Raster r3, Raster r4, Raster r5, Raster r6, Raster r7, Raster r8)), (o, w) => Write(w, o), "text/html");
        }
    }

    public static string ToJson<T>(this T obj)
        => JsonSerializer.Serialize(obj);

    public static T FromJson<T>(this string json)
        => JsonSerializer.Deserialize<T>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;

    public static T FindEnum<T>(string name) where T : Enum
        => Enum.GetValues(typeof(T)).Cast<T>().Single(x => x.ToString().Equals(name, StringComparison.InvariantCultureIgnoreCase));

    public static string GetDescription<T>(this T @enum) where T : Enum
        => $"{typeof(T).GetMember(@enum.ToString()).Single().GetCustomAttributes(false).OfType<DescriptionAttribute>().Single().Description}";

    public static string CreateMD5(this Stream stream)
    {
        using var md5 = MD5.Create();
        var hash = md5.ComputeHash(stream);
        var base64String = Convert.ToBase64String(hash);
        return base64String;
    }
}
