//*****************************************************************************************
//*                                                                                       *
//* This is an auto-generated file by Microsoft ML.NET CLI (Command-Line Interface) tool. *
//*                                                                                       *
//*****************************************************************************************

using System;
using System.IO;
using System.Linq;
using Microsoft.ML;
using IrisTrainModelML.Model.DataModels;


namespace IrisTrainModelML.ConsoleApp
{
    class Program
    {
        //Machine Learning model to load and use for predictions
        private const string MODEL_FILEPATH = @"MLModel.zip";

        //Dataset to use for predictions 
        private const string DATA_FILEPATH = @"data/iris.csv";

        static void Main(string[] args)
        {
            MLContext mlContext = new MLContext();

            // Training code used by ML.NET CLI and AutoML to generate the model
            Console.WriteLine("=============== Creating model ===============");
            ModelBuilder.CreateModel();
            Console.WriteLine("=============== Model created, hit any key to finish ===============");
            Console.ReadKey();
            ITransformer mlModel = mlContext.Model.Load(MODEL_FILEPATH, out DataViewSchema inputSchema);
            var predEngine = mlContext.Model.CreatePredictionEngine<ModelInput, ModelOutput>(mlModel);

            Random random = new Random();  
            //index = random.Next(1, 1000);  
            int y;

            for (int i=0;i<=10;i++)
            {

            // Create sample data to do a single prediction with it
            y= random.Next(1, 140); 
            ModelInput sampleData = CreateSingleDataSample(mlContext, DATA_FILEPATH,y);

            Console.WriteLine($"Iteration [{i}] for index {y}"); 
            // Try a single prediction
            ModelOutput predictionResult = predEngine.Predict(sampleData);

            Console.WriteLine($"SepalLength --> Actual value: [{sampleData.SepalLength}]");
            Console.WriteLine($"SepalWidth --> Actual value: [{sampleData.SepalWidth}]");
            Console.WriteLine($"PetalLength --> Actual value: [{sampleData.PetalLength}]");
            Console.WriteLine($"PetalWidth --> Actual value: [{sampleData.PetalWidth}]");
            Console.WriteLine($"Single Prediction --> Actual value: [{sampleData.Label}]"); 
            Console.WriteLine($"Predicted value: [{predictionResult.Prediction}]");
            Console.WriteLine($"Predicted scores: [{String.Join(",", predictionResult.Score)}]");
            } // of for loop 
            Console.WriteLine("=============== End of process, hit any key to finish ===============");
            Console.ReadKey();
        }

        // Method to load single row of data to try a single prediction
        // You can change this code and create your own sample data here (Hardcoded or from any source)
        private static ModelInput CreateSingleDataSample(MLContext mlContext, string dataFilePath,int index
        )
        {
            // Read dataset to get a single row for trying a prediction          
            IDataView dataView = mlContext.Data.LoadFromTextFile<ModelInput>(
                                            path: dataFilePath,
                                            hasHeader: true,
                                            separatorChar: ',',
                                            allowQuoting: true,
                                            allowSparse: false);

            // Here (ModelInput object) you could provide new test data, hardcoded or from the end-user application, instead of the row from the file.
            ModelInput sampleForPrediction = mlContext.Data.CreateEnumerable<ModelInput>(dataView, false)
                                                                        //.First();
                                                                        .ElementAt(index);
            return sampleForPrediction;
        }

        public static string GetAbsolutePath(string relativePath)
        {
            FileInfo _dataRoot = new FileInfo(typeof(Program).Assembly.Location);
            string assemblyFolderPath = _dataRoot.Directory.FullName;

            string fullPath = Path.Combine(assemblyFolderPath, relativePath);

            return fullPath;
        }
    }
}
