using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LRSeleznev.Data.Models
{
    public class UtilityPerSubscriber
    {
        public int id { get; set; }
        public Utility Utility { set; get;}
        public int Utilityid { set; get; }
        public int Subscriberid { set; get; }
        public DateTime DueDate { set; get; }
    }
}
