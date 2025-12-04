using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Extensions
{
    public static class ControllerBaseExtensions
    {
        public static Guid GetUserId(this ControllerBase controller)
        {
            var user = controller.User;

            var userIdClaim = user.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null || !Guid.TryParse(userIdClaim.Value, out var userId))
            {
                throw new UnauthorizedAccessException("User ID claim not found or invalid.");
            }
            return userId;
        }
    }
}