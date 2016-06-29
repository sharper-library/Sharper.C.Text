using System.Text.Encodings.Web;

namespace Sharper.C.Data.Languages
{
    public struct HTML
      : Language
    {
        public string Escape(string s)
        =>  HtmlEncoder.Default.Encode(s);
    }
}
