using LangSchool.AppDataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangSchool.Models
{
    public class ClientsListControl
    {
        public static List<Client> AllClients { get; set; }
        public static int ElementsPerPage { get; set; } = 10;
        public static List<List<Client>> PagedClients { get; set; }
        public static List<Client> CurrentPage 
        { 
            get
            {
                if(ElementsPerPage < 1)
                {
                    return AllClients;
                }

                return PagedClients[CurrentPageNumber];
            } 
        }
        public static int PagesCount { get => (int)Math.Ceiling(((double)AllClients.Count)/((double)ElementsPerPage)); }
        public static int CurrentPageNumber { get; set; } = 0;

        public static void Initialize()
        {
            AllClients = new List<Client>();
            PagedClients = new List<List<Client>>();
        }
        public static void SetClients(IEnumerable<Client> clients)
        {
            AllClients.Clear();
            AllClients.AddRange(clients);

            recountPages();
        }

        public static void SetElemtsPerPage(int count)
        {
            if(count <= AllClients.Count)
            ElementsPerPage = count;
            if (ElementsPerPage < 1) CurrentPageNumber = 1;
            else
            if (CurrentPageNumber > PagesCount-1) CurrentPageNumber = PagesCount-1;
            recountPages();
        }

        public static void recountPages()
        {
            PagedClients.Clear();
            if(ElementsPerPage > 0)
            for(int i = 0, p, k = 0; i < PagesCount; i++) 
            {
                p = 0;
                PagedClients.Add(new List<Client>());

                while (p < ElementsPerPage && k < AllClients.Count) 
                {
                    PagedClients[i].Add(AllClients[k]);
                        p++;
                        k++;
                }
            }
        }

        public static void NextPage()
        {
            if (CurrentPageNumber < PagesCount-1) CurrentPageNumber++;
        }

        public static void PrevPage()
        {
            if (CurrentPageNumber > 0) CurrentPageNumber--;
        }
    }
}
