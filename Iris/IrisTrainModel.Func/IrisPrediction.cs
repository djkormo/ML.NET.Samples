
using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.ML;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
namespace Company.Function
{
    /* 
    public static Class ModelInput 
    {
       
        public float SepalLength { get; set; }

        public float SepalWidth { get; set; }

        public float PetalLength { get; set; }

        public float PetalWidth { get; set; }

        public string Label { get; set; }

    }

   public static class ModelOutput
    {
        // ColumnName attribute is used to change the column name from
        // its default value, which is the name of the field.
        public String Prediction { get; set; }
        public float[] Score { get; set; }
    }
    
    */
    public static class IrisPrediction
    {
        [FunctionName("IrisPrediction")]
        public static IActionResult Run(
    [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)]HttpRequest req,
    [Blob("models/model.zip", FileAccess.Read)] Stream serializedModel,
    ILogger log )
{
    // Workaround for Azure Functions Host
    /* 
    if (typeof(Microsoft.ML.Runtime.Data.LoadTransform) == null ||
        typeof(Microsoft.ML.Runtime.Learners.LinearClassificationTrainer) == null ||
        typeof(Microsoft.ML.Runtime.Internal.CpuMath.SseUtils) == null ||
        typeof(Microsoft.ML.Runtime.FastTree.FastTree) == null)
    {
        log.Error("Error loading ML.NET");
        return new StatusCodeResult(500);
    }
    */

    //Read incoming request body
    string requestBody = new StreamReader(req.Body).ReadToEnd();


    log.Info(requestBody);

    //Bind request body to IrisData object
    IrisData data = JsonConvert.DeserializeObject<ModelInput>(requestBody);

    //Load prediction model
    var model = PredictionModel.ReadAsync<IrisData, IrisPrediction>(serializedModel).Result;

    //Make prediction
    ModelOutput prediction = model.Predict(data);

    //Return prediction
    return (IActionResult)new OkObjectResult(prediction.PredictedLabels);
}
}
}
