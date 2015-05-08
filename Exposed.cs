using System.Dynamic;
using System.Linq;
using System.Reflection;

namespace Annoyances.Net
{
    /// <summary>
    /// A dangerous class for exposing the contents of other classes without having to go fishing with reflection directly.
    /// Don't use it. No, seriously. Pretend you never saw it. Go back about your business.
    /// Ah well, okay then. But please be careful!
    /// <example>
    /// <code>
    /// dynamic e = new Exposed(someClass);
    /// e.PrivateProperty = 123;
    /// e.DoDangerousThing();
    /// </code>
    /// </example>
    /// </summary>
    public class Exposed : DynamicObject
    {
        /// <summary>
        /// The object being exposed
        /// </summary>
        private readonly object m_o;

        /// <summary>
        /// Bind promiscuously
        /// </summary>
        private const BindingFlags AllBindingFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;

        public Exposed(object o)
        {
            m_o = o;
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            PropertyInfo pi = m_o.GetType().GetProperties(AllBindingFlags).SingleOrDefault(m => m.Name.Equals(binder.Name));

            if (pi == null)
            {
                result = "Invalid Property!";
                return false;
            }

            result = pi.GetGetMethod(true).Invoke(m_o, new object[0]);

            return true;
        }

        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            PropertyInfo pi = m_o.GetType().GetProperties(AllBindingFlags).SingleOrDefault(m => m.Name.Equals(binder.Name));

            if (pi == null)
            {
                return false;
            }

            pi.GetSetMethod(true).Invoke(m_o, new[] { value });

            return true;
        }

        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            MethodInfo mi = m_o.GetType().GetMethods(AllBindingFlags).SingleOrDefault(m => m.Name.Equals(binder.Name));

            if (mi == null)
            {
                result = null;
                return false;
            }

            result = mi.Invoke(m_o, args);
            return true;
        }

        public override bool TryGetIndex(GetIndexBinder binder, object[] indexes, out object result)
        {
            PropertyInfo pi = m_o.GetType().GetProperties(AllBindingFlags).SingleOrDefault(m => m.GetIndexParameters().Length > 0);

            if (pi == null)
            {
                result = "Invalid Indexer!";
                return false;
            }

            result = pi.GetGetMethod(true).Invoke(m_o, indexes);

            return true;
        }

        public override bool TrySetIndex(SetIndexBinder binder, object[] indexes, object value)
        {
            PropertyInfo pi = m_o.GetType().GetProperties(AllBindingFlags).SingleOrDefault(m => m.GetIndexParameters().Length > 0);

            if (pi == null)
            {
                return false;
            }

            pi.SetValue(m_o, value, indexes);

            return true;
        }
    }
}

