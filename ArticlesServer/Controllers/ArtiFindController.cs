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
            try
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
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Response.StatusCode= (int)System.Net.HttpStatusCode.Forbidden;
                return null;
            }
        }
        [Route("LogInWithoutSession")]
        [HttpGet]
        public User LogInWithoutSession([FromQuery] string email, [FromQuery] string password)
        {
            User user = context.LogIn(email, password);
            if (user != null)
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
                return user;
            }
            else
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
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
            //List<Interest> newList = new List<Interest>();
            //newList.Add(list[num-1]);
            //foreach (Interest i in newList)
            //{
            //    foreach (ArticleInterestType t in i.ArticleInterestTypes)
            //    {
            //        t.Article.HtmlText = "kuku";
            //        foreach(ArticleInterestType ii in t.Article.ArticleInterestTypes)
            //        {
            //            ii.Article.HtmlText = "kuku";
            //        }
            //    }
            //}
            //return newList;
            //foreach (Interest i in list)
            //{
            //    foreach (ArticleInterestType t in i.ArticleInterestTypes)
            //    {
            //        t.Article.HtmlText = "kuku";
            //        foreach (ArticleInterestType ii in t.Article.ArticleInterestTypes)
            //        {
            //            ii.Article.HtmlText = "kuku";
            //        }
            //    }
            //}
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

                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/UserImage", $"{theUser.UserId}.jpg");
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

                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/UserImage", $"{theUser.UserId}.jpg");
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
        
        [Route("GetFavoriteArticles")]
        [HttpGet]
        public List<Article> GetFavoriteArticles()
        {
            User user = HttpContext.Session.GetObject<User>("theUser");
            List<Article> articles = context.GetFavoriteArticles(user);
            if(articles == null)
                Response.StatusCode=(int)System.Net.HttpStatusCode.NotFound;
            Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
            return articles;
        }
        [Route("UploadArticle")]
        [HttpPost]
        public async Task<ActionResult> AddArticle([ModelBinder(BinderType = typeof(JsonModelBinder))] Article myJsonObject,
   IList<IFormFile> file)
        {
            try
            {
                Article article=context.addArticle(myJsonObject);
               
                if (article == null)
                    return BadRequest();


                if (file.First() == null)
                    return BadRequest();

                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/ArticleImage", $"{article.ArticleId}.jpg");
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
        [Route("UptadeFavoriteArticle")]
        [HttpPost]
        public async Task<User> UptadeFavoriteArticle([FromBody] Article article)
        {
            User user= HttpContext.Session.GetObject<User>("theUser");
            if (!context.UptadeFavoriteArticle(article,user))
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
                return null;
            }
            User newuser = context.LogIn(user.Email, user.Pswd);
            if(newuser==null)
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
                return null;
            }
            Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
            return newuser;
        }
        [Route("RemoveFavoriteArticle")]
        [HttpPost]
        public async Task<User> RemoveFavoriteArticle([FromBody] Article article)
        {
            User user = HttpContext.Session.GetObject<User>("theUser");
            if (!context.RemoveFavoriteArticle(article, user))
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
                return null;
            }
            User newuser = context.LogIn(user.Email, user.Pswd);
            if (newuser == null)
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
                return null;
            }
            Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
            return newuser;
        }
        [Route("UnFollowInterest")]
        [HttpGet]
        public User UnFollowInterest([FromQuery] int userid,[FromQuery] int interestid)
        {
            bool suc= context.UnFollowInterest(userid,interestid);
            if (suc)
            {
                Response.StatusCode= (int)System.Net.HttpStatusCode.OK;
                User user = context.GetUserById(userid);
                return context.LogIn(user.Email,user.Pswd);
            }
            Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
            return null;
        }
        [Route("FollowInterest")]
        [HttpGet]
        public User FollowInterest([FromQuery] int userid, [FromQuery] int interestid)
        {
            bool suc = context.FollowInterest(userid, interestid);
            if (suc)
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
                User user = context.GetUserById(userid);
                return context.LogIn(user.Email, user.Pswd);
            }
            Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
            return null;
        }
        [Route("GetUserDetails")]
        [HttpGet]
        public User GetUserDetails([FromQuery]int id)
        {
            User user= context.GetUserDetails(id);
            if(user==null)
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
                return null ;
            }
            Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
            return user;
        }
        [Route("UnFollowUser")]
        [HttpGet]
        public User UnFollowUser( [FromQuery] int follewUserId)
        {
            User user = HttpContext.Session.GetObject<User>("theUser");
            bool suc = context.UnfollewUser(user.UserId, follewUserId); ;
            if (suc)
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
                
                 user = context.GetUserById(user.UserId);
                return context.LogIn(user.Email, user.Pswd);
            }
            Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
            return null;
        }
        [Route("FollowUser")]
        [HttpGet]
        public User FollowUser([FromQuery] int follewUserId)
        {
            User user = HttpContext.Session.GetObject<User>("theUser");
            bool suc = context.follewUser(user.UserId, follewUserId);
            if (suc)
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
               
                user = context.GetUserById( user.UserId);
                return context.LogIn(user.Email, user.Pswd);
            }
            Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
            return null;
        }
        [Route("LogOut")]
        [HttpGet]
        public List<Interest> LogOut()
        {
            HttpContext.Session.Remove("theUser");
            context.SaveChanges();
            return context.GetInterest();
        }
        [Route("uptadeUserDetailsWithImage")]
        [HttpPost]
        public async Task<ActionResult> uptadeUserDetailsWithImage([ModelBinder(BinderType = typeof(JsonModelBinder))] User myJsonObject,
    IList<IFormFile> file)
        {
            try
            {

                User theUser = myJsonObject;
                if (!context.uptadeUserDetails(theUser))
                    return BadRequest();


                if (file.First() == null)
                    return BadRequest();

                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/UserImage", $"{theUser.UserId}.jpg");
                FileInfo fileInfo= new FileInfo(path);
                if (fileInfo.Exists)
                {
                    System.IO.File.Delete(path);
                    fileInfo.Delete();
                }
                
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
        [Route("uptadeUserDetails")]
        [HttpPost]
        public async Task<ActionResult> uptadeUserDetails([FromBody] User theUser)
        {
            try
            {
                
                if (context.uptadeUserDetails(theUser))
                    return Ok();
                return BadRequest();
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }
        [Route("UploadComment")]
        [HttpPost]
        public Comment UploadComment([FromBody] Comment comment)
        {
            Comment comment1 = context.AddComment(comment);
            if (comment1!=null)
            {
                
                    Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
                    return comment1;
                
                
            }
            else
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
                return null;
            }
        }
        [Route("GetArticleComments")]
        [HttpGet]
        public List<Comment> GetArticleComments([FromQuery]int articleId)
        {
            List<Comment> comments=context.GetArticleComments(articleId);
            if(comments!=null)
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
                return comments;
            }
            Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
            return null;
        }
        [Route("GetArticleAuthors")]
        [HttpGet]
        public string GetArticleAuthors([FromQuery]int articleId)
        {
            string authors = context.GetArticleAuthors(articleId);
            Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
            return authors;
        }
        [Route("AddInterest")]
        [HttpPost]
        public Interest AddInterest([FromBody] string InterestName)
        {
            Interest interest = context.AddInterest(InterestName);
            if(interest!=null)
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
                return interest;
            }
            Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
            return interest;
        }
        [Route("GetArticleIntersetType")]
        [HttpGet]
        public List<ArticleInterestType> GetArticleIntersetType([FromQuery] int articleId)
        {
            List<ArticleInterestType> interest = context.GetArticleIntersetType(articleId);
            if (interest != null)
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
                return interest;
            }
            Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
            return interest;
        }
        [Route("GetAuthorsArticle")]
        [HttpGet]
        public List<AuthorsArticle> GetAuthorsArticle([FromQuery] int articleId)
        {
            List<AuthorsArticle> interest = context.GetAuthorsArticle(articleId);
            if (interest != null)
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
                return interest;
            }
            Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
            return interest;
        }
        
    }
}
//scaffold-dbcontext "Server=localhost\sqlexpress;Database=ArtiFindDB;Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models -force Server=localhost\sqlexpress;Database=ArtiFindDB;Trusted_Connection=True;