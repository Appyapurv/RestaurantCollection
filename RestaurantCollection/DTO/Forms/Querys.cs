using System;
using Microsoft.AspNetCore.Mvc;

namespace RestaurantCollection.WebApi.DTO.Forms
{
    /// <summary>
    /// Query
    /// </summary>
    /// 
    public class Querys
    {
        /// <summary>
        /// City of the restaurant
        /// </summary>
        public string city { get; set; }
        /// <summary>
        /// Id of the restaurant
        /// </summary>
        public int id { get; set; }
    }
}
