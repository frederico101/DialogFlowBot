using System.IO;
using Google.Cloud.Dialogflow.V2;
using Google.Protobuf;
using Microsoft.AspNetCore.Mvc;

namespace Pizzaria.Controllers
{
    [ApiController]
    [Route("pedido")]
    public class PedidoController : ControllerBase
    {

        private static readonly JsonParser jsonParser = new JsonParser(JsonParser.Settings
        .Default.WithIgnoreUnknownFields(true));
         
        [Route("")]
        public IActionResult BoasVindas() => Ok("Seja bem vindo a Raj's pizzaria");

        [HttpPost]
        public ContentResult DialogAction()
        {
            WebhookRequest request;
            using (var reader = new StreamReader(Request.Body))
            { request = jsonParser.Parse<WebhookRequest>(reader); }

            double total = 0;
            if (request.QueryResult.Action == "pizza")
            {
                var requestParameters = request.QueryResult.Parameters;
                total = requestParameters.Fields["quantidade"].NumberValue;
            }

            WebhookResponse response = new WebhookResponse
            {

                FulfillmentText = $@"Seu pedido sera entregue em breve, obrigado por escolher a nossa pizzaria"
            };

            string responseJson = response.ToString();

            return Content(responseJson, "application/json");
        }
    }
}