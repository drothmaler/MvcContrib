using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using MvcContrib.FluentHtml.Behaviors;

namespace MvcContrib.FluentHtml.Elements
{
	/// <summary>
	/// Generate an HTML input element of type 'checkbox.'
	/// </summary>
	public class CheckBox : CheckBoxBase<CheckBox>
	{
		/// <summary>
		/// Generate an HTML input element of type 'checkbox.'
		/// </summary>
		/// <param name="name">Value used to set the 'name' an 'id' attributes of the element</param>
		public CheckBox(string name) : base(name) { }

		/// <summary>
		/// Generate an HTML input element of type 'checkbox.'
		/// </summary>
		/// <param name="name">Value of the 'name' attribute of the element.  Also used to derive the 'id' attribute.</param>
		/// <param name="forMember">Expression indicating the view model member assocaited with the element.</param>
		/// <param name="behaviors">Behaviors to apply to the element.</param>
		public CheckBox(string name, MemberExpression forMember, IEnumerable<IBehaviorMarker> behaviors) :
			base(name, forMember, behaviors) { }

        /// <summary>
        /// Infers the id from name
        /// </summary>
        /// <remarks>
        /// This is to fix the wrong label behavior in the default implementation
        /// </remarks>
        protected override void InferIdFromName()
        {
            if (!Builder.Attributes.ContainsKey("id"))
            {
                this.Attr("id", string.Format("{0}{1}", Builder.Attributes["name"], (base.elementValue == null) ? null : string.Format("_{0}", base.elementValue)).FormatAsHtmlId());
            }
        }
	}
}
