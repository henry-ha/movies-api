using System.Collections.Generic;
using System.Web.Mvc;

namespace MoviesWeb.Models.ViewModels
{
    public class HomeViewModel
    {
        public string SearchText { get; set; }
                
        public List<SelectListItem> Filters
        {
            get
            {
                return new List<SelectListItem>()
                {
                    new SelectListItem() { Text = "Select", Value = ""},
                    new SelectListItem() { Text = "Title", Value = "title"},
                    new SelectListItem() { Text = "Year of Release", Value = "year"},
                    new SelectListItem() { Text = "Genre", Value = "genre"}
                };
            }
        }
    }
}