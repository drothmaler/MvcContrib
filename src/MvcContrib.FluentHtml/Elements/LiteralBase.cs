using System.Collections.Generic;
using System.Linq.Expressions;
using MvcContrib.FluentHtml.Behaviors;
using MvcContrib.FluentHtml.Html;

namespace MvcContrib.FluentHtml.Elements
{
	/// <summary>
	/// Base class for a literal (text inside a span element).
	/// </summary>
	public abstract class LiteralBase<T> : Element<T> where T : LiteralBase<T>
	{
        private string format;
        protected object rawValue;
        private string html;
        private string overridenId;
        private bool htmlWasSpecified;

		protected LiteralBase(string name, MemberExpression forMember, IEnumerable<IBehaviorMarker> behaviors) :
			base(HtmlTag.Span, forMember, behaviors)
		{
			SetName(name);
		}

		protected LiteralBase(string name) : base(HtmlTag.Span)
		{
			SetName(name);
		}

		protected void SetName(string name)
		{
			((IElement)this).SetAttr(HtmlAttribute.Name, name);
		}

		/// <summary>
		/// Set the inner text of the span element.
		/// </summary>
		/// <param name="value">The value of the inner text.</param>
		public virtual T Value(object value)
		{
			rawValue = value;
			return (T)this;
		}

		/// <summary>
		/// Set the inner HTML of the span element.
		/// </summary>
		/// <param name="value">The HTML.</param>
		public virtual T Html(string value)
		{
			html = value;
			htmlWasSpecified = true;
			return (T)this;
		}

		/// <summary>
		/// Specify a format string to be applied to the value.  The format string can be either a
		/// specification (e.g., '$#,##0.00') or a placeholder (e.g., '{0:$#,##0.00}').
		/// </summary>
		/// <param name="value">A format string.</param>
		public virtual T Format(string value)
		{
			format = value;
			return (T)this;
		}

		public override T Id(string value)
		{
			overridenId = value;
			return (T)this;
		}

		public override string ToString()
		{
			SetId();

			if(htmlWasSpecified)
			{
				Builder.InnerHtml = html;
			}
			else
			{
				Builder.SetInnerText(FormatValue(rawValue));
			}

			return base.ToString();
		}

		protected virtual void InferIdFromName()
		{
			if(!Builder.Attributes.ContainsKey(HtmlAttribute.Id))
			{
				Attr(HtmlAttribute.Id, Builder.Attributes[HtmlAttribute.Name].FormatAsHtmlId());
			}
		}

		private void SetId()
		{
			if(!string.IsNullOrEmpty(overridenId))
			{
				base.Id(overridenId);
			}
			else
			{
				InferIdFromName();
			}
		}

		protected virtual string FormatValue(object value)
		{
			return string.IsNullOrEmpty(format)
					? value == null
						? null
						: value.ToString()
					: (format.StartsWith("{0") && format.EndsWith("}"))
						? string.Format(format, value)
						: string.Format("{0:" + format + "}", value);
		}
	}
}