namespace ExpressionTreesDemo
{
    using System;
    using System.Dynamic;
    using System.Reflection;

    public class ExposedObject : DynamicObject
    {
        private readonly object obj;
        private readonly Type type;

        public ExposedObject(object obj)
        {
            this.obj = obj;
            this.type = obj.GetType();
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            var name = binder.Name;

            var property = this.type
                .GetProperty(name, BindingFlags.NonPublic | BindingFlags.Instance);

            if (property == null)
            {
                // Check for field

                return base.TryGetMember(binder, out result);
            }

            result = property.GetValue(this.obj);

            return true;
        }
    }
}
