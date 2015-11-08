using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LYSApp.Model;

namespace LYSApp.Domain.SearchManagement
{
    public interface ISearchManagement
    {
        IList<House> GetHouses(SearchViewModel serchViewModel);

        IList<SearchAreaViewModel> GetAreas(string term);

        PropertyDetailsViewModel GetPropertyDetails(int PGDetailsID);
    }

}