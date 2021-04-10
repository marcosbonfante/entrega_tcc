using System;
using System.Linq;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace SGM.WebAPI.Core.Usuario
{
    public class AspNetUser : IAspNetUser
    {
        private readonly IHttpContextAccessor _accessor;

        public AspNetUser(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        public string Name => _accessor.HttpContext.User.Identity.Name;

        public Guid ObterUserId()
        {
            return EstaAutenticado() ? Guid.Parse(_accessor.HttpContext.User.GetUserId()) : Guid.Empty;
        }

        public string ObterUserEmail()
        {
            return EstaAutenticado() ? _accessor.HttpContext.User.GetUserEmail() : "";
        }

        public string ObterUserToken()
        {
            return EstaAutenticado() ? _accessor.HttpContext.User.GetUserToken() : "";
        }
        public string ObterUserRefreshToken()
        {
            return EstaAutenticado() ? _accessor.HttpContext.User.GetUserRefreshToken() : "";
        }

        public bool EstaAutenticado()
        {
            return _accessor.HttpContext.User.Identity.IsAuthenticated;
        }

        public bool PossuiRole(string role)
        {
            var claims = _accessor.HttpContext.User.Claims;
            var l = (from u in claims where u.Type == "role" select u).ToList();

            if (l == null) { return false; }
            if (l.Count == 0) { return false; }

            foreach (var c in l)
            {
                if (c.Value == role) { return true; }
            }
           
            return _accessor.HttpContext.User.IsInRole(role);
        }

        public IEnumerable<Claim> ObterClaims()
        {
            return _accessor.HttpContext.User.Claims;
        }

        public HttpContext ObterHttpContext()
        {
            return _accessor.HttpContext;
        }
    }
}