using System.Collections.Generic;
using System.Linq.Expressions;
using MvcContrib.FluentHtml.Behaviors;
using MvcContrib.FluentHtml.Html;

namespace MvcContrib.FluentHtml.Elements
{
	/// <summary>
	/// Base class for HTML input element of type 'checkbox.'
	/// </summary>
	public abstract class CheckBoxBase<T> : ICheckable<T> where T : CheckBoxBase<T>
	{
		protected CheckBoxBase(string name) : base(HtmlInputType.Checkbox, name)
		{
			elementValue = "true";
		}

		protected CheckBoxBase(string name, MemberExpression forMember, IEnumerable<IBehaviorMarker> behaviors)
			: base(HtmlInputType.Checkbox, name, forMember, behaviors)
		{
			elementValue = "true";
		}

		public override string ToString()
		{
			var html = ToCheckBoxOnlyHtml();
			var hiddenId = "_Hidden";
			if (((IElement)this).Builder.Attributes.ContainsKey(HtmlAttribute.Id))
			{
				hiddenId = Builder.Attributes[HtmlAttribute.Id] + hiddenId;
			}
			var hidden = new Hidden(Builder.Attributes[HtmlAttribute.Name]).Id(hiddenId).Value("false").ToString();
			return string.Concat(html, hidden);
		}

		public string ToCheckBoxOnlyHtml()
		{
			return base.ToString();
		}

		protected override void ApplyModelState(System.Web.Mvc.ModelState state) 
		{
			var isChecked = state.Value.ConvertTo(typeof(bool?)) as bool?;

			if (isChecked.HasValue) 
			{
				Checked(isChecked.Value);
			}
		}
	}
}