using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArticlesServerBL.Models;
using System.IO;

namespace ArticlesServer.Controllers
{
    [Route("ArticlesAPI")]
    [ApiController]
    public class ArticlesController : ControllerBase
    {
        #region Add connection to the db context using dependency injection
        ArticlesDBContext context;
        public ArticlesController(ArticlesDBContext context)
        {
            this.context = context;
        }
        #endregion
    }
}
