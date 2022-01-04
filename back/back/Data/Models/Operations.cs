using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LRSeleznev.Data.Models
{
    public class Operations : IOperations
    {
        public List<Subscriber> SelectSubscribersIncludeUtility(List<Subscriber> subscribers, int id)
        {
            var selected = subscribers.Where(subscriber => subscriber.utilities.Any(i => i.Utility.id == id))
               .ToList();
            return selected;
        }

        public List<Subscriber> SelectSubscribersWithDebt(List<Subscriber> subscribers)
        {
            var selected = subscribers.Where(subscriber => subscriber.balance < 0).ToList();
            return selected;
        }
    }
}
