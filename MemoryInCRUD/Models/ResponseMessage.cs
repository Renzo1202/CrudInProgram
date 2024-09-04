using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryInCRUD.Models
{
    public class ResponseMessage<T>
    {
        public bool STATUS { get; set; }
        public string MESSAGE { get; set; }
        public T Data { get; set; }
    }
}
