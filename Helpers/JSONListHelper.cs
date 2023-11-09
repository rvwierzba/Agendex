using System.Text.Json;

namespace Agendex.Helpers
{
    public static class JSONListHelper
    {
        public static String GetEventListJSONString(List<Models.Event> events)
        {
            var eventList = new List<Event>();
           
            foreach(var model in events)
            {
                var myEvent = new Event
                {
                    id = model.Id,
                    title = model.Name,
                    start = model.StartTime,
                    end = model.EndTime,
                    resouceId = model.Location.Id,
                    description = model.Description
                };
                eventList.Add(myEvent);
            }
            return JsonSerializer.Serialize(eventList);
        }

        public static String GetResourceListJSONString(List<Models.Location> locations)
        {
            var resouceList = new List<Resource>();
            foreach(var l in locations)
            {
                var resource = new Resource
                {
                    id = l.Id,
                    title = l.Name
                };
                resouceList.Add(resource);
            }
            return JsonSerializer.Serialize(resouceList);
        }

    }

    public class Event
    {
        public int id { get; set; }
        public String title { get; set; }
        public DateTime start { get; set; }
        public DateTime end { get; set; }
        public int resouceId { get; set; }
        public String description { get; set; }
    }

    public class Resource
    {
        public int id { get; set; }
        public String title { get; set; }

    }
}
