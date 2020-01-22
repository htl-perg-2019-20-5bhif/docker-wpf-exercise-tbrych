using System;
using System.ComponentModel.DataAnnotations;

namespace Homework09_WPF.Model
{
    //This class is only used to send (and receive) the date for booking a car
    public class BookDate
    {
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

    }
}
