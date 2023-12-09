using Core.Domain.DomainUtil;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Core.Model.UserModels
{
    public class UserRegisterModel : BaseUserModel
    {
        public IFormFile? ProfileImage { get; set; }
    }
}
