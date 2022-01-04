using LRSeleznev.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace LRSeleznev.Data.Models
{
    public class Subscriber
    {
        public int id { get; set; }
        public string fullname { get; set; }
        public string passportdata { get; set; }
        public string telephonenumber { get; set; }
        public float balance { get; set; }

        public List<UtilityPerSubscriber> utilities { get; set; } = new List<UtilityPerSubscriber>();

        public void AddUtility(Utility buffer)
        {
            utilities.Add(new UtilityPerSubscriber { Utility = buffer });
        }

        public bool DeleteUtility(int id)
        {
            var utilityToRemove = utilities.FirstOrDefault(r => r.id == id);
            if (utilityToRemove != null)
            {
                utilities.Remove(utilityToRemove);
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool ChangeUtilityDueDate(int id, DateTime newdate)
        {
            var tochange = utilities.FirstOrDefault(r => r.id == id);
            if (tochange != null)
            {
                tochange.DueDate = newdate;
                return true;
            }
            else return false;
        }
    }
}
