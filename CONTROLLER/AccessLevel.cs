using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoBiometria.CONTROLLER
{
    public class AccessLevel
    {

            public string Name { get; set; }
            public string Value { get; set; }
            public override string ToString() { return this.Name; }
    }
}
