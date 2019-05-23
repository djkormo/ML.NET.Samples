using System;
using System.IO;
using System.Linq;
using Microsoft.ML;
using IrisTrainModelML.Model.DataModels;
namespace IrisTrainModel
{
    //Machine Learning model to load and use for predictions
    class Program
    {
        
        private const string MODEL_FILEPATH = @"../MLModel.zip";

        //Dataset to use for predictions 
        private const string DATA_FILEPATH = @"data/iris.csv";
        static void Main(string[] args)
        {
            MLContext mlContext = new MLContext();
            ITransformer mlModel = mlContext.Model.Load(MODEL_FILEPATH, out DataViewSchema inputSchema);
            var predEngine = mlContext.Model.CreatePredictionEngine<ModelInput, ModelOutput>(mlModel);

            // Test data for prediction

            ModelInput SampleData = new ModelInput()
            {
                SepalLength = 3.3f,
                SepalWidth = 1.6f,
                PetalLength = 0.2f,
                PetalWidth = 5.1f
            };
            
            var prediction = predEngine.Predict(SampleData);

            Console.WriteLine($"SepalLength --> Actual value: [{SampleData.SepalLength}]");
            Console.WriteLine($"SepalWidth --> Actual value: [{SampleData.SepalWidth}]");
            Console.WriteLine($"PetalLength --> Actual value: [{SampleData.PetalLength}]");
            Console.WriteLine($"PetalWidth --> Actual value: [{SampleData.PetalWidth}]");
            Console.WriteLine($"Predicted flower type is: {prediction.Prediction}");
            Console.WriteLine("Press any key");
            Console.ReadKey();
        }
    }
}
