namespace Glidergun;

public static class RasterExt
{
    #region Math

    public static Raster Abs(this Raster raster) => raster.SpatialAnalyst.AbsAsync(raster).Result;
    public static Raster ACos(this Raster raster) => raster.SpatialAnalyst.ACosAsync(raster).Result;
    public static Raster ACosH(this Raster raster) => raster.SpatialAnalyst.ACosHAsync(raster).Result;
    public static Raster ASin(this Raster raster) => raster.SpatialAnalyst.ASinAsync(raster).Result;
    public static Raster ASinH(this Raster raster) => raster.SpatialAnalyst.ASinHAsync(raster).Result;
    public static Raster ATan(this Raster raster) => raster.SpatialAnalyst.ATanAsync(raster).Result;
    public static Raster ATanH(this Raster raster) => raster.SpatialAnalyst.ATanHAsync(raster).Result;
    public static Raster BitwiseNot(this Raster raster) => raster.SpatialAnalyst.BitwiseNotAsync(raster).Result;
    public static Raster BooleanNot(this Raster raster) => raster.SpatialAnalyst.BooleanNotAsync(raster).Result;
    public static Raster Cos(this Raster raster) => raster.SpatialAnalyst.CosAsync(raster).Result;
    public static Raster CosH(this Raster raster) => raster.SpatialAnalyst.CosHAsync(raster).Result;
    public static Raster Exp(this Raster raster) => raster.SpatialAnalyst.ExpAsync(raster).Result;
    public static Raster Exp10(this Raster raster) => raster.SpatialAnalyst.Exp10Async(raster).Result;
    public static Raster Exp2(this Raster raster) => raster.SpatialAnalyst.Exp2Async(raster).Result;
    public static Raster Float(this Raster raster) => raster.SpatialAnalyst.FloatAsync(raster).Result;
    public static Raster Int(this Raster raster) => raster.SpatialAnalyst.IntAsync(raster).Result;
    public static Raster IsNull(this Raster raster) => raster.SpatialAnalyst.IsNullAsync(raster).Result;
    public static Raster Ln(this Raster raster) => raster.SpatialAnalyst.LnAsync(raster).Result;
    public static Raster Log10(this Raster raster) => raster.SpatialAnalyst.Log10Async(raster).Result;
    public static Raster Log2(this Raster raster) => raster.SpatialAnalyst.Log2Async(raster).Result;
    public static Raster Negate(this Raster raster) => raster.SpatialAnalyst.NegateAsync(raster).Result;
    public static Raster RoundDown(this Raster raster) => raster.SpatialAnalyst.RoundDownAsync(raster).Result;
    public static Raster RoundUp(this Raster raster) => raster.SpatialAnalyst.RoundUpAsync(raster).Result;
    public static Raster Sin(this Raster raster) => raster.SpatialAnalyst.SinAsync(raster).Result;
    public static Raster SinH(this Raster raster) => raster.SpatialAnalyst.SinHAsync(raster).Result;
    public static Raster Square(this Raster raster) => raster.SpatialAnalyst.SquareAsync(raster).Result;
    public static Raster SquareRoot(this Raster raster) => raster.SpatialAnalyst.SquareRootAsync(raster).Result;
    public static Raster Tan(this Raster raster) => raster.SpatialAnalyst.TanAsync(raster).Result;
    public static Raster TanH(this Raster raster) => raster.SpatialAnalyst.TanHAsync(raster).Result;
    public static Raster ATan2(this Raster raster, double n) => raster.SpatialAnalyst.ATan2Async(raster, n).Result;
    public static Raster ATan2(this Raster raster, int n) => raster.SpatialAnalyst.ATan2Async(raster, n).Result;
    public static Raster ATan2(this Raster raster1, Raster raster2) => raster1.SpatialAnalyst.ATan2Async(raster1, raster2).Result;
    public static Raster BitwiseAnd(this Raster raster, double n) => raster.SpatialAnalyst.BitwiseAndAsync(raster, n).Result;
    public static Raster BitwiseAnd(this Raster raster, int n) => raster.SpatialAnalyst.BitwiseAndAsync(raster, n).Result;
    public static Raster BitwiseAnd(this Raster raster1, Raster raster2) => raster1.SpatialAnalyst.BitwiseAndAsync(raster1, raster2).Result;
    public static Raster BitwiseLeftShift(this Raster raster, double n) => raster.SpatialAnalyst.BitwiseLeftShiftAsync(raster, n).Result;
    public static Raster BitwiseLeftShift(this Raster raster, int n) => raster.SpatialAnalyst.BitwiseLeftShiftAsync(raster, n).Result;
    public static Raster BitwiseLeftShift(this Raster raster1, Raster raster2) => raster1.SpatialAnalyst.BitwiseLeftShiftAsync(raster1, raster2).Result;
    public static Raster BitwiseOr(this Raster raster, double n) => raster.SpatialAnalyst.BitwiseOrAsync(raster, n).Result;
    public static Raster BitwiseOr(this Raster raster, int n) => raster.SpatialAnalyst.BitwiseOrAsync(raster, n).Result;
    public static Raster BitwiseOr(this Raster raster1, Raster raster2) => raster1.SpatialAnalyst.BitwiseOrAsync(raster1, raster2).Result;
    public static Raster BitwiseRightShift(this Raster raster, double n) => raster.SpatialAnalyst.BitwiseRightShiftAsync(raster, n).Result;
    public static Raster BitwiseRightShift(this Raster raster, int n) => raster.SpatialAnalyst.BitwiseRightShiftAsync(raster, n).Result;
    public static Raster BitwiseRightShift(this Raster raster1, Raster raster2) => raster1.SpatialAnalyst.BitwiseRightShiftAsync(raster1, raster2).Result;
    public static Raster BitwiseXOr(this Raster raster, double n) => raster.SpatialAnalyst.BitwiseXOrAsync(raster, n).Result;
    public static Raster BitwiseXOr(this Raster raster, int n) => raster.SpatialAnalyst.BitwiseXOrAsync(raster, n).Result;
    public static Raster BitwiseXOr(this Raster raster1, Raster raster2) => raster1.SpatialAnalyst.BitwiseXOrAsync(raster1, raster2).Result;
    public static Raster BooleanAnd(this Raster raster, double n) => raster.SpatialAnalyst.BooleanAndAsync(raster, n).Result;
    public static Raster BooleanAnd(this Raster raster, int n) => raster.SpatialAnalyst.BooleanAndAsync(raster, n).Result;
    public static Raster BooleanAnd(this Raster raster1, Raster raster2) => raster1.SpatialAnalyst.BooleanAndAsync(raster1, raster2).Result;
    public static Raster BooleanOr(this Raster raster, double n) => raster.SpatialAnalyst.BooleanOrAsync(raster, n).Result;
    public static Raster BooleanOr(this Raster raster, int n) => raster.SpatialAnalyst.BooleanOrAsync(raster, n).Result;
    public static Raster BooleanOr(this Raster raster1, Raster raster2) => raster1.SpatialAnalyst.BooleanOrAsync(raster1, raster2).Result;
    public static Raster BooleanXOr(this Raster raster, double n) => raster.SpatialAnalyst.BooleanXOrAsync(raster, n).Result;
    public static Raster BooleanXOr(this Raster raster, int n) => raster.SpatialAnalyst.BooleanXOrAsync(raster, n).Result;
    public static Raster BooleanXOr(this Raster raster1, Raster raster2) => raster1.SpatialAnalyst.BooleanXOrAsync(raster1, raster2).Result;
    public static Raster CombinatorialAnd(this Raster raster, int n) => raster.SpatialAnalyst.CombinatorialAndAsync(raster, n).Result;
    public static Raster CombinatorialAnd(this Raster raster1, Raster raster2) => raster1.SpatialAnalyst.CombinatorialAndAsync(raster1, raster2).Result;
    public static Raster CombinatorialOr(this Raster raster, int n) => raster.SpatialAnalyst.CombinatorialOrAsync(raster, n).Result;
    public static Raster CombinatorialOr(this Raster raster1, Raster raster2) => raster1.SpatialAnalyst.CombinatorialOrAsync(raster1, raster2).Result;
    public static Raster CombinatorialXOr(this Raster raster, int n) => raster.SpatialAnalyst.CombinatorialXOrAsync(raster, n).Result;
    public static Raster CombinatorialXOr(this Raster raster1, Raster raster2) => raster1.SpatialAnalyst.CombinatorialXOrAsync(raster1, raster2).Result;
    public static Raster Diff(this Raster raster, double n) => raster.SpatialAnalyst.DiffAsync(raster, n).Result;
    public static Raster Diff(this Raster raster, int n) => raster.SpatialAnalyst.DiffAsync(raster, n).Result;
    public static Raster Diff(this Raster raster1, Raster raster2) => raster1.SpatialAnalyst.DiffAsync(raster1, raster2).Result;
    public static Raster Divide(this Raster raster, double n) => raster.SpatialAnalyst.DivideAsync(raster, n).Result;
    public static Raster Divide(this Raster raster, int n) => raster.SpatialAnalyst.DivideAsync(raster, n).Result;
    public static Raster Divide(this Raster raster1, Raster raster2) => raster1.SpatialAnalyst.DivideAsync(raster1, raster2).Result;
    public static Raster EqualTo(this Raster raster, double n) => raster.SpatialAnalyst.EqualToAsync(raster, n).Result;
    public static Raster EqualTo(this Raster raster, int n) => raster.SpatialAnalyst.EqualToAsync(raster, n).Result;
    public static Raster EqualTo(this Raster raster1, Raster raster2) => raster1.SpatialAnalyst.EqualToAsync(raster1, raster2).Result;
    public static Raster GreaterThan(this Raster raster, double n) => raster.SpatialAnalyst.GreaterThanAsync(raster, n).Result;
    public static Raster GreaterThan(this Raster raster, int n) => raster.SpatialAnalyst.GreaterThanAsync(raster, n).Result;
    public static Raster GreaterThan(this Raster raster1, Raster raster2) => raster1.SpatialAnalyst.GreaterThanAsync(raster1, raster2).Result;
    public static Raster GreaterThanEqual(this Raster raster, double n) => raster.SpatialAnalyst.GreaterThanEqualAsync(raster, n).Result;
    public static Raster GreaterThanEqual(this Raster raster, int n) => raster.SpatialAnalyst.GreaterThanEqualAsync(raster, n).Result;
    public static Raster GreaterThanEqual(this Raster raster1, Raster raster2) => raster1.SpatialAnalyst.GreaterThanEqualAsync(raster1, raster2).Result;
    public static Raster LessThan(this Raster raster, double n) => raster.SpatialAnalyst.LessThanAsync(raster, n).Result;
    public static Raster LessThan(this Raster raster, int n) => raster.SpatialAnalyst.LessThanAsync(raster, n).Result;
    public static Raster LessThan(this Raster raster1, Raster raster2) => raster1.SpatialAnalyst.LessThanAsync(raster1, raster2).Result;
    public static Raster LessThanEqual(this Raster raster, double n) => raster.SpatialAnalyst.LessThanEqualAsync(raster, n).Result;
    public static Raster LessThanEqual(this Raster raster, int n) => raster.SpatialAnalyst.LessThanEqualAsync(raster, n).Result;
    public static Raster LessThanEqual(this Raster raster1, Raster raster2) => raster1.SpatialAnalyst.LessThanEqualAsync(raster1, raster2).Result;
    public static Raster Minus(this Raster raster, double n) => raster.SpatialAnalyst.MinusAsync(raster, n).Result;
    public static Raster Minus(this Raster raster, int n) => raster.SpatialAnalyst.MinusAsync(raster, n).Result;
    public static Raster Minus(this Raster raster1, Raster raster2) => raster1.SpatialAnalyst.MinusAsync(raster1, raster2).Result;
    public static Raster Mod(this Raster raster, double n) => raster.SpatialAnalyst.ModAsync(raster, n).Result;
    public static Raster Mod(this Raster raster, int n) => raster.SpatialAnalyst.ModAsync(raster, n).Result;
    public static Raster Mod(this Raster raster1, Raster raster2) => raster1.SpatialAnalyst.ModAsync(raster1, raster2).Result;
    public static Raster NotEqual(this Raster raster, double n) => raster.SpatialAnalyst.NotEqualAsync(raster, n).Result;
    public static Raster NotEqual(this Raster raster, int n) => raster.SpatialAnalyst.NotEqualAsync(raster, n).Result;
    public static Raster NotEqual(this Raster raster1, Raster raster2) => raster1.SpatialAnalyst.NotEqualAsync(raster1, raster2).Result;
    public static Raster Over(this Raster raster, double n) => raster.SpatialAnalyst.OverAsync(raster, n).Result;
    public static Raster Over(this Raster raster, int n) => raster.SpatialAnalyst.OverAsync(raster, n).Result;
    public static Raster Over(this Raster raster1, Raster raster2) => raster1.SpatialAnalyst.OverAsync(raster1, raster2).Result;
    public static Raster Plus(this Raster raster, double n) => raster.SpatialAnalyst.PlusAsync(raster, n).Result;
    public static Raster Plus(this Raster raster, int n) => raster.SpatialAnalyst.PlusAsync(raster, n).Result;
    public static Raster Plus(this Raster raster1, Raster raster2) => raster1.SpatialAnalyst.PlusAsync(raster1, raster2).Result;
    public static Raster Power(this Raster raster, double n) => raster.SpatialAnalyst.PowerAsync(raster, n).Result;
    public static Raster Power(this Raster raster, int n) => raster.SpatialAnalyst.PowerAsync(raster, n).Result;
    public static Raster Power(this Raster raster1, Raster raster2) => raster1.SpatialAnalyst.PowerAsync(raster1, raster2).Result;
    public static Raster Times(this Raster raster, double n) => raster.SpatialAnalyst.TimesAsync(raster, n).Result;
    public static Raster Times(this Raster raster, int n) => raster.SpatialAnalyst.TimesAsync(raster, n).Result;
    public static Raster Times(this Raster raster1, Raster raster2) => raster1.SpatialAnalyst.TimesAsync(raster1, raster2).Result;

    #endregion

    #region Extra

    public static Raster Aspect(this Raster raster, ZUnit zUnit = ZUnit.Meter)
        => raster.SpatialAnalyst.AspectAsync(raster, zUnit).Result;

    public static Raster Con(this Raster raster1, Raster raster2, Raster raster3)
        => raster1.SpatialAnalyst.ConAsync(raster1, raster2, raster3).Result;

    public static Raster Con(this Raster raster1, Raster raster2, int n)
        => raster1.SpatialAnalyst.ConAsync(raster1, raster2, n).Result;

    public static Raster Con(this Raster raster1, int n, Raster raster2)
        => raster1.SpatialAnalyst.ConAsync(raster1, n, raster2).Result;

    public static Raster Con(this Raster raster1, int n1, int n2)
        => raster1.SpatialAnalyst.ConAsync(raster1, n1, n2).Result;

    public static Raster Con(this Raster raster1, Raster raster2, double n)
        => raster1.SpatialAnalyst.ConAsync(raster1, raster2, n).Result;

    public static Raster Con(this Raster raster1, double n, Raster raster2)
        => raster1.SpatialAnalyst.ConAsync(raster1, n, raster2).Result;

    public static Raster Con(this Raster raster1, double n1, double n2)
        => raster1.SpatialAnalyst.ConAsync(raster1, n1, n2).Result;

    public static Raster Color(this Raster raster, ColorRamp colorRamp)
        => raster.SpatialAnalyst.ColorAsync(raster, colorRamp).Result;

    public static Raster Extract(this Raster raster, int band)
        => raster.SpatialAnalyst.ExtractAsync(raster, band).Result;

    public static Raster[] Extract(this Raster raster, params int[] bands)
        => raster.SpatialAnalyst.ExtractAsync(raster, bands).Result;

    public static T[] ExtractValues<T>(this Raster raster, params double[][] points)
        => raster.SpatialAnalyst.ExtractValuesAsync<T>(raster, points).Result;

    public static Raster Focal(this Raster raster, Statistics statistics, int radius, bool ignoreNodata = true)
        => raster.SpatialAnalyst.FocalAsync(raster, statistics, radius, ignoreNodata).Result;

    public static Raster Focal(this Raster raster, Statistics statistics, int width, int height, bool ignoreNodata = true)
        => raster.SpatialAnalyst.FocalAsync(raster, statistics, width, height, ignoreNodata).Result;

    public static Raster Hillshade(this Raster raster, double zFactor = 1, double azimuth = 315, double altitude = 45)
        => raster.SpatialAnalyst.HillshadeAsync(raster, zFactor, azimuth, altitude).Result;

    public static Raster Project(this Raster raster, int inWkid, int outWkid)
        => raster.SpatialAnalyst.ProjectAsync(raster, inWkid, outWkid).Result;

    public static Raster Project(this Raster raster, int wkid)
        => raster.SpatialAnalyst.ProjectAsync(raster, wkid).Result;

    public static Raster Randomize(this Raster raster)
        => raster.SpatialAnalyst.RandomizeAsync(raster).Result;

    public static Raster Resample(this Raster raster, double cellSize, Resampling resampling = Resampling.Nearest)
        => raster.SpatialAnalyst.ResampleAsync(raster, cellSize, resampling).Result;

    public static Raster Slope(this Raster raster, double zFactor = 1, ZUnit zUnit = ZUnit.Meter)
        => raster.SpatialAnalyst.SlopeAsync(raster, zFactor, zUnit).Result;

    public static string Thumbnail(this Raster raster)
        => raster.SpatialAnalyst.ThumbnailAsync(raster).Result;

    #endregion
}
