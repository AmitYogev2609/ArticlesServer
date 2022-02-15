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

namespace ArticlesServer.Controllers
{
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
        public async Task<ActionResult> SignUPWithIamge()
        {
            try
            { 
                if (!Request.Form.ContainsKey("myJsonObject") || (Request.Form.Files == null || !Request.Form.Files.Any()))
                { return BadRequest(); }
                var jsonModel = Request.Form.First(f => f.Key == "myJsonObject").Value;

                //Deserilize
                var stringReader = new StringReader(jsonModel);
                var jsonFile = await stringReader.ReadToEndAsync();
                User theUser = JsonSerializer.Deserialize<User>(jsonFile);
                theUser = context.Signup(theUser);
                if (theUser == null)
                    return BadRequest();
                IFormFile file = Request.Form.Files.First();
                if (file == null)
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
