import arcpy, base64, grpc, inspect, os, subprocess, sys, threading, time, uuid
from arcpy.sa.ParameterClasses import NbrCircle, NbrRectangle
from arcpy import sa
from concurrent import futures
from glidergun_pb2 import *
from glidergun_pb2_grpc import *

port = sys.argv[1]

arcpy.CheckOutExtension("Spatial")

filename = inspect.getframeinfo(inspect.currentframe()).filename
root = os.path.dirname(os.path.abspath(filename))
d = f"{root}/_output"
q = f"{root}/_queue"

os.makedirs(q, exist_ok=True)


def target():
    process = subprocess.Popen([sys.executable, f"{root}/glidergun_proc.py", q])
    process.communicate()


thread = threading.Thread(target=target)
thread.start()


def dispatch(code):
    path = f"{q}/{str(uuid.uuid4())}.py"
    with open(path, "w") as f:
        f.write(code)
    while os.path.isfile(path):
        time.sleep(0.1)


cache = {}


class Grpc(GrpcServicer):
    def token(self, request, context):
        key = str(uuid.uuid4())
        os.makedirs(d + "/" + key)
        return TokenResponse(key=key)

    def check(self, request, context):
        raster = cache.get(request.hash)

        if raster == None:
            return RasterInfoResponse()

        if raster["key"] != request.key:
            rasterId = temp()
            set_raster(request.key, rasterId, get_raster(raster["key"], raster["id"]))
            return get_raster_info(request.key, rasterId)

        return get_raster_info(request.key, raster["id"])

    def create(self, request, context):
        rasterId = temp()
        extension = Format.Name(request.format).lower()
        filename = d + "/" + request.key + "/_" + rasterId + "." + extension
        with open(filename, "wb") as f:
            f.write(request.data)
        set_raster(request.key, rasterId, sa.Raster(filename))

        cache[request.hash] = {"key": request.key, "id": rasterId}

        return get_raster_info(request.key, rasterId)

    def save(self, request, context):
        rasterId = temp()
        extension = Format.Name(request.format).lower()
        filename = d + "/" + request.key + "/_" + rasterId + "." + extension
        raster = get_raster(request.key, request.rasterId)
        arcpy.CopyRaster_management(raster, filename)
        with open(filename, "rb") as f:
            data = f.read()
        return RasterResponse(data=data)

    def thumbnail(self, request, context):
        raster = get_raster(request.key, request.rasterId)
        return ThumbnailResponse(thumbnail=thumbnail(request.key, raster))

    def mathRaster(self, request, context):
        rasterId = temp()
        raster = get_raster(request.key, request.rasterId)
        func = getattr(sa, Op.Name(request.op))
        result = func(raster)
        set_raster(request.key, rasterId, result)
        return get_raster_info(request.key, rasterId)

    def mathRasterRaster(self, request, context):
        rasterId = temp()
        raster1 = get_raster(request.key, request.rasterId)
        raster2 = get_raster(request.key, request.raster2Id)
        func = getattr(sa, Op.Name(request.op))
        result = func(raster1, raster2)
        set_raster(request.key, rasterId, result)
        return get_raster_info(request.key, rasterId)

    def mathRasterInt(self, request, context):
        rasterId = temp()
        raster = get_raster(request.key, request.rasterId)
        func = getattr(sa, Op.Name(request.op))
        result = func(raster, request.intValue)
        set_raster(request.key, rasterId, result)
        return get_raster_info(request.key, rasterId)

    def mathRasterDouble(self, request, context):
        rasterId = temp()
        raster = get_raster(request.key, request.rasterId)
        func = getattr(sa, Op.Name(request.op))
        result = func(raster, request.doubleValue)
        set_raster(request.key, rasterId, result)
        return get_raster_info(request.key, rasterId)

    def mathIntRaster(self, request, context):
        rasterId = temp()
        raster = get_raster(request.key, request.rasterId)
        func = getattr(sa, Op.Name(request.op))
        result = func(request.intValue, raster)
        set_raster(request.key, rasterId, result)
        return get_raster_info(request.key, rasterId)

    def mathDoubleRaster(self, request, context):
        rasterId = temp()
        raster = get_raster(request.key, request.rasterId)
        func = getattr(sa, Op.Name(request.op))
        result = func(request.doubleValue, raster)
        set_raster(request.key, rasterId, result)
        return get_raster_info(request.key, rasterId)

    def mathRasterRasterRaster(self, request, context):
        rasterId = temp()
        raster1 = get_raster(request.key, request.rasterId)
        raster2 = get_raster(request.key, request.raster2Id)
        raster3 = get_raster(request.key, request.raster3Id)
        func = getattr(sa, Op.Name(request.op))
        result = func(raster1, raster2, raster3)
        set_raster(request.key, rasterId, result)
        return get_raster_info(request.key, rasterId)

    def mathRasterRasterInt(self, request, context):
        rasterId = temp()
        raster = get_raster(request.key, request.rasterId)
        raster2 = get_raster(request.key, request.raster2Id)
        func = getattr(sa, Op.Name(request.op))
        result = func(raster, raster2, request.intValue)
        set_raster(request.key, rasterId, result)
        return get_raster_info(request.key, rasterId)

    def mathRasterIntRaster(self, request, context):
        rasterId = temp()
        raster = get_raster(request.key, request.rasterId)
        raster2 = get_raster(request.key, request.raster2Id)
        func = getattr(sa, Op.Name(request.op))
        result = func(raster, request.intValue, raster2)
        set_raster(request.key, rasterId, result)
        return get_raster_info(request.key, rasterId)

    def mathRasterIntInt(self, request, context):
        rasterId = temp()
        raster = get_raster(request.key, request.rasterId)
        func = getattr(sa, Op.Name(request.op))
        result = func(raster, request.intValue, request.int2Value)
        set_raster(request.key, rasterId, result)
        return get_raster_info(request.key, rasterId)

    def mathRasterRasterDouble(self, request, context):
        rasterId = temp()
        raster = get_raster(request.key, request.rasterId)
        raster2 = get_raster(request.key, request.raster2Id)
        func = getattr(sa, Op.Name(request.op))
        result = func(raster, raster2, request.doubleValue)
        set_raster(request.key, rasterId, result)
        return get_raster_info(request.key, rasterId)

    def mathRasterDoubleRaster(self, request, context):
        rasterId = temp()
        raster = get_raster(request.key, request.rasterId)
        raster2 = get_raster(request.key, request.raster2Id)
        func = getattr(sa, Op.Name(request.op))
        result = func(raster, request.doubleValue, raster2)
        set_raster(request.key, rasterId, result)
        return get_raster_info(request.key, rasterId)

    def mathRasterDoubleDouble(self, request, context):
        rasterId = temp()
        raster = get_raster(request.key, request.rasterId)
        func = getattr(sa, Op.Name(request.op))
        result = func(raster, request.doubleValue, request.double2Value)
        set_raster(request.key, rasterId, result)
        return get_raster_info(request.key, rasterId)

    def aspect(self, request, context):
        rasterId = temp()
        raster = get_raster(request.key, request.rasterId)
        result = sa.Aspect(
            raster, "GEODESIC", ZUnit.Name(request.zUnit), "GEODESIC_AZIMUTHS"
        )
        set_raster(request.key, rasterId, result)
        return get_raster_info(request.key, rasterId)

    def color(self, request, context):
        rasterId = temp()
        inputPath = d + "/" + request.key + "/" + request.rasterId
        outputPath = d + "/" + request.key + "/" + rasterId
        layerName = temp()
        code = f"""
arcpy.MakeRasterLayer_management(arcpy.sa.Colormap(r'{inputPath}', '{sanitize(request.colorramp)}'), '{layerName}')
arcpy.CopyRaster_management('{layerName}', r'{outputPath}')
"""
        dispatch(code)
        return get_raster_info(request.key, rasterId)

    def composite(self, request, context):
        rasterId = temp()
        inputPaths = ", ".join(
            map(
                lambda x: "r'" + d + "/" + request.key + "/" + x + "'",
                request.rasterIds,
            )
        )
        outputPath = d + "/" + request.key + "/" + rasterId
        dispatch(f"arcpy.ia.CompositeBand([{inputPaths}]).save(r'{outputPath}')")
        return get_raster_info(request.key, rasterId)

    def extract(self, request, context):
        rasterId = temp()
        result = sa.Raster(
            d + "/" + request.key + "/" + request.rasterId + "c" + str(request.band)
        )
        set_raster(request.key, rasterId, result)
        return get_raster_info(request.key, rasterId)

    def extractValues(self, request, context):
        directory = d + "/" + request.key
        inputPath = directory + "/" + request.rasterId
        inShapefile = temp() + ".shp"
        outShapefile = temp() + ".shp"
        inShapefilePath = directory + "/" + inShapefile
        outShapefilePath = directory + "/" + outShapefile
        arcpy.CreateFeatureclass_management(directory, inShapefile, "POINT")
        cursor = arcpy.da.InsertCursor(inShapefilePath, ["SHAPE@XY"])
        for p in request.points:
            cursor.insertRow([(p.x, p.y)])
        del cursor
        code = f"arcpy.sa.ExtractValuesToPoints(r'{inShapefilePath}', r'{inputPath}', r'{outShapefilePath}')"
        dispatch(code)
        values = [
            row[0] for row in arcpy.da.SearchCursor(outShapefilePath, "RASTERVALU")
        ]
        return ExtractValuesResponse(values=values)

    def focal(self, request, context):
        rasterId = temp()
        inputPath = d + "/" + request.key + "/" + request.rasterId
        outputPath = d + "/" + request.key + "/" + rasterId
        nbr = (
            f"arcpy.sa.NbrCircle({request.radius})"
            if request.radius > 0
            else f"arcpy.sa.NbrRectangle({request.width}, {request.height})"
        )
        statistics = Statistics.Name(request.statistics)
        data = "DATA" if request.ignoreNodata else "NODATA"
        code = f"arcpy.sa.FocalStatistics(r'{inputPath}', {nbr}, '{statistics}', '{data}').save(r'{outputPath}')"
        dispatch(code)
        return get_raster_info(request.key, rasterId)

    def hillshade(self, request, context):
        rasterId = temp()
        raster = get_raster(request.key, request.rasterId)
        result = sa.Hillshade(
            raster,
            azimuth=request.azimuth,
            altitude=request.altitude,
            z_factor=request.zFactor,
        )
        set_raster(request.key, rasterId, result)
        return get_raster_info(request.key, rasterId)

    def project(self, request, context):
        rasterId = temp()
        raster = get_raster(request.key, request.rasterId)
        outputPath = d + "/" + request.key + "/" + rasterId
        arcpy.ProjectRaster_management(
            raster,
            outputPath,
            out_coor_system=arcpy.SpatialReference(request.outWkid),
            in_coor_system=arcpy.SpatialReference(request.inWkid),
        )
        return get_raster_info(request.key, rasterId)

    def randomize(self, request, context):
        rasterId = temp()
        raster = get_raster(request.key, request.rasterId)
        result = sa.CreateRandomRaster(
            cell_size=raster.meanCellWidth, extent=raster.extent
        )
        set_raster(request.key, rasterId, result)
        return get_raster_info(request.key, rasterId)

    def resample(self, request, context):
        rasterId = temp()
        inputPath = d + "/" + request.key + "/" + request.rasterId
        outputPath = d + "/" + request.key + "/" + rasterId
        code = f"arcpy.Resample_management(r'{inputPath}', r'{outputPath}', {request.cellSize}, '{Resampling.Name(request.resampling)}')"
        dispatch(code)
        return get_raster_info(request.key, rasterId)

    def slope(self, request, context):
        rasterId = temp()
        inputPath = d + "/" + request.key + "/" + request.rasterId
        outputPath = d + "/" + request.key + "/" + rasterId
        code = f"arcpy.sa.Slope(r'{inputPath}', 'DEGREE', {request.zFactor}, 'GEODESIC', '{ZUnit.Name(request.zUnit)}').save(r'{outputPath}')"
        dispatch(code)
        return get_raster_info(request.key, rasterId)


def temp():
    return str(uuid.uuid4())[:8]


def sanitize(text: str):
    return text.replace("'", "").replace('"', "")


def set_raster(key, rasterId, raster):
    raster.save(d + "/" + key + "/" + rasterId)


def get_raster(key, rasterId):
    return sa.Raster(d + "/" + key + "/" + rasterId)


def thumbnail(key, raster):
    filename = d + "/" + key + "/_" + temp()
    outputCellSize = raster.meanCellWidth * raster.width / 600
    arcpy.Resample_management(raster, filename, outputCellSize)

    if hasattr(sa.Raster, "getColormap") and raster.getColormap("") != None:
        arcpy.CopyRaster_management(
            filename,
            filename + ".png",
            colormap_to_RGB="ColormapToRGB",
            pixel_type="8_BIT_UNSIGNED",
        )
    else:
        arcpy.CopyRaster_management(
            filename,
            filename + ".png",
            pixel_type="8_BIT_UNSIGNED",
            scale_pixel_value="ScalePixelValue",
        )

    with open(filename + ".png", "rb") as f:
        data = base64.b64encode(f.read()).decode("utf-8")

    return f"<img src='data:image/png;base64, {data}' />"


def get_raster_info(key, rasterId):
    raster = get_raster(key, rasterId)
    return RasterInfoResponse(
        id=rasterId,
        bandCount=raster.bandCount,
        width=raster.width,
        height=raster.height,
        pixelType=raster.pixelType,
        isInteger=raster.isInteger,
        noDataValue=raster.noDataValue,
        minimum=raster.minimum,
        maximum=raster.maximum,
        mean=raster.mean,
        standardDeviation=raster.standardDeviation,
        meanCellWidth=raster.meanCellWidth,
        meanCellHeight=raster.meanCellHeight,
        wkid=raster.spatialReference.factoryCode,
        wkt=raster.spatialReference.exportToString(),
        xmin=raster.extent.XMin,
        ymin=raster.extent.YMin,
        xmax=raster.extent.XMax,
        ymax=raster.extent.YMax,
    )


def serve():
    size = 41943040
    server = grpc.server(
        futures.ThreadPoolExecutor(max_workers=10),
        options=[
            ("grpc.max_send_message_length", size),
            ("grpc.max_receive_message_length", size),
        ],
    )
    add_GrpcServicer_to_server(Grpc(), server)
    server.add_insecure_port("[::]:" + port)
    server.start()
    print(f"Server started at port {port}.")
    server.wait_for_termination()


if __name__ == "__main__":
    serve()
