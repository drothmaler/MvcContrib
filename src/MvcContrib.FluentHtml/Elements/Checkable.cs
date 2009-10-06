using System.Collections.Generic;
using System.Linq.Expressions;
using MvcContrib.FluentHtml.Behaviors;
using MvcContrib.FluentHtml.Html;

namespace MvcContrib.FluentHtml.Elements
{
    public class Checkable<T> : Input<T> where T : Checkable<T>
    {
        protected Checkable(string type, string name) : base(type, name) { }

        protected Checkable(string type, string name, MemberExpression forMember, IEnumerable<IBehaviorMarker> behaviors)
            : base(type, name, forMember, behaviors) { }

        /// <summary>
        /// Set the checked attribute.
        /// </summary>
        /// <param name="value">Whether the checkbox should be checked.</param>
        public virtual T Checked(bool value)
        {
            if (value)
            {
                Attr(HtmlAttribute.Checked, HtmlAttribute.Checked);
            }
            else
            {
                ((IElement)this).RemoveAttr(HtmlAttribute.Checked);
            }
            return (T)this;
        }

        /// <summary>
        /// Infers the id from name
        /// </summary>
        /// <remarks>
        /// This is to fix the wrong label behavior in the default implementation
        /// </remarks>
        protected override void InferIdFromName()
        {
            if (!Builder.Attributes.ContainsKey(HtmlAttribute.Id))
            {
                Attr(HtmlAttribute.Id, string.Format("{0}{1}",
                    Builder.Attributes[HtmlAttribute.Name],
                    elementValue == null
                        ? null
                        : string.Format("_{0}", elementValue)).FormatAsHtmlId());
            }
        }

    }
}