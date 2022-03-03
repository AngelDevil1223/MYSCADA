using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySCADA
{
    public class ControlList
    {
        public static List<ControlList> objDGVBind = new List<ControlList>();

        public string procedureName { get; set; }
        public string ParameterName { get; set; }
        public string ControlName { get; set; }

        public ControlList(String procedureNames, String ParamName, string ControlNames)
        {
            procedureName = procedureNames;
            ParameterName = ParamName;
            ControlName = ControlNames;
        }
    }
}
