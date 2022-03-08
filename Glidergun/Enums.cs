using System.ComponentModel;

namespace Glidergun;

public enum AddAttributes
{
    ValueOnly, All
}

public enum AddLink
{
    AddLink, NoLink
}

public enum AggregationType
{
    Sum, Maximum, Mean, Median, Minimum
}

public enum Algorithm
{
    esriHSVAlgorithm, esriCIELabAlgorithm, esriLabLChAlgorithm
}

public enum AnalysisType
{
    Frequency, Observers
}

public enum APriori
{
    Equal, Sample, File
}

public enum ColorRamp
{
    [Description("Aspect")] Aspect,
    [Description("Basic Random")] BasicRandom,
    [Description("Bathymetric Scale")] BathymetricScale,
    [Description("Bathymetry #1")] Bathymetry1,
    [Description("Bathymetry #2")] Bathymetry2,
    [Description("Bathymetry #3")] Bathymetry3,
    [Description("Bathymetry #4")] Bathymetry4,
    [Description("Bathymetry #5")] Bathymetry5,
    [Description("Black and White")] BlackandWhite,
    [Description("Black to White")] BlacktoWhite,
    [Description("Blue Green-Orange (Continuous)")] BlueGreenOrangeContinuous,
    [Description("Blue Green-Pink (Continuous)")] BlueGreenPinkContinuous,
    [Description("Blue-Green (Continuous)")] BlueGreenContinuous,
    [Description("Blue-Purple (Continuous)")] BluePurpleContinuous,
    [Description("Blues")] Blues,
    [Description("Blues (Continuous)")] BluesContinuous,
    [Description("Brown-Green (Continuous)")] BrownGreenContinuous,
    [Description("Categorical Dark (Continuous)")] CategoricalDarkContinuous,
    [Description("Categorical Dark 6 (Continuous)")] CategoricalDark6Continuous,
    [Description("Categorical Light (Continuous)")] CategoricalLightContinuous,
    [Description("Categorical Light 6 (Continuous)")] CategoricalLight6Continuous,
    [Description("Cividis")] Cividis,
    [Description("Condition Number")] ConditionNumber,
    [Description("Cool Grey")] CoolGrey,
    [Description("Cool Tones")] CoolTones,
    [Description("Cyan to Purple")] CyantoPurple,
    [Description("Cyans")] Cyans,
    [Description("Dark Glazes")] DarkGlazes,
    [Description("Elevation #1")] Elevation1,
    [Description("Elevation #10")] Elevation10,
    [Description("Elevation #11")] Elevation11,
    [Description("Elevation #12")] Elevation12,
    [Description("Elevation #2")] Elevation2,
    [Description("Elevation #3")] Elevation3,
    [Description("Elevation #4")] Elevation4,
    [Description("Elevation #5")] Elevation5,
    [Description("Elevation #6")] Elevation6,
    [Description("Elevation #7")] Elevation7,
    [Description("Elevation #8")] Elevation8,
    [Description("Elevation #9")] Elevation9,
    [Description("Enamel")] Enamel,
    [Description("Errors")] Errors,
    [Description("Grays (Continuous)")] GraysContinuous,
    [Description("Green Blues")] GreenBlues,
    [Description("Green-Blue (Continuous)")] GreenBlueContinuous,
    [Description("Greens")] Greens,
    [Description("Greens (Continuous)")] GreensContinuous,
    [Description("Heat Map : Blue-Cyan-Green")] HeatMapBlueCyanGreen,
    [Description("Heat Map : Dark Blue-Cyan")] HeatMapDarkBlueCyan,
    [Description("Heat Map : Dark Blue-White")] HeatMapDarkBlueWhite,
    [Description("Heat Map : Dark Bronze-Yellow")] HeatMapDarkBronzeYellow,
    [Description("Heat Map : Dark Gold-White")] HeatMapDarkGoldWhite,
    [Description("Heat Map : Dark Green-Yellow")] HeatMapDarkGreenYellow,
    [Description("Heat Map : Dark Magenta-Yellow")] HeatMapDarkMagentaYellow,
    [Description("Heat Map : Dark Metal-Blue-White")] HeatMapDarkMetalBlueWhite,
    [Description("Heat Map : Dark Orange-Yellow")] HeatMapDarkOrangeYellow,
    [Description("Heat Map : Dark Purple-Yellow")] HeatMapDarkPurpleYellow,
    [Description("Heat Map : Neutral Blue-White")] HeatMapNeutralBlueWhite,
    [Description("Heat Map : Neutral Bronze-Yellow")] HeatMapNeutralBronzeYellow,
    [Description("Heat Map : Neutral Gold-White")] HeatMapNeutralGoldWhite,
    [Description("Heat Map : Neutral Green-Yellow")] HeatMapNeutralGreenYellow,
    [Description("Heat Map : Neutral Magenta-Yellow")] HeatMapNeutralMagentaYellow,
    [Description("Heat Map : Neutral Metal-Blue-White")] HeatMapNeutralMetalBlueWhite,
    [Description("Heat Map : Neutral Orange-Yellow")] HeatMapNeutralOrangeYellow,
    [Description("Heat Map : Neutral Purple-Yellow")] HeatMapNeutralPurpleYellow,
    [Description("Heat Map : Purple-Red-Yellow")] HeatMapPurpleRedYellow,
    [Description("Heat Map 1")] HeatMap1,
    [Description("Heat Map 1 - Semitransparent")] HeatMap1Semitransparent,
    [Description("Heat Map 2")] HeatMap2,
    [Description("Heat Map 2 - Semitransparent")] HeatMap2Semitransparent,
    [Description("Heat Map 3")] HeatMap3,
    [Description("Heat Map 3 - Semitransparent")] HeatMap3Semitransparent,
    [Description("Heat Map 4")] HeatMap4,
    [Description("Heat Map 4 - Semitransparent")] HeatMap4Semitransparent,
    [Description("Inferno")] Inferno,
    [Description("LAS Class Codes")] LASClassCodes,
    [Description("Magentas")] Magentas,
    [Description("Magma")] Magma,
    [Description("Muted Pastels")] MutedPastels,
    [Description("Orange-Gray-Blue (Continuous)")] OrangeGrayBlueContinuous,
    [Description("Orange-Pink (Continuous)")] OrangePinkContinuous,
    [Description("Orange-Purple (Continuous)")] OrangePurpleContinuous,
    [Description("Orange-Red (Continuous)")] OrangeRedContinuous,
    [Description("Orange-Red Dark (Continuous)")] OrangeRedDarkContinuous,
    [Description("Oranges")] Oranges,
    [Description("Oranges (Continuous)")] OrangesContinuous,
    [Description("Pastel Terra Tones")] PastelTerraTones,
    [Description("Pastels")] Pastels,
    [Description("Pastels Blue to Red")] PastelsBluetoRed,
    [Description("Pink-Green (Continuous)")] PinkGreenContinuous,
    [Description("Pink-Red (Continuous)")] PinkRedContinuous,
    [Description("Plasma")] Plasma,
    [Description("Precipitation")] Precipitation,
    [Description("Prediction")] Prediction,
    [Description("Purple Blues")] PurpleBlues,
    [Description("Purple Reds")] PurpleReds,
    [Description("Purple-Blue (Continuous)")] PurpleBlueContinuous,
    [Description("Purple-Blue-Green (Continuous)")] PurpleBlueGreenContinuous,
    [Description("Purple-Green (Continuous)")] PurpleGreenContinuous,
    [Description("Purple-Red (Continuous)")] PurpleRedContinuous,
    [Description("Purples")] Purples,
    [Description("Purples (Continuous)")] PurplesContinuous,
    [Description("Red to Green")] RedtoGreen,
    [Description("Red-Blue (Continuous)")] RedBlueContinuous,
    [Description("Red-Blue-Green (Continuous)")] RedBlueGreenContinuous,
    [Description("Red-Gray (Continuous)")] RedGrayContinuous,
    [Description("Red-Purple (Continuous)")] RedPurpleContinuous,
    [Description("Reds")] Reds,
    [Description("Reds (Continuous)")] RedsContinuous,
    [Description("Red-Yellow-Blue (Continuous)")] RedYellowBlueContinuous,
    [Description("Red-Yellow-Green (Continuous)")] RedYellowGreenContinuous,
    [Description("Red-Yellow-Pink (Continuous)")] RedYellowPinkContinuous,
    [Description("Shadow to Sunshine")] ShadowtoSunshine,
    [Description("Slope")] Slope,
    [Description("Spectral (Continuous)")] SpectralContinuous,
    [Description("Spectrum By Wavelength-Full Bright")] SpectrumByWavelengthFullBright,
    [Description("Spectrum By Wavelength-Full Dark")] SpectrumByWavelengthFullDark,
    [Description("Spectrum By Wavelength-Full Light")] SpectrumByWavelengthFullLight,
    [Description("Spectrum-Full Bright")] SpectrumFullBright,
    [Description("Spectrum-Full Dark")] SpectrumFullDark,
    [Description("Spectrum-Full Light")] SpectrumFullLight,
    [Description("Surface")] Surface,
    [Description("Temperature")] Temperature,
    [Description("Terra Tones")] TerraTones,
    [Description("Verdant Tones")] VerdantTones,
    [Description("Viridis")] Viridis,
    [Description("Warm Grey")] WarmGrey,
    [Description("Warm Grey Shade")] WarmGreyShade,
    [Description("Warm Light Grey Shade")] WarmLightGreyShade,
    [Description("Warm Purple Grey Shade")] WarmPurpleGreyShade,
    [Description("Warm Tones")] WarmTones,
    [Description("Warm Yellow Shade")] WarmYellowShade,
    [Description("White to Black")] WhitetoBlack,
    [Description("Yellow Greens")] YellowGreens,
    [Description("Yellow to Red")] YellowtoRed,
    [Description("Yellow-Green (Continuous)")] YellowGreenContinuous,
    [Description("Yellow-Green-Blue (Continuous)")] YellowGreenBlueContinuous,
    [Description("Yellow-Orange-Brown (Continuous)")] YellowOrangeBrownContinuous,
    [Description("Yellow-Orange-Red (Continuous)")] YellowOrangeRedContinuous,
    [Description("Yellow-Pink-Purple (Continuous)")] YellowPinkPurpleContinuous,
    [Description("Yellow-Red-Purple (Continuous)")] YellowRedPurpleContinuous,
    [Description("Yellows")] Yellows
}

public enum ComputeCovariance
{
    Covariance, MeanOnly
}

public enum ContourType
{
    Contour, ContourPolygon, ContourShell, ContourShellUp
}

public enum DataType
{
    Integer, Float
}

public enum DistanceCalculation
{
    Variance, MeanOnly
}

public enum ExtentHandling
{
    Expand, Truncate
}

public enum ExtractionArea
{
    Inside, Outside
}

public enum FilterType
{
    Low, High
}

public enum InterpolateValues
{
    None, Interpolate
}

public enum Interpolation
{
    None, Bilinear
}

public enum Method
{
    Planar, Geodesic
}

public enum MissingValues
{
    Data, Nodata
}

public enum NeighborhoodUnit
{
    Cell, Map
}

public enum NumberNeighbors
{
    Four, Eight
}

public enum OrdinarySemivariogram
{
    Spherical, Circular, Exponential, Gaussian, Linear
}

public enum RasterFormat
{
    bil, bip, bsq, dat, img, jp2, mrf, tif
}

public enum RegressionType
{
    Linear, Logistic
}

public enum Slices
{
    AllSlices, CurrentSlice
}

public enum SlopeMeasurement
{
    Degree, PercentRise
}

public enum SourceDirection
{
    FromSource, ToSource
}

public enum SliceType
{
    EqualInterval, EqualArea, NaturalBreaks
}

public enum SplineType
{
    Regularized, Tension
}

public enum StatisticsType
{
    Mean, Maximum, Median, Minimum, Percentile, Range, Std, Sum
}

public enum UniversalSemivariogram
{
    Spherical, Circular, Exponential, Gaussian, Linear
}

public enum ZoneConnectivity
{
    Within, Cross
}
