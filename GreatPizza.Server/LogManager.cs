using GreatPizza.Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace GreatPizza.Server
{
    public class LogManager
    {
        private volatile static LogManager innerInstance = null;
        private static object locker = new object();

        protected internal LogManager()
        {

        }

        public static LogManager Instance
        {
            get
            {
                if (innerInstance == null)
                {
                    lock (locker)
                    {
                        if (innerInstance == null)
                        {
                            innerInstance = new LogManager();
                        }
                    }
                }
                return innerInstance;
            }
        }

        public async Task<bool> Write(Exception exception)
        {
            bool result = true;
            try
            {
                using (var dbContext = new GreatPizzaDbContext())
                {
                    dbContext.Log.Add(new Log
                    {
                        Message = exception.ToString()
                    });
                    await dbContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                result = false;
            }
            return result;
        }
    }
}