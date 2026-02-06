using Gezenti.Core.Utilities.Abstrak;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gezenti.Application.Services.Repositories
{
    public interface IMailService
    {
      
        Task<IDataResult<string>> SendMail(int userId, string userEmail);

     
        Task<IDataResult<string>> ForgetSendMail(int userId, string userEmail);
    }
}
