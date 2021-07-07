using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Web;

namespace MyFinishProject.Extensions
{
    /// <summary>
    /// Получение пользователя  по ID
    /// </summary>
    public static class UserExtensions
    {
        public static string GetUserId(this ClaimsPrincipal identity)
        {
            var userId = identity.Claims.FirstOrDefault(x => x.Type == "userId").Value;

            if (string.IsNullOrWhiteSpace(userId))
            {
                throw new Exception("Ползователь не найден");
            }

            return userId;
        }
    }
}