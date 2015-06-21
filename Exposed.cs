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
    /// <remarks><pre>
    ///   ,                          _                            (__)    )
    ///  /|   |                     | |                           (..)   /|\
    ///   |___|   _    ,_     _     | |    _                     (o_o)  / | \
    ///   |   |\ |/   /  |   |/     |/ \_ |/                     ___) \/,-|,-\
    ///   |   |/ |__/    |_/ |__/    \_/  |__/                 //,-/_\ )  '  '
    ///                                                           (//,-'\
    ///    |                                           |          (  ( . \_
    ///  __|    ,_     __,    __,   __    _  _     ,   |        gnv `._\(___`.
    /// /  |   /  |   /  |   /  |  /  \_ / |/ |   / \_ |             '---' _)/
    /// \_/|_/    |_/ \_/|_/ \_/|/ \__/    |  |_/  \/  o
    ///                        /|
    ///                        \|
    ///
    /// </pre>
    /// If you're using this class for unit testing private methods, this quotation from
    /// Michael Feathers' book _Working Effectively with Legacy Code_ is worth reading first:
    ///     Big classes can hide too much. This question comes up over and over
    ///     again from people new to unit testing: "How do I test private methods?" Many
    ///     people spend a lot of time trying to figure out how to get around this problem
    ///     but [...] the real answer is that if you have the urge to test a private
    ///     method, the method shouldn't be private; if making the method public bothers
    ///     you, chances are, it is because it is part of a separate responsibility: it
    ///     should be on another class.
    /// </remarks>
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
            return TryGetField(binder, out result) || TryGetProperty(binder, out result);
        }

        private bool TryGetField(GetMemberBinder binder, out object result)
        {
            FieldInfo fi = m_o.GetType().GetFields(AllBindingFlags).SingleOrDefault(f => f.Name.Equals(binder.Name));

            if (fi == null)
            {
                result = "Invalid Field!";
                return false;
            }

            result = fi.GetValue(m_o);
            return true;
        }

        private bool TryGetProperty(GetMemberBinder binder, out object result)
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
            return TrySetField(binder, value) || TrySetProperty(binder, value);
        }

        private bool TrySetField(SetMemberBinder binder, object value)
        {
            FieldInfo fi = m_o.GetType().GetFields(AllBindingFlags).SingleOrDefault(f => f.Name.Equals(binder.Name));

            if (fi == null)
            {
                return false;
            }

            fi.SetValue(m_o, value);
            return true;
        }

        private bool TrySetProperty(SetMemberBinder binder, object value)
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

