using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace MySCADA
{
    public class ScadaForm
    {
        public List<FElement> FormElements { get; set; } = new();
    }
    public class FElement
    {
        public string Type { get; set; }
        public List<FElementAttribute> Attributes { get; set; } = new();
        public List<FElementEvent> Events { get; set; } = new();
    }
    public class FElementAttribute
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
    }
    public class FElementEvent
    {
        public string Event { get; set; }
        public string Handler { get; set; }
    }
}
