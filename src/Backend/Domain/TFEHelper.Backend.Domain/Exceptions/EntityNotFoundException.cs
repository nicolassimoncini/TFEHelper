using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TFEHelper.Backend.Domain.Exceptions
{
    public class EntityNotFoundException<T> : BaseException
    {
        protected EntityNotFoundException(int id) 
            : base($"The {typeof(T).GetType().Name} with Id = {id} was not found.")
        {            
        }
    }
}
