using DocFlow.BusinessLayer.Interfaces;
using DocFlow.BusinessLayer.Interfaces.GraphServices;
using DocFlow.BusinessLayer.Services.ApplicationServices;
using DocFlow.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Claims;

namespace DocFlow.Controllers
{
    public abstract class BaseController : Controller
    {
        public readonly DocFlowCotext _context;

        public BaseController(
                DocFlowCotext context
            )
        {
            _context = context;
        }

        public virtual string Token
        {
            get
            {
                return HttpContext.User?.Claims?.Where(x => x.Type.Contains(ClaimTypes.Authentication)).FirstOrDefault()?.Value;
            }
        }

        public virtual Guid UserId
        {
            get
            {
                Guid userId = Guid.Empty;
                var claimValue = HttpContext.User?.Claims.Where(x => x.Type.Contains(ClaimTypes.Actor)).FirstOrDefault()?.Value;

                if (Guid.TryParse(claimValue, out userId))
                {
                    return userId;
                }

                return userId;
            }
        }
    }
}