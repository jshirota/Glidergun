import arcpy, glob, os, time

q = sys.argv[1]

arcpy.CheckOutExtension("Spatial")

while True:
    time.sleep(0.1)
    for f in glob.glob(f"{q}/*.py"):
        try:
            code = open(f, "r").read()
            exec(code)
        except Exception as e:
            print(str(e))
        finally:
            os.remove(f)
