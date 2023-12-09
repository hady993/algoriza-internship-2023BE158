using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Helpful
{
    public static class HelpfulMessages
    {
        public static IdentityResult IdentityResultError(string message)
        {
            var error = new IdentityError();
            error.Description = message;
            return IdentityResult.Failed(error);
        }
    }
}
