using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MySCADA
{
    public partial class CodeEditor : Form
    {
        public CodeEditor(string codeFile)
        {
            InitializeComponent();

            var code = ScadaProject.ActiveProject.ReadCode(codeFile);
            richTextBox1.Text = code;
            FormatCode();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            FormatCode();
        }

        void FormatCode()
        {
            string keywords = @"\b(abstract|as|base|break|case|catch|checked|continue|default|delegate|do|else|event|explicit|extern|false|finally|fixed|for|foreach|goto|if|implicit|in|interface|internal|is|lock|namespace|new|null|object|operator|out|override|params|private|protected|public|readonly|ref|return|sealed|sizeof|stackalloc|switch|this|throw|true|try|typeof|unchecked|unsafe|using|virtual|volatile|while)\b";
            MatchCollection keywordMatches = Regex.Matches(richTextBox1.Text, keywords);

            string types = @"\b(Console)\b";
            MatchCollection typeMatches = Regex.Matches(richTextBox1.Text, types);

            string comments = @"(\/\/.+?$|\/\*.+?\*\/)";
            MatchCollection commentMatches = Regex.Matches(richTextBox1.Text, comments, RegexOptions.Multiline);

            string strings = "\".+?\"";
            MatchCollection stringMatches = Regex.Matches(richTextBox1.Text, strings);

            string stringz = "bool|byte|char|class|const|decimal|double|enum|float|int|long|sbyte|short|static|string|struct|uint|ulong|ushort|void";
            MatchCollection stringzMatchez = Regex.Matches(richTextBox1.Text, stringz);

            int originalIndex = richTextBox1.SelectionStart;
            int originalLength = richTextBox1.SelectionLength;
            Color originalColor = Color.Black;

            richTextBox1.SelectionStart = 0;
            richTextBox1.SelectionLength = richTextBox1.Text.Length;
            richTextBox1.SelectionColor = originalColor;

            foreach (Match m in keywordMatches)
            {
                richTextBox1.SelectionStart = m.Index;
                richTextBox1.SelectionLength = m.Length;
                richTextBox1.SelectionColor = Color.Blue;
            }

            foreach (Match m in typeMatches)
            {
                richTextBox1.SelectionStart = m.Index;
                richTextBox1.SelectionLength = m.Length;
                richTextBox1.SelectionColor = Color.DarkCyan;
            }

            foreach (Match m in commentMatches)
            {
                richTextBox1.SelectionStart = m.Index;
                richTextBox1.SelectionLength = m.Length;
                richTextBox1.SelectionColor = Color.Green;
            }

            foreach (Match m in stringMatches)
            {
                richTextBox1.SelectionStart = m.Index;
                richTextBox1.SelectionLength = m.Length;
                richTextBox1.SelectionColor = Color.Brown;
            }

            foreach (Match m in stringzMatchez)
            {
                richTextBox1.SelectionStart = m.Index;
                richTextBox1.SelectionLength = m.Length;
                richTextBox1.SelectionColor = Color.Purple;
            }

            richTextBox1.SelectionStart = originalIndex;
            richTextBox1.SelectionLength = originalLength;
            richTextBox1.SelectionColor = originalColor;

            richTextBox1.Focus();
        }
    }
}
