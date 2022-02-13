using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySCADA.Drawing
{
    public class ShpCollection : System.Collections.ObjectModel.Collection
      <ScShape>
    {

        private readonly ScCanvas canvas;
        public ShpCollection(ScCanvas c)
        {

            canvas = c;

        }

        public event EventHandler CollectionChanged;

        protected virtual void OnCollectionChanged(EventArgs e)
        {

            if (CollectionChanged != null) this.CollectionChanged
               (this, e);

        }

        private string FreeName(Type t)
        {

            if (t.IsSubclassOf(typeof(ScShape)))
            {
                var shapes = new List<ScShape>();

                foreach (ScShape s in this)
                {

                    if (t == s.GetType())
                    {

                        shapes.Add(s);

                    }

                }

                var ht = new Hashtable(shapes.Count);

                foreach (ScShape s in shapes)

                    ht[s.Name] = null;

                ScShape instance = (ScShape)Activator.CreateInstance
                   (t, new object[] { Point.Empty });

                string defName = instance.ShapeName();

                int i = 1;

                while (ht.ContainsKey(defName + i))
                    i++;

                return defName + i;

            }

            else
            {

                return String.Empty;

            }

        }
    }
}
