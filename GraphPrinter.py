import struct
import numpy as np
import matplotlib
import matplotlib.pyplot as plt


typeSize = 8
unpackType = 'd'
inFileName = "TheoricalRML"
outFileName = inFileName +"Img.png"
res = 1000

heatMap = []
lineCount = 0
rowCount =0

with open(inFileName, "rb", buffering = 10000000) as f:
    
    lineCount = struct.unpack('i', f.read(4))[0]
    rowCount = struct.unpack('i', f.read(4))[0]
    #print(lineCount)
    #print(rowCount)

    for line in range(lineCount):
        heatMapLine =[]
        
#        lineBytes = f.read(typeSize * rowCount)
#        values = struct.unpack(unpackType, lineBytes)
#        
#        for value in values:
#            heatMapLine.append(value)
        for rowNumber in range(rowCount):
            byte = f.read(typeSize)
            value = struct.unpack(unpackType, byte)[0]
            heatMapLine.append(value)
            
        heatMap.append(heatMapLine)
        
heatMap = np.asarray(heatMap)  



fig, ax = plt.subplots()
im = ax.imshow(heatMap, extent = [-((rowCount-1)/2), (rowCount-1)/2, -((lineCount-1)/2) , (lineCount-1)/2])

plt.colorbar(im)

plt.savefig(outFileName, dpi=res)
plt.show()
