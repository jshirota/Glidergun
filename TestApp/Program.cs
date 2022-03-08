using Glidergun;

await Task.Delay(2000);

SpatialAnalyst sa = new(args[0]);

var dem = await sa.CreateAsync(@"..\..\..\..\Data\dem.tif");

var dem_ft = 3.28084 * dem;

Console.WriteLine(dem_ft);
