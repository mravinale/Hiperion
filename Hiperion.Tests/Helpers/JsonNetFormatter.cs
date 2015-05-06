namespace Hiperion.Tests.Helpers
{
    using System;
    using System.IO;
    using System.Net;
    using System.Net.Http.Formatting;
    using System.Net.Http.Headers;
    using System.Text;
    using System.Threading.Tasks;
    using Newtonsoft.Json;

    public class JsonNetFormatter : MediaTypeFormatter
    {
        private readonly JsonSerializerSettings _settings;
        private readonly Encoding _encoding;

        public JsonNetFormatter(JsonSerializerSettings settings)
        {
            _settings = settings ?? new JsonSerializerSettings();

            SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/json"));
            _encoding = new UTF8Encoding(false, true);
        }

        public override bool CanReadType(Type type)
        {
            return true;
        }

        public override bool CanWriteType(Type type)
        {
            return true;
        }

        protected Task<object> OnReadFromStreamAsync(Type type, Stream stream, HttpContentHeaders contentHeaders)
        {
            var ser = JsonSerializer.Create(_settings);

            return Task.Factory.StartNew(() =>
                {
                    using (var sr = new StreamReader(stream, _encoding))
                    using (var jsonReader = new JsonTextReader(sr))
                    {
                        var result = ser.Deserialize(jsonReader, type);
                        return result;
                    }
                });
        }

        protected Task OnWriteToStreamAsync(Type type, object value, Stream stream, HttpContentHeaders contentHeaders, TransportContext transportContext)
        {
            var ser = JsonSerializer.Create(_settings);

            return Task.Factory.StartNew(() =>
                {
                    using (var jsonWriter = new JsonTextWriter(new StreamWriter(stream)))
                    {
                        ser.Serialize(jsonWriter, value);
                        jsonWriter.Flush();
                    }
                });
        }
    }
}