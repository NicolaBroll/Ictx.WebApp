using System;
using System.Collections.Generic;
using System.Text;

namespace Ictx.WebApp.Fwk.Models
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

    public class PaginationModel
    {
        const int _maxPageSize = (int)PAGE_SIZE.PAGE_MAX;
        const int _minPageSize = (int)PAGE_SIZE.PAGE_MIN;

        private int _pageSize;
        private int _page;

        /// <summary>
        /// Pagina corrente
        /// </summary>
        public int Page
        {
            get
            {
                return _page <= 0 ? 1 : _page;
            }
            set
            {
                _page = value;
            }
        }

        /// <summary>
        /// Dimensione pagina
        /// </summary>
        public int PageSize
        {
            get
            {
                if (this._pageSize > _maxPageSize)
                {
                    return _maxPageSize;
                }

                if (this._pageSize < _minPageSize)
                {
                    return _minPageSize;
                }

                return this._pageSize;
            }
            set
            {
                this._pageSize = value;
            }
        }
    }

    public enum PAGE_SIZE
    {
        PAGE_15 = 15,
        PAGE_30 = 30,
        PAGE_50 = 50,
        PAGE_MAX = 500,
        PAGE_MIN = 15
    }
}
