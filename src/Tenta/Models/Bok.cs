using System;
using System.Collections.Generic;

namespace Tenta.Models
{
    public partial class Bok
    {
        public int BokId { get; set; }
        public string Titel { get; set; }
        public string Forfattare { get; set; }
        public bool Ilager { get; set; }
        public int AntalSidor { get; set; }
    }
}
