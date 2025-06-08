namespace Cardboard.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class ElementAttribute<TElement> : Attribute
    {
        public Type ElementType { get; set; }

        public ElementAttribute()
        {
            ElementType = typeof(TElement);
        }
    }
}