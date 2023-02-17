using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pr14
{
    /// <summary>
    /// частичный класс 
    /// </summary>
    public partial class Client
    {
        public string FIO
        {
            get
            {
                return LastName + " " + FirstName + " " + Patronymic;
            }
        }


    }
}
