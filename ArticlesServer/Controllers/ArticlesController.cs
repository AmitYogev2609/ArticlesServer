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
    [Route("ArtiFindAPI")]
    [ApiController]
    public class ArticlesController : ControllerBase
    {
        #region Add connection to the db context using dependency injection
        ArtiFindDBContext context;
        public ArticlesController(ArtiFindDBContext context)
        {
            this.context = context;
        }
        #endregion
    }
}
