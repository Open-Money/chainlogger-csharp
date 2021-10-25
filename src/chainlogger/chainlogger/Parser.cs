using Newtonsoft.Json;

namespace Om.Chainlogger
{
    /// <summary>
    ///  A parser class to interchange between json objects and c# string.
    ///  ....
    ///  Methods
    ///  ....
    ///  jsonEncode(object)
    ///  jsonDecode(string)
    /// </summary>
    class Parser
    {
        /// <summary>
        /// Returns the json representation of given element to string.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns> value (string)</returns>
        public static string JsonEncode(object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        /// <summary>
        /// Returns the json representation of given element to json.
        /// </summary>
        /// <param name="json"></param>
        /// <returns> value (object)</returns>
        public static object JsonDecode(string json)
        {
            return JsonConvert.DeserializeObject(json);
        }
    }
}
