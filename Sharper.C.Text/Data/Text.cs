using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace Sharper.C.Data
{
    public static class Text
    {
        public static Text<A> Concat<A>(params Text<A>[] ss)
          where A : struct, Language
        =>  Text<A>.Concat(ss);
    }

    public sealed class Text<A>
      where A : struct, Language
    {
        private readonly ImmutableList<string> fragments;

        private Text(ImmutableList<string> fragments)
        {   this.fragments = fragments;
        }

        public static Text<A> Literal(string s)
        =>  new Text<A>(new[] { s }.ToImmutableList());

        public static Text<A> Escape(string s)
        =>  new Text<A>(new[] { default(A).Escape(s) }.ToImmutableList());

        internal static Text<A> Concat(params Text<A>[] ss)
        =>  new Text<A>(ss.SelectMany(s => s.fragments).ToImmutableList());

        public Text<A> Append(Text<A> s)
        =>  Concat(this, s);

        public Text<A> Lit(string s)
        =>  new Text<A>(fragments.Add(s));

        public Text<A> Esc(string s)
        =>  new Text<A>(fragments.Add(default(A).Escape(s)));

        public async Task WriteTo(TextWriter w)
        {   foreach (var s in fragments)
            {   await w.WriteAsync(s);
            }
        }

        public string RenderString()
        =>  string.Concat(fragments);

        public IEnumerable<string> RenderSeq()
        {   foreach (var s in fragments)
            {   yield return s;
            }
        }

        public static Text<A> operator+(Text<A> x, Text<A> y)
        =>  x.Append(y);
    }
}
