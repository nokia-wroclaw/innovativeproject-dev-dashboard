using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Dashboard.Application.Interfaces.Services
{
    public interface IFetchDataService
    {
        Task UpdateCiDataForProjectAsync(int projectId);
    }
}
