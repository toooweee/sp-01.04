using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pz17
{
    public class Student
    {
        public String Name { get; set; } = "Ислам Гадиляев";
        public String Group { get; set; } = "4pk2";

        public override string ToString()
        {
            return $"Студент {this.Name} из группы {this.Group}";
        }

    }
}
