using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AIShellAn.Server.Controllers.v1
{
    [Route("api/v1/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class AnLongSpeechController : ControllerBase
    {
    }
}