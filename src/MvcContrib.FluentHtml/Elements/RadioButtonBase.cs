using System.Collections.Generic;
using System.Linq.Expressions;
using MvcContrib.FluentHtml.Behaviors;
using MvcContrib.FluentHtml.Html;

namespace MvcContrib.FluentHtml.Elements
{
	/// <summary>
	/// Base class for a radio button.
	/// </summary>
	public abstract class RadioButtonBase<T> : Checkable<T> where T : RadioButtonBase<T>
	{
		private string _format;
		protected RadioButtonBase(string name) : base(HtmlInputType.Radio, name) { }

		protected RadioButtonBase(string name, MemberExpression forMember, IEnumerable<IBehaviorMarker> behaviors)
			: base(HtmlInputType.Radio, name, forMember, behaviors) { }

		/// <summary>
		/// Specify a format string for the HTML output.
		/// </summary>
		/// <param name="format">A format string.</param>
		public virtual T Format(string format)
		{
			_format = format;
			return (T)this;
		}

		public override string ToString()
		{
			return _format == null
				? base.ToString()
				: string.Format(_format, base.ToString());
		}
	}
}