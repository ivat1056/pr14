using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pr14.classs
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

        public string LastName { get; private set; }
        public string FirstName { get; private set; }
        public string Patronymic { get; private set; }
    }
}
