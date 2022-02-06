using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArticlesServerBL.Models;
using System.IO;
using System.Security.Cryptography;


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
        [Route("GetInitialInterests")]
        [HttpGet]
        public List<Interest> GetInitialInterests()
        {
            List<Interest> list = context.GetInitialInterest();
            if (list!=null)
                Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
            else
                Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
           
            return list;
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
