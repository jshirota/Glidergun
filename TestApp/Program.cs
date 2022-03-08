using Glidergun;

await Task.Delay(2000);

SpatialAnalyst sa = new(args[0]);

var dem = await sa.CreateAsync(@"..\..\..\..\Data\dem.tif");

var r = dem.Randomize();

Console.WriteLine(r);
