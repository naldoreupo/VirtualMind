using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VirtualMind.Exam.Transversal;

namespace VirtualMind.Exam.Infraestructure
{

    public class BaseRepository<T> where T : class
    {
        protected DbContext Context = new VirtualMindDBContext();
        protected DbSet<T> DbSet;

        public BaseRepository()
        {
            DbSet = Context.Set<T>();
        }

        public async Task<Response<Boolean>> Save(T entidad)
        {
            try
            {
                DbSet.Add(entidad);
                await Context.SaveChangesAsync();

                return new Response<Boolean>()
                {
                    Status = true,
                    Message = "Operation Successfully Completed"
                };
            }
            catch (Exception e)
            {
                return new Response<Boolean>()
                {
                    Status = false,
                    Message = e.Message
                };
            }
        }
    }
}
