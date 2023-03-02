using DALConverter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLConverter.Services
{
    public interface IConvertService
    {

        public Task<List<DisplayDetails>> Convert(string route, string format);
    }
}
