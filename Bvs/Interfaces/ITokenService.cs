using Bvs.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bvs_API.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(Admin admin);
    }
}
