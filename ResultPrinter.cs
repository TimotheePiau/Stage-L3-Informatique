using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace StageL3_ManhattanLatticeInhomogeneousWalk
{
    public enum DataType {HEATMAP, PATH};

    class ResultPrinter
    {
        public string fileName { get; private set; }
        public string filePath
        {
            get
            {
                return dataDirectory + fileName;
            }
        }
        public DataType dataType { get; private set; }

        private string dataDirectory = "../../../Data/";


        public ResultPrinter(string fileName, DataType dataType)
        {
            this.fileName = fileName;
            this.dataType = dataType;
        }

        public void PrintByteResult(double[,] matrix)
        {
            using (BinaryWriter binaryWritter = new BinaryWriter(new BufferedStream(File.Open(filePath, FileMode.Create), 2000000)))
            {
                binaryWritter.Write(matrix.GetLength(0));
                binaryWritter.Write(matrix.GetLength(1));

                for (int l = matrix.GetLength(0) - 1; l >= 0; l--)
                {
                    for (int r = 0; r < matrix.GetLength(1); r++)
                    {
                        binaryWritter.Write(matrix[l, r]);
                    }
                }
            }
        }

        //using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
        //{
        //    for (int l = matrix.GetLength(0) - 1; l > 0; l--)
        //    {
        //        PrintByteLine(matrix, l, fileStream);
        //    }

        //    Console.WriteLine("The data was written to {0} " + ".", fileStream.Name);
        //}

        //private void PrintByteLine(float[,] matrix, int line, FileStream fileStream)
        //{
        //    byte[] byteLine = ConvertLineToByte(matrix, line);

        //    for (int r = 0; r < matrix.GetLength(1); r++)
        //    {
        //        fileStream.WriteByte(byteLine[r]);
        //    }

        //    //fileStream.Seek(0, SeekOrigin.Begin);
        //}

        //private byte[] ConvertLineToByte(float[,] matrix, int lineNumber)
        //{
        //    float[] line = GetLine(matrix, lineNumber);
        //    byte[] byteLine = new byte[4 * matrix.GetLength(1)];

        //    Buffer.BlockCopy(line, 0, byteLine, 0, byteLine.Length);

        //    return byteLine;
        //}

        //private float[] GetLine(float[,] matrix, int lineNumber)
        //{
        //    float[] line = new float[matrix.GetLength(1)];

        //    for(int r = 0; r < matrix.GetLength(1); r++)
        //    {
        //        line[r] = matrix[lineNumber, r];
        //    }

        //    return line;
        //}

        public void PrintResult(double[,] matrix, string title = "Graph")
        {
            using (StreamWriter streamWriter = new StreamWriter(filePath, true))
            {
                List<string> matrixFormated = ConvertMatrix(matrix);

                streamWriter.WriteLine(title);
                streamWriter.WriteLine(dataType.ToString("g"));
                streamWriter.WriteLine("{0}x{1}", matrix.GetLength(0), matrix.GetLength(1));
                streamWriter.WriteLine("");

                foreach(string line in matrixFormated)
                {
                    streamWriter.WriteLine(line);
                }

                streamWriter.WriteLine("");
            }
        }

        private List<string> ConvertMatrix(double[,] matrix)
        {
            List<string> convertedMatrix = new List<string>();
            string currentLine = "[";

            for (int i = matrix.GetLength(0) - 1; i > 0; i--)
            {
                currentLine += ConvertLine(matrix, i);
                convertedMatrix.Add(currentLine);

                currentLine = "";
            }

            if (matrix.GetLength(0) > 0)
                currentLine += ConvertLine(matrix, 0);

            currentLine += "]";

            convertedMatrix.Add(currentLine);

            return convertedMatrix;
        }

        private string ConvertLine(double[,] matrix, int lineIndex)
        {
            string line = "";

            if (lineIndex > matrix.GetLength(0))
                return line;

            line = "[";
            if(matrix.GetLength(1) > 0)
            {
                for (int i = 0; i < matrix.GetLength(1) - 1; i++)
                {
                    line += "(" + matrix[lineIndex, i] + "), ";
                }
                line += "(" + matrix[lineIndex, matrix.GetLength(1)-1] + ")";
            }
            line += "]";

            return line;
        }
    }
}
