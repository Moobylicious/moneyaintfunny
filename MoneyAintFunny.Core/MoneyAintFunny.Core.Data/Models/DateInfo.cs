using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoneyAintFunny.Data.Models
{
    //For quick lookup/grouping by things such as year, quarter, week of year, day of month, day of week etc.
    //we have a pre-populated date dimension table.
    //This will get pre-populated via a script when the db is created.
    //I've borrowed this definition from a previous project, but have removed some overkill things.
    public class DateInfo : SharedEntity
    {
        //Id is the year x 10000 plus month x 100 plus the day of month, so 4th sept 2017 would be 20170904
        //this is useful for sorting and makes for a quick and easy reference.  Since we won't be adding values dynamically here, we don't
        //want this to be a classic identity column.
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int DateId { get; set; }

        //actual day of month, from 1-31 depending on month.
        public int DayOfMonth { get; set; }

        //the actual date (minus any time)
        public DateTime Date { get; set; }

        public int DayOfWeek { get; set; }
        //Basic week of month, based on assuming first week starts on 1st of the month, so these weeks aren't 
        //starting and ending on fixed days...
        public int WeekOfMonth { get; set; }

        public int DayOfYear { get; set; }
        //Iso Week
        public int WeekOfYear { get; set; }

        //Combination of the year and iso week e.g. 201645
        public int IsoYearWeek { get; set; }

        //Week of month but using ISO weeks, so if 1st of month is last day of an iso week, then week '2' in the month starts on the 2nd.
        //This means that a particular date may be in one ISO week, but that week may be labelled week 5 in one month but week 1 in the next.
        //Not sure how much use this is to anyone... but it's there.
        public int IsoWeekOfMonth { get; set; }
        //month - 1 to 12
        public int Month { get; set; }
        public int Quarter { get; set; }
        public int Year { get; set; }
    }    
}
