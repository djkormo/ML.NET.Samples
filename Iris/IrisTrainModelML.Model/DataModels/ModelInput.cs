//*****************************************************************************************
//*                                                                                       *
//* This is an auto-generated file by Microsoft ML.NET CLI (Command-Line Interface) tool. *
//*                                                                                       *
//*****************************************************************************************

using Microsoft.ML.Data;

namespace IrisTrainModelML.Model.DataModels
{
    public class ModelInput
    {
        [ColumnName("SepalLength"), LoadColumn(0)]
        public string SepalLength { get; set; }


        [ColumnName("SepalWidth"), LoadColumn(1)]
        public string SepalWidth { get; set; }


        [ColumnName("PetalLength"), LoadColumn(2)]
        public string PetalLength { get; set; }


        [ColumnName("PetalWidth"), LoadColumn(3)]
        public string PetalWidth { get; set; }


        [ColumnName("Label"), LoadColumn(4)]
        public string Label { get; set; }


    }
}
