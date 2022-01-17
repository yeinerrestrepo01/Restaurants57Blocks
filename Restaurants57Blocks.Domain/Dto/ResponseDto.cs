using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurants57Blocks.Domain.Dto
{
    /// <summary>
    /// Clase de mapeo para respuestas de servicios
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ResponseDto<T>
    {
        /// <summary>
        /// Inicializador de clase<class>ResponseDto</class>
        /// </summary>
        public ResponseDto()
        {
            IsSuccess = false;
            Message = string.Empty;
            StatusCode = 200;
        }
        public bool IsSuccess { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }
}
