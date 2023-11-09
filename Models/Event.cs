using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Agendex.Models
{
    public class Event
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [DisplayName("Nome")]
        public String Name { get; set; }

        [DisplayName("Descrição")]
        public String Description { get; set; }

        [DisplayName("Hora Inicio")]
        public DateTime StartTime { get; set;}

        [DisplayName("Hora Fim")]
        public DateTime EndTime { get; set; }

        public Event(IFormCollection form, Location location)
        {
           Name = form["Event.Name"].ToString();
           Description = form["Description"].ToString();
           StartTime = DateTime.Parse(form["Event.StartTime"].ToString());
           EndTime = DateTime.Parse(form["Event.EndTime"].ToString());
           Location = location;
        }

        public void UpdateEvent(IFormCollection form, Location location)
        {
            Name = form["Event.Name"].ToString();
            Description = form["Event.Description"].ToString();
            StartTime = DateTime.Parse(form["Event.StartTime"].ToString());
            EndTime = DateTime.Parse(form["Event.EndTime"].ToString());
            Location = location;
        }

        public Event()
        {

        }

        //Relacionamento
        public virtual Location Location { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
