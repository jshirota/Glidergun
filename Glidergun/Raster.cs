using System.Text.Json.Serialization;

namespace Glidergun;

public sealed class Raster : RasterInfo
{
    [JsonIgnore]
    public SpatialAnalyst SpatialAnalyst { get; }

    public Raster(SpatialAnalyst spatialAnalyst, RasterInfoResponse raster)
    {
        SpatialAnalyst = spatialAnalyst;

        var r = raster.ToJson().FromJson<RasterInfo>();

        foreach (var p in typeof(RasterInfo).GetProperties())
            p.SetValue(this, p.GetValue(r));

        Extent = new(raster.Xmin, raster.Ymin, raster.Xmax, raster.Ymax);
    }

    public double[] GetXCoordinates()
    {
        var xmin = Extent.Xmin + MeanCellWidth / 2;
        return Enumerable.Range(0, Width).Select(n => xmin + MeanCellWidth * n).ToArray();
    }

    public double[] GetYCoordinates()
    {
        var ymin = Extent.Ymin + MeanCellHeight / 2;
        return Enumerable.Range(0, Height).Select(n => ymin + MeanCellHeight * n).ToArray();
    }

    #region Operators

    public static Raster operator +(Raster raster1, Raster raster2) => raster1.SpatialAnalyst.PlusAsync(raster1, raster2).Result;
    public static Raster operator +(Raster raster, int n) => raster.SpatialAnalyst.PlusAsync(raster, n).Result;
    public static Raster operator +(Raster raster, double n) => raster.SpatialAnalyst.PlusAsync(raster, n).Result;
    public static Raster operator +(int n, Raster raster) => raster.SpatialAnalyst.PlusAsync(n, raster).Result;
    public static Raster operator +(double n, Raster raster) => raster.SpatialAnalyst.PlusAsync(n, raster).Result;
    public static Raster operator +(Raster raster) => raster.SpatialAnalyst.PlusAsync(0, raster).Result;
    public static Raster operator -(Raster raster1, Raster raster2) => raster1.SpatialAnalyst.MinusAsync(raster1, raster2).Result;
    public static Raster operator -(Raster raster, int n) => raster.SpatialAnalyst.MinusAsync(raster, n).Result;
    public static Raster operator -(Raster raster, double n) => raster.SpatialAnalyst.MinusAsync(raster, n).Result;
    public static Raster operator -(int n, Raster raster) => raster.SpatialAnalyst.MinusAsync(n, raster).Result;
    public static Raster operator -(double n, Raster raster) => raster.SpatialAnalyst.MinusAsync(n, raster).Result;
    public static Raster operator -(Raster raster) => raster.SpatialAnalyst.MinusAsync(0, raster).Result;
    public static Raster operator *(Raster raster1, Raster raster2) => raster1.SpatialAnalyst.TimesAsync(raster1, raster2).Result;
    public static Raster operator *(Raster raster, int n) => raster.SpatialAnalyst.TimesAsync(raster, n).Result;
    public static Raster operator *(Raster raster, double n) => raster.SpatialAnalyst.TimesAsync(raster, n).Result;
    public static Raster operator *(int n, Raster raster) => raster.SpatialAnalyst.TimesAsync(n, raster).Result;
    public static Raster operator *(double n, Raster raster) => raster.SpatialAnalyst.TimesAsync(n, raster).Result;
    public static Raster operator /(Raster raster1, Raster raster2) => raster1.SpatialAnalyst.DivideAsync(raster1, raster2).Result;
    public static Raster operator /(Raster raster, int n) => raster.SpatialAnalyst.DivideAsync(raster, n).Result;
    public static Raster operator /(Raster raster, double n) => raster.SpatialAnalyst.DivideAsync(raster, n).Result;
    public static Raster operator /(int n, Raster raster) => raster.SpatialAnalyst.DivideAsync(n, raster).Result;
    public static Raster operator /(double n, Raster raster) => raster.SpatialAnalyst.DivideAsync(n, raster).Result;
    public static Raster operator %(Raster raster1, Raster raster2) => raster1.SpatialAnalyst.ModAsync(raster1, raster2).Result;
    public static Raster operator %(Raster raster, int n) => raster.SpatialAnalyst.ModAsync(raster, n).Result;
    public static Raster operator %(Raster raster, double n) => raster.SpatialAnalyst.ModAsync(raster, n).Result;
    public static Raster operator %(int n, Raster raster) => raster.SpatialAnalyst.ModAsync(n, raster).Result;
    public static Raster operator %(double n, Raster raster) => raster.SpatialAnalyst.ModAsync(n, raster).Result;
    public static Raster operator ==(Raster raster1, Raster raster2) => raster1.SpatialAnalyst.EqualToAsync(raster1, raster2).Result;
    public static Raster operator ==(Raster raster, int n) => raster.SpatialAnalyst.EqualToAsync(raster, n).Result;
    public static Raster operator ==(Raster raster, double n) => raster.SpatialAnalyst.EqualToAsync(raster, n).Result;
    public static Raster operator ==(int n, Raster raster) => raster.SpatialAnalyst.EqualToAsync(n, raster).Result;
    public static Raster operator ==(double n, Raster raster) => raster.SpatialAnalyst.EqualToAsync(n, raster).Result;
    public static Raster operator !=(Raster raster1, Raster raster2) => raster1.SpatialAnalyst.NotEqualAsync(raster1, raster2).Result;
    public static Raster operator !=(Raster raster, int n) => raster.SpatialAnalyst.NotEqualAsync(raster, n).Result;
    public static Raster operator !=(Raster raster, double n) => raster.SpatialAnalyst.NotEqualAsync(raster, n).Result;
    public static Raster operator !=(int n, Raster raster) => raster.SpatialAnalyst.NotEqualAsync(n, raster).Result;
    public static Raster operator !=(double n, Raster raster) => raster.SpatialAnalyst.NotEqualAsync(n, raster).Result;
    public static Raster operator >(Raster raster1, Raster raster2) => raster1.SpatialAnalyst.GreaterThanAsync(raster1, raster2).Result;
    public static Raster operator >(Raster raster, int n) => raster.SpatialAnalyst.GreaterThanAsync(raster, n).Result;
    public static Raster operator >(Raster raster, double n) => raster.SpatialAnalyst.GreaterThanAsync(raster, n).Result;
    public static Raster operator >(int n, Raster raster) => raster.SpatialAnalyst.GreaterThanAsync(n, raster).Result;
    public static Raster operator >(double n, Raster raster) => raster.SpatialAnalyst.GreaterThanAsync(n, raster).Result;
    public static Raster operator <(Raster raster1, Raster raster2) => raster1.SpatialAnalyst.LessThanAsync(raster1, raster2).Result;
    public static Raster operator <(Raster raster, int n) => raster.SpatialAnalyst.LessThanAsync(raster, n).Result;
    public static Raster operator <(Raster raster, double n) => raster.SpatialAnalyst.LessThanAsync(raster, n).Result;
    public static Raster operator <(int n, Raster raster) => raster.SpatialAnalyst.LessThanAsync(n, raster).Result;
    public static Raster operator <(double n, Raster raster) => raster.SpatialAnalyst.LessThanAsync(n, raster).Result;
    public static Raster operator >=(Raster raster1, Raster raster2) => raster1.SpatialAnalyst.GreaterThanEqualAsync(raster1, raster2).Result;
    public static Raster operator >=(Raster raster, int n) => raster.SpatialAnalyst.GreaterThanEqualAsync(raster, n).Result;
    public static Raster operator >=(Raster raster, double n) => raster.SpatialAnalyst.GreaterThanEqualAsync(raster, n).Result;
    public static Raster operator >=(int n, Raster raster) => raster.SpatialAnalyst.GreaterThanEqualAsync(n, raster).Result;
    public static Raster operator >=(double n, Raster raster) => raster.SpatialAnalyst.GreaterThanEqualAsync(n, raster).Result;
    public static Raster operator <=(Raster raster1, Raster raster2) => raster1.SpatialAnalyst.LessThanEqualAsync(raster1, raster2).Result;
    public static Raster operator <=(Raster raster, int n) => raster.SpatialAnalyst.LessThanEqualAsync(raster, n).Result;
    public static Raster operator <=(Raster raster, double n) => raster.SpatialAnalyst.LessThanEqualAsync(raster, n).Result;
    public static Raster operator <=(int n, Raster raster) => raster.SpatialAnalyst.LessThanEqualAsync(n, raster).Result;
    public static Raster operator <=(double n, Raster raster) => raster.SpatialAnalyst.LessThanEqualAsync(n, raster).Result;
    public static Raster operator &(Raster raster1, Raster raster2) => raster1.SpatialAnalyst.BooleanAndAsync(raster1, raster2).Result;
    public static Raster operator &(Raster raster, int n) => raster.SpatialAnalyst.BooleanAndAsync(raster, n).Result;
    public static Raster operator &(Raster raster, double n) => raster.SpatialAnalyst.BooleanAndAsync(raster, n).Result;
    public static Raster operator &(int n, Raster raster) => raster.SpatialAnalyst.BooleanAndAsync(n, raster).Result;
    public static Raster operator &(double n, Raster raster) => raster.SpatialAnalyst.BooleanAndAsync(n, raster).Result;
    public static Raster operator |(Raster raster1, Raster raster2) => raster1.SpatialAnalyst.BooleanOrAsync(raster1, raster2).Result;
    public static Raster operator |(Raster raster, int n) => raster.SpatialAnalyst.BooleanOrAsync(raster, n).Result;
    public static Raster operator |(Raster raster, double n) => raster.SpatialAnalyst.BooleanOrAsync(raster, n).Result;
    public static Raster operator |(int n, Raster raster) => raster.SpatialAnalyst.BooleanOrAsync(n, raster).Result;
    public static Raster operator |(double n, Raster raster) => raster.SpatialAnalyst.BooleanOrAsync(n, raster).Result;
    public static Raster operator !(Raster raster) => raster.SpatialAnalyst.BooleanNotAsync(raster).Result;

    #endregion

    #region Overrides

    public override bool Equals(object? obj) => base.Equals(obj);
    public override int GetHashCode() => base.GetHashCode();

    #endregion
}

public class RasterInfo
{
    public string Id { get; init; } = default!;
    public int BandCount { get; init; }
    public int Width { get; init; }
    public int Height { get; init; }
    public string PixelType { get; init; } = default!;
    public bool IsInteger { get; init; }
    public double? NoDataValue { get; init; }
    public double? Minimum { get; init; }
    public double? Maximum { get; init; }
    public double? Mean { get; init; }
    public double? StandardDeviation { get; init; }
    public double MeanCellWidth { get; init; }
    public double MeanCellHeight { get; init; }
    public int? Wkid { get; init; }
    public string? Wkt { get; init; }
    public Extent Extent { get; init; } = default!;

    public override string ToString()
    {
        var (min, max) = IsInteger ? ($"{Minimum}", $"{Maximum}") : ($"{Minimum:F6}", $"{Maximum:F6}");

        return $@"{new
        {
            Range = $"{min} ~ {max}",
            Mean = $"{Mean:F6}",
            Size = $"{Width} x {Height}",
            PixelType,
            CellSize = $"{MeanCellWidth:F6}",
            Wkid
        }}";
    }
}

public class Extent
{
    public double Xmin { get; init; }
    public double Ymin { get; init; }
    public double Xmax { get; init; }
    public double Ymax { get; init; }

    public Extent(double xmin, double ymin, double xmax, double ymax)
        => (Xmin, Ymin, Xmax, Ymax) = (xmin, ymin, xmax, ymax);

    public void Deconstruct(out double xmin, out double ymin, out double xmax, out double ymax)
        => (xmin, ymin, xmax, ymax) = (Xmin, Ymin, Xmax, Ymax);

    public override string ToString()
        => $"{Xmin} {Ymin} {Xmax} {Ymax}";
}
