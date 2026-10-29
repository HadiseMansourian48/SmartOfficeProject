using SmartOffice.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartOffice.Application.Interfaces.Auth
{
    public interface IJwtTokenGenerator
    {
        (string Token, DateTime ExpiresAt) Generate(User user);
    }
}
