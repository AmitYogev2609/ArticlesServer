using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArticlesServerBL.Models;
using System.IO;
using System.Security.Cryptography;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ArticlesServer.Controllers
{

    
    public class JsonModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
            {
                throw new ArgumentNullException(nameof(bindingContext));
            }

            // Check the value sent in
            var valueProviderResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            if (valueProviderResult != ValueProviderResult.None)
            {
                bindingContext.ModelState.SetModelValue(bindingContext.ModelName, valueProviderResult);

                // Attempt to convert the input value
                var valueAsString = valueProviderResult.FirstValue;
                var result = Newtonsoft.Json.JsonConvert.DeserializeObject(valueAsString, bindingContext.ModelType);
                if (result != null)
                {
                    bindingContext.Result = ModelBindingResult.Success(result);
                    return Task.CompletedTask;
                }
            }

            return Task.CompletedTask;
        }
    }

    [Route("ArtiFindAPI")]
    [ApiController]
    public class ArtiFindController : ControllerBase
    {
        #region Add connection to the db context using dependency injection
        ArtiFindDBContext context;
        public ArtiFindController(ArtiFindDBContext context)
        {
            this.context = context;
        }
        #endregion
        [Route("Login")]
        [HttpGet]
        public User LogIn([FromQuery] string email, [FromQuery] string password) 
        {
            User user = context.LogIn(email, password);
            if(user != null)
            {
                HttpContext.Session.SetObject("theUser", user);
                Response.StatusCode= (int)System.Net.HttpStatusCode.OK;
                return user;
            }
            else
            {
                Response.StatusCode= (int)System.Net.HttpStatusCode.Forbidden;
                return null;
            }
        }
        [Route("EmailExist")]
        [HttpGet]
        public bool EmailExist([FromQuery] string email)
        {
            bool exsit = context.EmailExist(email);

            if (exsit)
                Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
            else
                Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
            return exsit;

        }
        [Route("UserNameExist")]
        [HttpGet]
        public bool UserNameExist([FromQuery] string UserName)
        {
            bool exsit = context.UserNameExist(UserName);
            if(exsit)
            Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
            else
                Response.StatusCode=(int)System.Net.HttpStatusCode.Forbidden;
            return exsit;

        }
        [Route("GetInterests")]
        [HttpGet]
        public List<Interest> GetInterests()
        {
            List<Interest> list = context.GetInterest();
            if (list!=null)
                Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
            else
                Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
           
            return list;
        }


        [Route("SignUpWithImage")]
        [HttpPost]
        public async Task<ActionResult> SignUPWithIamge([ModelBinder(BinderType = typeof(JsonModelBinder))] User myJsonObject,
    IList<IFormFile> file)
        {
            try
            { 
                User theUser = context.Signup(myJsonObject);
                if (theUser == null)
                    return BadRequest();


                if (file.First() == null)
                    return BadRequest();

                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", $"{theUser.UserId}.jpg");
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.First().CopyToAsync(stream);

                }
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest();
            }

        }

            [Route("SignUpWithImage_Old")]
        [HttpPost]
        public async Task<ActionResult> SignUPWithIamge(IFormCollection data, IFormFile file)
        {
            try
            {
                bool b1 = Request.Form.ContainsKey("myJsonObject");
                bool b2 = Request.Form.Files == null;
                bool b3 = !Request.Form.Files.Any();
                if (!Request.Form.ContainsKey("myJsonObject") || Request.Form.Files == null || Request.Form.Files.Any())
                { return BadRequest(); }
                var jsonModel = Request.Form.First(f => f.Key == "myJsonObject").Value;
                
                //Deserilize
                JsonSerializerOptions options = new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.Preserve,
                    Encoder = JavaScriptEncoder.Create(UnicodeRanges.Hebrew, UnicodeRanges.BasicLatin),
                    PropertyNameCaseInsensitive = true
                };
                var stringReader = new StringReader(jsonModel);
                var jsonFile = await stringReader.ReadToEndAsync();
                User theUser = JsonSerializer.Deserialize<User>(jsonFile, options);
                theUser = context.Signup(theUser);
                if (theUser == null)
                    return BadRequest();

                var stringFile = Request.Form.First(f => f.Key == "file").Value;
                var theFile = new StreamReader(stringFile);

                IFormFile file1 = Request.Form.Files.First();
                if (file1 == null)
                    return BadRequest();

                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", $"{theUser.UserId}.jpg");
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                    
                }
                 return Ok();
            }
            catch (Exception e)
            { 
                return BadRequest(); 
            }
        }
        //sign up without image
        [Route("SignUp")]
        [HttpPost]
        public async Task<ActionResult> Signup([FromBody]User theUser)
        {
            try
            { 
            theUser = context.Signup(theUser);
            if (theUser == null)
                return BadRequest();
            return Ok();
            }
            catch(Exception e)
            {
                return BadRequest();
            }
        }
        [Route("GetArticles")]
        [HttpGet]
        public List<Article> GetArticles()
        {
            User user = HttpContext.Session.GetObject<User>("theUser");
            List<Article> articles = context.GetFollwedArticles(user);
            if(articles == null)
                Response.StatusCode=(int)System.Net.HttpStatusCode.NotFound;
            Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
            return articles;
        }
        [Route("GetUser")]
        [HttpGet]
        public List<User> GetUsers()
        {
            List<User> users = context.GetUsers();
            if(users == null)
                Response.StatusCode =(int)System.Net.HttpStatusCode.NotFound;
            Response.StatusCode= (int)System.Net.HttpStatusCode.OK;
            return users;
        }
        [Route("GetAllArticles")]
        [HttpGet]
        public List<Article> GetAllArticles()
        {
            List<Article> articles=context.GetAllArticles();
            if(articles == null)
                Response.StatusCode=((int)System.Net.HttpStatusCode.NotFound);
            Response.StatusCode= (int)System.Net.HttpStatusCode.OK;
            return articles;
        }
        //[Route("SendResetCode")]
        //[HttpGet]
        //public User CheckEmailAndSentCode([FromQuery] string email)
        //{
        //    string resetCode = context.checkEmailAndGetCode(email);
        //    if (resetCode != null)
        //    {

        //        Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
        //        return resetCode;
        //    }
        //    else
        //    {
        //        Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
        //        return null;
        //    }
        //}
    }
        }
