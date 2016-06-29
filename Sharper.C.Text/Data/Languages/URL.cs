using System.Text.Encodings.Web;

namespace Sharper.C.Data.Languages
{
    public struct URL
      : Language
    {
        public string Escape(string s)
        =>  UrlEncoder.Default.Encode(s);
    }
}
