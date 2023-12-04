using Newtonsoft.Json;

namespace SessionExtension.Helper
{
    public static class SessionExtension
    {
        public static void SetObjectAsJson(this ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static T GetObjectFromJson<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default : JsonConvert.DeserializeObject<T>(value);
        }

        public static void SetStr(this ISession session, string key, string value) => session.SetString(key, value);

        public static string GetStr(this ISession session, string key)
        {
            var value = session.GetString(key);
            return String.IsNullOrEmpty(value) ? string.Empty : value;
        }
    }
}
