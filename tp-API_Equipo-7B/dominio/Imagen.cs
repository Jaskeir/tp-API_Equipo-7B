using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    public class Imagen
    {
        public string Url { get; }
        public Imagen(string url)
        {
            Url = url;
        }
    }
}
