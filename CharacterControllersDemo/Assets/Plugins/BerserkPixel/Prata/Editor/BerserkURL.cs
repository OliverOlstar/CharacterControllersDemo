using UnityEngine;

namespace BerserkPixel.Prata
{
    public static class BerserkURL
    {
        public static readonly string URL_REVIEWS = "https://u3d.as/2LJQ#reviews";

        public static readonly string URL_STORE_PAGE = "https://assetstore.unity.com/publishers/51713";

        public static readonly string URL_PRATA_DOCS = "https://docs.berserkpixel.studio/prata/prata.html";

        public static readonly string URL_PRATA_YOUTUBE = "https://youtu.be/hgb1KRoywBI";

        public static readonly string URL_SUPPORT_EMAIL = "support@berserkpixel.studio";

        public static readonly string URL_BUSINESS_EMAIL = "hello@berserkpixel.studio";

        public static void OpenEmailEditor(string receiver, string subject, string body)
        {
            string url = $"mailto:{receiver}" + $"?subject={subject.Replace(" ", "%20")}" +
                         $"&body={body.Replace(" ", "%20")}";

            Application.OpenURL(url);
        }
    }
}