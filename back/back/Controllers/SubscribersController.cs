using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LRSeleznev.Data.Models;
using Microsoft.AspNetCore.Authorization;

namespace LRSeleznev.Data.Contexts
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscribersController : ControllerBase
    {
        private readonly SystemContext _context;
        private readonly IOperations _opr;

        public SubscribersController(SystemContext context,IOperations opr)
        {
            _opr = opr;
            _context = context;
        }

        [Route("getrole")]
        [Authorize(Roles = "admin")]
        public CreatedAtActionResult GetRole()
        {
            var role = "admin";
            return CreatedAtAction("GetRole", new { role });
        }

        // GET: api/Subscribers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Subscriber>>> GetSubscribers()
        {
            var subscribers = _context.Subscribers.Include(p => p.utilities).ThenInclude(i => i.Utility).Where(c=>c.passportdata!=null && c.telephonenumber!=null).OrderByDescending(d=>d.id);
            return await subscribers.ToArrayAsync();
        }

        // GET: api/Subscribers/5
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<Subscriber>> GetSubscriber(int id)
        {
            var subscriber = _context.Subscribers.Include(i => i.utilities).ThenInclude(u => u.Utility).FirstOrDefault(i => i.id == id);
            if (subscriber == null)
            {
                return CreatedAtAction("GetSubscriber", new { result = "Пользователь не найден."});
            }

            return subscriber;
        }

        // GET: api/Subscribers/getbyutility/5
        [Authorize]
        [HttpGet("getbyutility/{id}")]
        public CreatedAtActionResult GetByUtility(int id)
        {
            var subscribers = _opr.SelectSubscribersIncludeUtility(_context.Subscribers.Include(i => i.utilities).ThenInclude(u => u.Utility).ToList(), id);
            if (subscribers.Count == 0)
                return CreatedAtAction("GetByUtility", new
                {
                    result = "Не найдено пользователей с данной услугой."
                });
            else
            {
                return CreatedAtAction("GetByUtility", new
                {
                    result = subscribers
                });
            }
        }
        // GET: api/Subscribers/debt
        [Authorize(Roles = "admin")]
        [HttpGet("debt")]
        public CreatedAtActionResult GetDebt()
        {
            var subscribers = _opr.SelectSubscribersWithDebt(_context.Subscribers.Include(i => i.utilities).ThenInclude(u => u.Utility).ToList());
            if (subscribers.Count == 0)
                return CreatedAtAction("GetDebt", new
                {
                    result = "Не найдено пользователей с задолженностью."
                });
            else
            {
                return CreatedAtAction("GetDebt", new
                {
                    result = subscribers
                });
            }
        }

        // GET: api/Subscribers/stat
        [Authorize(Roles = "admin")]
        [HttpGet("stat")]
        public async Task<ActionResult<List<float>>> GetStat()
        {
            var utilities = _context.Utility;
            List<float> result = new List<float>();
            float min = utilities.Select(a => a.price).Min();
            result.Add(min);
            float max= utilities.Select(a => a.price).Max();
            result.Add(max);
            float avg = utilities.Select(a => a.price).Average();
            result.Add(avg);
            return result;
        }

        [HttpGet("topstat")]
        public CreatedAtActionResult GetStatDesc()
        {
            float amount = new float();
            List<UtilityPerSubscriber> utilities = _context.UtilityIncludes.ToList();
            List<Subscriber> subscribers = _context.Subscribers.ToList();
            Dictionary<string,float> amountpersubscriber = new Dictionary<string, float>();
            //subscribers.ForEach
            foreach (var b in from Subscriber b in subscribers select b)
            {
                amount = 0;
                foreach (var a in from UtilityPerSubscriber a in utilities where a.Subscriberid == b.id select a)
                    amount += _context.Utility.FirstOrDefault(e => e.id == a.Utilityid).price;
                amountpersubscriber.Add(b.fullname, amount);
            }
            return CreatedAtAction("GetStatDesc", new { result=amountpersubscriber.OrderByDescending(summ=>summ.Value)}); 
        }

        [HttpGet("isfavourite")]
        public async Task<ActionResult<IEnumerable<Utility>>> GetFavourite()
        {
            var utilities = _context.Utility.Where(p => p.isfavourite == true);
            return await utilities.ToListAsync();
        }

        // PUT: api/Subscribers/
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut]
        public async Task<ActionResult<Subscriber>> ChangeSubscriber([FromForm] int id, [FromForm] int id1, [FromForm] DateTime newdate)
        {
            var subscribers = _context.Subscribers.Include(i => i.utilities).ThenInclude(u => u.Utility).FirstOrDefault(i => i.id == id);
            if (subscribers != null)
            {
                if (subscribers.ChangeUtilityDueDate(id1, newdate))
                {
                    await _context.SaveChangesAsync();
                    return CreatedAtAction("ChangeSubscriber", new
                    {
                        result = "Данные были изменены."
                    });
                }
                else
                {
                    return CreatedAtAction("ChangeSubscriber", new
                    {
                        result = "У пользователя такая услуга не зарегистрирована."
                    });
                }
            }
            else
            {
                return CreatedAtAction("ChangeSubscriber", new
                {
                    result = "Пользователя с таким уникальным номером не существует."
                });

            }
        }

        // POST: api/Subscribers/AddUtility
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [Authorize(Roles = "admin")]
        [HttpPost("addutility/{id}/{ids}")]
        public async Task<ActionResult<Subscriber>> AddUtility( int id, int ids)
        {
            var subscriber = _context.Subscribers.FirstOrDefault(i => i.id == id);
            if(subscriber==null) return CreatedAtAction("AddUtility", new { 
                result = "Пользователя с таким уникальным номером не существует." });
                UtilityPerSubscriber buffer = new UtilityPerSubscriber();
                var utilitycheck = _context.Utility.FirstOrDefault(i=> i.id==ids);
                if (utilitycheck != null)
                {
                    subscriber.AddUtility(utilitycheck);
                    //buffer.Subscriberid = id;
                    //buffer.Utilityid = a;
                    //buffer.Utility = utilitycheck;
                    //_context.UtilityIncludes.Add(buffer);
                }
                else return CreatedAtAction("AddUtility", new { 
                   result = "Услуги с таким уникальным номером не существует." });
            await  _context.SaveChangesAsync();
            return CreatedAtAction("AddUtility", new { 
                result="Услуги добавлены пользователю.",subscriber.fullname});
        }

        // POST: api/Subscribers/AddSubscriber
        [Authorize]
        [HttpPost("AddSubscriber")]
        public async Task<ActionResult<Subscriber>> AddSubscriber(Subscriber subscriber)
        {
            _context.Subscribers.Add(subscriber);
            await _context.SaveChangesAsync();

            return CreatedAtAction("AddSubscriber", new { 
                result = "Пользователь добавлен, его уникальный номер:",subscriber.id});
        }

        // DELETE: api/Subscribers/5
        [Authorize(Roles = "admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Subscriber>> DeleteSubscriber(int id)
        {
            var subscriber = _context.Subscribers.Include(i => i.utilities).ThenInclude(u => u.Utility).FirstOrDefault(i => i.id == id);
            if (subscriber == null)
            {
                return CreatedAtAction("DeleteSubscriber", new { 
                    result = "Пользователя с таким уникальным номером не существует." });
            }

            _context.Subscribers.Remove(subscriber);
            await _context.SaveChangesAsync();

            return CreatedAtAction("DeleteSubscriber", new { 
                result = "Пользователь удален." });
        }

        [Authorize(Roles = "admin")]
        [HttpDelete("deleteutility/{id}/{id1}")]
        public async Task<ActionResult<Subscriber>> DeleteUtility(int id,int id1)
        {
            var subscriber = _context.Subscribers.FirstOrDefault(i => i.id == id);
            if (subscriber == null) return CreatedAtAction("DeleteUtility", new { 
                result = "Пользователя с таким уникальным номером не существует." });
            var utilityinclude = _context.UtilityIncludes.FirstOrDefault(i => i.Utilityid == id1 && i.Subscriberid==id);
            if (utilityinclude != null) _context.UtilityIncludes.Remove(utilityinclude);
            else return CreatedAtAction("DeleteUtility", new { 
                result = "У пользователя нет такой услуги." });
            await _context.SaveChangesAsync();
            return CreatedAtAction("AddUtility", new { 
                result = "Услуга удалена у пользователя:", subscriber.fullname });
        }
        private bool SubscriberExists(int id)
        {
            return _context.Subscribers.Any(e => e.id == id);
        }
    }
}
