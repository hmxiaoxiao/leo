using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Leo.DB
{
    public interface IDB<T> where T : new ()
    {
        bool Insert();

        bool Update();

        bool Delete();
    }
}
