using Google.Protobuf;
using Grpc.Net.Client;
using System.Collections.Concurrent;
using static Glidergun.Grpc;

namespace Glidergun;

public class SpatialAnalyst
{
    private readonly GrpcClient grpc;
    private readonly string key;
    private readonly ConcurrentDictionary<object, object> cache = new();

    public SpatialAnalyst(string url, Action<Type, Action<object, TextWriter>, string>? registerDotNetInteractiveFormatter = null)
    {
        var options = new GrpcChannelOptions();
        var size = options.MaxReceiveMessageSize * 10;
        options.MaxReceiveMessageSize = size;
        options.MaxSendMessageSize = size;

        grpc = new(GrpcChannel.ForAddress(url, options));

        var response = grpc.token(new TokenRequest { });
        key = response.Key;

        if (registerDotNetInteractiveFormatter is not null)
            this.RegisterWithDotNetInteractive(registerDotNetInteractiveFormatter);
    }

    public async Task<Raster> CreateAsync(Stream stream, Format format)
    {
        var hash = stream.CreateMD5();

        if (cache.TryGetValue(hash, out var item))
            return (Raster)item;

        var response = await grpc.checkAsync(new CheckRequest { Key = key, Hash = hash });

        if (string.IsNullOrEmpty(response.Id))
        {
            stream.Position = 0;
            response = await grpc.createAsync(new CreateRequest { Key = key, Hash = hash, Data = await ByteString.FromStreamAsync(stream), Format = format });
        }

        var raster = new Raster(this, response);

        cache.AddOrUpdate(hash, raster, (_, _) => raster);

        return raster;
    }

    public Task<Raster> CreateAsync(string fileName)
    {
        var format = Utility.FindEnum<Format>(fileName.Split('.').Last());
        return CreateAsync(File.OpenRead(fileName), format);
    }

    public async Task SaveAsync(Raster raster, Format format, Stream stream)
    {
        var response = await grpc.saveAsync(new SaveRequest { Key = key, RasterId = raster.Id, Format = format });
        response.Data.WriteTo(stream);
    }

    #region Math

    private Task<Raster> MathAsync(Op op, Raster raster) => Memoize((op, raster), async () =>
    {
        var response = await grpc.mathRasterAsync(new MathRequest { Key = key, Op = op, RasterId = raster.Id });
        return new Raster(this, response);
    });

    public Task<Raster> AbsAsync(Raster raster) => MathAsync(Op.Abs, raster);
    public Task<Raster> ACosAsync(Raster raster) => MathAsync(Op.Acos, raster);
    public Task<Raster> ACosHAsync(Raster raster) => MathAsync(Op.AcosH, raster);
    public Task<Raster> ASinAsync(Raster raster) => MathAsync(Op.Asin, raster);
    public Task<Raster> ASinHAsync(Raster raster) => MathAsync(Op.AsinH, raster);
    public Task<Raster> ATanAsync(Raster raster) => MathAsync(Op.Atan, raster);
    public Task<Raster> ATanHAsync(Raster raster) => MathAsync(Op.AtanH, raster);
    public Task<Raster> BitwiseNotAsync(Raster raster) => MathAsync(Op.BitwiseNot, raster);
    public Task<Raster> BooleanNotAsync(Raster raster) => MathAsync(Op.BooleanNot, raster);
    public Task<Raster> CosAsync(Raster raster) => MathAsync(Op.Cos, raster);
    public Task<Raster> CosHAsync(Raster raster) => MathAsync(Op.CosH, raster);
    public Task<Raster> ExpAsync(Raster raster) => MathAsync(Op.Exp, raster);
    public Task<Raster> Exp10Async(Raster raster) => MathAsync(Op.Exp10, raster);
    public Task<Raster> Exp2Async(Raster raster) => MathAsync(Op.Exp2, raster);
    public Task<Raster> FloatAsync(Raster raster) => MathAsync(Op.Float, raster);
    public Task<Raster> IntAsync(Raster raster) => MathAsync(Op.Int, raster);
    public Task<Raster> IsNullAsync(Raster raster) => MathAsync(Op.IsNull, raster);
    public Task<Raster> LnAsync(Raster raster) => MathAsync(Op.Ln, raster);
    public Task<Raster> Log10Async(Raster raster) => MathAsync(Op.Log10, raster);
    public Task<Raster> Log2Async(Raster raster) => MathAsync(Op.Log2, raster);
    public Task<Raster> NegateAsync(Raster raster) => MathAsync(Op.Negate, raster);
    public Task<Raster> RoundDownAsync(Raster raster) => MathAsync(Op.RoundDown, raster);
    public Task<Raster> RoundUpAsync(Raster raster) => MathAsync(Op.RoundUp, raster);
    public Task<Raster> SinAsync(Raster raster) => MathAsync(Op.Sin, raster);
    public Task<Raster> SinHAsync(Raster raster) => MathAsync(Op.SinH, raster);
    public Task<Raster> SquareAsync(Raster raster) => MathAsync(Op.Square, raster);
    public Task<Raster> SquareRootAsync(Raster raster) => MathAsync(Op.SquareRoot, raster);
    public Task<Raster> TanAsync(Raster raster) => MathAsync(Op.Tan, raster);
    public Task<Raster> TanHAsync(Raster raster) => MathAsync(Op.TanH, raster);

    private Task<Raster> MathAsync<T1, T2>(Op op, T1 value1, T2 value2) => Memoize((op, value1, value2), async () =>
    {
        var task = (value1, value2) switch
        {
            (Raster r1, Raster r2) => grpc.mathRasterRasterAsync(new MathRequest { Key = key, Op = op, RasterId = r1.Id, Raster2Id = r2.Id }),
            (Raster r, int n) => grpc.mathRasterIntAsync(new MathRequest { Key = key, Op = op, RasterId = r.Id, IntValue = n }),
            (Raster r, double n) => grpc.mathRasterDoubleAsync(new MathRequest { Key = key, Op = op, RasterId = r.Id, DoubleValue = n }),
            (int n, Raster r) => grpc.mathIntRasterAsync(new MathRequest { Key = key, Op = op, IntValue = n, RasterId = r.Id }),
            (double n, Raster r) => grpc.mathDoubleRasterAsync(new MathRequest { Key = key, Op = op, DoubleValue = n, RasterId = r.Id }),
            _ => throw new InvalidOperationException()
        };

        var response = await task;
        return new Raster(this, response);
    });

    public Task<Raster> ATan2Async(int n, Raster raster) => MathAsync(Op.Atan2, n, raster);
    public Task<Raster> ATan2Async(Raster raster, double n) => MathAsync(Op.Atan2, raster, n);
    public Task<Raster> ATan2Async(Raster raster, int n) => MathAsync(Op.Atan2, raster, n);
    public Task<Raster> ATan2Async(Raster raster1, Raster raster2) => MathAsync(Op.Atan2, raster1, raster2);
    public Task<Raster> BitwiseAndAsync(double n, Raster raster) => MathAsync(Op.BitwiseAnd, n, raster);
    public Task<Raster> BitwiseAndAsync(int n, Raster raster) => MathAsync(Op.BitwiseAnd, n, raster);
    public Task<Raster> BitwiseAndAsync(Raster raster, double n) => MathAsync(Op.BitwiseAnd, raster, n);
    public Task<Raster> BitwiseAndAsync(Raster raster, int n) => MathAsync(Op.BitwiseAnd, raster, n);
    public Task<Raster> BitwiseAndAsync(Raster raster1, Raster raster2) => MathAsync(Op.BitwiseAnd, raster1, raster2);
    public Task<Raster> BitwiseLeftShiftAsync(double n, Raster raster) => MathAsync(Op.BitwiseLeftShift, n, raster);
    public Task<Raster> BitwiseLeftShiftAsync(int n, Raster raster) => MathAsync(Op.BitwiseLeftShift, n, raster);
    public Task<Raster> BitwiseLeftShiftAsync(Raster raster, double n) => MathAsync(Op.BitwiseLeftShift, raster, n);
    public Task<Raster> BitwiseLeftShiftAsync(Raster raster, int n) => MathAsync(Op.BitwiseLeftShift, raster, n);
    public Task<Raster> BitwiseLeftShiftAsync(Raster raster1, Raster raster2) => MathAsync(Op.BitwiseLeftShift, raster1, raster2);
    public Task<Raster> BitwiseOrAsync(double n, Raster raster) => MathAsync(Op.BitwiseOr, n, raster);
    public Task<Raster> BitwiseOrAsync(int n, Raster raster) => MathAsync(Op.BitwiseOr, n, raster);
    public Task<Raster> BitwiseOrAsync(Raster raster, double n) => MathAsync(Op.BitwiseOr, raster, n);
    public Task<Raster> BitwiseOrAsync(Raster raster, int n) => MathAsync(Op.BitwiseOr, raster, n);
    public Task<Raster> BitwiseOrAsync(Raster raster1, Raster raster2) => MathAsync(Op.BitwiseOr, raster1, raster2);
    public Task<Raster> BitwiseRightShiftAsync(double n, Raster raster) => MathAsync(Op.BitwiseRightShift, n, raster);
    public Task<Raster> BitwiseRightShiftAsync(int n, Raster raster) => MathAsync(Op.BitwiseRightShift, n, raster);
    public Task<Raster> BitwiseRightShiftAsync(Raster raster, double n) => MathAsync(Op.BitwiseRightShift, raster, n);
    public Task<Raster> BitwiseRightShiftAsync(Raster raster, int n) => MathAsync(Op.BitwiseRightShift, raster, n);
    public Task<Raster> BitwiseRightShiftAsync(Raster raster1, Raster raster2) => MathAsync(Op.BitwiseRightShift, raster1, raster2);
    public Task<Raster> BitwiseXOrAsync(double n, Raster raster) => MathAsync(Op.BitwiseXor, n, raster);
    public Task<Raster> BitwiseXOrAsync(int n, Raster raster) => MathAsync(Op.BitwiseXor, n, raster);
    public Task<Raster> BitwiseXOrAsync(Raster raster, double n) => MathAsync(Op.BitwiseXor, raster, n);
    public Task<Raster> BitwiseXOrAsync(Raster raster, int n) => MathAsync(Op.BitwiseXor, raster, n);
    public Task<Raster> BitwiseXOrAsync(Raster raster1, Raster raster2) => MathAsync(Op.BitwiseXor, raster1, raster2);
    public Task<Raster> BooleanAndAsync(double n, Raster raster) => MathAsync(Op.BooleanAnd, n, raster);
    public Task<Raster> BooleanAndAsync(int n, Raster raster) => MathAsync(Op.BooleanAnd, n, raster);
    public Task<Raster> BooleanAndAsync(Raster raster, double n) => MathAsync(Op.BooleanAnd, raster, n);
    public Task<Raster> BooleanAndAsync(Raster raster, int n) => MathAsync(Op.BooleanAnd, raster, n);
    public Task<Raster> BooleanAndAsync(Raster raster1, Raster raster2) => MathAsync(Op.BooleanAnd, raster1, raster2);
    public Task<Raster> BooleanOrAsync(double n, Raster raster) => MathAsync(Op.BooleanOr, n, raster);
    public Task<Raster> BooleanOrAsync(int n, Raster raster) => MathAsync(Op.BooleanOr, n, raster);
    public Task<Raster> BooleanOrAsync(Raster raster, double n) => MathAsync(Op.BooleanOr, raster, n);
    public Task<Raster> BooleanOrAsync(Raster raster, int n) => MathAsync(Op.BooleanOr, raster, n);
    public Task<Raster> BooleanOrAsync(Raster raster1, Raster raster2) => MathAsync(Op.BooleanOr, raster1, raster2);
    public Task<Raster> BooleanXOrAsync(double n, Raster raster) => MathAsync(Op.BooleanXor, n, raster);
    public Task<Raster> BooleanXOrAsync(int n, Raster raster) => MathAsync(Op.BooleanXor, n, raster);
    public Task<Raster> BooleanXOrAsync(Raster raster, double n) => MathAsync(Op.BooleanXor, raster, n);
    public Task<Raster> BooleanXOrAsync(Raster raster, int n) => MathAsync(Op.BooleanXor, raster, n);
    public Task<Raster> BooleanXOrAsync(Raster raster1, Raster raster2) => MathAsync(Op.BooleanXor, raster1, raster2);
    public Task<Raster> CombinatorialAndAsync(int n, Raster raster) => MathAsync(Op.CombinatorialAnd, n, raster);
    public Task<Raster> CombinatorialAndAsync(Raster raster, int n) => MathAsync(Op.CombinatorialAnd, raster, n);
    public Task<Raster> CombinatorialAndAsync(Raster raster1, Raster raster2) => MathAsync(Op.CombinatorialAnd, raster1, raster2);
    public Task<Raster> CombinatorialOrAsync(int n, Raster raster) => MathAsync(Op.CombinatorialOr, n, raster);
    public Task<Raster> CombinatorialOrAsync(Raster raster, int n) => MathAsync(Op.CombinatorialOr, raster, n);
    public Task<Raster> CombinatorialOrAsync(Raster raster1, Raster raster2) => MathAsync(Op.CombinatorialOr, raster1, raster2);
    public Task<Raster> CombinatorialXOrAsync(int n, Raster raster) => MathAsync(Op.CombinatorialXor, n, raster);
    public Task<Raster> CombinatorialXOrAsync(Raster raster, int n) => MathAsync(Op.CombinatorialXor, raster, n);
    public Task<Raster> CombinatorialXOrAsync(Raster raster1, Raster raster2) => MathAsync(Op.CombinatorialXor, raster1, raster2);
    public Task<Raster> DiffAsync(double n, Raster raster) => MathAsync(Op.Diff, n, raster);
    public Task<Raster> DiffAsync(int n, Raster raster) => MathAsync(Op.Diff, n, raster);
    public Task<Raster> DiffAsync(Raster raster, double n) => MathAsync(Op.Diff, raster, n);
    public Task<Raster> DiffAsync(Raster raster, int n) => MathAsync(Op.Diff, raster, n);
    public Task<Raster> DiffAsync(Raster raster1, Raster raster2) => MathAsync(Op.Diff, raster1, raster2);
    public Task<Raster> DivideAsync(double n, Raster raster) => MathAsync(Op.Divide, n, raster);
    public Task<Raster> DivideAsync(int n, Raster raster) => MathAsync(Op.Divide, n, raster);
    public Task<Raster> DivideAsync(Raster raster, double n) => MathAsync(Op.Divide, raster, n);
    public Task<Raster> DivideAsync(Raster raster, int n) => MathAsync(Op.Divide, raster, n);
    public Task<Raster> DivideAsync(Raster raster1, Raster raster2) => MathAsync(Op.Divide, raster1, raster2);
    public Task<Raster> EqualToAsync(double n, Raster raster) => MathAsync(Op.EqualTo, n, raster);
    public Task<Raster> EqualToAsync(int n, Raster raster) => MathAsync(Op.EqualTo, n, raster);
    public Task<Raster> EqualToAsync(Raster raster, double n) => MathAsync(Op.EqualTo, raster, n);
    public Task<Raster> EqualToAsync(Raster raster, int n) => MathAsync(Op.EqualTo, raster, n);
    public Task<Raster> EqualToAsync(Raster raster1, Raster raster2) => MathAsync(Op.EqualTo, raster1, raster2);
    public Task<Raster> GreaterThanAsync(double n, Raster raster) => MathAsync(Op.GreaterThan, n, raster);
    public Task<Raster> GreaterThanAsync(int n, Raster raster) => MathAsync(Op.GreaterThan, n, raster);
    public Task<Raster> GreaterThanAsync(Raster raster, double n) => MathAsync(Op.GreaterThan, raster, n);
    public Task<Raster> GreaterThanAsync(Raster raster, int n) => MathAsync(Op.GreaterThan, raster, n);
    public Task<Raster> GreaterThanAsync(Raster raster1, Raster raster2) => MathAsync(Op.GreaterThan, raster1, raster2);
    public Task<Raster> GreaterThanEqualAsync(double n, Raster raster) => MathAsync(Op.GreaterThanEqual, n, raster);
    public Task<Raster> GreaterThanEqualAsync(int n, Raster raster) => MathAsync(Op.GreaterThanEqual, n, raster);
    public Task<Raster> GreaterThanEqualAsync(Raster raster, double n) => MathAsync(Op.GreaterThanEqual, raster, n);
    public Task<Raster> GreaterThanEqualAsync(Raster raster, int n) => MathAsync(Op.GreaterThanEqual, raster, n);
    public Task<Raster> GreaterThanEqualAsync(Raster raster1, Raster raster2) => MathAsync(Op.GreaterThanEqual, raster1, raster2);
    public Task<Raster> LessThanAsync(double n, Raster raster) => MathAsync(Op.LessThan, n, raster);
    public Task<Raster> LessThanAsync(int n, Raster raster) => MathAsync(Op.LessThan, n, raster);
    public Task<Raster> LessThanAsync(Raster raster, double n) => MathAsync(Op.LessThan, raster, n);
    public Task<Raster> LessThanAsync(Raster raster, int n) => MathAsync(Op.LessThan, raster, n);
    public Task<Raster> LessThanAsync(Raster raster1, Raster raster2) => MathAsync(Op.LessThan, raster1, raster2);
    public Task<Raster> LessThanEqualAsync(double n, Raster raster) => MathAsync(Op.LessThanEqual, n, raster);
    public Task<Raster> LessThanEqualAsync(int n, Raster raster) => MathAsync(Op.LessThanEqual, n, raster);
    public Task<Raster> LessThanEqualAsync(Raster raster, double n) => MathAsync(Op.LessThanEqual, raster, n);
    public Task<Raster> LessThanEqualAsync(Raster raster, int n) => MathAsync(Op.LessThanEqual, raster, n);
    public Task<Raster> LessThanEqualAsync(Raster raster1, Raster raster2) => MathAsync(Op.LessThanEqual, raster1, raster2);
    public Task<Raster> MinusAsync(double n, Raster raster) => MathAsync(Op.Minus, n, raster);
    public Task<Raster> MinusAsync(int n, Raster raster) => MathAsync(Op.Minus, n, raster);
    public Task<Raster> MinusAsync(Raster raster, double n) => MathAsync(Op.Minus, raster, n);
    public Task<Raster> MinusAsync(Raster raster, int n) => MathAsync(Op.Minus, raster, n);
    public Task<Raster> MinusAsync(Raster raster1, Raster raster2) => MathAsync(Op.Minus, raster1, raster2);
    public Task<Raster> ModAsync(double n, Raster raster) => MathAsync(Op.Mod, n, raster);
    public Task<Raster> ModAsync(int n, Raster raster) => MathAsync(Op.Mod, n, raster);
    public Task<Raster> ModAsync(Raster raster, double n) => MathAsync(Op.Mod, raster, n);
    public Task<Raster> ModAsync(Raster raster, int n) => MathAsync(Op.Mod, raster, n);
    public Task<Raster> ModAsync(Raster raster1, Raster raster2) => MathAsync(Op.Mod, raster1, raster2);
    public Task<Raster> NotEqualAsync(double n, Raster raster) => MathAsync(Op.NotEqual, n, raster);
    public Task<Raster> NotEqualAsync(int n, Raster raster) => MathAsync(Op.NotEqual, n, raster);
    public Task<Raster> NotEqualAsync(Raster raster, double n) => MathAsync(Op.NotEqual, raster, n);
    public Task<Raster> NotEqualAsync(Raster raster, int n) => MathAsync(Op.NotEqual, raster, n);
    public Task<Raster> NotEqualAsync(Raster raster1, Raster raster2) => MathAsync(Op.NotEqual, raster1, raster2);
    public Task<Raster> OverAsync(double n, Raster raster) => MathAsync(Op.Over, n, raster);
    public Task<Raster> OverAsync(int n, Raster raster) => MathAsync(Op.Over, n, raster);
    public Task<Raster> OverAsync(Raster raster, double n) => MathAsync(Op.Over, raster, n);
    public Task<Raster> OverAsync(Raster raster, int n) => MathAsync(Op.Over, raster, n);
    public Task<Raster> OverAsync(Raster raster1, Raster raster2) => MathAsync(Op.Over, raster1, raster2);
    public Task<Raster> PlusAsync(double n, Raster raster) => MathAsync(Op.Plus, n, raster);
    public Task<Raster> PlusAsync(int n, Raster raster) => MathAsync(Op.Plus, n, raster);
    public Task<Raster> PlusAsync(Raster raster, double n) => MathAsync(Op.Plus, raster, n);
    public Task<Raster> PlusAsync(Raster raster, int n) => MathAsync(Op.Plus, raster, n);
    public Task<Raster> PlusAsync(Raster raster1, Raster raster2) => MathAsync(Op.Plus, raster1, raster2);
    public Task<Raster> PowerAsync(double n, Raster raster) => MathAsync(Op.Power, n, raster);
    public Task<Raster> PowerAsync(int n, Raster raster) => MathAsync(Op.Power, n, raster);
    public Task<Raster> PowerAsync(Raster raster, double n) => MathAsync(Op.Power, raster, n);
    public Task<Raster> PowerAsync(Raster raster, int n) => MathAsync(Op.Power, raster, n);
    public Task<Raster> PowerAsync(Raster raster1, Raster raster2) => MathAsync(Op.Power, raster1, raster2);
    public Task<Raster> TimesAsync(double n, Raster raster) => MathAsync(Op.Times, n, raster);
    public Task<Raster> TimesAsync(int n, Raster raster) => MathAsync(Op.Times, n, raster);
    public Task<Raster> TimesAsync(Raster raster, double n) => MathAsync(Op.Times, raster, n);
    public Task<Raster> TimesAsync(Raster raster, int n) => MathAsync(Op.Times, raster, n);
    public Task<Raster> TimesAsync(Raster raster1, Raster raster2) => MathAsync(Op.Times, raster1, raster2);

    private Task<Raster> MathAsync<T1, T2, T3>(Op op, T1 value1, T2 value2, T3 value3) => Memoize((op, value1, value2, value3), async () =>
    {
        var task = (value1, value2, value3) switch
        {
            (Raster r1, Raster r2, Raster r3) => grpc.mathRasterRasterRasterAsync(new MathRequest { Key = key, Op = op, RasterId = r1.Id, Raster2Id = r2.Id, Raster3Id = r3.Id }),
            (Raster r1, Raster r2, int n) => grpc.mathRasterRasterIntAsync(new MathRequest { Key = key, Op = op, RasterId = r1.Id, Raster2Id = r2.Id, IntValue = n }),
            (Raster r, int n, Raster r2) => grpc.mathRasterIntRasterAsync(new MathRequest { Key = key, Op = op, RasterId = r.Id, IntValue = n, Raster2Id = r.Id }),
            (Raster r, int n1, int n2) => grpc.mathRasterIntIntAsync(new MathRequest { Key = key, Op = op, RasterId = r.Id, IntValue = n1, Int2Value = n2 }),
            (Raster r1, Raster r2, double n) => grpc.mathRasterRasterDoubleAsync(new MathRequest { Key = key, Op = op, RasterId = r1.Id, Raster2Id = r2.Id, DoubleValue = n }),
            (Raster r, double n, Raster r2) => grpc.mathRasterDoubleRasterAsync(new MathRequest { Key = key, Op = op, RasterId = r.Id, DoubleValue = n, Raster2Id = r.Id }),
            (Raster r, double n1, double n2) => grpc.mathRasterDoubleDoubleAsync(new MathRequest { Key = key, Op = op, RasterId = r.Id, DoubleValue = n1, Double2Value = n2 }),
            _ => throw new InvalidOperationException()
        };

        var response = await task;
        return new Raster(this, response);
    });

    public Task<Raster> ConAsync(Raster raster1, Raster raster2, Raster raster3) => MathAsync(Op.Con, raster1, raster2, raster3);
    public Task<Raster> ConAsync(Raster raster1, Raster raster2, int n) => MathAsync(Op.Con, raster1, raster2, n);
    public Task<Raster> ConAsync(Raster raster1, int n, Raster raster2) => MathAsync(Op.Con, raster1, n, raster2);
    public Task<Raster> ConAsync(Raster raster1, int n1, int n2) => MathAsync(Op.Con, raster1, n1, n2);
    public Task<Raster> ConAsync(Raster raster1, Raster raster2, double n) => MathAsync(Op.Con, raster1, raster2, n);
    public Task<Raster> ConAsync(Raster raster1, double n, Raster raster2) => MathAsync(Op.Con, raster1, n, raster2);
    public Task<Raster> ConAsync(Raster raster1, double n1, double n2) => MathAsync(Op.Con, raster1, n1, n2);

    #endregion

    public Task<Raster> AspectAsync(Raster raster, ZUnit zUnit = ZUnit.Meter) => Memoize((raster.Id, zUnit), async () =>
    {
        var response = await grpc.aspectAsync(new AspectRequest { Key = key, RasterId = raster.Id, ZUnit = zUnit });
        return new Raster(this, response);
    });

    public Task<Raster> ColorAsync(Raster raster, ColorRamp colorRamp) => Memoize(new { raster.Id, colorRamp }, async () =>
    {
        var request = new ColorRequest { Key = key, RasterId = raster.Id, Colorramp = colorRamp.GetDescription() };

        var response = await grpc.colorAsync(request);
        return new Raster(this, response);
    });

    public Task<Raster> CompositeAsync(params Raster[] rasters) => Memoize(rasters.Select(x => x.Id).ToJson(), async () =>
    {
        var request = new CompositeRequest { Key = key };
        request.RasterIds.AddRange(rasters.Select(x => x.Id));

        var response = await grpc.compositeAsync(request);
        return new Raster(this, response);
    });

    public Task<Raster> ExtractAsync(Raster raster, int band) => Memoize((raster, band), async () =>
    {
        var request = new ExtractRequest { Key = key, RasterId = raster.Id, Band = band };

        var response = await grpc.extractAsync(request);
        return new Raster(this, response);
    });

    public Task<Raster[]> ExtractAsync(Raster raster, params int[] bands)
        => Task.WhenAll((bands.Any() ? bands : Enumerable.Range(1, raster.BandCount)).Select(x => ExtractAsync(raster, x)));

    public async Task<T[]> ExtractValuesAsync<T>(Raster raster, params double[][] points)
    {
        var request = new ExtractValuesRequest { Key = key, RasterId = raster.Id };
        request.Points.AddRange(points.Select(p => new Point { X = p[0], Y = p[1] }));

        var response = await grpc.extractValuesAsync(request);

        var underlyingType = Nullable.GetUnderlyingType(typeof(T));
        var nullable = underlyingType != null;
        var type = nullable ? underlyingType! : typeof(T);

        return response.Values
            .Select(x => (T)(nullable && x == -9999 ? null : Convert.ChangeType(x, type))!)
            .ToArray();
    }

    private Task<Raster> FocalAsync(Raster raster, int radius, int width, int height, Statistics statistics, bool ignoreNodata) => Memoize((raster.Id, radius, width, height, statistics, ignoreNodata), async () =>
    {
        var response = await grpc.focalAsync(new FocalRequest { Key = key, RasterId = raster.Id, Radius = radius, Width = width, Height = height, Statistics = statistics, IgnoreNodata = ignoreNodata });
        return new Raster(this, response);
    });

    public Task<Raster> FocalAsync(Raster raster, Statistics statistics, int radius, bool ignoreNodata = true)
        => FocalAsync(raster, radius, 0, 0, statistics, ignoreNodata);

    public Task<Raster> FocalAsync(Raster raster, Statistics statistics, int width, int height, bool ignoreNodata = true)
        => FocalAsync(raster, 0, width, height, statistics, ignoreNodata);

    public Task<Raster> HillshadeAsync(Raster raster, double zFactor = 1, double azimuth = 315, double altitude = 45) => Memoize((raster.Id, zFactor, azimuth, altitude), async () =>
    {
        var response = await grpc.hillshadeAsync(new HillshadeRequest { Key = key, RasterId = raster.Id, ZFactor = zFactor, Azimuth = azimuth, Altitude = altitude });
        return new Raster(this, response);
    });

    public Task<Raster> ProjectAsync(Raster raster, int inWkid, int outWkid) => Memoize((raster.Id, inWkid, outWkid), async () =>
    {
        var response = await grpc.projectAsync(new ProjectRequest { Key = key, RasterId = raster.Id, InWkid = inWkid, OutWkid = outWkid });
        return new Raster(this, response);
    });

    public Task<Raster> ProjectAsync(Raster raster, int wkid)
        => ProjectAsync(raster, raster.Wkid > 0 ? raster.Wkid.Value : wkid, wkid);

    public async Task<Raster> RandomizeAsync(Raster raster)
    {
        var response = await grpc.randomizeAsync(new RandomizeRequest { Key = key, RasterId = raster.Id });
        return new Raster(this, response);
    }

    public Task<Raster> ResampleAsync(Raster raster, double cellSize, Resampling resampling = Resampling.Nearest) => Memoize((raster.Id, cellSize, resampling), async () =>
    {
        var response = await grpc.resampleAsync(new ResampleRequest { Key = key, RasterId = raster.Id, CellSize = cellSize, Resampling = resampling });
        return new Raster(this, response);
    });

    public Task<Raster> SlopeAsync(Raster raster, double zFactor = 1, ZUnit zUnit = ZUnit.Meter) => Memoize((raster.Id, zFactor, zUnit), async () =>
    {
        var response = await grpc.slopeAsync(new SlopeRequest { Key = key, RasterId = raster.Id, ZFactor = zFactor, ZUnit = zUnit });
        return new Raster(this, response);
    });

    public Task<string> ThumbnailAsync(Raster raster) => Memoize(raster.Id, async () =>
    {
        var response = await grpc.thumbnailAsync(new ThumbnailRequest { Key = key, RasterId = raster.Id });
        return response.Thumbnail;
    });

    private async Task<T> Memoize<T>(object key, Func<Task<T>> func)
    {
        if (cache.TryGetValue(key, out var item))
            return (T)item;

        var result = (await func())!;
        cache.AddOrUpdate(key, result, (_, _) => result);
        return result;
    }
}
