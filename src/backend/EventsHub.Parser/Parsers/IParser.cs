using System.Threading.Tasks;

namespace EventsHub.Parser.Parsers
{
    public interface IParser
    {
        Task Parse();
    }
}
