using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TFEHelper.Backend.Domain.Classes.Models;

namespace TFEHelper.Backend.Domain.Exceptions
{
    public abstract class EntityNotFoundException : BaseException
    {
        protected EntityNotFoundException(string message) 
            : base(message)
        {            
        }
    }

    public class EntityNotFoundException<T> : EntityNotFoundException where T : ITFEHelperModel
    {
        public EntityNotFoundException()
            : base($"There are no {typeof(T).Name} which satisfies the specified search criteria.")
        {
        }

        public EntityNotFoundException(int id)
            : base($"The required {typeof(T).Name} with Id = {id} was not found.")
        {
        }
    }
}
