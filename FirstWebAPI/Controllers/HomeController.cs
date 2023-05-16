using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;


namespace FirstWebAPI.Controllers
{
    [Route("api/[controller]")] //URL: localhost/api/home 
    [ApiController]
    public class HomeController : ControllerBase
    {
        //URL: localhost/api/home 
        [HttpGet(template: "")]
        public string Get()
        {
            return DateTime.Now.ToString();
        }
        //URL: localhost/api/home/5 
        [HttpGet(template: "{days}")]
        public string NextDate(int days)
        {
            return DateTime.Now.AddDays(days).ToString();
        }
        //URL: localhost/api/home/message/5 
        [HttpGet(template: "message/{number}")]
        public HttpResponseMessage GetInfo(string number)
        {
            MediaTypeHeaderValue mediaType = new MediaTypeHeaderValue("application/json");

            if (string.IsNullOrEmpty(number))
            {
                var responseMessage = new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest)
                {
                    Content = new StringContent("Invalid value passed.", mediaType),
                    ReasonPhrase = "Bad Request By Client"
                };

                return responseMessage;

            } else
            {
                var responseMessage = new HttpResponseMessage(System.Net.HttpStatusCode.OK)
                {
                    Content = new StringContent(
                        DateTime.Now.AddDays(Convert.ToInt32(number)).ToString() + " is the new date",
                        mediaType),
                    ReasonPhrase = $"Successfully added {number} to Date"
                };
                return responseMessage;
            }
        }

        [HttpGet(template: "senddata/{value}")]
        public IActionResult SendData(int value)
        {
            var item = new { ItemId = 500, Value = value, Message = "Exists in DB." };

            if (value == 0)
                return BadRequest();
            else if (value < 10)
                return NotFound();
            else
                //return Ok("Value is found in the DB.");
                return Ok(item);
        }
        [HttpGet(template: "actionresult/{value}")]
        public ActionResult<TestClass> SendActionResult(int value)
        {
            TestClass tc = new() { NumProp = value, NameProp = "Exists in DB." };

            if (value == 0)
                return BadRequest();
            else if (value < 10)
                return NotFound();
            else
                //return Ok("Value is found in the DB.");
                return Ok(tc);
        }
        //URL: api/home/insert
        [HttpPost(template: "")]
        public IActionResult InsertData(TestClass obj)
        {
             if(obj == null)
                return BadRequest();
            else
            {
                TestClass tc = new TestClass() { NumProp = obj.NumProp, NameProp = obj.NameProp};
                return Ok(tc);
            }
        }
        [HttpPut(template:"{id}")]
        public IActionResult PutData(int id, TestClass model)
        {
            if(id==0)
                return BadRequest(); 
            if(model.NumProp!=id)
                return NotFound(id);
            return Ok(model);
        }
        [HttpDelete(template:"{id}")]
        public IActionResult DeleteData(int id) { if(id==0) return BadRequest(); return Ok(id); }
    }

    public class TestClass { public int NumProp { get; set; } public string NameProp { get; set; } }
}
