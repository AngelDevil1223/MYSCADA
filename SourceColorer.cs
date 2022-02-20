using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MySCADA
{
	public class SourceColorer
	{
		private string _commentCssClass;
		private string _keywordCssClass;
		private string _quotesCssClass;
		private string _typeCssClass;
		private bool _addStyleDefinition;
		private HashSet<string> _keywords;
		private bool _addPreTags;
		public HashSet<string> Keywords
		{
			get { return _keywords; }
		}

		public string CommentCssClass
		{
			get { return _commentCssClass; }
			set { _commentCssClass = value; }
		}
		public string KeywordCssClass
		{
			get { return _keywordCssClass; }
			set { _keywordCssClass = value; }
		}
		public string QuotesCssClass
		{
			get { return _quotesCssClass; }
			set { _quotesCssClass = value; }
		}
		public string TypeCssClass
		{
			get { return _typeCssClass; }
			set { _typeCssClass = value; }
		}
		public bool AddStyleDefinition
		{
			get { return _addStyleDefinition; }
			set { _addStyleDefinition = value; }
		}
		public bool AddPreTags
		{
			get { return _addPreTags; }
			set { _addPreTags = value; }
		}

		public SourceColorer()
		{
			_addStyleDefinition = true;
			_commentCssClass = "comment";
			_keywordCssClass = "keyword";
			_quotesCssClass = "quotes";
			_typeCssClass = "type";
			_keywords = new HashSet<string>()
		{
			"static", "using", "true", "false","new",
			"namespace", "void", "private", "public",
			"bool", "string", "return", "class","internal",
			"const", "readonly", "int", "double","lock",
			"float", "if", "else", "foreach", "for","var",
			"get","set","byte\\[\\]","char\\[\\]","int\\[\\]","string\\[\\]" 
		};
		}
		public string Highlight(string source)
		{
			StringBuilder builder = new StringBuilder();
			if (AddStyleDefinition)
			{
				builder.Append("<style>");
				builder.AppendFormat(".{0}  {{ color: #0000FF  }} ", KeywordCssClass);
				builder.AppendFormat(".{0}  {{ color: #2B91AF  }} ", TypeCssClass);
				builder.AppendFormat(".{0}  {{ color: green    }} ", CommentCssClass);
				builder.AppendFormat(".{0}  {{ color: maroon   }} ", QuotesCssClass);
				builder.Append("</style>");
			}

			if (AddPreTags)
				builder.Append("<pre>");

			builder.Append(HighlightSource(source));

			if (AddPreTags)
				builder.Append("</pre>");

			return builder.ToString();
		}
		protected virtual string HighlightSource(string content)
		{
			if (string.IsNullOrEmpty(CommentCssClass))
				throw new InvalidOperationException("The CommentCssClass should not be null or empty");
			if (string.IsNullOrEmpty(KeywordCssClass))
				throw new InvalidOperationException("The KeywordCssClass should not be null or empty");
			if (string.IsNullOrEmpty(QuotesCssClass))
				throw new InvalidOperationException("The CommentCssClass should not be null or empty");
			if (string.IsNullOrEmpty(TypeCssClass))
				throw new InvalidOperationException("The TypeCssClass should not be null or empty");

			const string COMMENTS_TOKEN = "`````";
			const string MULTILINECOMMENTS_TOKEN = "~~~~~";
			const string QUOTES_TOKEN = "Â¬Â¬Â¬Â¬Â¬";

			Regex regex = new Regex(@"/\*([^*]|[\r\n]|(\*+([^*/]|[\r\n])))*\*+/", RegexOptions.Singleline);
			List<string> multiLineComments = new List<string>();
			if (regex.IsMatch(content))
			{
				foreach (Match item in regex.Matches(content))
				{
					if (!multiLineComments.Contains(item.Value))
						multiLineComments.Add(item.Value);
				}
			}

			for (int i = 0; i < multiLineComments.Count; i++)
			{
				content = content.ReplaceToken(multiLineComments[i], MULTILINECOMMENTS_TOKEN, i);
			}

			List<string> quotes = new List<string>();
			bool onEscape = false;
			bool onComment1 = false;
			bool onComment2 = false;
			bool inQuotes = false;
			int start = -1;
			for (int i = 0; i < content.Length; i++)
			{
				if (content[i] == '/' && !inQuotes && !onComment1)
					onComment1 = true;
				else if (content[i] == '/' && !inQuotes && onComment1)
					onComment2 = true;
				else if (content[i] == '"' && !onEscape && !onComment2)
				{
					inQuotes = true; 
					if (start > -1)
					{
						string quote = content.Substring(start, i - start + 1);
						if (!quotes.Contains(quote))
							quotes.Add(quote);
						start = -1;
						inQuotes = false;
					}
					else
					{
						start = i;
					}
				}
				else if (content[i] == '\\' || content[i] == '\'')
					onEscape = true;
				else if (content[i] == '\n')
				{
					onComment1 = false;
					onComment2 = false;
				}
				else
				{
					onEscape = false;
				}
			}

			for (int i = 0; i < quotes.Count; i++)
			{
				content = content.ReplaceToken(quotes[i], QUOTES_TOKEN, i);
			}

			regex = new Regex("(/{2,3}.+)\n", RegexOptions.Multiline);
			List<string> comments = new List<string>();
			if (regex.IsMatch(content))
			{
				foreach (Match item in regex.Matches(content))
				{
					if (!comments.Contains(item.Value + "\n"))
						comments.Add(item.Value);
				}
			}

			for (int i = 0; i < comments.Count; i++)
			{
				content = content.ReplaceToken(comments[i], COMMENTS_TOKEN, i);
			}
			content = Regex.Replace(content, "('.{1,2}')", "<span class=\"quote\">$1</span>", RegexOptions.Singleline);
			regex = new Regex(@"((?:\s|^)[A-Z]\w+(?:\s))", RegexOptions.Singleline);
			List<string> highlightedClasses = new List<string>();
			if (regex.IsMatch(content))
			{
				foreach (Match item in regex.Matches(content))
				{
					string val = item.Groups[1].Value;
					if (!highlightedClasses.Contains(val))
						highlightedClasses.Add(val);
				}
			}

			for (int i = 0; i < highlightedClasses.Count; i++)
			{
				content = content.ReplaceWithCss(highlightedClasses[i], TypeCssClass);
			}
			regex = new Regex(@"(?:\s|\[)([A-Z]\w+)(?:\])", RegexOptions.Singleline);
			highlightedClasses = new List<string>();
			if (regex.IsMatch(content))
			{
				foreach (Match item in regex.Matches(content))
				{
					string val = item.Groups[1].Value;
					if (!highlightedClasses.Contains(val))
						highlightedClasses.Add(val);
				}
			}

			for (int i = 0; i < highlightedClasses.Count; i++)
			{
				content = content.ReplaceWithCss(highlightedClasses[i], TypeCssClass);
			}

			regex = new Regex(@"(?:\s|\[|\()([A-Z]\w+(?:<|&lt;))", RegexOptions.Singleline);
			highlightedClasses = new List<string>();
			if (regex.IsMatch(content))
			{
				foreach (Match item in regex.Matches(content))
				{
					string val = item.Groups[1].Value;
					if (!highlightedClasses.Contains(val))
						highlightedClasses.Add(val);
				}
			}

			for (int i = 0; i < highlightedClasses.Count; i++)
			{
				string val = highlightedClasses[i];
				val = val.Replace("<", "").Replace("&lt;", "");
				content = content.ReplaceWithCss(highlightedClasses[i], val, "&lt;", TypeCssClass);
			}
			regex = new Regex(@"new\s+([A-Z]\w+)(?:\()", RegexOptions.Singleline);
			highlightedClasses = new List<string>();
			if (regex.IsMatch(content))
			{
				foreach (Match item in regex.Matches(content))
				{
					string val = item.Groups[1].Value;
					if (!highlightedClasses.Contains(val))
						highlightedClasses.Add(val);
				}
			}

			for (int i = 0; i < highlightedClasses.Count; i++)
			{
				content = content.ReplaceWithCss(highlightedClasses[i], TypeCssClass);
			}

			foreach (string keyword in _keywords)
			{
				Regex regexKeyword = new Regex("(" + keyword + @")(>|&gt;|\s|\n|;|<)", RegexOptions.Singleline);
				content = regexKeyword.Replace(content, "<span class=\"keyword\">$1</span>$2");
			}

			for (int i = 0; i < multiLineComments.Count; i++)
			{
				content = content.ReplaceTokenWithCss(multiLineComments[i], MULTILINECOMMENTS_TOKEN, i, CommentCssClass);
			}

			for (int i = 0; i < quotes.Count; i++)
			{
				content = content.ReplaceTokenWithCss(quotes[i], QUOTES_TOKEN, i, QuotesCssClass);
			}

			for (int i = 0; i < comments.Count; i++)
			{
				string comment = comments[i];
				for (int n = 0; n < quotes.Count; n++)
				{
					comment = comment.Replace(string.Format("{0}{1}{0}", QUOTES_TOKEN, n), quotes[n]);
				}
				content = content.ReplaceTokenWithCss(comment, COMMENTS_TOKEN, i, CommentCssClass);
			}
			return content;
		}
	}

	public static class FormatterExtensions
	{
		public static string ReplaceWithCss(this string content, string source, string cssClass)
		{
			return content.Replace(source, string.Format("<span class=\"{0}\">{1}</span>", cssClass, source));
		}

		public static string ReplaceWithCss(this string content, string source, string replacement, string cssClass)
		{
			return content.Replace(source, string.Format("<span class=\"{0}\">{1}</span>", cssClass, replacement));
		}

		public static string ReplaceWithCss(this string content, string source, string replacement, string suffix, string cssClass)
		{
			return content.Replace(source, string.Format("<span class=\"{0}\">{1}</span>{2}", cssClass, replacement, suffix));
		}

		public static string ReplaceTokenWithCss(this string content, string source, string token, int counter, string cssClass)
		{
			string formattedToken = string.Format("{0}{1}{0}", token, counter);
			return content.Replace(formattedToken, string.Format("<span class=\"{0}\">{1}</span>", cssClass, source));
		}

		public static string ReplaceToken(this string content, string source, string token, int counter)
		{
			string formattedToken = string.Format("{0}{1}{0}", token, counter);
			return content.Replace(source, formattedToken);
		}
	}
}
