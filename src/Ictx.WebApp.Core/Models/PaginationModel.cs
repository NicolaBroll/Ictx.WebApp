using System.Collections.Generic;

namespace Ictx.WebApp.Core.Models
{
    public static class PaginationModel
    {
        /// <summary>
        /// Rappresenta l'oggetto restituito da una query che utilizza la paginazione. Contiene l'elenco degli oggetti letti dalla query
        /// ed il numero totale degli oggetti senza considerare la paginazione. 
        /// </summary>
        /// <typeparam name="T">Tipo dell'oggetto selezionato dalla query</typeparam>
        public class PageResult<T>
        {
            public IEnumerable<T> Data { get; set; }
            public int TotalCount { get; set; }

            public PageResult()
            {
            }
            public PageResult(IEnumerable<T> data, int totalCount)
            {
                this.Data = data;
                this.TotalCount = totalCount;
            }
        }

        public class PaginationFilterModel
        {
            const short maxPageSize = (short)PAGE_SIZE.PAGE_MAX;

            private short _pageSize { get; set; } = maxPageSize;
            private short _page { get; set; } = 1;


            /// <summary>
            /// Pagina corrente
            /// </summary>
            public short Page
            {
                get
                {
                    return _page < 0 ? (short)1 : _page;
                }
                set
                {
                    _page = value;
                }
            }

            /// <summary>
            /// Dimensione pagina
            /// </summary>
            public short PageSize
            {
                get
                {
                    return _pageSize < 0 ? (short)PAGE_SIZE.PAGE_MIN : _pageSize;
                }
                set
                {
                    _pageSize = (value > maxPageSize) ? maxPageSize : value;
                }
            }

            //public string Order { get; set; }
            //public string OrderDirection { get; set; }
        }

        public enum PAGE_SIZE
        {
            PAGE_15 = 15,
            PAGE_30 = 30,
            PAGE_50 = 50,
            PAGE_MAX = 500,
            PAGE_MIN = 15,
            PAGE_ALL = 9999
        }
    }
}
