using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DALConverter.Models;

namespace DALConverter
{
    public interface IConvertRepository
    {

        public Task<IEnumerable<string>> Converter(string route, string format); 
    }
}
