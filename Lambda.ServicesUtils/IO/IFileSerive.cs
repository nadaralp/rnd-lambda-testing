using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lambda.ServicesUtils.IO
{
    public interface IFileSerive
    {
        Task CreateFileFromStringAsync(string fileContent, string path);
    }
}