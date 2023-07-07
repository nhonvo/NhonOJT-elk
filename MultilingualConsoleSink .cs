using Serilog.Core;
using Serilog.Events;

namespace NhonOJT_elk
{
    public class MultilingualConsoleSink : ILogEventSink
    {
        private readonly IFormatProvider _formatProvider;
        private readonly LoggingLevelSwitch _levelSwitch;
        

        private static readonly IDictionary<string, string> EnglishTranslations = new Dictionary<string, string>
    {
        {"Information", "Information"},
        {"Warning", "Warning"},
        {"Error", "Error"}
    };

        private static readonly IDictionary<string, string> VietnameseTranslations = new Dictionary<string, string>
    {
        {"Information", "Thông tin"},
        {"Warning", "Cảnh báo"},
        {"Error", "Lỗi"}
    };

        public MultilingualConsoleSink(IFormatProvider formatProvider, LoggingLevelSwitch levelSwitch)
        {
            _formatProvider = formatProvider;
            _levelSwitch = levelSwitch;
        }
        public MultilingualConsoleSink ()
        {
            
        }

        public void Emit(LogEvent logEvent)
        {
            var logLevel = logEvent.Level.ToString();
            string translatedLevel = GetTranslatedLevel(logLevel);

            var message = logEvent.RenderMessage(_formatProvider);
            var formattedMessage = $"{translatedLevel}: {message}";

            Console.WriteLine(formattedMessage);
        }

        public string GetTranslatedLevel(string logLevel)
        {
            if (Thread.CurrentThread.CurrentCulture.Name == "vi-VN")
            {
                if (VietnameseTranslations.TryGetValue(logLevel, out var translatedLevel))
                {
                    return translatedLevel;
                }
            }

            // Fallback to English translations
            if (EnglishTranslations.TryGetValue(logLevel, out var fallbackLevel))
            {
                return fallbackLevel;
            }

            return logLevel;
        }
    }

}