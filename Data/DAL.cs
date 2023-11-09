using Agendex.Models;
using Microsoft.EntityFrameworkCore;

namespace Agendex.Data
{
    public interface IDAL
    {
        public List<Event> GetEvents();
        public List<Event> GetMyEvents(String userId);
        public Event GetEvent(int id);
        public void CreateEvent(IFormCollection form);
        public void UpdateEvent(IFormCollection form);
        public void DeleteEvent(int id);
        public List<Location> GetLocations();
        public Location GetLocation(int id);
        public void CreateLocation(Location location);
    }

    public class DAL : IDAL
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public void CreateEvent(IFormCollection form)
        {
            var newEvent = new Event(form, db.Locations.FirstOrDefault(x => x.Name == form["Location"].ToString()));
            db.Events.Add(newEvent);
            db.SaveChanges();
        }

        public void CreateLocation(Location location)
        {
            db.Locations.Add(location);
            db.SaveChanges();
        }

        public void DeleteEvent(int id)
        {
            var myEvent = db.Events.Find(id);
            db.Events.Remove(myEvent);
            db.SaveChanges();
        }

        public Event GetEvent(int id)
        {
            return db.Events.FirstOrDefault(x => x.Id == id);
        }

        public List<Event> GetEvents()
        {
            return db.Events.ToList();
        }
        public Location GetLocation(int id)
        {
            return db.Locations.Find(id);
        }

        public List<Location> GetLocations()
        {
           return db.Locations.ToList();
        }

        public List<Event> GetMyEvents(String userId)
        {
            return db.Events.Where(x => x.User.Id == userId).ToList();
        }

        public void UpdateEvent(IFormCollection form)
        {
            var myEvent = db.Events.FirstOrDefault(x => x.Id == int.Parse(form["Evento.Id"]));
            var location = db.Locations.FirstOrDefault(x => x.Name == form["Location"]);
            myEvent.UpdateEvent(form, location);
            db.Entry(myEvent).State = EntityState.Modified;
            db.SaveChanges();

        }
    }
}
