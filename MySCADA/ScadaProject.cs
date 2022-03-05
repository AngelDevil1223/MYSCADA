using Newtonsoft.Json;
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
        public delegate void FormAddedEvent();

        public event FormAddedEvent FormAdded;

        public string Name { get; set; }
        public string Location { get; set; }
        public string DefaultForm { get; set; }
        public List<UserForm> UserForms { get; set; } = new List<UserForm>();
        public void RaiseEvent()
        {
            FormAdded.Invoke();
        }

        public static ScadaProject ActiveProject { get; set; }

        public static string ToFileFormat(ScadaProject p)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(ScadaProject));

            using (StringWriter stringWriter = new StringWriter())
            {
                serializer.Serialize(stringWriter, p);
                return stringWriter.ToString();
            }
        }
        public static ScadaProject FromXml(string file)
        {
            var fl=XmlReader.Create(new StringReader(File.ReadAllText(file)));
            XmlSerializer serializer = new XmlSerializer(typeof(ScadaProject));
            var proj = (ScadaProject)serializer.Deserialize(fl);
            return proj;
        }
        public void SaveChanges()
        {
            var text = ToFileFormat(this);
            File.WriteAllText($"{Location}\\{Name}.scdproj", text);
            RaiseEvent();
        }

        public ScadaForm ReadForm(string formName)
        {
            var str = ReadRawForm(formName);
            if (string.IsNullOrEmpty(str))
            {
                return new ScadaForm();
            }
            return JsonConvert.DeserializeObject<ScadaForm>(str);
        }
        public string ReadRawForm(string formName)
        {
            var file = $"{Location}\\UserForms\\{formName}";
            var str = File.ReadAllText(file);
            return str;
        }

        public string ReadCode(string formName)
        {
            var file = $"{Location}\\UserForms\\{formName}";
            var str = File.ReadAllText(file);
            return str;
        }
        public TreeNode ToNode()
        {
            var parent = new TreeNode();
            parent.Text = Name;

            var nforms = new TreeNode() { Text = "Custom Forms" };
            parent.Nodes.Add(nforms);

            UserForms.ForEach(x =>
            {
                nforms.Nodes.Add(new TreeNode()
                {
                    Text = x.FormName,
                    Name=x.FormName
                });
            });
            return parent;
        }

        public UserForm GetDefaultForm()
        {
            return UserForms.FirstOrDefault(x => x.FormName == DefaultForm);
        }
    }

    public class UserForm
    {
        public string FormName { get; set; }
        public string DesignerFile { get; set; }
        public string ScriptFile { get; set; }
    }
}
