namespace Cardboard.Core.Attributes
{
    public class ElementAttribute<TElement> : Attribute
    {
        public Type ElementType { get; set; }

        public ElementAttribute()
        {
            ElementType = typeof(TElement);
        }
    }
}