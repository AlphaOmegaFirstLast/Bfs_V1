//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Bfs.Core.Services.ChatGpt
//{
//    internal class TestDataGenerator
//    {
//        public async Task<string> GetApiSchemaAsync(string host)
//        {
//            var apiUrl = $@"{host}/swagger/v1/swagger.json";

//            using (var httpClient = new HttpClient())
//            {
//                var response = await httpClient.GetAsync(apiUrl);
//                if (response.IsSuccessStatusCode)
//                {
//                    var schema = await response.Content.ReadAsStringAsync();
//                    return schema;
//                }
//                else
//                {
//                    return response.StatusCode.ToString();
//                }
//            }
//        }

//        //public void GetData(string host, string TableNameCapital)
//        //{
//        //    var host = "http://localhost:2101";
//        //    var swaggerSchemaText = Task.Run(() => GetApiSchemaAsync(host).Result;

//        //    var SwaggerDocument = Newtonsoft.Json.JsonConvert.DeserializeObject<SwaggerDocument>(swaggerSchemaText);
//        //    var tableDefinition = SwaggerDocument.Components.Schemas.FirstOrDefault(d => d.Key.Equals(TableNameCapital, StringComparison.OrdinalIgnoreCase)).Value;
//        //    var tableProperties = System.Text.Json.JsonSerializer.Serialize(tableDefinition.Properties);

//        //var aiMessage = $@"Generate 5 JSON data records following the schema of table {TableNameCapital} below. where Id in range[1,5], isDeleted=0. " ;
//        // aiMessage = aiMessage + tableProperties.Replace(@",""Properties"":null,""Required"":null", "");

//        //    // ContinueWith is used, since we cannot use async await here because the button click event handler cannot be async void
//        //    ChatGpt.SendCompletionRequest(aiMessage).ContinueWith(task =>
//        //    {
//        //        if (task.Status == TaskStatus.RanToCompletion)
//        //        {
//        //            var aiResponse = task.Result;
//        //            FileManager.SaveFile(txtOutputTestDataFile.Text, aiResponse);
//        //            SetMessage(aiResponse);
//        //        }
//        //        else if (task.IsFaulted)
//        //        {
//        //            SetMessage("Error: " + task.Exception?.GetBaseException().Message);
//        //        }
//        //    }, TaskScheduler.FromCurrentSynchronizationContext());
//        //}
//    }
//}
