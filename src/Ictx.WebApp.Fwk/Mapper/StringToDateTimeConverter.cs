using System;
using AutoMapper;

namespace Ictx.WebApp.Fwk.Mapper;

public class StringToDateTimeConverter : ITypeConverter<string, DateTime>
{
    public DateTime Convert(string source, DateTime destination, ResolutionContext context)
    {
        DateTime dateTime;

        if (string.IsNullOrEmpty(source))
        {
            return default(DateTime);
        }

        if (DateTime.TryParse(source, out dateTime))
        {
            return dateTime;
        }

        return default(DateTime);
    }
}