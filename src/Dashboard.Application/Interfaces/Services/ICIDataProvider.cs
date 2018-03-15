using Dashboard.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Dashboard.Application.Interfaces.Services
{
    public interface ICIDataProvider
    {
        Task<Pipeline> GetMasterAsync();
        Task<IEnumerable<Pipeline>> GetAllAsync();
    }
}
