using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

namespace MySCADA
{
    public class ScadaProject
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public List<UserForm> UserForms { get; set; } = new List<UserForm>();

        public static ScadaProject ActiveProject { get; set; }

        public static string ToFileFormat(ScadaProject p)
        {
            string xml = "";
            XmlSerializer serializer = new XmlSerializer(typeof(ScadaProject));

            using (StringWriter stringWriter = new StringWriter())
            {
                serializer.Serialize(stringWriter, p);
                return stringWriter.ToString();
            }
        }
        public static ScadaProject FromXml(Stream st)
        {
            var serializer = new XmlSerializer(typeof(ScadaProject));
            return (ScadaProject)serializer.Deserialize(st);
        }
        public TreeNode ToNode()
        {
            var parent = new TreeNode();
            parent.Text = Name;

            var nforms = new TreeNode() { Name = "Custom Forms" };
            parent.Nodes.Add(nforms);

            UserForms.ForEach(x =>
            {
                nforms.Nodes.Add(new TreeNode()
                {
                    Name = x.FormName
                });
            });
            return parent;
        }
    }

    public class UserForm
    {
        public string FormName { get; set; }
        public string DesignerFile { get; set; }
        public string ScriptFile { get; set; }
    }
}
