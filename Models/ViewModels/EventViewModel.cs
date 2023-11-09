using Microsoft.AspNetCore.Mvc.Rendering;

namespace Agendex.Models.ViewModels
{
    public class EventViewModel
    {
        public Event Event { get; set; }
        public List<SelectListItem> Location = new List<SelectListItem>();
        public String LocationName { get; set; }

        public EventViewModel(Event myEvent, List<Location> locations)
        {
            Event = myEvent;
            LocationName = myEvent.Location.Name;
            foreach(var location in locations)
            {
                Location.Add(new SelectListItem() { Text = location.Name});
            }
        }

        public EventViewModel(List<Location> locations)
        {
            foreach (var location in locations)
            {
                Location.Add(new SelectListItem() { Text = location.Name });
            }
        }

        public EventViewModel()
        {
            
        }

    }
       
}
