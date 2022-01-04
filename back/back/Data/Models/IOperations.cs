using System;
using System.Collections.Generic;

namespace LRSeleznev.Data.Models
{
    public interface IOperations
    {
        List<Subscriber> SelectSubscribersIncludeUtility(List<Subscriber> subscribers, int id);
        List<Subscriber> SelectSubscribersWithDebt(List<Subscriber> subscribers);

    }
}
